using Should;

namespace MyLinearProgramSolver.Tests
{
    public static class ShouldHelpers
    {
        public static void ShouldEqual(this double[] actual, double[] expected, double tolerance)
        {
            for (var i = 0; i < expected.Length; i++)
            {
                actual[i].ShouldEqual(expected[i], tolerance);
            }
        }
    }
}
