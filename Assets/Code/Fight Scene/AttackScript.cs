using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class AttackScript : MonoBehaviour
{
    public GameObject owner;

    [SerializeField]
    private string animationName;

    [SerializeField]
    private bool magicAttack;

    [SerializeField]
    private float magicCost;

    [SerializeField]
    private float minMultiplier;

    [SerializeField]
    private float maxMultiplier;





    private FighterStats attackerStats;
    private FighterStats targetStats;
    private float damage = 0.0f;
    private float xMagicNewScale;
    private Vector2 magicScale;


    private void Start()
    {
        magicScale = GameObject.Find("PlayerMagicFill").GetComponent<RectTransform>().localScale;
    }


    public void Attack(GameObject victim, string spellType)
    {

        //Debug.Log("This turn's victim is " + victim.tag);
        //Debug.Log("This turn's caster is " + owner.tag); 
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();
        
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
                    multiplier *= 2;
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
                    Debug.Log("vaporize");
                    multiplier *= 2;
                    targetStats.status = FighterStats.elementalStatus.none;
                }
                else
                {
                    targetStats.status = FighterStats.elementalStatus.water;
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
            Debug.Log("The " + victim + " was attacked with spell type " + spellType + " with " + targetStats.status + " element applied");
            damage = damage * (100 / (100 + targetStats.defense));

            //leave out animation for now
            //owner.GetComponent<Animator>().Play(animationName)

            //Debug.Log(Mathf.RoundToInt(damage));
            targetStats.ReceiveDamage(Mathf.RoundToInt(damage));
            //attackerStats.updateMagicFill(magicCost);


            if (victim.CompareTag("Enemy"))
            {
                ApplyElement();
            }
        }
    
    }

    public void ApplyElement()
    {
        Debug.Log(this.gameObject.name);
    }
    

}
