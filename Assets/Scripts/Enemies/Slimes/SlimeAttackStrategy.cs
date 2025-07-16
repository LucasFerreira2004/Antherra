using UnityEngine;

public interface ISlimeAttackStrategy
{
    void Attack(SlimeContext context);
}

public abstract class SlimeAttackStrategy : ScriptableObject, ISlimeAttackStrategy
{
    public abstract void Attack(SlimeContext context);
}
