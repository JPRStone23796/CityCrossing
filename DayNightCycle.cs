using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {


    GameManager GM;
    float currentSunCycle;

	// Use this for initialization
	void Start () {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
       
	}
	
	
	void Update () {
       if(GM.CurrentPlaythroughStats.CurrentTime()>=240 && GM.CurrentPlaythroughStats.CurrentTime()<1200)
        {
            if(GM.CurrentPlaythroughStats.CurrentTime()>=240 && GM.CurrentPlaythroughStats.CurrentTime()<=720)
            {
                currentSunCycle += (Time.deltaTime * 10);
                gameObject.GetComponent<Light>().intensity = (1.9f/480f) * currentSunCycle;
            }

            if (GM.CurrentPlaythroughStats.CurrentTime() > 720 && GM.CurrentPlaythroughStats.CurrentTime() < 1200)
            {
                currentSunCycle -= (Time.deltaTime * 10);
                gameObject.GetComponent<Light>().intensity = (1.9f / 480f) * currentSunCycle;
            }
        }
        else
        {
            gameObject.GetComponent<Light>().intensity = 0;
        }
	}
}
