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
   // [SerializeField] GameObject player;
    [SerializeField] Button ModeOneButton;
    [SerializeField] Button ModeTwoButton;
    [SerializeField] Button tutorialButton;  
    [SerializeField] Button settingButton;        

    void Awake()
    {
        instance = this;


        ModeOneButton.onClick.AddListener(PlayButton);
        ModeTwoButton.onClick.AddListener(CrazyButton);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void CrazyButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
