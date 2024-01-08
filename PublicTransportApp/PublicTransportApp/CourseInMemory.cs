namespace PublicTransportApp
{
    public class CourseInMemory : CourseBase
    {
        private List<int> numberOfPassengers = new List<int>();

        public CourseInMemory(string type, string lineNumber, string courseNumber) 
            : base(type, lineNumber, courseNumber)
        {
        }

        public override event VehicleCapacityIsExceededDelegade VehicleCapacityIsExceeded;

        public override void AddNumberOfPassangers(int value)
        {
            if (value >= 0)
            {
                this.numberOfPassengers.Add(value);
                CheckEventVehicleCapacityIsExceeded(value);
            }
            else
            {
                throw new Exception($"{value} is not a valid number.");
            }
        }

        public override Statistics GetStatistics()
        {
            var stats = new Statistics(Type);

            foreach (var number in  this.numberOfPassengers)
            {
                stats.AddNumberOfPassangers(number);
            }

            return stats;
        }

        public override void ShowResults()
        {
            var stats = GetStatistics();
            if (stats.Counter > 0)
            {
                Console.WriteLine(" ");
                Console.WriteLine($"Stats for course {Type}{LineNumber}_{CourseNumber}");
                Console.Write($"There are {stats.Counter} correct values given: ");
                foreach (var number in this.numberOfPassengers)
                {  
                    Console.Write($" {number};"); 
                }
                Console.WriteLine($"\nMinimal value: {stats.Min}");
                Console.WriteLine($"Maximum value: {stats.Max}");
                Console.WriteLine($"Average value: {stats.Average:N2}");
                Console.WriteLine($"Course {Type}{LineNumber}_{CourseNumber} is with {stats.AverageType}");
            }
            else
            {
                Console.WriteLine($"\nStats for course {Type}{LineNumber}_{CourseNumber}");
                Console.WriteLine("No value was given. Statistics cannot be calculated for this course.");
            }
        }

        public override void CheckEventVehicleCapacityIsExceeded(int value)
        {
            if (VehicleCapacityIsExceeded != null && IsExceededVehicleCapacity(value))
            {
                VehicleCapacityIsExceeded(this, new EventArgs());
            }
        }

    }
}