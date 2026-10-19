using _3GenAlg.Algorithm;
using _3GenAlg.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace _3GenAlg.IslandModel
{
    /// <summary>
    /// Один остров: популяция + ГА + своя история + очередь мигрантов.
    /// </summary>
    internal class Island
    {
        private readonly int _id;
        private readonly Population _population;
        private readonly GeneticAlgorithm _ga;
        private readonly Random _rnd;

        // Очередь входящих мигрантов (пишут другие острова)
        public ConcurrentQueue<Individual> IncomingMigrants { get; } = new ConcurrentQueue<Individual>();

        // История для визуализации
        public List<int> GenerationHistory { get; } = new List<int>();
        public List<double> BestFitnessHistory { get; } = new List<double>();
        public List<double> AverageFitnessHistory { get; } = new List<double>();

        // Лучший за всё время
        private Individual _allTimeBest;
        private int _currentGeneration = 0;

        public int Id => _id;
        public int PopulationSize => _population.Individuals.Count;

        public Island(int id, Population population, GeneticAlgorithm ga, Random rnd)
        {
            _id = id;
            _population = population;
            _ga = ga;
            _rnd = rnd;

            // Подписываемся на событие — собираем статистику
            _ga.GenerationCompleted += (gen, pop) =>
            {
                int globalGen = _currentGeneration + gen;

                double best = pop.Individuals[0].Fitness;
                double avg = pop.Individuals.Average(i => i.Fitness);

                GenerationHistory.Add(globalGen);
                BestFitnessHistory.Add(best);
                AverageFitnessHistory.Add(avg);

                if (best < _allTimeBest.Fitness)
                    _allTimeBest = pop.Individuals[0];
            };

            // Инициализация лучшего за всё время
            _population.EvaluateAll();
            _population.Sort();
            _allTimeBest = _population.Individuals[0];
        }

        /// <summary>
        /// Эволюция на N поколений.
        /// </summary>
        public void RunFor(int generations)
        {
            _ga.RunFor(generations);
            _currentGeneration += generations;
        }

        /// <summary>
        /// Отправляет count лучших особей в очередь указанного острова.
        /// </summary>
        public void SendMigrantsTo(Island other, int count)
        {
            // Популяция уже отсортирована после RunFor
            var migrants = _population.Individuals.Take(count).ToList();

            foreach (var m in migrants)
            {
                // Копируем маршрут, чтобы не испортить исходную особь
                var copy = new Individual((int[])m.Route.Clone());
                other.IncomingMigrants.Enqueue(copy);
            }
        }

        /// <summary>
        /// Забирает всех мигрантов из своей очереди.
        /// </summary>
        public List<Individual> ReceiveMigrants()
        {
            var migrants = new List<Individual>();
            while (IncomingMigrants.TryDequeue(out var m))
                migrants.Add(m);
            return migrants;
        }

        /// <summary>
        /// Заменяет худших особей мигрантами.
        /// </summary>
        public void ReplaceWorst(List<Individual> migrants)
        {
            if (migrants.Count == 0) return;

            _population.Sort(); // на всякий случай

            int replaceCount = Math.Min(migrants.Count, _population.Individuals.Count);

            for (int i = 0; i < replaceCount; i++)
            {
                _population.Individuals[_population.Individuals.Count - 1 - i] = migrants[i];
            }
        }

        public Individual GetBest() => _allTimeBest;
    }
}