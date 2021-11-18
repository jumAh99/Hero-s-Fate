using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Unit : MonoBehaviour
{
    //unit information 
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    public int maxAp;
    public int currentAp;
    public int regen;
    public Sprite hpBarSprite;

    public bool TakeDamage(int dmg)
    {

        currentHP -= dmg;

        //if the unit died
        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int getDamage()
    {
        return this.damage;
    }

    public void Heal(int amount)
    {
        //increase the current HP by the amount given 
        currentHP += amount;

        if (currentHP > maxHP)
        {
            //set the current HP = to the mac hp 
            currentHP = maxHP;
        }
    }

    public void APRegen(int amount)
    {
        //increase the current HP by the amount given 
        currentAp += amount;

        if (currentAp > maxAp)
        {
            //set the current HP = to the mac hp 
            currentAp = maxAp;
        }
    }

    public void APReduct(int amount)
    {
        //increase the current HP by the amount given 
        currentAp -= amount;

        if (currentAp < 0)
        {
            //set the current HP = to 0
            currentAp = 0;
        }
    }
}
