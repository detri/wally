using TMPro;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public float maxHealth = 15.0f;
    public string enemyName = "Enemy";
    public float moveSpeed = 1.0f;
    public float damage = 4.0f;
    public float currentHealth = 15.0f;
    public float expValue = 5.0f;
    public GameObject damageNumbers;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public GameObject expPickupPrefab;
    public Vector3 Center => spriteRenderer.bounds.center;
    
    private Transform playerPosition;
    private Rigidbody2D body;
    private float damageTimeout = 0.5f;
    private float curDamageTimeout = 0.5f;
    private static readonly int TakingDamage = Animator.StringToHash("TakingDamage");

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var direction = (playerPosition.position - transform.position).normalized;
        body.velocity = direction * moveSpeed;
        curDamageTimeout += Time.deltaTime;
        curDamageTimeout = Mathf.Clamp(curDamageTimeout, 0, damageTimeout);

        if (body.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (body.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DamagePlayer(other);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        DamagePlayer(other);
    }

    private void DamagePlayer(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && curDamageTimeout >= damageTimeout)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
            curDamageTimeout = 0.0f;
        }
    }

    public void TakeDamage(float amount, Vector2 projectileDirection)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        var dmgObject = Instantiate(damageNumbers, Center, Quaternion.identity);
        var textMesh = dmgObject.GetComponent<TMP_Text>();
        textMesh.text = $"{amount}";
        dmgObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1f, 2f), 0f) * projectileDirection + new Vector2(0, 3f);
        Destroy(dmgObject, 2.0f);
        
        animator.SetBool(TakingDamage, true);
        
        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void EndTakingDamage()
    {
        animator.SetBool(TakingDamage, false);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void Die()
    {
        var expPickup = Instantiate(expPickupPrefab, Center, Quaternion.identity);
        expPickup.GetComponent<ExpPickup>().ChangeValue(expValue);
        Destroy(gameObject);
    }
}
