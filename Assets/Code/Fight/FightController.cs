using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Transactions;
using UnityEngine.SocialPlatforms;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FightController : MonoBehaviour
{
    // Start is called before the first frame update

    private List<FighterStats> fighterStats;

    [SerializeField]
    private GameObject battleMenu;

    [SerializeField]
    private GameObject fightOverPanel;

    [SerializeField]
    private TMP_Text fightOverPanelText;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private Button rematchButton;

    public TMP_Text turnText;

    public GameObject damageTextPrefab;

    public GameObject reactionTextPrefab;

    public GameObject[] enemyPrefabs;


    void Start()
    {
        //set who the enemy
        SetEnemy(GlobalVariables.currentEnemy);


        fighterStats = new List<FighterStats>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        FighterStats currentFighterStats = player.GetComponent<FighterStats>();
        currentFighterStats.CalculateNextTurn(0);
        fighterStats.Add(currentFighterStats);

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        FighterStats currentEnemyStats = enemy.GetComponent<FighterStats>();
        currentEnemyStats.CalculateNextTurn(0);
        fighterStats.Add(currentEnemyStats);

        fighterStats.Sort();
        this.battleMenu.SetActive(false);

        //sets the gameover screen
        fightOverPanel.SetActive(false);
        //fightOverPanelText.gameObject.SetActive(false);
        rematchButton.gameObject.SetActive(false);


        NextTurn();

    }

    public void SetTurnText(string currentUnit)
    {
        turnText.text = currentUnit + "'s turn!";
    }

    public void NextTurn()
    {

        FighterStats currentFighterStats = fighterStats[0];
        fighterStats.Remove(currentFighterStats);


        if (!currentFighterStats.GetDead()) //if fighter is not dead
        {
            GameObject currentUnit = currentFighterStats.gameObject;

            SetTurnText(currentUnit.tag);

            currentFighterStats.CalculateNextTurn(currentFighterStats.nextActTurn); //recalc whose turn it is
            fighterStats.Add(currentFighterStats);
            fighterStats.Sort(); //get the next attacker by atk speed

            if (currentUnit.CompareTag("Player"))
            {
                battleMenu.SetActive(true);
            }
            else
            {
                string attackType;

                if (Random.Range(0, 2) == 1)
                {
                    attackType = "elementOne";
                }
                else
                {
                    attackType = "elementTwo";
                }

                currentUnit.GetComponent<FighterAction>().SelectAttack(attackType);
            }

        }
        else
        {
            Debug.Log(currentFighterStats.gameObject.tag + " is dead.");
            NextTurn();
        }
    }

    public void EndFight(string loser)
    {
        //Debug.Log(loser);
        fightOverPanel.SetActive(true);
        //fightOverPanelText.gameObject.SetActive(true);
        turnText.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);


        if (loser == "Enemy")
        {
            GlobalVariables.money += 100;
            //Debug.Log(GlobalVariables.money);

            fightOverPanelText.text = "You Win! You have gained 100 breads. You now have " + GlobalVariables.money + " breads.";


        }
        else if (loser == "Player")
        {
            fightOverPanelText.text = "You Lose 50 breads! Git Gud! You now have " + GlobalVariables.money + " breads.";

            GlobalVariables.money -= 50;

            if (GlobalVariables.money <= 0)
            {
                GlobalVariables.money = 0;
            }

            rematchButton.gameObject.SetActive(true);

        }

    }

    public void ReloadFight()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //resets the scene
    }

    public void ExitFight()
    {
        SceneManager.LoadScene("ExplorationScene");
    }

    private void SetEnemy(string enemyName)
    {
        GameObject enemyPrefab = null;

        if (enemyName == "BoarFight")
        {
            enemyPrefab = enemyPrefabs[0];
        }

        if (enemyName == "SnakeFight")
        {
            enemyPrefab = enemyPrefabs[1];
        }

        if (enemyName == "GhostFight")
        {
            enemyPrefab = enemyPrefabs[2];
        }

        if (enemyName == "DinoFight")
        {
            enemyPrefab = enemyPrefabs[3];
        }

        if (enemyName == "MushroomFight")
        {
            enemyPrefab = enemyPrefabs[4];
        }

        if(enemyPrefab != null)
        {
            Instantiate(enemyPrefab, new Vector3(-4.5f, 1.8f, 0), Quaternion.identity);
        }
       
    }
}
