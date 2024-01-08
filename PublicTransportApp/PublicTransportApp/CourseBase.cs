namespace PublicTransportApp
{
    public abstract class CourseBase : ICourse
    {
        public string Type { get; set; }

        public string LineNumber { get; private set; }

        public string CourseNumber { get; private set; }

        public CourseBase(string type, string lineNumber, string courseNumber)
        {
            this.Type = type;
            this.LineNumber = lineNumber;
            this.CourseNumber = courseNumber;
        }

        public abstract void AddNumberOfPassangers(int value);

        public void AddNumberOfPassangers(string value)
        {
            if (int.TryParse(value, out int number))
            {
                this.AddNumberOfPassangers(number);
            }
            else
            {
                if (value == "")
                {
                    throw new Exception("' ' is not a valid number.");
                }
                else
                {
                    throw new Exception($"{value} is not a valid number.");
                }
            }
        }

        public abstract Statistics GetStatistics();

        public abstract void ShowResults();

        public delegate void VehicleCapacityIsExceededDelegade(object sender, EventArgs args);

        public abstract event VehicleCapacityIsExceededDelegade VehicleCapacityIsExceeded;

        protected abstract void CheckEventVehicleCapacityIsExceeded(int value);

        public bool IsExceededVehicleCapacity(int value)
        {
            switch (Type)
            {
                case "B":
                    return value > TransportCapacity.BusCapacity;
                case "M":
                    return value > TransportCapacity.MetroCapacity;
                case "S":
                    return value > TransportCapacity.TramCapacity;
                case "T":
                    return value > TransportCapacity.TrainCapacity;
                default:
                    throw new ArgumentException($"Invalid transport type: {Type}");
            }
        }
    }
}