using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace MyLinearProgramSolver.Tests
{
    [TestClass]
    public class MapPrimalToDualTests
    {
        private LinearProgramDictionaryReader _reader;
        private IMapPrimalToDual _mapPrimalToDual;
        private IMapDualToPrimal _mapDualToPrimal;
        private SimpleRestoreObjective _restore;

        private const string DictionaryFiles =
            @".\part3TestCases\fromLecture\";

        private const string DictionaryFiles10 =
            @".\part3TestCases\unitTests\10\";

        private const double Tolerance = .0001;

        [TestInitialize]
        public void Init()
        {
            _reader = new LinearProgramDictionaryReader();
            _mapPrimalToDual = new HackyMapPrimalToDual(); 
            _mapDualToPrimal = new HackyMapDualToPrimal();
            _restore = new SimpleRestoreObjective();
        }

        [TestMethod]
        public void ShouldMap_Vl01()
        {
            var primal = _reader.Read(DictionaryFiles + "vl1.dict");
            var dual = _mapPrimalToDual.Map(primal);

            //verify m,n
            dual.M.ShouldEqual(primal.N);
            dual.N.ShouldEqual(primal.M);

            //verify indices
            dual.BasicIndices.Length.ShouldEqual(primal.NonBasicIndices.Length);
            dual.BasicIndices[1].ShouldEqual(1);
            dual.BasicIndices[2].ShouldEqual(2);

            dual.NonBasicIndices.Length.ShouldEqual(primal.BasicIndices.Length);
            dual.NonBasicIndices[1].ShouldEqual(3);
            dual.NonBasicIndices[2].ShouldEqual(4);
            dual.NonBasicIndices[3].ShouldEqual(5);
            dual.NonBasicIndices[4].ShouldEqual(6);

            VerifyDual(primal, dual);
        }

        [TestMethod]
        public void ShouldSolveDual_Vl01()
        {
            var primal = _reader.Read(DictionaryFiles + "vl1.dict");
            var dual = _mapPrimalToDual.Map(primal);

            var solver = new LinearProgramSolverWeek2(new SimpleAnalyzeEnteringLeavingVariables(), new HackyPivot2());
            var solution = solver.Solve(dual);

            //verify objective
            solution.ObjectiveValue.ShouldEqual(4.0, Tolerance);

            //verify indices
            //dual.BasicIndices.ShouldEqual(new[] { 0, 1, 3 });
            //dual.NonBasicIndices.ShouldEqual(new[] { 0, 5, 2, 6, 4 });

            //verify coefficients
            dual.Coefficients[1].ShouldEqual(new[] { 1, -2.0 / 3.0, 1.0 / 3.0, -1.0 / 3.0, 2.0 / 3.0 }, Tolerance);
            dual.Coefficients[2].ShouldEqual(new[] { 1, -1.0 / 3.0, 2.0 / 3.0, -2.0 / 3.0, 1.0 / 3.0 }, Tolerance);

            //verify objective coefficients
            dual.ObjectiveCoefficients.ShouldEqual(new[] {4.0, -2.0, -2.0, -2.0, -2.0 }, Tolerance);
        }

        [TestMethod]
        public void ShouldMapDual_Vl01()
        {
            var primal = _reader.Read(DictionaryFiles + "vl1.dict");
            var dual = _mapPrimalToDual.Map(primal);

            var solver = new LinearProgramSolverWeek2(new SimpleAnalyzeEnteringLeavingVariables(), new HackyPivot2());
            solver.Solve(dual);

            var newPrimal = _mapDualToPrimal.Map(dual);

            newPrimal.Coefficients[1].ShouldEqual(new[] { 2.0, 2.0 / 3.0, 1.0 / 3.0 }, Tolerance);
            newPrimal.Coefficients[2].ShouldEqual(new[] { 2.0, -1.0 / 3.0, -2.0 / 3.0 }, Tolerance);
            newPrimal.Coefficients[3].ShouldEqual(new[] { 2.0, 1.0 / 3.0, 2.0 / 3.0 }, Tolerance);
            newPrimal.Coefficients[4].ShouldEqual(new[] { 2.0, -2.0 / 3.0, -1.0 / 3.0 }, Tolerance);
            newPrimal.ObjectiveCoefficients.ShouldEqual(new[] { -4.0, -1.0, -1.0 }, Tolerance);
        }

        [TestMethod]
        public void ShouldRestoreObjective_Vl01()
        {
            var primal = _reader.Read(DictionaryFiles + "vl1.dict");
            var dual = _mapPrimalToDual.Map(primal);

            var solver = new LinearProgramSolverWeek2(new SimpleAnalyzeEnteringLeavingVariables(), new HackyPivot2());
            solver.Solve(dual);

            var newPrimal = _mapDualToPrimal.Map(dual);
            _restore.Restore(newPrimal, dual.OriginalObjectiveCoefficients, dual.OriginalNonBasicIndices);
            
            newPrimal.Coefficients[1].ShouldEqual(new[] { 2.0, 2.0 / 3.0, 1.0 / 3.0 }, Tolerance);
            newPrimal.Coefficients[2].ShouldEqual(new[] { 2.0, -1.0 / 3.0, -2.0 / 3.0 }, Tolerance);
            newPrimal.Coefficients[3].ShouldEqual(new[] { 2.0, 1.0 / 3.0, 2.0 / 3.0 }, Tolerance);
            newPrimal.Coefficients[4].ShouldEqual(new[] { 2.0, -2.0 / 3.0, -1.0 / 3.0 }, Tolerance);
            newPrimal.ObjectiveCoefficients.ShouldEqual(new[] { 6, 4.0 / 3.0, 5.0 / 3.0 }, Tolerance);
        }

        [TestMethod]
        public void ShouldMap_10_Dict00()
        {
            var primal = _reader.Read(DictionaryFiles10 + "test0.dict");
            var dual = _mapPrimalToDual.Map(primal);

            //verify m,n
            dual.M.ShouldEqual(primal.N);
            dual.N.ShouldEqual(primal.M);

            dual.BasicIndices.ShouldEqual(new[] { 0, 1, 2, 3, 4, 5 });
            dual.NonBasicIndices.ShouldEqual(new[] { 0, 6, 7, 8, 9, 10 });

            VerifyDual(primal, dual);
        }

        [TestMethod]
        public void ShouldSolveDual_10_Dict00()
        {
            var primal = _reader.Read(DictionaryFiles10 + "test0.dict");
            var dual = _mapPrimalToDual.Map(primal);

            var solver = new LinearProgramSolverWeek2(new SimpleAnalyzeEnteringLeavingVariables(), new HackyPivot2());
            var solution = solver.Solve(dual);

            //verify objective
            solution.ObjectiveValue.ShouldEqual(2.4, Tolerance);
            solution.PivotCount.ShouldEqual(1);

            //verify indices
            dual.BasicIndices.ShouldEqual(new[] { 0, 1, 8, 3, 4, 5 });
            dual.NonBasicIndices.ShouldEqual(new[] { 0, 6, 7, 2, 9, 10 });

            //verify coefficients
            dual.Coefficients[1].ShouldEqual(new[] { .9, -6.8, 1.3, .1, 4.7, 9.0 }, Tolerance);
            dual.Coefficients[2].ShouldEqual(new[] { .1, -.2, -.3, -.1, -.7, 0.0 }, Tolerance);
            dual.Coefficients[3].ShouldEqual(new[] { .2, 11.6, -5.6, .8, -1.4, 3.0 }, Tolerance);
            dual.Coefficients[4].ShouldEqual(new[] { 1.8, .4, -7.4, -.8, -9.6, 7.0 }, Tolerance);
            dual.Coefficients[5].ShouldEqual(new[] { .1, 7.8, 6.7, .9, 12.3, 4.0 }, Tolerance);
        }

        [TestMethod]
        public void ShouldMapDual_10_Dict00()
        {
            var primal = _reader.Read(DictionaryFiles10 + "test0.dict");
            var dual = _mapPrimalToDual.Map(primal);

            var solver = new LinearProgramSolverWeek2(new SimpleAnalyzeEnteringLeavingVariables(), new HackyPivot2());
            solver.Solve(dual);

            var newPrimal = _mapDualToPrimal.Map(dual);

            newPrimal.Coefficients[1].ShouldEqual(new[] { 7.8, 6.8, .2, -11.6, -.4, -7.8 }, Tolerance);
            newPrimal.Coefficients[2].ShouldEqual(new[] { 8.2, -1.3, .3, 5.6, 7.4, -6.7 }, Tolerance);
            newPrimal.Coefficients[3].ShouldEqual(new[] { 2.4, -.1, .1, -.8, .8, -.9 }, Tolerance);
            newPrimal.Coefficients[4].ShouldEqual(new[] { 22.8, -4.7, .7, 1.4, 9.6, -12.3 }, Tolerance);
            newPrimal.Coefficients[5].ShouldEqual(new[] { 43.0, -9.0, 0.0, -3.0, -7.0, -4.0 }, Tolerance);

            newPrimal.BasicIndices.ShouldEqual(new[] { 0, 6, 7, 2, 9, 10 });
            newPrimal.NonBasicIndices.ShouldEqual(new[] { 0, 1, 8, 3, 4, 5 });

            newPrimal.ObjectiveCoefficients.ShouldEqual(new[] {-2.4, -.9, -.1, -.2, -1.8, -.1}, Tolerance);
        }

        [TestMethod]
        public void ShouldSolveDual_10_Dict12()
        {
            var primal = _reader.Read(DictionaryFiles10 + "test12.dict");
            var dual = _mapPrimalToDual.Map(primal);

            var solver = new LinearProgramSolverWeek2(new SimpleAnalyzeEnteringLeavingVariables(), new HackyPivot2());
            var solution = solver.Solve(dual);

            //verify objective
            solution.ObjectiveValue.ShouldEqual(1.0, Tolerance);
            solution.PivotCount.ShouldEqual(1);

            //verify indices
            dual.BasicIndices.ShouldEqual(new[] { 0, 1, 2, 3, 4, 5, 6, 10 });
            dual.NonBasicIndices.ShouldEqual(new[] { 0, 8, 9, 7, 11, 12, 13 });

            //verify coefficients
            dual.Coefficients[1].ShouldEqual(new[] { .75, 2.75, -2.25, .25, -7.0, -2.25, 6.25 }, Tolerance);
            dual.Coefficients[2].ShouldEqual(new[] { .5, -.5, 1.5, .5, 3.0, -3.5, .5 }, Tolerance);
            dual.Coefficients[3].ShouldEqual(new[] { .875, -.625, 2.875, .125, 5, 8.375, 6.625 }, Tolerance);
            dual.Coefficients[4].ShouldEqual(new[] { 1.625, -.875, 2.625, -.625, 10.0, 5.125, -7.125 }, Tolerance);
            dual.Coefficients[5].ShouldEqual(new[] { 1.375, -.125, 7.375, -.375, 0.0, 8.875, -2.875 }, Tolerance);
            dual.Coefficients[6].ShouldEqual(new[] { 2.25, 7.25, 8.25, -1.25, 1.0, -9.75, -6.25 }, Tolerance);
            dual.Coefficients[7].ShouldEqual(new[] { .125, .625, .125, -.125, 1.0, -.375, -.625 }, Tolerance);
        }

        [TestMethod]
        public void ShouldMap_10_Dict01()
        {
            var primal = _reader.Read(DictionaryFiles10 + "test1.dict");
            var dual = _mapPrimalToDual.Map(primal);

            //verify m,n
            dual.M.ShouldEqual(primal.N);
            dual.N.ShouldEqual(primal.M);

            dual.BasicIndices.ShouldEqual(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
            dual.NonBasicIndices.ShouldEqual(new[] { 0, 9, 10, 11, 12, 13 });

            VerifyDual(primal, dual);
        }

        private static void VerifyDual(LinearProgram primal, DualLinearProgram dual)
        {
            //verify coefficients
            dual.Coefficients.Length.ShouldEqual(primal.Coefficients[0].Length);

            //verify orig objective
            dual.OriginalObjectiveCoefficients.ShouldEqual(primal.ObjectiveCoefficients, Tolerance);

            for (var m = 1; m <= dual.M; m++)
            {
                dual.Coefficients[m][0].ShouldEqual(1, Tolerance);

                for (var n = 1; n <= dual.N; n++)
                {
                    dual.Coefficients[m][n].ShouldEqual(-primal.Coefficients[n][m], Tolerance);
                }
            }

            //verify objective coeficients
            dual.ObjectiveCoefficients[0].ShouldEqual(0.0);

            for (var n = 1; n <= dual.N; n++)
            {
                dual.ObjectiveCoefficients[n].ShouldEqual(-primal.Coefficients[n][0]);
            }

            //verify objective
            dual.ObjectiveValue.ShouldEqual(0.0);
        }
    }
}