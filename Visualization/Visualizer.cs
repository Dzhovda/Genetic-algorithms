using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using _1GenAlg2ex.Core;
using _1GenAlg2ex.Interfaces;

namespace _1GenAlg2ex.Visualization
{
    internal class Visualizer
    {
        private static double[,] BuildSurface(
    IFitnessFunction fitness,
    List<(double Min, double Max)> bounds,
    int steps)
        {
            double[,] surface = new double[steps, steps];
            double minX = bounds[0].Min;
            double maxX = bounds[0].Max;
            double minY = bounds[1].Min;
            double maxY = bounds[1].Max;

            for (int i = 0; i < steps; i++)
            {
                for (int j = 0; j < steps; j++)
                {
                    double x = minX + (maxX - minX) * i / (steps - 1);
                    double y = minY + (maxY - minY) * j / (steps - 1);
                    surface[j, i] = fitness.Evaluate(new List<double> { x, y });
                }
            }
            return surface;
        }

        public static ScottPlot.Plot BuildPopulationPlot(
            Population population,
            IFitnessFunction fitness,
            List<(double Min, double Max)> bounds,
            int generation,
            int steps = 100)
        {
            // 1. Строим тепловую карту
            double[,] surface = BuildSurface(fitness, bounds, steps);
            var plot = new ScottPlot.Plot();

            var heatmap = plot.Add.Heatmap(surface);
            heatmap.Rectangle = new ScottPlot.CoordinateRect(
                bounds[0].Min, bounds[0].Max,   // left, right (X)
                bounds[1].Min, bounds[1].Max    // bottom, top (Y)
            );
            heatmap.Colormap = new ScottPlot.Colormaps.Viridis();

            // 2. Накладываем точки популяции
            double[] xs = population.Individuals.Select(i => i.Phenotype[0]).ToArray();
            double[] ys = population.Individuals.Select(i => i.Phenotype[1]).ToArray();

            plot.XLabel("X (первый аргумент)");
            plot.YLabel("Y (второй аргумент)");

            var scatter = plot.Add.ScatterPoints(xs, ys);
            scatter.Color = ScottPlot.Colors.Red;
            scatter.MarkerSize = 8;

            plot.Title($"Поколение {generation}");
            return plot;
        }
    }
}
