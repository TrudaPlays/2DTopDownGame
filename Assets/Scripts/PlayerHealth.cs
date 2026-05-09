using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    public HealthUI healthUI;
    public PlayerSFX sfxScript;


    public SpriteRenderer spriteRenderer;
    public Sprite startingImage;

    public static event Action OnPlayerDeath;
    public bool isPlayerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = startingImage;
        GameController.OnGameReset += ResetHealth;
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth;
        healthUI.setMaxHearts(maxHealth);
        isPlayerDead = false;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        KnightEnemyMovement enemy = collision.GetComponent<KnightEnemyMovement>();
        if (enemy)
        {
            TakeDamage(enemy.damage);
        }
    }*/

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateHearts(currentHealth);
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            //player is dead
            isPlayerDead = true;
            OnPlayerDeath.Invoke();

        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }


}
