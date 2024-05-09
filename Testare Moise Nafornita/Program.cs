
using NMeasure;
using System.Diagnostics.Metrics;
using static NMeasure.U;

public class Program

{
    static void Main(string[] args)
    {

        StandardUnitConfiguration.Use();

        // Main program of the application
        var dist1 = new Measure(500, U.Meter);
        var dist2 = new Measure(5.0m, U.Kilometer);

        var distToHighSchool = dist1 + dist2;
        Console.WriteLine(distToHighSchool.ConvertTo(Kilometer));
    }
}