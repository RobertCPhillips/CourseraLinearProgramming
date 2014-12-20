using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace MyLinearProgramSolver.Tests
{
    [TestClass]
    public class Assignment3SolverTests
    {
        private LinearProgramDictionaryReader _reader;
        private ILinearProgramSolver _solver;

        private const string DictionaryFiles10 =
            @".\part3TestCases\unitTests\10\";

        private const double Tolerance = .000001;

        [TestInitialize]
        public void Init()
        {
            _reader = new LinearProgramDictionaryReader();

            var analyze = new SimpleAnalyzeEnteringLeavingVariables();
            var pivotor = new HackyPivot2();
            var primalToDual = new HackyMapPrimalToDual();
            var dualToPrimal = new HackyMapDualToPrimal();
            var restore = new SimpleRestoreObjective();

            _solver = new LinearProgramSolverWeek3(analyze, pivotor, primalToDual, dualToPrimal, restore);
        }

        [TestMethod]
        public void ShouldSolve_10_Dict00()
        {
            var primal = _reader.Read(DictionaryFiles10 + "test0.dict");
            var solution = _solver.Solve(primal);

            solution.ObjectiveValue.ShouldEqual(32.612030, Tolerance);
        }

        [TestMethod]
        public void ShouldSolve_10_Dict01_Unbounded()
        {
            var primal = _reader.Read(DictionaryFiles10 + "test1.dict");
            var solution = _solver.Solve(primal);

            solution.SolutionType.ShouldEqual(LinearProgramSolutionType.Unbounded);
        }

        [TestMethod]
        public void ShouldSolve_10_Dict10_Ubounded()
        {
            var primal = _reader.Read(DictionaryFiles10 + "test10.dict");
            var solution = _solver.Solve(primal);

            solution.SolutionType.ShouldEqual(LinearProgramSolutionType.Unbounded);
        }

        [TestMethod]
        public void ShouldSolve_10_Dict11_Unbounded()
        {
            var primal = _reader.Read(DictionaryFiles10 + "test11.dict");
            var solution = _solver.Solve(primal);

            solution.SolutionType.ShouldEqual(LinearProgramSolutionType.Unbounded);
        }

        [TestMethod]
        public void ShouldSolve_10_Dict12()
        {
            var primal = _reader.Read(DictionaryFiles10 + "test12.dict");
            var solution = _solver.Solve(primal);

            solution.ObjectiveValue.ShouldEqual(17.200000, Tolerance);
        }

        [TestMethod]
        public void ShouldSolve_10_Dict13()
        {
            var primal = _reader.Read(DictionaryFiles10 + "test13.dict");
            var solution = _solver.Solve(primal);

            solution.ObjectiveValue.ShouldEqual(14.566667, Tolerance);
        }

        [TestMethod]
        public void ShouldSolve_10_Dict14()
        {
            var primal = _reader.Read(DictionaryFiles10 + "test14.dict");
            var solution = _solver.Solve(primal);

            solution.ObjectiveValue.ShouldEqual(-1.008584, Tolerance);
        }

        [TestMethod]
        public void ShouldSolve_10_Dict22_Infeasible()
        {
            var primal = _reader.Read(DictionaryFiles10 + "test22.dict");
            var solution = _solver.Solve(primal);

            solution.SolutionType.ShouldEqual(LinearProgramSolutionType.Infeasible);
        }
    }
}