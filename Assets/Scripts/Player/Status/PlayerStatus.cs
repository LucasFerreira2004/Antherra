using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private static int playModeIndex = 0;
    private BaseStatusStrategy baseStatusStrategy;

    [SerializeField] private List<PlayerMode> playerModes;
    [SerializeField] private BaseStatusFactory baseStatusFactory;

    public void Awake()
    {
        baseStatusStrategy = baseStatusFactory.GetBaseStatus(playerModes[0]);
        GetComponent<SpriteRenderer>().color = baseStatusStrategy.CharacterSpriteColor;
    }

    public void OnChangeMode()
    {
        playModeIndex = (playModeIndex + 1) % playerModes.Count;
        baseStatusStrategy = baseStatusFactory.GetBaseStatus(playerModes[playModeIndex]);
        GetComponent<SpriteRenderer>().color = baseStatusStrategy.CharacterSpriteColor;
    }

    // Delegações de status
    public float Speed
    {
        get => baseStatusStrategy.Speed;
        set => baseStatusStrategy.Speed = Mathf.Max(0, value);
    }

    public int BulletDamage
    {
        get => baseStatusStrategy.BulletDamage;
        set => baseStatusStrategy.BulletDamage = value;
    }

    public float FireRate
    {
        get => baseStatusStrategy.BulletFireRate;
        set => baseStatusStrategy.BulletFireRate = Mathf.Max(0.1f, value);
    }

    public float BulletSpeed
    {
        get => baseStatusStrategy.BulletSpeed;
        set => baseStatusStrategy.BulletSpeed = Mathf.Max(0.1f, value);
    }

    public float BulletRange
    {
        get => baseStatusStrategy.BulletRange;
        set => baseStatusStrategy.BulletRange = Mathf.Max(0.1f, value);
    }

    public List<PlayerMode> PlayerMode => playerModes;
}
