using static PublicTransportApp.CourseBase;

namespace PublicTransportApp
{
    public interface ICourse
    {
        string Type { get; }

        string LineNumber { get; }

        string CourseNumber { get; }

        void AddNumberOfPassangers(int value);

        void AddNumberOfPassangers(string value);

        void ShowResults();

        Statistics GetStatistics();

        public event VehicleCapacityIsExceededDelegade VehicleCapacityIsExceeded;
    }
}
