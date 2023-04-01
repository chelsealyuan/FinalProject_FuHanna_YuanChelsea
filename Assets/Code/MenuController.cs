using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    public GameObject tutorialMenu;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Hide();
    }

    public void Show()
    {
        //ShowMainMenu();
        gameObject.SetActive(true);
        Time.timeScale = 0;
        //PlayerController.instance.isPaused = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        if (PlayerController.instance != null)
        {
            //PlayerController.instance.isPaused = false;
        }
    }
}
