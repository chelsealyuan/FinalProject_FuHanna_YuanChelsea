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
        showContext();
        gameObject.SetActive(true);
       
        //PlayerController.instance.isPaused = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
       
    }

    void SwitchMenu(GameObject menuChoice)
    {
        contextMenu.SetActive(false);
        tutorialMenu.SetActive(false);


        menuChoice.SetActive(true);
    }

    public void showContext()
    {
        SwitchMenu(contextMenu);
    }

    public void showTutorial()
    {
        SwitchMenu(tutorialMenu);
    }
}
