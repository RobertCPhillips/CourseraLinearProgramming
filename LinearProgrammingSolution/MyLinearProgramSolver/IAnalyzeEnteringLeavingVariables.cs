namespace MyLinearProgramSolver
{
    public interface IAnalyzeEnteringLeavingVariables
    {
        int Entering { get; }
        int Leaving { get; }

        void Analyze(LinearProgram linearProgram);
    }
}