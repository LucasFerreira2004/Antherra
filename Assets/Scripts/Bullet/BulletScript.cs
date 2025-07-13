using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private bool initialized = false;
    private float range;
    private float damage;

    private Vector2 startPosition;

    public void Init(float range, float damage)
    {
        this.range = range;
        this.damage = damage;
        initialized = true;
        startPosition = transform.position;
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
        if (other.CompareTag("Enemy"))
        {
            // Aqui você pode aplicar o dano ao inimigo
            // Exemplo:
            // other.GetComponent<Enemy>().TakeDamage(damage);

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

