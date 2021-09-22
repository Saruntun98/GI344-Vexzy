using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    public static GameplayUIManager instance;

    //[SerializeField] GameObject pauseMenu;
    //[SerializeField] Button resumeButton;
    //[SerializeField] Button quitButton;

    //[SerializeField] GameObject medkitItem;
    //[SerializeField] GameObject keyItem;
    //[SerializeField] GameObject checkpointItem;
    //[SerializeField] Text medkitItemText;
    //[SerializeField] Text keyItemText;
    //[SerializeField] Text checkpointItemText;
    [SerializeField] HealthBar hpBar;
    //[SerializeField] Image hpBar;
    [SerializeField] Image staminaBar;

    private float currentHP;
    private float maxHP;
    private float currentStamina;
    private float maxStamina;

    void Awake()
    {
        instance = this;
        

        //resumeButton.onClick.AddListener(Resume);
        //quitButton.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        //ItemCountCheck();
        StatusBarUpdate();
        hpBar.SetHealth(currentHP);
        //PauseCheck();
    }

    void StatusBarUpdate()
    {
        currentHP = PlayerStatus._instance.curHealth;
        currentStamina = PlayerStatus._instance.stamina;
        maxHP = PlayerStatus._instance.maxHealth;
        maxStamina = PlayerStatus._instance.maxStamina;

        hpBar.SetMaxHealth(maxHP);
        //hpBar.fillAmount = currentHP / maxHP;
        staminaBar.fillAmount = currentStamina / maxStamina;
    }

    /*void PauseCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }/*
    
    /*void ItemCountCheck()
    {
        // if (PlayerStatus.instance.keyItem < 1)
        //     keyItem.SetActive(false);
        // else
        // {
        keyItem.SetActive(true);
        keyItemText.text = $"{GameManager.instance.currentKeyItem}/{GameManager.instance.keyItemCount}";
        // }

        if (PlayerStatus.instance.medKit < 1)
            medkitItem.SetActive(false);
        else
        {
            medkitItem.SetActive(true);
            medkitItemText.text = $"{PlayerStatus.instance.medKit}";
        }

        if (!PlayerStatus.instance.checkPointItem && !PlayerStatus.instance.isCheckpoint)
            checkpointItem.SetActive(false);
        else
        {
            checkpointItem.SetActive(true);
            checkpointItemText.text = $"Deploy";
        }

        if (PlayerStatus.instance.isCheckpoint)
        {
            checkpointItem.SetActive(true);
            checkpointItemText.text = $"Checkpoint";
        }
    }*/

    /*void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Quit()
    {
        // Application.Quit();
        SceneManager.LoadScene(0);
    }*/
}
