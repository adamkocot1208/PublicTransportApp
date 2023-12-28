namespace PublicTransportApp.Tests
{
    public class Tests
    {
        [Test]
        public void CourseCollectNumberOfPassangers_ShouldCorrectStatistics()
        {
            var course = new CourseInMemory("B", "M13", "123456");
            course.AddNumberOfPassangers(15);
            course.AddNumberOfPassangers(25);
            course.AddNumberOfPassangers(45);
            course.AddNumberOfPassangers(25);
            course.AddNumberOfPassangers(15);

            var stats = course.GetStatistics();

            Assert.AreEqual(5, stats.Counter);
            Assert.AreEqual(15, stats.Min);
            Assert.AreEqual(45, stats.Max);
            Assert.AreEqual(25, stats.Average);
            Assert.AreEqual("normal filling.", stats.AverageType);

        }
    }
}