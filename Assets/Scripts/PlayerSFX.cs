using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class PlayerSFX : MonoBehaviour
{
    public AudioClip enemyHurtSound;
    public AudioClip gameFinishedSound;
    public AudioClip gameOverSound;
    public AudioClip punchSound;
    public AudioClip playerHurtSound;

    public AudioSource audioSource;
    
    public void PlayerDied()
    {
        audioSource.PlayOneShot(gameOverSound);
    }

    public void PlayerGotHit()
    {
        audioSource.PlayOneShot(playerHurtSound);
    }

    public void PlayerPunched()
    {
        audioSource.PlayOneShot(punchSound);
    }

    public void GameFinished()
    {
        audioSource.PlayOneShot(gameFinishedSound);
    }

    public void EnemyHurt()
    {
        audioSource.PlayOneShot(enemyHurtSound);
    }
}
