using UnityEngine;

public interface ITakeDamage
{
    public void TakeDamage(float damage);
}

public class BulletScript : MonoBehaviour
{
    private bool initialized = false;
    private float range;
    private float damage;

    private Vector2 startPosition;
    private GameObject owner;

    public void Init(float range, float damage, GameObject owner)
    {
        this.range = range;
        this.damage = damage;
        initialized = true;
        startPosition = transform.position;
        this.owner = owner;
    }
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (!initialized)
        {
            Debug.LogError("Bullet was not initialized! Use Init(range, damage) after instantiation.");
            Destroy(gameObject);
            return;
        }
        if (Vector2.Distance(startPosition, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == owner) return;

        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            // Aqui você pode aplicar o dano ao inimigo
            // Exemplo:
            other.GetComponent<ITakeDamage>().TakeDamage(damage);

            Destroy(gameObject);
        }
    }

    //getter e setter
    public float Range
    {
        get => range;
        set => range = Mathf.Max(0.1f, value);
    }

    public float Damage
    {
        get => damage;
        set => damage = Mathf.Max(0.1f, value);
    }
}

