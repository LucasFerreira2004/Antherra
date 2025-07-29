
using System;
using UnityEngine;

public abstract class IPowerUpEffectSO : ScriptableObject, IPowerUpEffect
{
    [Header("PowerUp Info")]
    [SerializeField] private string powerUpName;
    [TextArea]
    [SerializeField] private string powerUpDescription;
    [SerializeField] private Sprite icon;
    public abstract void Apply(PlayerStatus target);

    public string Name
    {
        get => powerUpName;
    }

    public string Description
    {
        get => powerUpDescription;
    }

    public Sprite Icon
    {
        get => icon;
        set => icon = value;
    }
}