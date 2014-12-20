namespace MyLinearProgramSolver
{
    public class DualLinearProgram : LinearProgram
    {
        public int[] OriginalNonBasicIndices { get; set; }
        public double[] OriginalObjectiveCoefficients { get; set; }
        public int[][] Complements { get; set; }
    }
}