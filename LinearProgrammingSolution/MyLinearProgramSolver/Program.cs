using System;
using System.IO;

namespace MyLinearProgramSolver
{
    class Program
    {
        private const string Assignment1DictionaryFiles = @".\part1TestCases\assignmentParts\";
        private const string Assignment2DictionaryFiles = @".\part2TestCases\assignmentParts\";
        private const string Assignment3DictionaryFiles = @".\part3TestCases\assignmentParts\";


        static void Main(string[] args)
        {
            //WriteAssignment1Solution(Assignment1DictionaryFiles + @"part1.dict");
            //WriteAssignment1Solution(Assignment1DictionaryFiles + @"part2.dict");
            //WriteAssignment1Solution(Assignment1DictionaryFiles + @"part3.dict");
            //WriteAssignment1Solution(Assignment1DictionaryFiles + @"part4.dict");
            //WriteAssignment1Solution(Assignment1DictionaryFiles + @"part5.dict");

            //WriteAssignment2Solution(Assignment2DictionaryFiles + @"part1.dict");
            //WriteAssignment2Solution(Assignment2DictionaryFiles + @"part2.dict");
            //WriteAssignment2Solution(Assignment2DictionaryFiles + @"part3.dict");
            //WriteAssignment2Solution(Assignment2DictionaryFiles + @"part4.dict");
            //WriteAssignment2Solution(Assignment2DictionaryFiles + @"part5.dict");

            WriteAssignment3Solution(Assignment3DictionaryFiles + @"part1.dict");
            WriteAssignment3Solution(Assignment3DictionaryFiles + @"part2.dict");
            WriteAssignment3Solution(Assignment3DictionaryFiles + @"part3.dict");
            WriteAssignment3Solution(Assignment3DictionaryFiles + @"part4.dict");
            WriteAssignment3Solution(Assignment3DictionaryFiles + @"part5.dict");
            WriteAssignment3Solution(Assignment3DictionaryFiles + @"part6.dict");
            WriteAssignment3Solution(Assignment3DictionaryFiles + @"part7.dict");
            WriteAssignment3Solution(Assignment3DictionaryFiles + @"part8.dict");
            WriteAssignment3Solution(Assignment3DictionaryFiles + @"part9.dict");
            WriteAssignment3Solution(Assignment3DictionaryFiles + @"part10.dict");
        }

        private static void WriteAssignment1Solution(string fileName)
        {
            var reader = new LinearProgramDictionaryReader();
            var linearProgram = reader.Read(fileName);

            var analyze = new SimpleAnalyzeEnteringLeavingVariables();
            analyze.Analyze(linearProgram);

            var calculator = new HackyPivot();
            calculator.Pivot(analyze.Entering, analyze.Leaving, linearProgram);
            var obj = linearProgram.ObjectiveValue;

            if (analyze.Entering != 0 && analyze.Leaving != 0)
            {
                var output = String.Format("{1}{0}{2}{0}{3}",
                    Environment.NewLine,
                    analyze.Entering,
                    analyze.Leaving,
                    obj);

                File.WriteAllText(fileName + ".output", output);
            }
        }

        private static void WriteAssignment2Solution(string fileName)
        {
            var reader = new LinearProgramDictionaryReader();
            var linearProgram = reader.Read(fileName);

            var analyze = new SimpleAnalyzeEnteringLeavingVariables();
            var calculator = new HackyPivot2();
            var solver = new LinearProgramSolverWeek2(analyze, calculator);
            var solution = solver.Solve(linearProgram);

            if (solution.SolutionType == LinearProgramSolutionType.Unbounded)
            {
                const string output = "UNBOUNDED";
                File.WriteAllText(fileName + ".output", output);
            }
            else
            {
                var output = String.Format("{1}{0}{2}",
                    Environment.NewLine,
                    solution.ObjectiveValue,
                    solution.PivotCount);

                File.WriteAllText(fileName + ".output", output);
            }
        }

        private static void WriteAssignment3Solution(string fileName)
        {
            var reader = new LinearProgramDictionaryReader();
            var linearProgram = reader.Read(fileName);

            var analyze = new SimpleAnalyzeEnteringLeavingVariables();
            var calculator = new HackyPivot2();
            var primalToDual = new HackyMapPrimalToDual();
            var dualToPrimal = new HackyMapDualToPrimal();
            var restore = new SimpleRestoreObjective();

            var solver = new LinearProgramSolverWeek3(analyze, calculator, primalToDual, dualToPrimal, restore);

            var solution = solver.Solve(linearProgram);

            if (solution.SolutionType == LinearProgramSolutionType.Unbounded)
            {
                const string output = "UNBOUNDED";
                File.WriteAllText(fileName + ".output", output);
            }
            else if (solution.SolutionType == LinearProgramSolutionType.Infeasible)
            {
                const string output = "INFEASIBLE";
                File.WriteAllText(fileName + ".output", output);
            }
            else
            {
                var output = String.Format("{0}", solution.ObjectiveValue);
                File.WriteAllText(fileName + ".output", output);
            }
        }
    }
}
