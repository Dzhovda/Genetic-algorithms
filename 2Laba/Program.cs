using _2GenAlgEx2.Algorithm;
using _2GenAlgEx2.Core;
using _2GenAlgEx2.Interfaces;
using _2GenAlgEx2.Operators;
using _2GenAlgEx2.Visualization;
using System.Linq;
using System;
using System.Collections.Generic;

namespace _2GenAlgEx2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            // === Аргументы по умолчанию ===
            string generatorType = "random";
            int cityCount = 100;
            int populationSize = 50;
            int generations = 500;
            string selectorType = "tournament";
            string mutationType = "swap";
            string crossoverType = "order";
            string elitismType = "2";
            double mutationRate = 0.05;

            // === Парсинг аргументов ===
            if (args.Length > 0) generatorType = args[0];
            if (args.Length > 1) int.TryParse(args[1], out cityCount);
            if (args.Length > 2) int.TryParse(args[2], out populationSize);
            if (args.Length > 3) int.TryParse(args[3], out generations);
            if (args.Length > 4) selectorType = args[4];
            if (args.Length > 5) mutationType = args[5];
            if (args.Length > 6) crossoverType = args[6];
            if (args.Length > 7) elitismType = args[7];
            if (args.Length > 8) double.TryParse(args[8], out mutationRate);

            // === Создание генератора ===
            IMapGenerator mapGenerator = generatorType switch
            {
                "random" => new RandomMapGenerator(0, 100, cityCount, random),
                "line" => new LineMapGenerator(cityCount),
                _ => new RandomMapGenerator(0, 100, cityCount, random)
            };

            // === Map и Population ===
            Map map = new Map(mapGenerator);
            Population population = new Population(map, populationSize, random);

            // === Операторы ===
            ICrossover crossover = crossoverType switch
            {
                "order" => new OrderCrossover(random),
                "cycle" => new CycleCrossover(),
                _ => new OrderCrossover(random)
            };

            IMutation mutation = mutationType switch
            {
                "swap" => new SwapMutation(mutationRate, random),
                "inversion" => new InversionMutation(mutationRate, random),
                _ => new SwapMutation(mutationRate, random)
            };

            ISelector selector = selectorType switch
            {
                "tournament" => new TournamentSelector(3, random),
                "roulette" => new RouletteSelector(random),
                _ => new TournamentSelector(3, random)
            };

            IElitism elitism;
            if (elitismType == "none")
                elitism = new NoElitism();
            else if (int.TryParse(elitismType, out int eliteCount))
                elitism = new Elitism(eliteCount);
            else
                elitism = new Elitism(2);

            // === Genetic Algorithm ===
            GeneticAlgorithm geneticAlgorithm = new GeneticAlgorithm(population, elitism, selector, mutation, crossover);

            List<int> generationHistory = new List<int>();
            List<double> bestFitnessHistory = new List<double>();
            List<double> averageFitnessHistory = new List<double>();

            geneticAlgorithm.GenerationCompleted += (gen, pop) =>
            {
                generationHistory.Add(gen);
                bestFitnessHistory.Add(pop.Individuals[0].Fitness);
                double avg = pop.Individuals.Average(i => i.Fitness);
                averageFitnessHistory.Add(avg);
            };

            Console.WriteLine($"Конфигурация: {generatorType}, {cityCount} городов, {populationSize} особей, {generations} поколений");
            Console.WriteLine($"Селекция: {selectorType}, Мутация: {mutationType} ({mutationRate}), Кроссовер: {crossoverType}, Элитизм: {elitismType}");

            Individual best = geneticAlgorithm.Run(generations);

            Visualizer.SaveRoutePlot(map, best, generations, "route.png");
            Visualizer.SaveConvergencePlot(
                generationHistory,
                bestFitnessHistory,
                averageFitnessHistory,
                "convergence.png");

            Console.WriteLine($"Лучший фитнес: {best.Fitness}");
        }
    }
}