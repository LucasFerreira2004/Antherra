using UnityEngine;

public interface ISlimeMovementStrategy
{
    void Move(SlimeScript context);
}

public abstract class SlimeMovementStrategy : ScriptableObject, ISlimeMovementStrategy
{
    public abstract void Move(SlimeScript context);
}
