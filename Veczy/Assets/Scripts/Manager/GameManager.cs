using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance = null;
    [SerializeField] private bool gameOver = false;
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPointPlayer;
    //มอนสเตอร์
    //[SerializeField] GameObject  sad;
    [SerializeField] Text levelText;
    [SerializeField] Text endGameText;
    private int currentLevel;
    private int finalLevel = 5;
    private float generateSpawnTime = 1;
    private float currentSpawnTime = 0;
    private GameObject newEnemy;

    private List<EnemyHealth> enemies = new List<EnemyHealth>();
    private List<EnemyHealth> killememies = new List<EnemyHealth>();

    // Health Power Up
    [SerializeField] GameObject healthPowerUp;
    [SerializeField] GameObject[] enemySpawnPoint;
    [SerializeField] int maxPowerUp = 4;

    private float powerUpSpawnTime = 3f;
    private float currentPowerUpSpawnTime = 0f;
    private int powerUp = 0;
    private GameObject newPowerUp;

    private void Awake()
    {
        instance = this;
        if (instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(spawn());
        StartCoroutine(powerUpSpawn());
        currentLevel = 1;
    }
    private void Update()
    {
        currentSpawnTime += Time.deltaTime;
        currentPowerUpSpawnTime += Time.deltaTime;
    }
    public bool GameOver
    {
        get
        {
            return gameOver;
        }
    }

    public GameObject Player
    {
        get
        {
            return player;
        }
    }
    public void PlayerHit(int currentHP)
    {
        if (currentHP>0)
        {
            gameOver = false;
        }
        else
        {
            gameOver = true;
            StartCoroutine(endGame("Game Over!"));
        }
    }

    public void RegisterEnemy(EnemyHealth enemy)
    {
            enemies.Add(enemy);
    }
    public void killedEnemy(EnemyHealth enemy)
    {
            killememies.Add(enemy);
    }

    public void RegisterPowerUp()
    {
        powerUp++;
    }

    void SpawnPlayer(Transform spawnPosition)
    {
        
    }
    
    IEnumerator spawn()
    {
        if (currentSpawnTime>generateSpawnTime)
        {
            currentSpawnTime = 0;
            if (enemies.Count<currentLevel)
            {

                SpawnPlayer(spawnPointPlayer);

                Transform spawnLocation = spawnPointPlayer;
                int randomEnemy = Random.Range(0, 3);

                if (randomEnemy==0)
                {
           //         newEnemy = Instantiate(soldier) as GameObject;
                }
                if (randomEnemy == 1)
                {
            //        newEnemy = Instantiate(tanker) as GameObject;
                }
                if (randomEnemy == 2)
                {
            //        newEnemy = Instantiate(ranger) as GameObject;
                }
                newEnemy.transform.position = spawnLocation.transform.position;
            }

            if (killememies.Count==currentLevel && currentLevel!=finalLevel)
            {
                enemies.Clear();
                killememies.Clear();
                yield return new WaitForSeconds(3f);
                currentLevel++;
                levelText.text = "Level = " + currentLevel;
            }
            if (killememies.Count == finalLevel)
            {
                StartCoroutine(endGame("Victory!"));
            }
        }
        yield return null;
        StartCoroutine(spawn());
    }

    IEnumerator endGame(string message)
    {
        endGameText.text = message;
        endGameText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator powerUpSpawn()
    {
        if (currentPowerUpSpawnTime > powerUpSpawnTime)
        {
            currentPowerUpSpawnTime = 0;
            if (powerUp < maxPowerUp)
            {

                int randomNumber = Random.Range(0, enemySpawnPoint.Length);//0 
                GameObject spawnLocation = enemySpawnPoint[randomNumber];
                newPowerUp = Instantiate(healthPowerUp);
                newPowerUp.transform.position = spawnLocation.transform.position;
            }
        }
        yield return null;
        StartCoroutine(powerUpSpawn());
    }
}
