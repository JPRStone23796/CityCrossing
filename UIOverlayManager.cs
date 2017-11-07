using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIOverlayManager : MonoBehaviour {

    public Text Money, MoneyProducing,IncomeTimer,DayCycle,DayCount;
    GameManager GameManager;
	void Start () {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        Money.text = "Money: " + GameManager.CurrentPlaythroughStats.CheckCurrentMoney().ToString();
        MoneyProducing.text = "Producing: " + GameManager.CurrentPlaythroughStats.CheckMoneyProducing().ToString();


        float minutes = Mathf.FloorToInt(GameManager.CurrentPlaythroughStats.TimeRemainingTillIncome() / 60f);
        float seconds = Mathf.FloorToInt(GameManager.CurrentPlaythroughStats.TimeRemainingTillIncome()- minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        IncomeTimer.text = niceTime;

        minutes = Mathf.FloorToInt(GameManager.CurrentPlaythroughStats.CurrentTime() / 60f);
        seconds = Mathf.FloorToInt(GameManager.CurrentPlaythroughStats.CurrentTime() - minutes * 60);
        niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);
        DayCycle.text = niceTime;

        DayCount.text = GameManager.CurrentPlaythroughStats.CurrentDay().ToString();
    }
}
