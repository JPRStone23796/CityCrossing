using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStats
{
    float CurrentMoney,MoneyProducing, TimeLeftUntil, ConstantTimeLimit, DayCycle;
    [Range(0,100)]
    int TownHappiness,DayCount;
    public PlayerStats()
    {
        CurrentMoney = 0;
        MoneyProducing = 0;
        TownHappiness = 0;
        TimeLeftUntil = 0;
        ConstantTimeLimit = 0;
        DayCycle = 0;
        DayCount = 0;
    }

    public void SetValues(float Money, float producing,int Happiness,float TimeLimit,float CurrentTime)
    {
        CurrentMoney = Money;
        MoneyProducing = producing;
        TownHappiness = Happiness;
        ConstantTimeLimit = TimeLimit;
        TimeLeftUntil = ConstantTimeLimit;
        DayCycle = CurrentTime;
        DayCount = 0;
    }
    public void ChangeMoneyProducing(float value)
    {
        MoneyProducing += value;
    }
    
    
    public void Purchase(float cost)
    {
        CurrentMoney -= cost;
    }    


    public void MoneyGain()
    {
        CurrentMoney += MoneyProducing;
    }

    public void ChangeHappiness(int ValueChange)
    {
        TownHappiness += ValueChange;
    }

    public void CountDown()
    {
        TimeLeftUntil -= Time.deltaTime;
        if (TimeLeftUntil <= 0)
        {
            TimeLeftUntil = ConstantTimeLimit;
            MoneyGain();
        }
    }


    public void TimeCycle()
    {
        if (DayCycle < 1440) { DayCycle += Time.deltaTime *10; }
        if (DayCycle >= 1440) { DayCycle = 0;DayCount++; }
        
        }



    public float CheckCurrentMoney()
    {
        return CurrentMoney;
    }

    public float CheckMoneyProducing()
    {
        return MoneyProducing;
    }


    public int CheckTownHappiness()
    {
        return TownHappiness;
    }

    public float TimeRemainingTillIncome()
    {
        return TimeLeftUntil;
    }

    public float CurrentTime()
    {
        return DayCycle;
    }
    public int CurrentDay()
    {
        return DayCount;
    }
}




public class GameManager : MonoBehaviour {

   public PlayerStats CurrentPlaythroughStats;
	

	void Awake () {
        CurrentPlaythroughStats = new PlayerStats();
        CurrentPlaythroughStats.SetValues(1000, 10,100,30,180);

    }
	
	// Update is called once per frame
	void Update () {
        CurrentPlaythroughStats.CountDown();
        CurrentPlaythroughStats.TimeCycle();
    }
}
