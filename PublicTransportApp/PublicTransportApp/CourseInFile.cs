namespace PublicTransportApp
{
    public class CourseInFile : CourseBase
    {
        private const string fileExtension = ".txt";
        private string fileName;

        public CourseInFile(string type, string lineNumber, string courseNumber) 
            : base(type, lineNumber, courseNumber)
        {
            fileName = $"{type}{lineNumber}_{courseNumber}{fileExtension}";
        }

        public override event VehicleCapacityIsExceededDelegade VehicleCapacityIsExceeded;

        public override void AddNumberOfPassangers(int value)
        {
            if (value >= 0)
            {
                using (var writer = File.AppendText(fileName))
                {
                    writer.WriteLine(value);
                }

                if (this.Type == "B" && value > TransportCapacity.BusCapacity)
                {
                    CheckEventVehicleCapacityIsExceeded();
                }
                else if (this.Type == "M" && value > TransportCapacity.MetroCapacity)
                {
                    CheckEventVehicleCapacityIsExceeded();
                }
                else if (this.Type == "S" && value > TransportCapacity.TramCapacity)
                {
                    CheckEventVehicleCapacityIsExceeded();
                }
                else if (this.Type == "T" && value > TransportCapacity.TrainCapacity)
                {
                    CheckEventVehicleCapacityIsExceeded();
                }
            }
            else
            {
                throw new Exception($"{value} is not a valid number.");
            }
        }

        public override void AddNumberOfPassangers(string value)
        {
            if (int.TryParse(value, out int number))
            {
                this.AddNumberOfPassangers(number);
            }
            else
            {
                throw new ArgumentException($"{value} is not a number.");
            }
        }

        public override Statistics GetStatistics()
        {
            var stats = new Statistics(Type);

            var values = new List<int>();
            if (File.Exists($"{fileName}"))
            {
                using (var reader = File.OpenText($"{fileName}"))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var number = int.Parse(line);
                        values.Add(number);
                        line = reader.ReadLine();
                    }
                }
            }

            foreach (var value in values)
            {
                stats.AddNumberOfPassangers(value);
            }

            return stats;
        }

        public override void Results()
        {
            var stats = GetStatistics();
            if (stats.Counter > 0)
            {
                Console.WriteLine(" ");
                Console.WriteLine($"Stats for course {Type}{LineNumber}_{CourseNumber}");
                Console.Write($"There are {stats.Counter} correct ratings given: ");
                using (var reader = File.OpenText($"{fileName}"))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        Console.Write($"{line}, ");
                        line = reader.ReadLine();
                    }
                }
                //ostatnia linia drukuje sie z przecinkiem
                Console.WriteLine($"\nMinimal value: {stats.Min}");
                Console.WriteLine($"Maximum value: {stats.Max}");
                Console.WriteLine($"Average value: {stats.Average:N2}");
                Console.WriteLine($"Course {Type}{LineNumber}_{CourseNumber} is with {stats.AverageType}");
            }
        }

        protected void CheckEventVehicleCapacityIsExceeded()
        {
            if (VehicleCapacityIsExceeded != null)
            {
                VehicleCapacityIsExceeded(this, new EventArgs());
            }
        }
    }
}
