using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class AttackScript : MonoBehaviour
{
    public GameObject owner;

    [SerializeField]
    private bool magicAttack;

    [SerializeField]
    private float magicCost;

    [SerializeField]
    private float minMultiplier;

    [SerializeField]
    private float maxMultiplier;

    private Animator _animator;


    private FighterStats attackerStats;
    private FighterStats targetStats;
    private float damage = 0.0f;
    private Vector2 magicScale;




    private void Start()
    {
        magicScale = GameObject.Find("PlayerMagicFill").GetComponent<RectTransform>().localScale;
        _animator = owner.GetComponent<Animator>();
    }


    public void Attack(GameObject victim, string spellType)
    {
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();

        if (owner.CompareTag("Player"))
        {
            Debug.Log(targetStats.status.ToString());
            if (targetStats.status.ToString() != "none")
            {
                _animator.SetTrigger("Reaction Attack");
            }
            else
            {
                _animator.SetTrigger("Normal Attack");
            }

        }
     

        if (attackerStats.magic >= magicCost)
        {
            float multiplier = Random.Range(minMultiplier, maxMultiplier);
          
            if (magicCost > 0)
            {
                attackerStats.updateMagicFill(magicCost);
            }

            //check what damage is being done
            if (spellType == "elementOne") //rn elementone is fire
            {
                if (targetStats.status == FighterStats.elementalStatus.water)
                {
                    Debug.Log("vaporize");
                    multiplier += maxMultiplier / 2;
                    targetStats.status = FighterStats.elementalStatus.none;
                }
                else if (targetStats.status == FighterStats.elementalStatus.earth)
                {
                   
                    Debug.Log("crystallize");
                    
                    attackerStats.defense += 30;
                    targetStats.status = FighterStats.elementalStatus.none;
                }
                else
                {
                    targetStats.status = FighterStats.elementalStatus.fire;
                }
            }

            if (spellType == "elementTwo") 
            {
                if (targetStats.status == FighterStats.elementalStatus.fire)
                {
                    multiplier += maxMultiplier / 2;
                    targetStats.status = FighterStats.elementalStatus.none;
                }
                else if (targetStats.status == FighterStats.elementalStatus.earth)
                {
                    Debug.Log("crystallize");
                    attackerStats.defense += 30;
                    targetStats.status = FighterStats.elementalStatus.none;
                }
                else
                {
                    targetStats.status = FighterStats.elementalStatus.water;
                }
            }


            if (spellType == "elementThree")
            {
                if (targetStats.status == FighterStats.elementalStatus.fire || targetStats.status == FighterStats.elementalStatus.water) 
                {
                    attackerStats.defense += 30;
                    targetStats.status = FighterStats.elementalStatus.none;
                }
                else
                {
                    targetStats.status = FighterStats.elementalStatus.earth;
                }
            }

            damage = multiplier * attackerStats.baseAtk;

            if (magicAttack)
            {
                //damage = multiplier * attackerStats.magicRange;
                attackerStats.magic = attackerStats.magic - magicCost;
            }

            //float defenseMultiplier = Random.Range(minDefenseMultiplier, maxDefenseMultiplier);

            //A diminishing returns defense calculation
            //Debug.Log("The " + victim + " was attacked with spell type " + spellType + " with " + targetStats.status + " element applied");
            damage = Mathf.RoundToInt(damage * (100 / (100 + targetStats.defense)));

            //leave out animation for now
            //owner.GetComponent<Animator>().Play(animationName)

          
            targetStats.ReceiveDamage(damage, victim);
            //attackerStats.updateMagicFill(magicCost);

            targetStats.SetElementalStatusIcon(targetStats.status);

        }
    
    }

}
