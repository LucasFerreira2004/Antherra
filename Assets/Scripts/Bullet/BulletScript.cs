using UnityEngine;

public interface IEnemyTakeDamage
{
    public void TakeDamage(float damage);
    event System.Action<GameObject> OnEnemyDeath;
}


public class BulletScript : MonoBehaviour
{
    private bool initialized = false;
    private float range;
    private int damage;

    private Vector2 startPosition;
    private GameObject owner;

    public void Init(float range, int damage, GameObject owner)
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

        bool isSameTag = gameObject.tag == other.gameObject.tag;

        bool isWall = other.gameObject.CompareTag("Wall") && !isSameTag;
        bool isBullet = other.gameObject.CompareTag("Bullet") && !isSameTag;

        // Destroi ao colidir com parede
        if (isBullet || isWall)
        {
            Destroy(gameObject);
            return;
        }

        bool isEnemy = other.gameObject.CompareTag("Enemy");
        bool isPlayer = other.gameObject.CompareTag("Player");

        if (isEnemy || isPlayer)
        {
            if (other.gameObject.tag == owner.tag) return;

            // Aqui você pode aplicar o dano ao inimigo
            // Exemplo:
            if (isEnemy)
            {
                other.gameObject.GetComponent<IEnemyTakeDamage>()?.TakeDamage(damage);

            }
            else if (isPlayer)
            {
                other.gameObject.GetComponent<PlayerStatus>().TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }

    //getter e setter
    public float Range
    {
        get => range;
        set => range = Mathf.Max(0.1f, value);
    }

    public int Damage
    {
        get => damage;
        set => damage = value;
    }
}

