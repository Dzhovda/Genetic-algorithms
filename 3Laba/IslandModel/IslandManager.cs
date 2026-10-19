using _3GenAlg.IslandModel;
using _3GenAlg.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _3GenAlg.IslandModel
{
    /// <summary>
    /// Управляет островами: запускает параллельную эволюцию,
    /// синхронизирует миграцию по кольцу.
    /// </summary>
    internal class IslandManager
    {
        private readonly List<Island> _islands;
        private readonly Barrier _barrier;
        private readonly int _migrationInterval;
        private readonly double _migrationRate;

        public IslandManager(List<Island> islands, int migrationInterval, double migrationRate)
        {
            _islands = islands;
            _migrationInterval = migrationInterval;
            _migrationRate = migrationRate;

            // Barrier на N участников — по одному на остров
            _barrier = new Barrier(_islands.Count);
        }

        /// <summary>
        /// Главный цикл островной модели.
        /// </summary>
        public Individual Run(int totalGenerations)
        {
            int iterations = totalGenerations / _migrationInterval;

            // Запускаем по одной задаче на остров
            Task[] tasks = new Task[_islands.Count];

            for (int i = 0; i < _islands.Count; i++)
            {
                int islandIndex = i;   // локальная копия для замыкания

                tasks[i] = Task.Run(() => RunIsland(islandIndex, iterations));
            }

            // Ждём завершения всех островов
            Task.WaitAll(tasks);

            // Находим лучшего среди всех островов
            Individual globalBest = _islands
                .Select(isl => isl.GetBest())
                .OrderBy(ind => ind.Fitness)
                .First();

            return globalBest;
        }

        /// <summary>
        /// Логика одного острова: эволюция + миграция.
        /// </summary>
        private void RunIsland(int islandIndex, int iterations)
        {
            Island current = _islands[islandIndex];

            for (int iter = 0; iter < iterations; iter++)
            {
                // 1. Эволюция на migrationInterval поколений
                current.RunFor(_migrationInterval);

                // 2. Барьер: ждём, пока все острова закончат эволюцию
                _barrier.SignalAndWait();

                // 3. Миграция
                int nextIndex = (islandIndex + 1) % _islands.Count;   // кольцо
                Island next = _islands[nextIndex];

                int migrantCount = (int)(current.PopulationSize * _migrationRate);

                current.SendMigrantsTo(next, migrantCount);

                // 4. Барьер: ждём, пока все отправят
                _barrier.SignalAndWait();

                // 5. Принимаем мигрантов и заменяем худших
                List<Individual> received = current.ReceiveMigrants();
                current.ReplaceWorst(received);
            }
        }
    }
}