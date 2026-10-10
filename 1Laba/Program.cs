using _1GenAlg2ex.Core;
using _1GenAlg2ex.Interfaces;
using _1GenAlg2ex.Visualization;
using _1GenAlg2ex.FitnessFunctions;
using _1GenAlg2ex.Operators;
using _1GenAlg2ex.Algorithm;
using System;
using System.Collections.Generic;
namespace _1GenAlg2ex
{
    // посмотреть контент менеджера по киберсредам
    // сделать новую ветку из developer и сделать чтобы нормально сохранялось
    internal class Program
    {
        static void Main(string[] args)
        {

            Random rnd = new Random();

            IFitnessFunction fitnessFunction = new RastriginFunction();

            List<(double min, double max)> bounds = new List<(double min, double max)> ();

            bounds.Add((-5.2, 5.2)); // растрыгин (-5.2, 5.2), аккли(-32.768, 32.768), расенброк (-5, 10)
            bounds.Add((-5.2, 5.2)); 

            Population population = new Population(100, 32, bounds, fitnessFunction, rnd);

            ICrossover crossover = new OnePointCrossover();
            IMutation mutation = new OneBitMutation();
            ISelector selector = new TournamentSelector(rnd, 3);

            var ga = new GeneticAlgorithm(population, crossover, mutation, selector, rnd, 2);

            // Подписка на событие визуализации
            int snapshotCounter = 0;
            ga.GenerationCompleted += (gen, pop) =>
            {
                var plot = Visualizer.BuildPopulationPlot(pop, fitnessFunction, bounds, gen);
                string filename = $"snapshot_{snapshotCounter:D3}_gen{gen:D3}.png";
                plot.SavePng(filename, 800, 600);
                Console.WriteLine($"Сохранён {filename}");
                snapshotCounter++;
            };

            // 7. Запустить
            Individual best = ga.Run(100);
        }
    }
}