using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{

	public Text nameText;
	public Text levelText;
	public Slider hpSlider;

	//assigns the different hud displays
	public void SetHud(Unit unit)
    {
		nameText.text = unit.unitName;
		levelText.text = "lvl " + unit.level;
		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;
    }

	public void SetHP(int hp)
    {
		hpSlider.value = hp;
    }
}
