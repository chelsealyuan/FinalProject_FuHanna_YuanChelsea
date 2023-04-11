using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public static PopupController instance;

    public GameObject finalMenu;
    public GameObject paymentMenu;
    public GameObject paymentRejection;

    public int paymentAmount;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        PlayerController.instance.isPaused = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        finalMenu.SetActive(false);
        paymentMenu.SetActive(false);
        paymentRejection.SetActive(false);

        Time.timeScale = 1;
        if (PlayerController.instance != null)
        {
            PlayerController.instance.isPaused = false;
        }
    }

    public void ShowFinal()
    {
        Show();
        finalMenu.SetActive(true);
    }

    public void ShowPayment()
    {
        Show();
        paymentMenu.SetActive(true);
    }

    public void ShowPaymentRejection()
    {
        Show();
        paymentMenu.SetActive(false);
        paymentRejection.SetActive(true);
    }

    public void SelectPay()
    {
        if(GlobalVariables.money < paymentAmount)
        {
            ShowPaymentRejection();
        }
        else
        {
            Debug.Log("pay");
            //GlobalVariables.money -= paymentAmount;
            //remove the obstacle
        }
    }

    public void RestartSelect()
    {
        Hide();
        ExplorationController.instance.RestartGame();
    }

    public void FinalCloseSelect()
    {
        Hide();
        ExplorationController.instance.SetFinalReward();
    }
}
