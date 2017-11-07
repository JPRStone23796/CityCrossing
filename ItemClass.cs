using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClass : MonoBehaviour
{

    public int ItemListSize;
    public GameObject Bridge, Lamp,House1,Tree_01,Tree_02,Rock_01,Rock_02;
    ClickAndDrag current;
    void Awake()
    {
        current = GetComponent<ClickAndDrag>();
        current.Items = new ItemBuilder[ItemListSize];
        for (int i = 0; i < ItemListSize; i++)
        {
            switch(i)
            {
                case 0:
                    current.Items[i] = new ItemBuilder();
                    current.Items[i].SetName(Bridge);
                    current.Items[i].SetScale(6,2);
                    current.Items[i].SetMoneyValues(200,10);
                  
                    break;

                case 1:
                    current.Items[i] = new ItemBuilder();
                    current.Items[i].SetName(Lamp);
                    current.Items[i].SetScale(1, 1);
                    current.Items[i].SetMoneyValues(20, 2);
                    break;
                case 2:
                    current.Items[i] = new ItemBuilder();
                    current.Items[i].SetName(House1);
                    current.Items[i].SetScale(5, 5);
                    current.Items[i].SetMoneyValues(400, 50);
                    break;
                case 3:
                    current.Items[i] = new ItemBuilder();
                    current.Items[i].SetName(Tree_01);
                    current.Items[i].SetScale(1, 1);
                    current.Items[i].SetMoneyValues(5, 1);

                    break;

                case 4:
                    current.Items[i] = new ItemBuilder();
                    current.Items[i].SetName(Tree_02);
                    current.Items[i].SetScale(1, 1);
                    current.Items[i].SetMoneyValues(5, 1);
                    break;
                case 5:
                    current.Items[i] = new ItemBuilder();
                    current.Items[i].SetName(Rock_01);
                    current.Items[i].SetScale(1, 1);
                    current.Items[i].SetMoneyValues(5, 1);
                    break;
                case 6:
                    current.Items[i] = new ItemBuilder();
                    current.Items[i].SetName(Rock_02);
                    current.Items[i].SetScale(2, 2);
                    current.Items[i].SetMoneyValues(5, 1);
                    break;
            }
        }
        
    }


}

