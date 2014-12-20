namespace MyLinearProgramSolver
{
    public interface IPivotDictionary
    {
        void Pivot(int entering, int leaving, LinearProgram linearProgram);
    }
}