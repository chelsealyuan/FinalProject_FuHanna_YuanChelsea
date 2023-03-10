using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FighterStats : MonoBehaviour, IComparable
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject healthFill;

    [SerializeField]
    private GameObject magicFill;


    [Header("Stats")]
    public float health;
    public float magic;
    public float baseAtk;
    public float magicRange;
    public float defense;
    public float speed;

    private float startHealth;
    private float startMagic;

    [HideInInspector]
    public int nextActTurn;

    private bool dead = false;

    // Resize health and magic bar
    private Transform healthTransform;
    private Transform magicTransform;

    private Vector2 healthScale;
    private Vector2 magicScale;

    private float xNewHealthScale;
    private float xNewMagicScale;

    public elementalStatus status;

    //status/elemental effects
    public enum elementalStatus
    {
        none, 
        fire, 
        water,
        earth
    }

    void Awake()
    {
        healthTransform = healthFill.GetComponent<RectTransform>();
        healthScale = healthFill.transform.localScale;

        magicTransform = magicFill.GetComponent<RectTransform>();
        magicScale = magicFill.transform.localScale;

        startHealth = health;
        startMagic = magic;

        status = elementalStatus.none;
    }


    public void ReceiveDamage(float damage)
    {
        //Debug.Log("receive damage");
        health = health - damage;
        //Leave off the animation for now
        //animator.Play("Damage");
        //Debug.Log("recieving damage");

        //set damage text
        if(health <= 0)
        {
            dead = true;
            //gameObject.tag = "Dead";
            Destroy(healthFill);
            EndScreen(gameObject.tag);

            Destroy(gameObject);



        }
        else if (damage > 0)
        {
            xNewHealthScale = healthScale.x * (health / startHealth);
            healthFill.transform.localScale = new Vector2(xNewHealthScale, healthScale.y);
        }

        Invoke("ContinueGame", 1);
    }

    public void updateMagicFill(float cost)
    {
        if (cost < 1)
        {
            magic = magic - cost;
            xNewMagicScale = magicScale.x * (magic / startMagic);
            magicFill.transform.localScale = new Vector2(xNewMagicScale, magicScale.y);

        }
        
    }

    //checks if thing is dead
    public bool GetDead()
    {
        return dead;
    }

    void EndScreen(string loser)
    {
        //Debug.Log(loser);
        GameObject.Find("GameControllerObject").GetComponent<GameController>().EndFight(loser); //calls up the screen

    }

    void ContinueGame()
    {
        GameObject.Find("GameControllerObject").GetComponent<GameController>().NextTurn(); //go to next turn
    }

    public void CalculateNextTurn(int currentTurn)
    {
        nextActTurn = currentTurn + Mathf.CeilToInt(100f / speed);
    }


    public int CompareTo(object otherStats)
    {
        int next = nextActTurn.CompareTo(((FighterStats)otherStats).nextActTurn);
        return next;
    }
}
