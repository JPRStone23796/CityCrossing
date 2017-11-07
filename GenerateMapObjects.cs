using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMapObjects : MonoBehaviour {

    CreateGrid CreateGrid;
    ClickAndDrag GM;
    Vector2[] CurrentPlacement;
    GameObject[] Spawn;

    // Use this for initialization
    void Start () {
        CreateGrid = GameObject.Find("Grid").GetComponent<CreateGrid>();
        GM = GameObject.Find("EditManager").GetComponent<ClickAndDrag>();
        CurrentPlacement = new Vector2[120];
        Spawn = new GameObject[4];
        Spawn[0] = GM.Items[3].RetrieveGameObject();
        Spawn[1] = GM.Items[4].RetrieveGameObject();
        Spawn[2] = GM.Items[5].RetrieveGameObject();
        Spawn[3] = GM.Items[6].RetrieveGameObject();
    }


   public int CurrentPlaced;
    bool ObjectAlreadyExist;
    GameObject currentGridObj;
 
    Vector3 SpawnPos;
    float startpointx, endpointx, endpointz, startpointz;
    void ItemPlacement()
    {
        if(CurrentPlaced<120)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    ObjectAlreadyExist = true;
                    do
                    {
                        Vector2 TempPos = new Vector2(Random.Range(0, (CreateGrid.worldWidth-1)), Random.Range(0, (CreateGrid.worldHeight-1)));
                        bool exist = false;

                        if (GM.Items[i+3].ScaleXVal() <= 1 && GM.Items[i+3].ScaleZVal() <= 1)
                        {
                            currentGridObj = GameObject.Find(TempPos.x.ToString("000") + TempPos.y.ToString("000"));
                            exist = currentGridObj.GetComponent<TileInformation>().ObjectAbove;
                        }
                        else
                        {
                            int xscale = GM.Items[i+3].ScaleXVal();
                            int zscale = GM.Items[i+3].ScaleZVal();
                            float midpointx,xpointR;
                            float midpointz,zPointR;
                            midpointx = xscale % 2;
                            midpointz = zscale % 2;
                            xpointR = xscale / 2;
                            zPointR = zscale / 2;


                            if (midpointx!=0)
                            {
                                startpointx = TempPos.x - xpointR + 0.5f;
                              
                                endpointx = startpointx + xscale - 1;
                            }
                            else
                            {
                                startpointx = TempPos.x - xpointR + 1f;
                                endpointx = startpointx + xscale - 1;
                            }

                            if (midpointz != 0)
                            {
                                startpointz = TempPos.y - zPointR + 0.5f;
                                endpointz = startpointz + zscale - 1;
                            }
                            else
                            {
                                startpointz = TempPos.y - zPointR + 1f;
                                endpointz = startpointz + zscale - 1;
                            }

                            if (endpointx < 140 && endpointz < 140)
                            {
                                for (float xpoint = startpointx; xpoint <= endpointx; xpoint++)
                                {
                                    for (float zpoint = startpointz; zpoint <= endpointz; zpoint++)
                                    {
                                        currentGridObj = GameObject.Find(xpoint.ToString("000") + zpoint.ToString("000"));
                                        exist = currentGridObj.GetComponent<TileInformation>().ObjectAbove;
                                        if (exist == true)
                                        {
                                            break;
                                        }

                                    }
                                    if (exist == true)
                                    {
                                        break;
                                    }
                                }
                                float xpos = GameObject.Find(startpointx.ToString("000") + startpointz.ToString("000")).transform.position.x + ((GameObject.Find(endpointx.ToString("000") + startpointz.ToString("000")).transform.position.x - GameObject.Find(startpointx.ToString("000") + startpointz.ToString("000")).transform.position.x) / 2);
                                float zpos = GameObject.Find(startpointx.ToString("000") + startpointz.ToString("000")).transform.position.z + ((GameObject.Find(startpointx.ToString("000") + endpointz.ToString("000")).transform.position.z - GameObject.Find(startpointx.ToString("000") + startpointz.ToString("000")).transform.position.z) / 2);
                                SpawnPos = new Vector3(xpos, currentGridObj.transform.position.y + (0.5f), zpos);
                            }
                            else { exist = true; }
                                      
                        }

                        ObjectAlreadyExist = exist;
                    }
                    while (ObjectAlreadyExist == true);

                      if(GM.Items[i+3].ScaleXVal() <= 1 && GM.Items[i+3].ScaleZVal() <= 1)
                         {
                        SpawnPos = new Vector3(currentGridObj.transform.position.x, currentGridObj.transform.position.y + (0.5f), currentGridObj.transform.position.z);
                        GameObject temp = Instantiate(Spawn[i], SpawnPos, Quaternion.identity);
                        temp.transform.SetParent(GameObject.Find("Objects").transform);
                        CurrentPlaced++;
                        currentGridObj.GetComponent<TileInformation>().ObjectAbove = true;
                         }
                      else
                       {
                        GameObject temp = Instantiate(Spawn[i], SpawnPos, Quaternion.identity);
                        temp.transform.SetParent(GameObject.Find("Objects").transform);
                        CurrentPlaced++;
                        for (float xpoint = startpointx; xpoint <= endpointx; xpoint++)
                        {
                            for (float zpoint = startpointz; zpoint <= endpointz; zpoint++)
                            {
                                currentGridObj = GameObject.Find(xpoint.ToString("000") + zpoint.ToString("000"));
                                currentGridObj.GetComponent<TileInformation>().ObjectAbove = true;
                               
                            }
                        }
                    }
                  
                }
            }

        }                  
    }





    void LargerScale()
    { 
}
	// Update is called once per frame
	void Update () {
        ItemPlacement();

    }
}
