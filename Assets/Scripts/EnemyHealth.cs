using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public PlayerMovement player;
    public float enemyHealth;
    public bool isBeingHit = false;
    public float timeBetweenDamage = 0;
    public float timer;
    public Animator animator;
    public KnightEnemyMovement enemyMovement;

    public PlayerSFX sfxScript;
    public GameController gameController;
    public SpriteRenderer spriteRenderer;
    private bool isFlashingRed = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    public void ResetEnemy()
    {
        StopAllCoroutines(); // Stop any active flash
        isFlashingRed = false; // Reset the flag
        spriteRenderer.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHit && timer <= 0)
        {
            EnemyTakeDamage();
        }
    }
    public void EnemyTakeDamage()
    {
        enemyHealth -= player.playerDamage;
        sfxScript.EnemyHurt();
        timer = timeBetweenDamage;

        StartCoroutine(FlashRed());

        if (enemyHealth <= 0)
        {
            Debug.Log("Enemy is dead!");
            gameController.numEnemiesDead++;
            gameObject.SetActive(false);
            isBeingHit = false;
            enemyMovement.isAttacking = false;
            spriteRenderer.color = Color.white;
            timer = 0;
        }
    }

    public void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= 1;
            isBeingHit = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDamage"))
        {
            Debug.Log("Enemy hit!");
            isBeingHit = true;
        }
    }

    private IEnumerator FlashRed()
    {
        isFlashingRed = true; // Tell LateUpdate to start coloring
        yield return new WaitForSeconds(0.2f);
        isFlashingRed = false; // Tell LateUpdate to stop
        spriteRenderer.color = Color.white;
    }

    void LateUpdate()
    {
        if (isFlashingRed)
        {
            spriteRenderer.color = Color.red;
        }
    }
}
