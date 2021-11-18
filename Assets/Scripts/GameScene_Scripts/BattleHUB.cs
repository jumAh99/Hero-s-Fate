using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleHUB : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI lvlText;
    public Slider hpSlider;
    public Slider apSlider;
    public GameObject panel;

    public void setHUB(Unit unit)
    {
        nameText.text = unit.unitName;
        lvlText.text = "lvl." + unit.unitLevel;

        //set the max HP value for the unit 
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;

        //set the max AP value for the unit
        apSlider.maxValue = unit.maxAp;
        apSlider.value = unit.currentAp;

        //set the right sprite for the unit
        gameObject.GetComponent<Image>().sprite = unit.hpBarSprite;
    }

    //function to set the hp of the player at game start
    public void setHP(int hp)
    {
        hpSlider.value = hp;
    }

    //function to set the action points of the player at game start
    public void setAP(int ap)
    {
        apSlider.value = ap;
    }
}
