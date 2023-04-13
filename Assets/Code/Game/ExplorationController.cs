using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorationController : MonoBehaviour
{

    public static ExplorationController instance;

    public GameObject finalReward;
    public Sprite finalSprite;

    public int obstaclePayment;

    void Awake()
    {
        instance = this;
    }

    public void SetFinalReward()
    {
        finalReward.GetComponent<SpriteRenderer>().sprite = finalSprite;
        finalReward.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void RestartGame()
    {
        Debug.Log("restarting");
    }
}
