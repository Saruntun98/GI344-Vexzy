using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject introMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject player;
    [SerializeField] Button startButton;
    [SerializeField] Button tutorialButton;  
    [SerializeField] Button settingButton;        

    void Awake()
    {
        instance = this;


        startButton.onClick.AddListener(PlayButton);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            mainMenu.SetActive(true);
            introMenu.SetActive(false);
        }
    }


    void PlayButton()
    {
        //if (!string.IsNullOrEmpty(playerSelectName))
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // GameManager.instance.playerName = playerSelectName;
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
