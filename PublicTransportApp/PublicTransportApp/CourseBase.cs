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

        public abstract void AddNumberOfPassangers(string value);

        public abstract Statistics GetStatistics();

        public abstract void Results();

        public delegate void VehicleCapacityIsExceededDelegade(object sender, EventArgs args);

        public abstract event VehicleCapacityIsExceededDelegade VehicleCapacityIsExceeded;

    }
}