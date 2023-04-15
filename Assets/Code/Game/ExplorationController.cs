using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExplorationController : MonoBehaviour
{

    public static ExplorationController instance;

    public GameObject finalReward;
    public Sprite finalSprite;

    public int obstaclePayment;
    public int finalPayment;

    public GameObject player;

    public GameObject[] obstacles;

    private GameObject[] chests;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //find all the badguys first and assign them to a variable,
        //then use the variable to turn them on and off.
        chests = GameObject.FindGameObjectsWithTag("Chest");
    }

    public void SetFinalReward()
    {
        finalReward.GetComponent<SpriteRenderer>().sprite = finalSprite;
        finalReward.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void RestartGame()
    {

        player.transform.position = new Vector2(0, 0);

        GlobalVariables.money = 0;


        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(true);
        }

        foreach(GameObject chest in chests)
        {
            chest.SetActive(true);
        }
    }
}
