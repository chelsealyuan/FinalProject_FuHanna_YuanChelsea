using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Transactions;
using UnityEngine.SocialPlatforms;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    private List<FighterStats> fighterStats;

    [SerializeField]
    private GameObject battleMenu;

    [SerializeField]
    private GameObject fightOverPanel;

    [SerializeField]
    private TMP_Text fightOverPanelText;

    public TMP_Text turnText;
    void Start()
    {
        //figures out who goes first depending on what the rate of speed each opponent has
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
        fightOverPanelText.gameObject.SetActive(false);


        NextTurn();
        
    }

    public void setTurnText(string currentUnit)
    {
        turnText.text = currentUnit + "'s turn!";
    }

    public void NextTurn()
    {   

        FighterStats currentFighterStats = fighterStats[0];
        fighterStats.Remove(currentFighterStats);

        //GameObject currentDead = GameObject.FindGameObjectWithTag("Dead");
        //checkObjDead(currentDead.name);

        if (!currentFighterStats.GetDead()) //if fighter is not dead
        {
            GameObject currentUnit = currentFighterStats.gameObject;
            /*
            Debug.Log("current unit is ");
            Debug.Log(currentUnit.tag);*/

            setTurnText(currentUnit.tag);

            currentFighterStats.CalculateNextTurn(currentFighterStats.nextActTurn); //recalc whose turn it is
            fighterStats.Add(currentFighterStats);
            fighterStats.Sort(); //get the next attacker by atk speed
            
            if (currentUnit.tag == "Player") //originally was currentUnit.tag == "Player"
            {
                this.battleMenu.SetActive(true);
            }
            else
            {
                //string attackType = Random.Range(0, 2) == 1 > "elementOne" : "elementTwo"; //deciding on the enemy's atk

                this.battleMenu.SetActive(false);
                string attackType;
                if (Random.Range(0,2) == 1) {
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
            //NextTurn();
        }
    }

    public void EndFight(string loser)
    {
        Debug.Log(loser);
        fightOverPanel.SetActive(true);
        fightOverPanelText.gameObject.SetActive(true);
        turnText.gameObject.SetActive(false);

        if (loser == "Enemy")
        {
            fightOverPanelText.text = "You Win";
        }
        else if (loser == "Player")
        {
            fightOverPanelText.text = " You Lose! Git Gud!";

        }

    }
    

}
