using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupController : MonoBehaviour
{
    public static PopupController instance;

    public GameObject finalMenu;
    public GameObject paymentMenu;
    public GameObject paymentRejection;

    private int paymentAmount;
    public TMP_Text paymentText;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Hide();
        paymentAmount = ExplorationController.instance.obstaclePayment;
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

        GlobalVariables.currentObstacle = null;
    }

    public void ShowFinal()
    {
        Show();
        finalMenu.SetActive(true);
    }

    public void ShowPayment()
    {
        paymentText.text = "Sacrifice " + paymentAmount + " breads to unlock?";
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
            int currentMoney = GlobalVariables.money - paymentAmount;
            if (currentMoney <= 0)
            {
                GlobalVariables.money = 0;
            }
            else
            {
                GlobalVariables.money -= paymentAmount;
            }

            //Destroy(GlobalVariables.currentObstacle);
            GlobalVariables.currentObstacle.SetActive(false);
            GlobalVariables.objectsDestroyed.Add(GlobalVariables.currentObstacle.name);

           

            Hide();
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
