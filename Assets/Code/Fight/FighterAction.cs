using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FighterAction : MonoBehaviour
{
    private GameObject enemy;
    private GameObject player;

    [SerializeField]
    private GameObject elementOnePrefab;

    [SerializeField]
    private GameObject elementTwoPrefab;

    [SerializeField]
    private GameObject elementThreePrefab;

    private GameObject currentAttack;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void SelectAttack(string btn)
    {
        GameObject victim = player;

        if (CompareTag("Player")) {
            victim = enemy;
        }

        Debug.Log(victim.tag);

        if (btn.CompareTo("elementOne") == 0)
        {
            elementOnePrefab.GetComponent<AttackScript>().Attack(victim, "elementOne");
        }

        else if (btn.CompareTo("elementTwo") == 0)
        {
            elementTwoPrefab.GetComponent<AttackScript>().Attack(victim, "elementTwo");
        }

        else if (btn.CompareTo("elementThree") == 0)
        { 
            elementThreePrefab.GetComponent<AttackScript>().Attack(victim, "elementThree");
        }

        else /* (btn.CompareTo("escape") == 0)*/
        {
            SceneManager.LoadScene("ExplorationScene");
        }
    }

}
