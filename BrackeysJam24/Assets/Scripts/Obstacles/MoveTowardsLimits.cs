using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsLimits : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject[] limits;
    private int currentLimit = 0;
    private GameObject currrentLimitObject;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool spawned;
    [SerializeField] Animator animator;
    private bool _takingDamage;
    private static readonly int HasSpawned = Animator.StringToHash("HasSpawned");
    private static readonly int Damage = Animator.StringToHash("TakeDamage");

    private bool facingLeft = true;
    [SerializeField] private int _damage = 1;
    [Header("Health Config")]
    [SerializeField] private int maxHealth = 5;
    private int _currentHealth;
    private static readonly int Die = Animator.StringToHash("Die");
    
    
    private GameObject player;
    [SerializeField] private bool hasAttack;
    [SerializeField] private float attackRange;
    private bool attacking;
    
    private void Start()
    {
        currrentLimitObject = limits[currentLimit];
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        player = GameObject.FindWithTag("Player");
        _currentHealth = maxHealth;
        StartCoroutine(Spawn());
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
            TakeDamage(1);
        
        if (Vector2.Distance(currrentLimitObject.transform.position, transform.position) < 0.1f)
        {
            currentLimit = (currentLimit + 1) % limits.Length;
            currrentLimitObject = limits[currentLimit];
        }

        if ((transform.position.x > currrentLimitObject.transform.position.x && !facingLeft) || (transform.position.x < currrentLimitObject.transform.position.x && facingLeft))
            Flip();

        if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            if(attacking || !hasAttack) return;
            Debug.Log("Attack");
            attacking = true;
        }
        else
        {
            attacking = false;
        }
        

        if (!spawned || _takingDamage || attacking) return;
        
        
        transform.position = Vector2.MoveTowards(transform.position, currrentLimitObject.transform.position, speed * Time.deltaTime);
    }
    
    private void Flip()
    {
        facingLeft = !facingLeft;
        transform.Rotate(0f, 180f, 0f);
    }
    
    public void TakeDamage(int damage)
    {
        if(_takingDamage || !spawned) return;
        _takingDamage = true;
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            animator.SetTrigger(Die);
            this.enabled = false;
            Destroy(gameObject, 2f);
        }
        else
        {
            animator.SetTrigger(Damage);

        }
    }
    
    public void ResetTakingDamage()
    {
        _takingDamage = false;
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        animator.SetTrigger(HasSpawned);
        spawned = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Damage Player with " + _damage + " damage");
            // other.GetComponent<Player>().TakeDamage(_damage);
        }
    }
}
