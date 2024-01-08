using PublicTransportApp;

Console.WriteLine("#############################################");
Console.WriteLine("#                                           #");
Console.WriteLine("#  Welcome to Public Transport console app  #");
Console.WriteLine("#                                           #");
Console.WriteLine("#############################################");

var flag = true;

do
{
    Console.WriteLine("");
    Console.WriteLine(
        "What do you want to do? \n" +
        "1 - add data into program memory and show statistics \n" +
        "2 - add data into .txt file and show statistics \n" +
        "X - close application\n" +
        "Press key 1, 2 or X");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            {
                AddValueToMemory();
                break;
            }
        case "2":
            {
                AddValueToFile();
                break;
            }
        case "x":
        case "X":
            {
                flag = false;
                break;
            }
        default:
            {
                Console.WriteLine("\n!!! ERROR !!! This operation is not allowed.\n");
                break;
            }
    }
} while (flag);

Console.WriteLine(
    "\nThank you for using our application\n" +
    "Press any key to leave.");
Console.ReadKey();

static void AddValueToMemory()
{
    string type = AddType();
    string lineNumber = AddLineNumber();
    string courseNumber = AddCourseNumber();
    var courseMemory = new CourseInMemory(type, lineNumber, courseNumber);
    courseMemory.VehicleCapacityIsExceeded += OnVehicleCapacityIsExceeded;
    AddValue(courseMemory);
    courseMemory.ShowResults();
}

static void AddValueToFile()
{
    string type = AddType();
    string lineNumber = AddLineNumber();
    string courseNumber = AddCourseNumber();
    var courseInFile = new CourseInFile(type, lineNumber, courseNumber);
    courseInFile.VehicleCapacityIsExceeded += OnVehicleCapacityIsExceeded;
    AddValue(courseInFile);
    courseInFile.ShowResults();
}

static string AddType()
{
    bool validType = false;
    string type = "";

    do
    {
        Console.WriteLine("\nSelect the type of transportation by entering the appropriate letter:\n" +
            "B - bus    M - metro   S - tram    T - train");

        type = Console.ReadLine();

        switch (type.ToUpper())
        {
            case "B":
            case "M":
            case "S":
            case "T":
                validType = true;
                break;
            case "":
                Console.WriteLine("\n!!! ERROR !!! Type of transport cannot be empty.");
                break;
            default:
                Console.WriteLine("\n!!! ERROR !!! Incorrect type of transport has been selected.");
                break;
        }

    } while (!validType);

    return type.ToUpper();
}

static string AddLineNumber()
{
    bool validLine = true;
    string lineNumber = "";

    do
    {
        Console.WriteLine("\nProvide the line number:");
        lineNumber = Console.ReadLine().Trim();

        if (string.IsNullOrEmpty(lineNumber))
        {
            Console.WriteLine("\n!!! ERROR !!! Line number of transport cannot be empty.");
        }
        else
        {
            validLine = false;
        }
    } while (validLine);

    return lineNumber;
}

static string AddCourseNumber()
{
    bool validCourse = true;
    string courseNumber = "";

    do
    {
        Console.WriteLine("\nProvide the course number:");
        courseNumber = Console.ReadLine().Trim();

        if (string.IsNullOrEmpty(courseNumber) || courseNumber.Length != 6)
        {
            Console.WriteLine("\nAre you sure you entered the correct course number? (the number should contain 6 characters)" +
                "\nChoose Y - yes or N - no");
            var input = Console.ReadLine().ToUpper();

            switch (input)
            {
                case "Y":
                    validCourse = false;
                    break;
                case "N":
                    break;
                default:
                    break;
            }
        }
        else
        {
            validCourse = false;
        }
    }while (validCourse);

    return courseNumber;
}

static void AddValue(ICourse course)
{
    Console.WriteLine("\nEnter the number of passengers:");

    while (true)
    {
        var value = Console.ReadLine();
        if (value == "E" || value == "e") 
        { 
            break;
        }

        try
        {
            course.AddNumberOfPassangers(value);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"!!! ERROR !!! {ex.Message}");
        }

        Console.WriteLine("\nEnter next value or end the entry by pressing 'E'");
    }
}

static void OnVehicleCapacityIsExceeded(object sender, EventArgs args)
{
    Console.WriteLine("\n!!! ATTENTION !!! The vehicle's capacity has been exceeded!");
}
