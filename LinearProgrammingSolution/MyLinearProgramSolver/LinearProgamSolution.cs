namespace MyLinearProgramSolver
{
    public class LinearProgamSolution
    {
        public double ObjectiveValue { get; private set; }
        public int PivotCount { get; private set; }
        public LinearProgramSolutionType SolutionType { get; private set; }

        public LinearProgamSolution(double objectiveValue, int pivotCount, LinearProgramSolutionType solutionType)
        {
            ObjectiveValue = objectiveValue;
            PivotCount = pivotCount;
            SolutionType = solutionType;
        }
    }
}