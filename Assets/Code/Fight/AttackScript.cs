using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static FighterStats;

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


    private GameObject GameControllerObj;

    private void Start()
    {
        magicScale = GameObject.Find("PlayerMagicFill").GetComponent<RectTransform>().localScale;
        _animator = owner.GetComponent<Animator>();
        GameControllerObj = GameObject.Find("GameControllerObject");
    }




    public void Attack(GameObject victim, string spellType)
    {
        attackerStats = owner.GetComponent<FighterStats>();
        targetStats = victim.GetComponent<FighterStats>();

        ElementalStatus newStatus = ElementalStatus.none;

        string reaction = "";
     

        if (attackerStats.magic >= magicCost)
        {
            float multiplier = Random.Range(minMultiplier, maxMultiplier);
          
            if (magicCost > 0)
            {
                attackerStats.UpdateMagicFill(magicCost);
            }

            //check what damage is being done
            if (spellType == "elementOne") //rn elementone is fire
            {
                if (targetStats.status == ElementalStatus.water)
                {
                    multiplier += maxMultiplier / 2;
                    newStatus = ElementalStatus.none;
                }
                else if (targetStats.status == ElementalStatus.earth)
                {
                    reaction = "defenseboost";
                    attackerStats.defense += maxMultiplier / 2;
                    newStatus = ElementalStatus.none;
                }
                else
                {
                    newStatus = ElementalStatus.fire;
                }
            }

            if (spellType == "elementTwo") 
            {
                if (targetStats.status == ElementalStatus.fire)
                {
                    multiplier += maxMultiplier / 2;
                    newStatus = ElementalStatus.none;
                }
                else if (targetStats.status == ElementalStatus.earth)
                {
                    reaction = "defenseboost";
                    attackerStats.defense += maxMultiplier / 2;
                    newStatus = ElementalStatus.none;
                }
                else
                {
                    newStatus = ElementalStatus.water;
                }
            }


            if (spellType == "elementThree")
            {
                if (targetStats.status == ElementalStatus.fire || targetStats.status == ElementalStatus.water) 
                {
                    reaction = "defenseboost";
                    attackerStats.defense += maxMultiplier / 2;
                    newStatus = ElementalStatus.none;
                }
                else
                {
                    newStatus = ElementalStatus.earth;
                }
            }

            damage = multiplier * attackerStats.baseAtk;

            if (magicAttack)
            {
                //damage = multiplier * attackerStats.magicRange;
                attackerStats.magic -= magicCost;
            }

            damage = Mathf.RoundToInt(damage * (100 / (100 + targetStats.defense)));


            StartCoroutine(AttackCoroutine(victim, reaction));

            targetStats.status = newStatus;
        }
    
    }


    //Used to execute attack sequence after animation has played
    IEnumerator AttackCoroutine(GameObject victim, string reaction)
    {
        //play attack animation
        float animationDuration = PlayAttackAnimation(reaction);
        yield return new WaitForSeconds(animationDuration);

        //show damage number text
        PlayDamageTextAnimation(damage, victim);

        //recieve damage
        targetStats.ReceiveDamage(damage, victim);

        //change elemental status
        targetStats.SetElementalStatusIcon(targetStats.status);
    }

    //Plays correct attack animation and returns duration for yield time in coroutine
    private float PlayAttackAnimation(string reaction)
    {
        float animationDuration = 0f;
        string currentAnimation;

        if (owner.CompareTag("Player"))
        {
            if (targetStats.status == ElementalStatus.none)
            {
                _animator.SetTrigger("Standard Attack");
                currentAnimation = "StandardAttack";
                

            }
            else if (targetStats.status == ElementalStatus.earth || reaction == "defenseboost")
            {
                _animator.SetTrigger("Defense Boost");
                currentAnimation = "DefenseBoost";
            }
            else
            {
                _animator.SetTrigger("Reaction Attack");
                currentAnimation = "ReactionAttack";
            }

        }
        else
        {
            _animator.SetTrigger("Attack");
            currentAnimation = "Attack";
        }

        RuntimeAnimatorController ac = _animator.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == currentAnimation)
            {
                animationDuration = ac.animationClips[i].length;
            }
        }

        return animationDuration;
    }


    //creates instance of the damage text, destroyed upon end of animation
    public void PlayDamageTextAnimation(float damage, GameObject victim)
    {
        //play damage text animation
        //create a damage text instance
        GameObject damageTextPrefab = GameControllerObj.GetComponent<GameController>().damageTextPrefab;
        GameObject DamageTextInstance = Instantiate(damageTextPrefab, victim.transform.position, victim.transform.rotation);
        //set instance with damage numbers
        DamageTextInstance.transform.GetChild(0).GetComponent<TextMeshPro>().SetText(damage.ToString());
    }

    

    
}
