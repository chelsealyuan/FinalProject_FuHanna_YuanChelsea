using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class FighterStats : MonoBehaviour, IComparable
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject healthFill;

    [SerializeField]
    private GameObject magicFill;

    [SerializeField]
    private GameObject elementPrefab; //holds the sprites of elementalStatus


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

    public Sprite[] spriteArray;


    private GameObject GameControllerObj;

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

        GameControllerObj = GameObject.Find("GameControllerObject");
    }


    public void SetElementalStatusIcon(elementalStatus status)
    {
        Debug.Log(status);

        Sprite elementSprite;

        switch (status)
        {
            case elementalStatus.none:
                elementSprite = spriteArray[0];
                elementPrefab.GetComponent<Image>().sprite = elementSprite;
                break;

            case elementalStatus.fire:
                elementSprite = spriteArray[1];
                elementPrefab.GetComponent<Image>().sprite = elementSprite;
                break;

            case elementalStatus.water:
                elementSprite = spriteArray[2];
                elementPrefab.GetComponent<Image>().sprite = elementSprite;
                break;

            case elementalStatus.earth:
                elementSprite = spriteArray[3];
                elementPrefab.GetComponent<Image>().sprite = elementSprite;
                break;

        }

        


    }


    public void ReceiveDamage(float damage, GameObject victim)
    {
        //play damage text animation
        //create a damage text instance
        GameObject damageTextPrefab = GameControllerObj.GetComponent<GameController>().damageTextPrefab;
        GameObject DamageTextInstance = Instantiate(damageTextPrefab, victim.transform.position + new Vector3(0.0f, 2.0f, 0.0f), victim.transform.rotation);
        //set instance with damage numbers
        DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(damage.ToString());



        //reduce health
        health -= damage;

        //Leave off the animation for now
        //animator.Play("Damage");


        //check if player or enemy is dead
        if (health <= 0)
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


        Invoke("ContinueGame", 2);
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
