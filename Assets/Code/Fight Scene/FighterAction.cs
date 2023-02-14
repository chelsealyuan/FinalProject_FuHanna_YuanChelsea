using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAction : MonoBehaviour
{
    private GameObject enemy;
    private GameObject player;

    [SerializeField]
    private GameObject elementOnePrefab;

    [SerializeField]
    private GameObject elementTwoPrefab;

    private GameObject currentAttack;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void SelectAttack(string btn)
    {
        GameObject victim = player;

        if (tag == "Player"){
            victim = enemy;
        }

        if (btn.CompareTo("elementOne") == 0)
        {
            Debug.Log("Element 1 Button Pressed");
            elementOnePrefab.GetComponent<AttackScript>().Attack(victim);
        }
        else if (btn.CompareTo("elementTwo") == 0)
        {
            Debug.Log("Element 2 Button Pressed");
            elementTwoPrefab.GetComponent<AttackScript>().Attack(victim);
        }
        else
        {
            Debug.Log("Run");
        }
    }

}
