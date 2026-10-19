using _3GenAlg.Core;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace _3GenAlg.Visualization
{
    /// <summary>
    /// Визуализация: маршрут лучшей особи и график сходимости.
    /// </summary>
    internal static class Visualizer
    {
        /// <summary>
        /// Рисует маршрут лучшей особи на карте городов.
        /// </summary>
        public static void SaveRoutePlot(Map map, Individual best, int generation, string filename)
        {
            var plot = new Plot();

            // 1. Рисуем города точками
            double[] xs = map.Cities.Select(c => c.x).ToArray();
            double[] ys = map.Cities.Select(c => c.y).ToArray();

            var citiesScatter = plot.Add.ScatterPoints(xs, ys);
            citiesScatter.Color = Colors.Blue;
            citiesScatter.MarkerSize = 8;
            citiesScatter.LegendText = "Города";

            // 2. Рисуем маршрут линиями
            int[] route = best.Route;
            double[] routeXs = new double[route.Length + 1];
            double[] routeYs = new double[route.Length + 1];

            for (int i = 0; i < route.Length; i++)
            {
                routeXs[i] = map.Cities[route[i]].x;
                routeYs[i] = map.Cities[route[i]].y;
            }
            // Замыкаем маршрут: последний → первый
            routeXs[route.Length] = map.Cities[route[0]].x;
            routeYs[route.Length] = map.Cities[route[0]].y;

            var routeLine = plot.Add.ScatterLine(routeXs, routeYs);
            routeLine.Color = Colors.Red;
            routeLine.LineWidth = 2;
            routeLine.LegendText = "Маршрут";

            plot.Title($"Лучший маршрут (поколение {generation}), длина = {best.Fitness:F2}");
            plot.XLabel("X");
            plot.YLabel("Y");
            plot.ShowLegend();

            plot.SavePng(filename, 800, 800);
        }

        /// <summary>
        /// Рисует график сходимости: лучший и средний фитнес по поколениям.
        /// </summary>
        public static void SaveConvergencePlot(
            List<int> generations,
            List<double> bestFitness,
            List<double> averageFitness,
            string filename)
        {
            var plot = new Plot();

            var bestLine = plot.Add.ScatterLine(
                generations.ToArray(),
                bestFitness.ToArray());
            bestLine.Color = Colors.Red;
            bestLine.LineWidth = 2;
            bestLine.LegendText = "Лучший фитнес";

            var avgLine = plot.Add.ScatterLine(
                generations.ToArray(),
                averageFitness.ToArray());
            avgLine.Color = Colors.Blue;
            avgLine.LineWidth = 2;
            avgLine.LegendText = "Средний фитнес";

            plot.Title("Сходимость генетического алгоритма");
            plot.XLabel("Поколение");
            plot.YLabel("Фитнес (длина маршрута)");
            plot.ShowLegend();

            plot.SavePng(filename, 1000, 600);
        }
    }
}