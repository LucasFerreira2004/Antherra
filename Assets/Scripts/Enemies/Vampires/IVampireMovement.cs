public interface IVampireMovement
{
    public float GetIdealDistance();
    public float GetMinAttackDistance();
    public void Move(VampireScript context);
}
