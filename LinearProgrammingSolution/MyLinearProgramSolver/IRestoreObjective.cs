namespace MyLinearProgramSolver
{
    public interface IRestoreObjective
    {
        void Restore(LinearProgram linearProgram, double[] originalCoeficients, int[] originalIndices);
    }
}