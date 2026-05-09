using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerSFX))]
public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject bearEnemy;
    public GameObject dinoEnemy;

    CinemachineConfiner confiner;
    [SerializeField] PolygonCollider2D mapBoundary;

    public GameObject gameOverScreen;
    public GameObject gameFinishedScreen;

    public List<GameObject> enemyList = new List<GameObject>();
    public float numEnemiesDead;
    public float numberOfEnemies;

    public bool gameIsFinished = false;

    public PlayerSFX sfxScript;
    public SpriteRenderer bearSpriteRenderer;
    public SpriteRenderer dinoSpriteRenderer;

    public static event Action OnGameReset;

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
    }

    // Start is called before the first frame update
    void Start()
    {
        CountEnemies();
        bearEnemy.transform.position = new Vector3(5.09f, -0.1f, 0f);
        dinoEnemy.transform.position = new Vector3(14.6f, 8.4f, 0f);
        player.transform.position = new Vector3(0f, 0f, 0f);
        PlayerHealth.OnPlayerDeath += GameOverScreen;
        gameOverScreen.SetActive(false);
        gameFinishedScreen.SetActive(false);

    }

    public void CheckEnemyCount()
    {
        if(numberOfEnemies == numEnemiesDead)
        {
            gameIsFinished = true;
            gameFinishedScreen.SetActive(true);
            Debug.Log("You finished the game!");

        }
    }

    void CountEnemies()
    {
        foreach(GameObject enemy in enemyList)
        {
            numberOfEnemies++;
        }
    }

    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
        sfxScript.PlayerDied();
        Time.timeScale = 0f;
    }

    public void ResetGameCamera()
    {
        confiner.m_BoundingShape2D = mapBoundary;
    }

    public void ResetGame()
    {
        gameIsFinished = false;
        numEnemiesDead = 0;
        bearEnemy.SetActive(true);
        dinoEnemy.SetActive(true);
        gameOverScreen.SetActive(false);
        gameFinishedScreen.SetActive(false);
        bearEnemy.transform.position = new Vector3(5.09f, -0.1f, 0f);
        dinoEnemy.transform.position = new Vector3(14.6f, 8.4f, 0f);
        player.transform.position = new Vector3(0f, 0f, 0f);
        bearEnemy.GetComponent<EnemyHealth>().ResetEnemy();
        dinoEnemy.GetComponent<EnemyHealth>().ResetEnemy();
        ResetGameCamera();
        Time.timeScale = 1f;
        OnGameReset?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemyCount();
    }
}
