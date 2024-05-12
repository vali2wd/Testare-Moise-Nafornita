using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Classification;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.FlowAnalysis;

namespace Testare_Moise_Nafornita
{
    internal class MethodToControlFlowGraph
    {
        private string className;
        private string relativePathToClass;
        private string methodName;
        public MethodToControlFlowGraph(string className, string relativePathToClass, string methodName)
        {
            this.relativePathToClass = relativePathToClass;
            this.methodName = methodName;
            this.className = className;
        }

        public void GenerateFlowGraph()
        {
            string code = @"" + File.ReadAllText(relativePathToClass) + @"";
            // Console.WriteLine(code);

            // Parse the code
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            // Get the method declaration node
            MethodDeclarationSyntax methodSyntax = syntaxTree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Where(m => m.Identifier.ValueText.Equals(methodName)).FirstOrDefault();

            if (methodSyntax != null)
            {
                // Build the control flow graph
                SemanticModel semanticModel = CSharpCompilation.Create("MyCompilation_" + className + "_" + methodName)
                    .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                    .AddSyntaxTrees(syntaxTree)
                    .GetSemanticModel(syntaxTree);
                ControlFlowGraph controlFlowGraph = ControlFlowGraph.Create(methodSyntax, semanticModel);

                // Generate DOT representation of the control flow graph
                string dotContent = GenerateDot(controlFlowGraph);
                // Console.WriteLine(dotContent);
                // Write DOT representation to a file
                string dotFilePath = "control_flow_graph_" + className + "_" + methodName + ".dot";
                File.WriteAllText(dotFilePath, dotContent);

                // Generate PNG image from DOT file

                string saveFileName = "control_flow_graph_" + className + "_" + methodName + ".png";
                string command = $"dot -Tpng \"{dotFilePath}\" -o \"{saveFileName}\"";
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C {command}",
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                };

                // Console.WriteLine("image file path: " + saveFileName);
                // Console.WriteLine(command);
            }
            else
            {
                Console.WriteLine("Method not found.");
            }
        }




        private string GenerateDot(ControlFlowGraph controlFlowGraph)
        {
            StringWriter stringWriter = new StringWriter();
            stringWriter.WriteLine("digraph ControlFlowGraph {");

            foreach (var block in controlFlowGraph.Blocks)
            {
                // Construct the label for the node
                string operationsString = string.Join("\\n", block.Operations.Select(op => $"{op.Syntax}"));


                if (block.BranchValue != null)
                {
                    //Console.WriteLine(block.BranchValue.Syntax);
                    operationsString = operationsString + "\\n" + block.BranchValue.Syntax;
                }

                string nodeLabel = $"{block.Ordinal}: {operationsString.Replace("\"", "\\\"")}";
                // Write the node definition with its label
                stringWriter.WriteLine($"{block.Ordinal} [label=\"{nodeLabel}\"];");

                // If the block has a fall-through successor, add the edge
                if (block.FallThroughSuccessor != null)
                {
                    var fallThroughSuccessor = block.FallThroughSuccessor.Destination;
                    stringWriter.WriteLine($"{block.Ordinal} -> {fallThroughSuccessor.Ordinal}");
                }

                // If the block has a conditional successor, add the edge with the condition
                if (block.ConditionalSuccessor != null)
                {
                    var successor = block.ConditionalSuccessor.Destination;

                    string condition = Enum.GetName(typeof(ControlFlowConditionKind), block.ConditionKind) ?? "else";

                    // Console.WriteLine(block.Ordinal + " " + block.ConditionKind);
                    // Construct the label for the edge with the condition
                    string edgeLabel = $"({condition})";

                    // Write the edge definition with its label
                    stringWriter.WriteLine($"{block.Ordinal} -> {successor.Ordinal} [label=\"{edgeLabel}\"];");
                }
            }

            stringWriter.WriteLine("}");
            return stringWriter.ToString();
        }

    }
}
