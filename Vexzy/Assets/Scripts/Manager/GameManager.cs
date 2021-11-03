using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

    //[SerializeField] List<GameObject> playerList = new List<GameObject>();
    
    [SerializeField] public bool gameOver = false;
    [SerializeField] GameObject player;
    //private GameObject player;
    //[SerializeField] GameObject[] spawnPoint;
    //[SerializeField] GameObject tanker;
    //[SerializeField] GameObject soldier;
    //[SerializeField] GameObject ranger;
    //[SerializeField] Text leveltext;
    //[SerializeField] Text endGametxt;
    private int currentLevel;
    private int finalLevel = 5;
    private float generateSpawnTime = 1;
    private float currentSpawnTime = 0;
    //private GameObject newEnemy;
    private bool isPlayerExist = false;
    private List<EnemyHealth> enemies = new List<EnemyHealth>();
    private List<EnemyHealth> killememies = new List<EnemyHealth>();

    [SerializeField] GameObject arrow;

    // Health Power Up
    [SerializeField] GameObject healthPowerUp;
    [SerializeField] GameObject[] healthspawnPoint;
    [SerializeField] int maxPowerUp = 4;

    [SerializeField] public Transform spawnPointPlayer;

    private float powerUpSpawnTime = 3f;
    private float currentPowerUpSpawnTime = 0f;
    private int powerUp = 0;
    private GameObject newPowerUp;

    public int keyItemCount;
    public int currentKeyItem;

    private void Awake()
    {
        /*if (instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }*/
        instance = this;
        Time.timeScale = 1;
    }

    private void Start()
    {
        SpawnPlayer(spawnPointPlayer);

        var keyCout = GameObject.FindGameObjectsWithTag("Key");
        keyItemCount = keyCout.Length;

        StartCoroutine(spawn());
        StartCoroutine(powerUpSpawn());
        currentLevel = 1;
    }
    private void Update()
    {
        HealthCheck();
        KeyItemCheck();
        Cheat();
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
    public GameObject Arrow
    {
        get
        {
            return arrow;
        }
    }

    void SpawnPlayer(Transform spawnPosition)
    {
        if (!isPlayerExist)
        {
            
            //player = (GameObject)Instantiate(playerList[0], spawnPosition.position, Quaternion.identity);
            //Instantiate(player, spawnPosition.position, Quaternion.identity);   
            player = Instantiate(player, spawnPosition.position, Quaternion.identity);       
            isPlayerExist = true;
            
        }
    }

    void KeyItemCheck()
    {
        // player.GetComponent
        if (player != null)
        {
            if (currentKeyItem >= keyItemCount)
            {
                //if (player.GetComponentInChildren<PlayerStatus>().isOnGate)
                if (player.GetComponentInChildren<PlayerStatus>().isOnGate)
                {
                    Debug.Log(true);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }

    void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            currentKeyItem += 1;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            PlayerStatus.instance.maxHealth = 10000000;
            PlayerStatus.instance.curHealth = PlayerStatus.instance.maxHealth;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


    public GameObject Player
    {
        get
        {
            return player;
        }
    }
    public void HealthCheck()
    {
        //player.GetComponentInChildren<PlayerStatus>().curHealth <= 0
        //if (PlayerStatus.instance.curHealth > 0)
        if (player.GetComponentInChildren<PlayerStatus>().curHealth > 0 && EggStatus.Instance.currentHealth > 0)
        {
            gameOver = false;
        }
        /*if (EggStatus.Instance.currentHealth > 0)
        {
            gameOver = false;
        }*/
        else
        {
            gameOver = true;
            Debug.Log("gameOver");
            //StartCoroutine(endGame("Game Over!"));
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
    IEnumerator spawn()
    {
        if (currentSpawnTime>generateSpawnTime)
        {
            currentSpawnTime = 0;
            if (enemies.Count<currentLevel)
            {

                //int randomNumber = UnityEngine.Random.Range(0,spawnPoint.Length);
                //GameObject spawnLocation = spawnPoint[randomNumber];
                /*int randomEnemy = UnityEngine.Random.Range(0, 3);

                if (randomEnemy==0)
                {
                    newEnemy = Instantiate(soldier) as GameObject;
                }
                if (randomEnemy == 1)
                {
                    newEnemy = Instantiate(tanker) as GameObject;
                }
                if (randomEnemy == 2)
                {
                    newEnemy = Instantiate(ranger) as GameObject;
                }*/
                //newEnemy.transform.position = spawnLocation.transform.position;
            }

            /*if (killememies.Count==currentLevel && currentLevel!=finalLevel)
            {
                enemies.Clear();
                killememies.Clear();
                yield return new WaitForSeconds(3f);
                currentLevel++;
                leveltext.text = "Level = " + currentLevel;
            }
            if (killememies.Count == finalLevel)
            {
                StartCoroutine(endGame("Victory!"));
            }*/
        }
        yield return null;
        StartCoroutine(spawn());
    }

    /*IEnumerator endGame(string message)
    {
        endGametxt.text = message;
        endGametxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartGame");
    }*/

    IEnumerator powerUpSpawn()
    {
        if (currentPowerUpSpawnTime > powerUpSpawnTime)
        {
            currentPowerUpSpawnTime = 0;
            if (powerUp < maxPowerUp)
            {

                int randomNumber = UnityEngine.Random.Range(0, healthspawnPoint.Length);//0 
                GameObject spawnLocation = healthspawnPoint[randomNumber];
                newPowerUp = Instantiate(healthPowerUp) as GameObject;
                newPowerUp.transform.position = spawnLocation.transform.position;
            }
        }
        yield return null;
        StartCoroutine(powerUpSpawn());
    }
}
