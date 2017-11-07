using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class CreateGrid : MonoBehaviour {

    public GameObject block1, Plane, Plane2, Plane3;

    [Range(10, 250)]
    public int worldWidth = 50;
    [Range(10, 250)]
    public int worldHeight  = 50;
    GameObject[,] GridSystem;
    ClickAndDrag GameManger;


    void Update()
    {
        
        while (worldWidth % 10 != 0)
        {
            worldWidth += 1;
        }
        while (worldHeight%10 !=0)
        {
            worldHeight += 1;
        }
    }





    void Start()
    {
        if (Application.isPlaying)
        {
            GameManger = GameObject.Find("EditManager").GetComponent<ClickAndDrag>();
            CreateWorld();
        }
        
    }

   

    void CreateWorld()
    {
        int i = 0;
        GridSystem = new GameObject[worldWidth, worldHeight];
        for (int x = 0; x < worldWidth; x += 1)
        {
          
            for (int z = 0; z < worldHeight; z += 1)
            {
                i++;
                GridSystem[x,z] = Instantiate(block1);
                GridSystem[x, z].transform.position = new Vector3(this.transform.position.x + x, this.transform.position.y, this.transform.position.z + z);
                GridSystem[x, z].transform.parent = transform.gameObject.transform;
            
              
                GridSystem[x, z].name = x.ToString("000") + z.ToString("000");
                //Vector3 newpos = new Vector3(GridSystem[x, z].transform.position.x, GridSystem[x, z].transform.position.y + 0.51f, GridSystem[x, z].transform.position.z);
                //GameObject tile = Instantiate(Plane3, newpos, transform.rotation);
                //tile.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
        }

        Vector3 xpos = GridSystem[((worldWidth/2)-1),((worldHeight/2)-1)].transform.position + (GridSystem[((worldWidth / 2)), ((worldHeight / 2) - 1)].transform.position - GridSystem[((worldWidth / 2) - 1), ((worldHeight / 2) - 1)].transform.position) / 2;
        Vector3 zpos = GridSystem[((worldWidth / 2) - 1), ((worldHeight / 2) - 1)].transform.position + (GridSystem[((worldWidth / 2) - 1), ((worldHeight / 2))].transform.position - GridSystem[((worldWidth / 2) - 1), ((worldHeight / 2) - 1)].transform.position) / 2;
        Vector3 pos = new Vector3( xpos.x, this.transform.position.y + 0.51f, zpos.z);

        var newPlane = Instantiate(Plane, pos, transform.rotation);
        var MainPlane = Instantiate(Plane2, pos, transform.rotation);
        float zdistance = Vector3.Distance(GridSystem[0, 0].transform.position, GridSystem[0, (worldHeight / 2)-1].transform.position);
        float xdistance = Vector3.Distance(GridSystem[0, 0].transform.position, GridSystem[(worldWidth/2)-1, 0].transform.position);
        //newPlane.transform.localScale = new Vector3(xdistance/ worldWidth, 0.1f, zdistance/ worldWidth);

        newPlane.transform.localScale = new Vector3(1f *(worldWidth/10), 0.1f, 1f * (worldHeight / 10));
        newPlane.GetComponent<Renderer>().material.mainTextureScale = new Vector2(worldWidth, worldHeight);
        newPlane.name = "GridPlane";
        MainPlane.transform.localScale = new Vector3(1f * (worldWidth / 10), 0.1f, 1f * (worldHeight / 10));
        MainPlane.GetComponent<Renderer>().material.mainTextureScale = new Vector2(worldWidth, worldHeight);
        MainPlane.name = "Ground";
        
        GameManger.Grid = GridSystem;
        GameManger.GridPlane = newPlane;
        GameManger.GroundPlane = MainPlane;
        GameManger.worldHeight = worldHeight;
        GameManger.worldWidth = worldWidth;
        newPlane.gameObject.SetActive(false);
    }
}
