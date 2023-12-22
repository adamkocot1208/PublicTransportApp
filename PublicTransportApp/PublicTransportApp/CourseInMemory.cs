namespace PublicTransportApp
{
    public class CourseInMemory : CourseBase
    {
        private List<int> numberOfPassengers = new List<int>();

        public CourseInMemory(string type, string lineNumber, int courseNumber) 
            : base(type, lineNumber, courseNumber)
        {
        }

        public override event VehicleCapacityIsExceededDelegade VehicleCapacityIsExceeded;

        public override void AddNumberOfPassangers(int value)
        {
            if (value >= 0)
            {
                this.numberOfPassengers.Add(value);
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
            var stats = new Statistics();

            foreach (var number in  this.numberOfPassengers)
            {
                stats.AddNumberOfPassangers(number);
            }

            return stats;
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