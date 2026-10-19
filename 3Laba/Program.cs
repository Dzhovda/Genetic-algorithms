using _3GenAlg.Algorithm;
using _3GenAlg.Core;
using _3GenAlg.Interfaces;
using _3GenAlg.IslandModel;
using _3GenAlg.Operators;
using _3GenAlg.Visualization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _3GenAlg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ===== 1. Значения по умолчанию =====
            string generatorType = "random";
            int cityCount = 100;
            int populationSize = 50;
            int generations = 500;
            string selectorType = "tournament";
            string mutationType = "swap";
            string crossoverType = "order";
            string elitismType = "2";
            double mutationRate = 0.05;

            // Островная модель
            int islandCount = 4;
            int migrationInterval = 5;      // каждые 5 поколений — миграция
            double migrationRate = 0.1;    // 10% особей мигрирует

            // ===== 2. Парсинг аргументов =====
            if (args.Length > 0) generatorType = args[0];
            if (args.Length > 1) int.TryParse(args[1], out cityCount);
            if (args.Length > 2) int.TryParse(args[2], out populationSize);
            if (args.Length > 3) int.TryParse(args[3], out generations);
            if (args.Length > 4) selectorType = args[4];
            if (args.Length > 5) mutationType = args[5];
            if (args.Length > 6) crossoverType = args[6];
            if (args.Length > 7) elitismType = args[7];
            if (args.Length > 8) double.TryParse(args[8], NumberStyles.Float,
                                     CultureInfo.InvariantCulture, out mutationRate);
            if (args.Length > 9) int.TryParse(args[9], out islandCount);
            if (args.Length > 10) int.TryParse(args[10], out migrationInterval);
            if (args.Length > 11) double.TryParse(args[11], NumberStyles.Float,
                                      CultureInfo.InvariantCulture, out migrationRate);

            // ===== 3. Общая карта (одна на все острова) =====
            // Создаём отдельный Random для генерации карты,
            // чтобы он не пересекался с Random островов.
            Random mapRnd = new Random(0);

            IMapGenerator mapGenerator = generatorType switch
            {
                "line" => new LineMapGenerator(cityCount),
                "tsplib" => new TspLibGenerator("kroA150.tsp"),
                _ => new RandomMapGenerator(0, 100, cityCount, mapRnd)
            };

            Map map = new Map(mapGenerator);

            // ===== 4. Создание островов =====
            List<Island> islands = new List<Island>();

            for (int i = 0; i < islandCount; i++)
            {
                // Свой Random с уникальным seed
                Random islandRnd = new Random(i + 1);

                // Своя популяция
                Population population = new Population(map, populationSize, islandRnd);

                // Свои операторы
                ICrossover crossover = crossoverType switch
                {
                    "cycle" => new CycleCrossover(),
                    _ => new OrderCrossover(islandRnd)
                };

                IMutation mutation = mutationType switch
                {
                    "inversion" => new InversionMutation(mutationRate, islandRnd),
                    _ => new SwapMutation(mutationRate, islandRnd)
                };

                ISelector selector = selectorType switch
                {
                    "roulette" => new RouletteSelector(islandRnd),
                    _ => new TournamentSelector(3, islandRnd)
                };

                IElitism elitism;
                if (elitismType == "none")
                    elitism = new NoElitism();
                else if (int.TryParse(elitismType, out int eliteCount))
                    elitism = new Elitism(eliteCount);
                else
                    elitism = new Elitism(2);

                // ГА для этого острова
                GeneticAlgorithm ga = new GeneticAlgorithm(
                    population, elitism, selector, mutation, crossover);

                // Обёртка — остров
                Island island = new Island(i, population, ga, mapRnd);
                islands.Add(island);
            }

            // ===== 5. IslandManager =====
            IslandManager manager = new IslandManager(
                islands, migrationInterval, migrationRate);

            // ===== 6. Информация о запуске =====
            Console.WriteLine($"=== Островная модель ===");
            Console.WriteLine($"Генератор: {generatorType}, городов: {cityCount}");
            Console.WriteLine($"Островов: {islandCount}, популяция на острове: {populationSize}");
            Console.WriteLine($"Поколений: {generations}, миграция каждые {migrationInterval}");
            Console.WriteLine($"Доля мигрантов: {migrationRate * 100}%");
            Console.WriteLine($"Селекция: {selectorType}, мутация: {mutationType}");
            Console.WriteLine($"Кроссовер: {crossoverType}, элитизм: {elitismType}");
            Console.WriteLine();

            // ===== 7. Запуск =====
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            Individual globalBest = manager.Run(generations);
            stopwatch.Stop();

            // ===== 8. Вывод результата =====
            Console.WriteLine($"=== Результат ===");
            Console.WriteLine($"Лучший фитнес: {globalBest.Fitness:F2}");
            Console.WriteLine($"Время: {stopwatch.Elapsed.TotalSeconds:F2} сек");

            // ===== 9. Визуализация =====
            // Маршрут лучшего острова
            Visualizer.SaveRoutePlot(map, globalBest, generations, "island_route.png");

            // График сходимости — 4 линии лучшего + 4 линии среднего
            IslandVisualizer.SaveIslandConvergence(islands, "island_convergence.png");

            Console.WriteLine("\nСохранены файлы:");
            Console.WriteLine("  island_route.png — маршрут лучшей особи");
            Console.WriteLine("  island_convergence.png — сходимость по островам");
        }
    }
}