using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeButton : MonoBehaviour
{
    [SerializeField]
    private bool element;
    private GameObject player;

    void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallback(temp));
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void AttachCallback(string btn)
    {
        if(btn.CompareTo("ElementalOneBtn") == 0)
        {
            player.GetComponent<FighterAction>().SelectAttack("elementOne");
        }
        else if(btn.CompareTo("ElementalTwoBtn") == 0)
        {
            player.GetComponent<FighterAction>().SelectAttack("elementTwo");
        }
        else
        {
            player.GetComponent<FighterAction>().SelectAttack("escape");

        }
    }
}
