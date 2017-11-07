using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuilder {

        GameObject ItemName;
        int ScaleX, ScaleZ;
        float rotationX, rotationZ;
        int Cost, MoneyReturns;


        public ItemBuilder()
        {
            ItemName = null;
            ScaleX = 0;
            ScaleZ = 0;
            rotationX = 0;
            rotationZ = 0;
            Cost = 0;
            MoneyReturns = 0;
        }
        public void SetName(GameObject Name)
        {
            ItemName = Name;
        }

        public void SetScale(int X, int Z)
        {
            ScaleX = X;
            ScaleZ = Z;
        }

        public void SetRotation(float X, float Z)
        {
            rotationX = X;
            rotationZ = Z;
        }

       public void SetMoneyValues(int cost,int moneyProduces)
    {
        Cost = cost;
        MoneyReturns = moneyProduces;
    }

    public GameObject RetrieveGameObject()
    {
        return ItemName;
    }

    public int ScaleXVal()
    {
        return ScaleX;
    }
    public int ScaleZVal()
    {
        return ScaleZ;
    }

    public int ItemCost()
    {
        return Cost;
    }

    public int ItemMoneyReturns()
    {
        return MoneyReturns;
    }

}

