
using UnityEngine;

public abstract class IPowerUpEffectSO : ScriptableObject, IPowerUpEffect   
{
    public abstract void Apply(PlayerStatus target);
}