using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ObjectSelectionUI : MonoBehaviour {

    public Button Tab1Btn, Tab2Btn, Tab3Btn, Tab4Btn;
    public GameObject[] Buttons,CurrentTabsBtns;
    public GameObject ParentTab,ParentObject;
    ClickAndDrag GridM;
	void Start () {
        GridM = GameObject.Find("EditManager").GetComponent<ClickAndDrag>();
        Buttons = new GameObject[4];
        Buttons[0] = Tab1Btn.gameObject;
        Buttons[1] = Tab2Btn.gameObject;
        Buttons[2] = Tab3Btn.gameObject;
        Buttons[3] = Tab4Btn.gameObject;

        CurrentTabsBtns = new GameObject[Tab1Btn.transform.childCount];
        for(int i=0;i< Tab1Btn.transform.childCount;i++)
        {
            CurrentTabsBtns[i] = Tab1Btn.transform.GetChild(i).gameObject;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        UIActive();       	
	}



    void UIActive()
    {
        if (GridM.GridView == true && GridM.TerrainView == false && GridM.DMode == false)
        {
            ParentTab.SetActive(true);
            ParentObject.SetActive(true);

            if(CurrentTabsBtns[0].activeSelf==false)
            {
                for (int i = 0; i < CurrentTabsBtns.Length; i++)
                {
                    CurrentTabsBtns[i].SetActive(true);
                }
            }
            CurrentTabsBtnPressed();


        }
        else if (((GridM.GridView == true && (GridM.TerrainView == true || GridM.DMode == true)) || GridM.GridView == false) && ParentTab.activeSelf == true)
        {
            ParentTab.SetActive(false);
            ParentObject.SetActive(false);
            DeselectButton();
            CurrentTabsBtns = new GameObject[Tab1Btn.transform.childCount];
            for (int i = 0; i < Tab1Btn.transform.childCount; i++)
            {
                CurrentTabsBtns[i] = Tab1Btn.transform.GetChild(i).gameObject;
            }


        }
    }

    private int CurrentButton;
    void CurrentTabsBtnPressed()
    {
        for (CurrentButton = 0; CurrentButton < CurrentTabsBtns.Length; CurrentButton++)
        {
            CurrentTabsBtns[CurrentButton].GetComponent<Button>().onClick.AddListener(TabBtnPressed);
            
        }
        if(Input.GetKeyDown(KeyCode.Delete) && buttonSelected==true)
        {
            DeselectButton();
        }
    }


    bool buttonSelected;
    void TabBtnPressed()
    {
        for(int i=0;i< CurrentTabsBtns.Length;i++)
        {
            CurrentTabsBtns[i].GetComponent<Image>().color = Color.white;
            if (CurrentTabsBtns[i].name == EventSystem.current.currentSelectedGameObject.name)
            {
                buttonSelected = true;
                CurrentTabsBtns[i].GetComponent<Image>().color = Color.green;
                GridM.setCurrentSelectedItem(i);
            }
        }
        
    }

    public bool IsButtonSelected()
    {
        return buttonSelected;
    }


    void DeselectButton()
    {
      
        buttonSelected = false;
        for (int i = 0; i < CurrentTabsBtns.Length; i++)
        {
            CurrentTabsBtns[i].GetComponent<Image>().color = Color.white;
           
        }
    }





   public  void Tab1Click()
    {
        ChangeUIOrder(0);
    }
   public  void Tab2Click()
    {
        ChangeUIOrder(1);
    }
   public  void Tab3Click()
    {
        ChangeUIOrder(2);
    }
    public void Tab4Click()
    {
        ChangeUIOrder(3);
    }

    void ChangeUIOrder(int order)
    {
      if(Buttons[order].gameObject != ParentTab.transform.GetChild((ParentTab.transform.childCount)-1).gameObject)
        {
            for (int i = 0; i < CurrentTabsBtns.Length; i++)
            {
                CurrentTabsBtns[i].GetComponent<Image>().color = Color.white;
                CurrentTabsBtns[i].SetActive(false);
            }
            buttonSelected = false;
            ParentTab.transform.DetachChildren();
            for (int i = (Buttons.Length-1); i>=0; i--)
            {
                if(i!=order)
                {
                    Buttons[i].gameObject.transform.SetParent(ParentTab.transform);
                }
            }
            Buttons[order].gameObject.transform.parent = ParentTab.transform;
            CurrentTabsBtns = new GameObject[Buttons[order].gameObject.transform.childCount];
            for (int i = 0; i < Buttons[order].gameObject.transform.childCount; i++)
                {
                    CurrentTabsBtns[i] = Buttons[order].gameObject.transform.GetChild(i).gameObject;
                }
            
            

        }
    }
}
