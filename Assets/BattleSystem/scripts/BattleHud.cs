using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{

	public Text nameText;
	public Text levelText;
	public Slider hpSlider;
	public Button attackOne;
	public Button attackTwo;

	//assigns the different hud displays
	public void SetHub(Talimental talimental)
    {
		nameText.text = talimental.name;
		levelText.text = "lvl " + talimental.level;
		hpSlider.maxValue = talimental.health;
		hpSlider.value = talimental.health;
		attackOne.GetComponentInChildren<Text>().text = talimental.moves[0];
		attackTwo.GetComponentInChildren<Text>().text = talimental.moves[1];
	}

	public void SetHP(int hp)
    {
		hpSlider.value = hp;
    }
}
