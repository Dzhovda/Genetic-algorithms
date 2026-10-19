using _3GenAlg.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace _3GenAlg
{
    /// <summary>
    /// Генератор, читающий координаты городов из файла формата TSPLIB (EUC_2D).
    /// </summary>
    internal class TspLibGenerator : IMapGenerator
    {
        private readonly string _filePath;

        public TspLibGenerator(string filePath)
        {
            _filePath = filePath;
        }

        public List<(double x, double y)> Generate()
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException($"TSPLIB файл не найден: {_filePath}");

            List<(double x, double y)> coordinates = new List<(double x, double y)>();

            bool inCoordSection = false;

            foreach (string rawLine in File.ReadLines(_filePath))
            {
                string line = rawLine.Trim();

                if (line.Length == 0) continue;

                // Начало секции координат
                if (line.StartsWith("NODE_COORD_SECTION"))
                {
                    inCoordSection = true;
                    continue;
                }

                // Конец файла
                if (line.StartsWith("EOF"))
                    break;

                // Читаем только внутри секции координат
                if (!inCoordSection) continue;

                // Формат строки: <id> <x> <y>
                string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 3) continue;

                // parts[0] — id города (игнорируем)
                double x = double.Parse(parts[1], CultureInfo.InvariantCulture);
                double y = double.Parse(parts[2], CultureInfo.InvariantCulture);

                coordinates.Add((x, y));
            }

            if (coordinates.Count == 0)
                throw new InvalidDataException($"В файле {_filePath} не найдено координат.");

            return coordinates;
        }
    }
}