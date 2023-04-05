using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MakeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject player;

    [SerializeField]
    private GameObject battleMenu;

    public GameObject buttonText;


    void Start()
    {
        string temp = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallback(temp));
        player = GameObject.FindGameObjectWithTag("Player");

        buttonText.SetActive(false);
    }

    public void AttachCallback(string btn)
    {
        if(btn.CompareTo("ElementalOneBtn") == 0)
        {
            player.GetComponent<FighterAction>().SelectAttack("elementOne");
            battleMenu.SetActive(false);
        }
        else if(btn.CompareTo("ElementalTwoBtn") == 0)
        {
            player.GetComponent<FighterAction>().SelectAttack("elementTwo");
            battleMenu.SetActive(false);
        }
        else if (btn.CompareTo("ElementalThreeBtn") == 0)
        {
            player.GetComponent<FighterAction>().SelectAttack("elementThree");
            battleMenu.SetActive(false);
        }
        else
        {
            player.GetComponent<FighterAction>().SelectAttack("escape");

        }

       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.SetActive(false);
    }


}
