//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Wave Enemy
    public enum SpawnState { SPAWNING, WAITING, COUNTING, FINISHED};
    [System.Serializable]
    public class WaveEnemy
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    //Game Over
    [SerializeField] public bool gameOver = false;
    [SerializeField] public bool gameWiner = false;
    [SerializeField] public bool gameRound = false;
    [SerializeField] int piller1;
    [SerializeField] int piller2;
    [SerializeField] public int piller3;
    [SerializeField] GameObject player;
    [SerializeField] public Transform spawnPointPlayer;  
    public string NameScenes;
    // Key
    public int keyItemCount;
    public int currentKeyItem;
    
    public bool isEnemyWave = false;
    public WaveEnemy[] waves;
    private int nextWave = 0;

    public Transform[] spawnPointsEnemy;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;



    // Nanager
    public static GameManager instance;
    //private float generateSpawnTime = 1;
    private float currentSpawnTime = 0;
    //private GameObject newEnemy;
    private bool isPlayerExist = false;
    private List<EnemyHealth> enemies = new List<EnemyHealth>();
    private List<EnemyHealth> killememies = new List<EnemyHealth>();


    // Enemy Up time
    public bool isEnemyTime = false;
    [SerializeField] public GameObject enemyType1;
    [SerializeField] public GameObject enemyType2;
    [SerializeField] public GameObject enemyType3;
    [SerializeField] int maxEnemy = 4;
    [SerializeField] private float powerUpSpawnTime = 3f; 
    [SerializeField] GameObject[] enemySpawnPointX2;  
    private float currentSpawnTimeEnemy = 0f;
    private int enemyCurrent = 0;
    private GameObject newPowerUp;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
    }

    private void Start()
    {
        SpawnPlayer(spawnPointPlayer);

        var keyCout = GameObject.FindGameObjectsWithTag("Key");
        keyItemCount = keyCout.Length;

        //StartCoroutine(EnemyUpSpawnX2());
        /*if(isEnemyWave)
        {
            EnemyV1();
        }
        EnemyV2();*/
        //Wave
        /*if(spawnPointsEnemy.Length == 0)
        {
            Debug.LogError("No spwan points enemy.");
        }*/
        waveCountdown = timeBetweenWaves;
    }
    private void Update()
    {
        RuleCheck();
        KeyItemCheck();
        Cheat();

        //enemyv1
        if(isEnemyWave)
        {
            EnemyV1();
        }

        //enemyv1
        if(isEnemyTime)
        {
            EnemyV2();
            currentSpawnTime += Time.deltaTime;
            currentSpawnTimeEnemy += Time.deltaTime;
            
            if(spawnPointsEnemy.Length == 0 && isEnemyTime == true)
            {
                Debug.LogError("No spwan points enemy.");
            }
            //waveCountdown = timeBetweenWaves;
        }
        //currentSpawnTime += Time.deltaTime;
        //currentSpawnTimeEnemy += Time.deltaTime;
    }
    public bool GameOver
    {
        get
        {
            return gameOver;
        }
    }
    public bool GameWiner
    {
        get
        {
            return gameOver;
        }
    }
    void WeveCompleted()
    {
        Debug.Log("Wave Completed!");
        StartCoroutine(StatusShow("Wave Completed!"));
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave +1 > waves.Length -1)
        {
            state=SpawnState.FINISHED;
            gameWiner = true;
            Debug.Log("All Waves Completed!");
            StartCoroutine(StatusShow("All Waves Completed!"));
            CheckWiner();
        }
        else
        {
            nextWave++;
        }
            //nextWave++;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                 return false;
             }
        }
        return true;
    }

    void SpawnPlayer(Transform spawnPosition)
    {
        if (!isPlayerExist)
        {  
            player = Instantiate(player, spawnPosition.position, Quaternion.identity);       
            isPlayerExist = true;
        }
    }
    void KeyItemCheck()
    {
        if (player != null)
        {
            if (currentKeyItem >= keyItemCount)
            {
                //if (player.GetComponentInChildren<PlayerStatus>().isOnGate)  isSpawned = true
                if (player.GetComponentInChildren<PlayerStatus>().isOnGate)
                {
                    //Debug.Log(true);
                    //SceneManager.LoadScene(NameScenes);
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                    if(spawnPet.Instance.isSpawned == true)
                    {
                        Debug.Log("true Warpppppppppppppppppppppppppp");
                        SceneManager.LoadScene(NameScenes); 
                    }
                    else
                    {
                        Debug.Log("No Warpppppppppppppppppppppppppp");
                    }
                }
                /*if (player.GetComponentInChildren<PlayerStatus>().isOnGate)
                {
                    Debug.Log(true);
                    SceneManager.LoadScene(NameScenes);
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }*/
            }
            if(gameRound)
            {
                if (currentKeyItem == piller1)
                {
                    EmissionControl.instance.materialPillerType1.EnableKeyword ("_EMISSION");              
                }
                if (currentKeyItem == piller2)
                {
                    EmissionControl.instance.materialPillerType2.EnableKeyword ("_EMISSION");                
                }
                if (currentKeyItem == piller3)
                {
                    EmissionControl.instance.materialPillerType3.EnableKeyword ("_EMISSION");
                    gameWiner = true;
                    //isEnemyTime = false;
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
    public void RuleCheck()
    {
        //player.GetComponentInChildren<PlayerStatus>().curHealth <= 0
        //if (PlayerStatus.instance.curHealth > 0)
        if (player.GetComponentInChildren<PlayerStatus>().curHealth <= 0)
        {
            isEnemyTime = false;
            gameOver = true;
            state=SpawnState.FINISHED;
            timingWorld.instance.timerIsRunning = false;
            Debug.Log("GameOver 01");
        }
        if (EggStatus.Instance.currentHealth <= 0) //EggStatus.Instance.currentHealth > 0 (player.GetComponentInChildren<PlayerStatus>().curHealth > 0) timungWorld.instance.timeRemaining < 0
        {
            isEnemyTime = false;
            gameOver = true;
            state=SpawnState.FINISHED;
            timingWorld.instance.timerIsRunning = false;
            Debug.Log("gameOver 02");
        }
        if (timingWorld.instance.timeRemaining <= 0) //timingWorld.instance.timeRemaining < 0
        {
            isEnemyTime = false;
            gameOver = true;
            state=SpawnState.FINISHED;
            Debug.Log("gameOver 03");
        }
        /*else
        {
            gameOver = true;
            Debug.Log("gameOver");
            //StartCoroutine(endGame("Game Over!"));
        }*/
    }

    public IEnumerator StatusShow(string message)
    {
        GameplayUIManager.instance.gameStatus.text = message;
        GameplayUIManager.instance.gameStatus.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        GameplayUIManager.instance.gameStatus.gameObject.SetActive(false);
    }
    public void CheckWiner()
    {
        if(gameWiner)
        {
            Debug.Log("Win");
            StartCoroutine(StatusShow("You Won!!"));
            //interactingCanvasUi.gameObject.SetActive(true);
            EggStatus.Instance.BeginEgg(); //Use real is enermy die and win 
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
        enemyCurrent++;
    }
    IEnumerator SpawnWave (WaveEnemy _wave)
    {
        Debug.Log("Spawning Wave: "+ _wave.name);
        state = SpawnState.SPAWNING;

        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        Debug.Log("Spawning Enemy: "+ _enemy.name);


        Transform _sp = spawnPointsEnemy[Random.Range (0, spawnPointsEnemy.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
    /*IEnumerator endGame(string message)
    {
        //endGametxt.text = message;
        //endGametxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartGame");
    }*/
    private void EnemyV1()
    {
        //Wave
        if(state == SpawnState.FINISHED)
        {
            return;
        }
        if(state == SpawnState.WAITING)
        {
            //check enemy alive
            if(!EnemyIsAlive())
            {
               WeveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }    
    }
    private void EnemyV2()
    {
        if(isEnemyTime)
        {
            if(isEnemyWave == false)
            {
                StartCoroutine(EnemyUpSpawnX2());
            }

            thisGameRoud();

            /*else
            {

            }*/
        }        
    }
    private void thisGameRoud()
    {
        if(currentKeyItem >= keyItemCount)
        {
            Debug.Log("Win V2");
            StartCoroutine(StatusShow("Winer!!"));
            isEnemyTime = false;
            EggStatus.Instance.BeginEgg();
        }        
    }
    IEnumerator EnemyUpSpawnX2()
    {
        if (currentSpawnTimeEnemy > powerUpSpawnTime)
        {
            currentSpawnTimeEnemy = 0;
            if (enemyCurrent < maxEnemy)
            {

                int randomNumber = UnityEngine.Random.Range(0, enemySpawnPointX2.Length);//0 
                GameObject spawnLocation = enemySpawnPointX2[randomNumber];
                int randomEnemy = UnityEngine.Random.Range(0, 3);
                //newPowerUp = Instantiate(enemyUp) as GameObject;
                if (randomEnemy==0)
                {
                    newPowerUp = Instantiate(enemyType1) as GameObject;
                }
                if (randomEnemy == 1)
                {
                    newPowerUp = Instantiate(enemyType2) as GameObject;
                }
                if (randomEnemy == 2)
                {
                    newPowerUp = Instantiate(enemyType3) as GameObject;
                }
                newPowerUp.transform.position = spawnLocation.transform.position;
            }
        }
        yield return null;
        StartCoroutine(EnemyUpSpawnX2());
    }
}
