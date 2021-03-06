using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //[SerializeField] GameObject introMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionMenu;
    [SerializeField] Button ModeOneButton;
    public string nextScenes;
    [SerializeField] Button HomeButton;
    [SerializeField] Button tutorialButton;  
    //[SerializeField] Button settingButton;        

    void Awake()
    {
        instance = this;


        ModeOneButton.onClick.AddListener(PlayButton);
        HomeButton.onClick.AddListener(BlackHomeButton);
        tutorialButton.onClick.AddListener(TutorialButton);
        /*if (Input.anyKeyDown)
        {
            mainMenu.SetActive(true);
            introMenu.SetActive(false);
            optionMenu.SetActive(false);
        }*/

    }

    void Update()
    {
        //mainMenu.SetActive(true);
        //optionMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }


    void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    void TutorialButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    /*void LoobButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }*/
    void BlackHomeButton()
    {
        SceneManager.LoadScene(nextScenes);
    }
    public void Quit()
    {
        Application.Quit();
    }
    
}
