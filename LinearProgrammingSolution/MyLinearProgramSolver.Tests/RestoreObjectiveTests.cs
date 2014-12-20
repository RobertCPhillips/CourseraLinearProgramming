using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyLinearProgramSolver.Tests
{
    [TestClass]
    public class RestoreObjectiveTests
    {
        [TestMethod]
        public void VideoLecture01()
        {
            var lp = new LinearProgram
            {
                M = 4,
                N = 2,
                BasicIndices = new[] {0, 1, 4, 2, 6},
                NonBasicIndices = new[] {0, 3, 5},
                Coefficients = new[]
                {
                    new[] {0.0, 0.0, 0.0},
                    new[] {2.0, 2.0/3.0, 1.0/3.0},
                    new[] {2.0, -1.0/3.0, -2.0/3.0},
                    new[] {2.0, 1.0/3.0, 2.0/3.0},
                    new[] {2.0, -2.0/3.0, -1.0/3.0}
                },
                ObjectiveCoefficients = new[] { 9.0, 9.0, 9.0 }
            };

            var restorer = new SimpleRestoreObjective();
            restorer.Restore(lp, new[] { 0.0, 1.0, 2.0 }, new[] { 0, 1, 2 });

            lp.ObjectiveCoefficients.ShouldEqual(new[] { 6.0, 4.0 / 3.0, 5.0 / 3.0 }, .000001);

        }
    }
}