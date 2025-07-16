using UnityEngine;

public interface ISlimeMovementStrategy
{
    void Move(SlimeContext context);
}

public abstract class SlimeMovementStrategy : ScriptableObject, ISlimeMovementStrategy
{
    public abstract void Move(SlimeContext context);
}
