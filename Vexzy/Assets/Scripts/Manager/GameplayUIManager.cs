using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameplayUIManager : MonoBehaviour
{
    public static GameplayUIManager instance;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOver;
    [SerializeField] Button resumeButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button restartButton;

    //[SerializeField] GameObject medkitItem;
    [SerializeField] GameObject keyItem;
    //[SerializeField] GameObject checkpointItem;
    //[SerializeField] Text medkitItemText;
    [SerializeField] Text keyItemText;
    //[SerializeField] Text checkpointItemText;
    [SerializeField] 
    HealthBar hpBar;
    //[SerializeField] Image hpBar;
    [SerializeField] 
    Image staminaBar;
    [SerializeField] 
    private Text valueText;
    private float currentHP;
    private float maxHP;
    private float currentStamina;
    private float maxStamina;

	public Slider slider;
    public Gradient gradient;
    public Image fill;

    [SerializeField] 
    public TextMeshProUGUI gameStatus;

    void Awake()
    {
        instance = this;
        

        resumeButton.onClick.AddListener(Resume);
        quitButton.onClick.AddListener(Quit);
        restartButton.onClick.AddListener(Restart);
    }

    // Update is called once per frame
    void Update()
    {
        ItemCountCheck();
        StatusBarUpdate();
        PauseCheck();
        GameOver();
        timingWorld.instance.IsTime();

        hpBar.SetHealth(currentHP);

        string[] tmp = valueText.text.Split('/');
        valueText.text = currentHP + " / " + maxHP;
    }

    public void SetMaxHealthPlayer(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealthPlayer(float health)
    {
        slider.value = health;
        
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
	
    void StatusBarUpdate()
    {
        currentHP = PlayerStatus.instance.curHealth;
        currentStamina = PlayerStatus.instance.stamina;
        maxHP = PlayerStatus.instance.maxHealth;
        maxStamina = PlayerStatus.instance.maxStamina;

        hpBar.SetMaxHealth(maxHP);
        //hpBar.fillAmount = currentHP / maxHP;
        staminaBar.fillAmount = currentStamina / maxStamina;
    }
    
    void ItemCountCheck()
    {
        // if (PlayerStatus.instance.keyItem < 1)
        //     keyItem.SetActive(false);
        // else
        // {
        keyItem.SetActive(true);
        keyItemText.text = $"{GameManager.instance.currentKeyItem}/{GameManager.instance.keyItemCount}";
        // }

        /*if (PlayerStatus.instance.medKit < 1)
            medkitItem.SetActive(false);
        else
        {
            medkitItem.SetActive(true);
            medkitItemText.text = $"{PlayerStatus.instance.medKit}";
        }*/

        /*if (!PlayerStatus.instance.checkPointItem && !PlayerStatus.instance.isCheckpoint)
            checkpointItem.SetActive(false);
        else
        {
            checkpointItem.SetActive(true);
            checkpointItemText.text = $"Deploy";
        }*/

        /*if (PlayerStatus.instance.isCheckpoint)
        {
            checkpointItem.SetActive(true);
            checkpointItemText.text = $"Checkpoint";
        }*/
    }

    void PauseCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void GameOver()
    {
        if (GameManager.instance.GameOver)
        {
            //Time.timeScale = 0;
            gameOver.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        // Application.Quit();
        SceneManager.LoadScene(0);
    }
}
