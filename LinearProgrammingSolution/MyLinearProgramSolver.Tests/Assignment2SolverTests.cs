using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace MyLinearProgramSolver.Tests
{
    [TestClass]
    public class Assignment2SolverTests
    {
        private LinearProgramDictionaryReader _reader;
        private ILinearProgramSolver _solver;

        private const string DictionaryFiles =
            @".\part2TestCases\unitTests\";

        private const double Tolerance = .000001;

        [TestInitialize]
        public void Init()
        {
            _reader = new LinearProgramDictionaryReader();

            var analyze = new SimpleAnalyzeEnteringLeavingVariables();
            var pivotor = new HackyPivot2();
            _solver = new LinearProgramSolverWeek2(analyze, pivotor);
        }

        [TestMethod]
        public void Dict1_ShouldPivot_3_WithObjective_7_0()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict1");
            var solution = _solver.Solve(linearProgram);

            solution.PivotCount.ShouldEqual(3);
            solution.ObjectiveValue.ShouldEqual(7.0, Tolerance);
        }

        [TestMethod]
        public void Dict2_ShouldPivot_1_WithObjective_4_0()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict2");
            var solution = _solver.Solve(linearProgram);

            solution.PivotCount.ShouldEqual(1);
            solution.ObjectiveValue.ShouldEqual(4.0, Tolerance);
        }

        [TestMethod]
        public void Dict3_ShouldPivot_2_WithObjective_3_0()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict3");
            var solution = _solver.Solve(linearProgram);

            solution.PivotCount.ShouldEqual(2);
            solution.ObjectiveValue.ShouldEqual(3.0, Tolerance);
        }

        [TestMethod]
        public void Dict4_ShouldPivot_3_WithObjective_28_0()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict4");
            var solution = _solver.Solve(linearProgram);

            solution.PivotCount.ShouldEqual(3);
            solution.ObjectiveValue.ShouldEqual(28.0, Tolerance);
        }

        [TestMethod]
        public void Dict5_ShouldPivot_4_WithObjective_60_0()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict5");
            var solution = _solver.Solve(linearProgram);

            solution.PivotCount.ShouldEqual(4);
            solution.ObjectiveValue.ShouldEqual(60.0, Tolerance);
        }

        [TestMethod]
        public void Dict6_ShouldBe_Unbounded()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict6");
            var solution = _solver.Solve(linearProgram);

            solution.PivotCount.ShouldEqual(0);
            solution.SolutionType.ShouldEqual(LinearProgramSolutionType.Unbounded);
        }

        [TestMethod]
        public void Dict7_ShouldPivot_1_WithObjective_6_0()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict7");
            var solution = _solver.Solve(linearProgram);

            solution.PivotCount.ShouldEqual(1);
            solution.ObjectiveValue.ShouldEqual(6.0, Tolerance);
        }

        [TestMethod]
        public void Dict8_ShouldPivot_2_WithObjective_6_729522()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict8");
            var solution = _solver.Solve(linearProgram);

            solution.PivotCount.ShouldEqual(2);
            solution.ObjectiveValue.ShouldEqual(6.729522,Tolerance);
        }

        [TestMethod]
        public void Dict9_ShouldPivot_2_WithObjective_0_272727()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict9");
            var solution = _solver.Solve(linearProgram);

            solution.PivotCount.ShouldEqual(2);
            solution.ObjectiveValue.ShouldEqual(0.272727, Tolerance);
        }

        [TestMethod]
        public void Dict10_ShouldPivot_18_WithObjective_9_332270()
        {
            var linearProgram = _reader.Read(DictionaryFiles + @"dict10");
            var solution = _solver.Solve(linearProgram);

            solution.PivotCount.ShouldEqual(18);
            solution.ObjectiveValue.ShouldEqual(9.332270, Tolerance);
        }
    }
}