using UnityEngine;

public interface ISlimeAttackStrategy
{
    void Attack(SlimeScript context);
}

public abstract class SlimeAttackStrategy : ScriptableObject, ISlimeAttackStrategy
{
    public abstract void Attack(SlimeScript context);
}
