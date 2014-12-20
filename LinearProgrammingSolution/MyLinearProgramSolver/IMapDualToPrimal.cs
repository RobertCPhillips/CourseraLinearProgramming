namespace MyLinearProgramSolver
{
    public interface IMapDualToPrimal
    {
        LinearProgram Map(DualLinearProgram dual);
    }
}