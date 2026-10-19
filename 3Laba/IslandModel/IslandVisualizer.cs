using ScottPlot;
using System.Collections.Generic;
using System.Linq;

namespace _3GenAlg.IslandModel
{
    internal static class IslandVisualizer
    {
        private static readonly ScottPlot.Color[] IslandColors =
        {
            ScottPlot.Colors.Red,
            ScottPlot.Colors.Blue,
            ScottPlot.Colors.Green,
            ScottPlot.Colors.Orange,
            ScottPlot.Colors.Purple,
            ScottPlot.Colors.Brown,
            ScottPlot.Colors.Magenta,
            ScottPlot.Colors.Cyan
        };

        public static void SaveIslandConvergence(List<Island> islands, string filenamePrefix)
        {
            SaveBestFitnessPlot(islands, $"{filenamePrefix}_best.png");
            SaveAverageFitnessPlot(islands, $"{filenamePrefix}_avg.png");
        }

        private static void SaveBestFitnessPlot(List<Island> islands, string filename)
        {
            var plot = new Plot();
            int bestIslandId = FindBestIsland(islands).Id;

            for (int i = 0; i < islands.Count; i++)
            {
                Island island = islands[i];
                if (island.GenerationHistory.Count == 0) continue;

                var line = plot.Add.ScatterLine(
                    island.GenerationHistory.ToArray(),
                    island.BestFitnessHistory.ToArray());

                line.Color = IslandColors[i % IslandColors.Length];
                line.LineWidth = (island.Id == bestIslandId) ? 3 : 1.5f;
                line.LegendText = (island.Id == bestIslandId)
                    ? $"Остров {island.Id} (лучший): {island.BestFitnessHistory.Last():F1}"
                    : $"Остров {island.Id}: {island.BestFitnessHistory.Last():F1}";
            }

            plot.Title("Сходимость лучшего фитнеса по островам");
            plot.XLabel("Поколение");
            plot.YLabel("Лучший фитнес");

            // Плашка с легендой в правом верхнем углу
            plot.Legend.Alignment = ScottPlot.Alignment.UpperRight;
            plot.ShowLegend();

            plot.SavePng(filename, 1200, 700);
        }

        private static void SaveAverageFitnessPlot(List<Island> islands, string filename)
        {
            var plot = new Plot();
            int bestIslandId = FindBestIsland(islands).Id;

            for (int i = 0; i < islands.Count; i++)
            {
                Island island = islands[i];
                if (island.GenerationHistory.Count == 0) continue;

                var line = plot.Add.ScatterLine(
                    island.GenerationHistory.ToArray(),
                    island.AverageFitnessHistory.ToArray());

                line.Color = IslandColors[i % IslandColors.Length];
                line.LineWidth = (island.Id == bestIslandId) ? 3 : 1.5f;
                line.LegendText = (island.Id == bestIslandId)
                    ? $"Остров {island.Id} (лучший): {island.AverageFitnessHistory.Last():F1}"
                    : $"Остров {island.Id}: {island.AverageFitnessHistory.Last():F1}";
            }

            plot.Title("Сходимость среднего фитнеса по островам");
            plot.XLabel("Поколение");
            plot.YLabel("Средний фитнес");

            plot.Legend.Alignment = ScottPlot.Alignment.UpperRight;
            plot.ShowLegend();

            plot.SavePng(filename, 1200, 700);
        }

        private static Island FindBestIsland(List<Island> islands)
        {
            return islands
                .OrderBy(isl => isl.GetBest().Fitness)
                .First();
        }
    }
}