using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    public GameObject tutorialMenu;
    public GameObject contextMenu;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Hide();
    }

    public void Show()
    {
        ShowContext();
        gameObject.SetActive(true);

        Time.timeScale = 0;
        PlayerController.instance.isPaused = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        Time.timeScale = 1;
        if (PlayerController.instance != null)
        {
            PlayerController.instance.isPaused = false;
        }
    }

    void SwitchMenu(GameObject menuChoice)
    {
        contextMenu.SetActive(false);
        tutorialMenu.SetActive(false);


        menuChoice.SetActive(true);
    }

    public void ShowContext()
    {
        SwitchMenu(contextMenu);
    }

    public void ShowTutorial()
    {
        SwitchMenu(tutorialMenu);
    }
}
