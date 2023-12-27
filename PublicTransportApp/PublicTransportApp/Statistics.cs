namespace PublicTransportApp
{
    public class Statistics
    {
        public int capacity {  get; set; }
        public int Min { get; private set; }

        public int Max { get; private set; }

        public int Sum { get; private set; }

        public int Counter { get; private set; }

        public float Average
        {
            get
            {
                return this.Sum / this.Counter;
            }
        }

        public string AverageType 
        {
            get 
            { 
                switch (this.Average)
                {
                    case var avg when avg > 1.5 * capacity:
                        return "Course with excessive filling.";
                    case var avg when avg >= 0.5 * capacity:
                        return "Course with normal filling.";
                    default:
                        return "Course with insufficient filling.";
                }
            }
        }

        public Statistics(string type)
        {
            this.Sum = 0;
            this.Counter = 0;
            this.Min = int.MaxValue;
            this.Max = int.MinValue;
            if (type == "B")
            {
                capacity = TransportCapacity.BusCapacity;
            }
            else if (type == "M")
            {
                capacity = TransportCapacity.MetroCapacity;
            }
            else if (type == "S")
            {
                capacity = TransportCapacity.TramCapacity;
            }
            else if (type == "T")
            {
                capacity = TransportCapacity.TrainCapacity;
            }
        }

        public void AddNumberOfPassangers(int value)
        {
            this.Counter++;
            this.Sum += value;
            this.Min = Math.Min(this.Min, value);
            this.Max = Math.Max(this.Max, value);
        }
    }
}
