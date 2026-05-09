using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnightEnemyMovement : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    public float attackRange;
    public Animator animator;
    public GameObject player;
    public bool isAttacking = false;
    public bool attackForever = false;
    public float hitBackSpeed;
    public PlayerHealth playerHealth;
    public Rigidbody2D playerRB;
    public float playerHitBackSpeed = 40f;
    public PlayerMovement playerMovement;
    public float playerHitBackTime = 0.2f;

    public PlayerSFX sfxScript;

    public SpriteRenderer spriteRenderer;

    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameController.OnGameReset += StopMoving;

    }

    private void StopMoving()
    {
        isAttacking = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        Vector2 delta = player.transform.position - transform.position;
        float distance = delta.magnitude;
        Vector2 direction = delta.normalized;

        if (distance < attackRange)
        {
            isAttacking = true;
        }
        else if(!attackForever)
        {
            isAttacking = false;
        }

        if (enemyHealth.isBeingHit)
        {
            sfxScript.EnemyHurt();
            rb.velocity = -direction * hitBackSpeed;
        }
        else
        {
            if (isAttacking)
            {
                rb.velocity = direction * moveSpeed;
                animator.SetBool("isWalking", true);
                return;
            }
            else
            {
                rb.velocity = Vector2.zero;
                animator.SetBool("isWalking", false);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerHurtBox"))
        {
            StartCoroutine(KnockPlayerBack());
            Debug.Log("Player hit!");
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private IEnumerator KnockPlayerBack()
    {
        Vector2 delta = player.transform.position - transform.position;
        Vector2 direction = delta.normalized;
        playerMovement.isBeingKnockedBack = true;
        sfxScript.PlayerGotHit();
        playerRB.velocity = direction * playerHitBackSpeed;
        yield return new WaitForSeconds(0.2f);
        playerRB.velocity = Vector2.zero;
        playerMovement.isBeingKnockedBack = false;

    }
}

