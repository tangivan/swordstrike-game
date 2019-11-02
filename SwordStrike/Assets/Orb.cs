using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orb : GenericEnemy
{

    private bool orbDead = false;



    public override void TakeDamage(float damageAmount, int wepElement)
    {
        switch (element)
        {
            case 1:
                health = health - elementalBonus(damageAmount, wepElement);
                break;
            case 2:
                health = health - elementalBonus(damageAmount, wepElement);
                break;
            case 3:
                health = health - elementalBonus(damageAmount, wepElement);
                break;
            default:
                health = health - elementalBonus(damageAmount, wepElement);
                break;
        }

        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
            healthBar.SetActive(false);
            orbDead = true;
        }

        healthBar.GetComponent<Slider>().value = health;
    }

    public override float elementalBonus(float damageAmount, float wepElement)
    {
        float bonus;
        switch (element)
        {

            case 1:
                if (wepElement == 2)
                {
                    bonus = damageAmount * 2;
                }
                else
                {
                    bonus = 0;
                }
                return bonus;
            case 2:
                if (wepElement == 3)
                {
                    bonus = damageAmount * 2;
                }
                else
                {
                    bonus = 0;
                }
                return bonus;

            case 3:
                if (wepElement == 4)
                {
                    bonus = damageAmount * 2;
                }
                else
                {
                    bonus = 0;
                }
                return bonus;
            default:
                if (wepElement == 1)
                {
                    bonus = damageAmount * 2;
                }
                else
                {
                    bonus = 0;
                }
                return bonus;


        }
    }

    public void disableRenders()
    {
        if(!orbDead)
        healthBar.SetActive(false);
    }

    public void enableRenders()
    {
        if(!orbDead)
        healthBar.SetActive(true);
    }


}
