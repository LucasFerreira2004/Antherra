using UnityEngine;

public interface ITakeDamage
{
    public void TakeDamage(float damage);
}

public class BulletScript : MonoBehaviour
{
    [SerializeField] private Animator bulletAnimator; 
    private bool initialized = false;
    private float range;
    private float damage;
    private Vector2 startPosition;
    private GameObject owner;
    private bool isExploding = false;

    public void Init(float range, float damage, GameObject owner)
    {
        this.range = range;
        this.damage = damage;
        initialized = true;
        startPosition = transform.position;
        this.owner = owner;
        bulletAnimator = GetComponent<Animator>();
    }

     private void StartExplosion()
    {
        if (isExploding) return;
        isExploding = true;
        Debug.Log("===================vai setar o trigger");
        bulletAnimator.SetBool("IsExploding", true);
        Debug.Log("trigger setado ====================");
        StartCoroutine(DestroyAfterAnimation());
    }

    private System.Collections.IEnumerator DestroyAfterAnimation()
    {
        float animationLength =  bulletAnimator.GetCurrentAnimatorStateInfo(1).length;
                Debug.Log("esperando");

        yield return new WaitForSeconds(animationLength);
        Destroy(gameObject);
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
            StartExplosion();
            Destroy(gameObject);
            return;
        }
        if (Vector2.Distance(startPosition, transform.position) >= range)
        {
            StartExplosion();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isExploding) return;
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

