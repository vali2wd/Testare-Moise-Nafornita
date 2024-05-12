
using System.Diagnostics;
using NMeasure;
using System.Diagnostics.Metrics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Testare_Moise_Nafornita;
using static NMeasure.U;

public class Program

{
    static void Main(string[] args)
    {

        StandardUnitConfiguration.Use();

        var dist1 = new Measure(500, U.Meter);
        var dist2 = new Measure(5.0m, U.Kilometer);

        var distToHighSchool = dist1 + dist2;
        Console.WriteLine(distToHighSchool.ConvertTo(Kilometer));

        new MethodToControlFlowGraph("Test_Convert", "../../../Test_Convert.cs", "SomeSum").GenerateFlowGraph();
        new MethodToControlFlowGraph("DistanceToCoverService", "../../../DistanceToCoverService.cs", "WhereAreYouGoingToday").GenerateFlowGraph();
        new MethodToControlFlowGraph("PumpGasService", "../../../PumpGasService.cs", "PumpGas").GenerateFlowGraph();
    }
}