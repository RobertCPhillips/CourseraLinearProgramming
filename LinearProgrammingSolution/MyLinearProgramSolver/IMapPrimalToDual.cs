namespace MyLinearProgramSolver
{
    public interface IMapPrimalToDual
    {
        DualLinearProgram Map(LinearProgram primal);
    }
}