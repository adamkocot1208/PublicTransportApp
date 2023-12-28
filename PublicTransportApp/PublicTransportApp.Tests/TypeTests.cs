namespace PublicTransportApp.Tests
{
    public class TypeTests
    {
        [Test]
        public void TwoCourseAreNotEqual()
        {
            var course1 = new CourseInMemory("B", "M13", "123456");
            var course2 = new CourseInMemory("B", "M13", "123456");

            Assert.AreNotEqual(course1, course2);
        }

        [Test]
        public void CheckCorrespondingParameters()
        {
            var course1 = new CourseInMemory("B", "M13", "123456");
            var course2 = new CourseInMemory("B", "M15", "123456");

            Assert.AreEqual(course1.Type, course2.Type);
            Assert.AreNotEqual(course1.LineNumber, course2.LineNumber);
            Assert.AreEqual(course1.CourseNumber, course2.CourseNumber);
        }
    }
}
