using UnityEngine;

public abstract class IBaseStatus : ScriptableObject
{
    public abstract float Speed { get; set; }
    public abstract int BulletDamage { get; set; }
    public abstract float BulletFireRate { get; set; }
    public abstract float BulletSpeed { get; set; }
    public abstract float BulletRange { get; set; }
}
