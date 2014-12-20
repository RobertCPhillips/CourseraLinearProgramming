namespace MyLinearProgramSolver
{
    public interface ILinearProgramSolver
    {
        LinearProgamSolution Solve(LinearProgram linearProgram);
    }
}