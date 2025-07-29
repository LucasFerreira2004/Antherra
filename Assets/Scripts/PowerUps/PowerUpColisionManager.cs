using UnityEngine;

public class PowerUpColisionManager : MonoBehaviour
{
    [SerializeField] private IPowerUpEffectSO powerUpEffect;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            powerUpEffect.Apply(collision.gameObject.GetComponent<PlayerStatus>());
        }

    }
    
    public IPowerUpEffectSO PowerUpEffect
    {
        get => powerUpEffect;
        set => powerUpEffect = value;
    }
}