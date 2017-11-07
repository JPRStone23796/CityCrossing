using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ClickAndDrag : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ObjectParent = GameObject.Find("Objects");
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ItemSelected = GameObject.Find("UIManager").GetComponent<ObjectSelectionUI>();
        ObjectBtn.gameObject.SetActive(false);
        TileBtn.gameObject.SetActive(false);
        DestroyBtn.gameObject.SetActive(false);
    }

    public GameObject[,] Grid;
    public GameObject Prefab, prefab2,prefab3,GridPlane,GroundPlane,TilePrefab,TileParent,ObjectParent;
    private GameObject loading;
    private GameManager GameManager;
    Vector3 center,SpawnCenter;
    public bool GridView,TerrainView;
    string currentBlock;
    public GameObject current;
    bool buttonpressed;
    int x, z;
        public int Cost,MoneyReturn;
    public int ScaleX, ScaleZ;
    int CurrentScaleX, CurrentScaleZ, rotationX, rotationZ;
    int direction = 1;
    public int worldWidth = 50;
    public int worldHeight = 50;
   public ItemBuilder[] Items;
    ObjectSelectionUI ItemSelected;



    public Button ObjectBtn, TileBtn, DestroyBtn;
    void UIManagement()
    {
        buttonpressed = false;
         if(buttonpressed==false)
          {
            ObjectBtn.onClick.AddListener(ObjectBtnPressed);
            TileBtn.onClick.AddListener(TileBtnPressed);
            DestroyBtn.onClick.AddListener(DestroyBtnPressed);
        }

    }


    public void setCurrentSelectedItem(int item)
    {
        CurrentSelectedItem = item;
        current = Items[CurrentSelectedItem].RetrieveGameObject();
        ScaleX = Items[CurrentSelectedItem].ScaleXVal();
        ScaleZ = Items[CurrentSelectedItem].ScaleZVal();
        Cost = Items[CurrentSelectedItem].ItemCost();
        MoneyReturn = Items[CurrentSelectedItem].ItemMoneyReturns();
    }
    void DestroyBtnPressed()
    {
        DMode = true;
        TerrainView = false;
        buttonpressed = true;
        Destroy(loading);
    }
    void TileBtnPressed()
    {
        DMode = false;
        TerrainView = true;
        buttonpressed = true;
        current = TilePrefab;
        ScaleX = 1;
        ScaleZ = 1;
    }
    void ObjectBtnPressed()
    {
        DMode = false;
        TerrainView = false;
        buttonpressed = true;
        current = Items[CurrentSelectedItem].RetrieveGameObject();
        ScaleX = Items[CurrentSelectedItem].ScaleXVal();
        ScaleZ = Items[CurrentSelectedItem].ScaleZVal();
        Cost = Items[CurrentSelectedItem].ItemCost();
        MoneyReturn = Items[CurrentSelectedItem].ItemMoneyReturns();
    }


    void RemoveGridView()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buttonpressed = false;
            if (GridPlane.gameObject.activeSelf == true && buttonpressed == false)
            {
                GridPlane.gameObject.SetActive(false);
                GroundPlane.gameObject.SetActive(true);
                buttonpressed = true;
                GridView = false;
                TerrainView = false;
                DMode = false;
                Destroy(loading);
                ObjectBtn.gameObject.SetActive(false);
                TileBtn.gameObject.SetActive(false);
                DestroyBtn.gameObject.SetActive(false);
            }
            if (GridPlane.gameObject.activeSelf == false && buttonpressed == false)
            {
                GridPlane.gameObject.SetActive(true);
                GroundPlane.gameObject.SetActive(false);
                buttonpressed = true;
                GridView = true;
                ObjectBtn.gameObject.SetActive(true);
                TileBtn.gameObject.SetActive(true);
                DestroyBtn.gameObject.SetActive(true);
            }
           
        }
        
    }

   

    public bool DMode;
   

    void SetRotation()
    {
        if (loading != null)
        {
            switch (direction)
            {
                case 1:
                    CurrentScaleX = ScaleX;
                    CurrentScaleZ = ScaleZ;
                    loading.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    break;

                case 2:
                    CurrentScaleX = ScaleZ;
                    CurrentScaleZ = ScaleX;
                    loading.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                    break;

                case 3:
                    CurrentScaleX = -ScaleX;
                    CurrentScaleZ = ScaleZ;
                    loading.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    break;

                case 4:
                    CurrentScaleX = ScaleZ;
                    CurrentScaleZ = -ScaleX;
                    loading.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                    break;
            }


        }
    }






    void SpawnItem()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameManager.CurrentPlaythroughStats.Purchase(Cost);
            GameManager.CurrentPlaythroughStats.ChangeMoneyProducing(MoneyReturn);
            Quaternion rotation = Quaternion.Euler(new Vector3(rotationX, loading.transform.eulerAngles.y, rotationZ));
            Vector3 pos = new Vector3(loading.transform.position.x, 0.5f, loading.transform.position.z);
            var temp = Instantiate(current, pos, rotation);
            temp.name = "Works";
            temp.tag = "Object";
            temp.transform.GetChild(0).tag = temp.tag = "Object";
            temp.gameObject.transform.parent = ObjectParent.transform;
           
        }
    }
    void ChangeRotation()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            switch (direction)
            {
                case 1: direction++; break;
                case 2: direction++; break;
                case 3: direction++; break;
                case 4: direction = 1; break;
            }

        }
    }

   public int CurrentSelectedItem = 0;
    //void ChangeSpawnItem()
    //{
    //    if (current == null)
    //    {
    //        current = Items[CurrentSelectedItem].RetrieveGameObject();
    //        ScaleX = Items[CurrentSelectedItem].ScaleXVal();
    //        ScaleZ = Items[CurrentSelectedItem].ScaleZVal();
    //        Cost = Items[CurrentSelectedItem].ItemCost();
    //        MoneyReturn = Items[CurrentSelectedItem].ItemMoneyReturns();
    //    }

    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        buttonpressed = false;
    //        CurrentSelectedItem++;
    //        if (CurrentSelectedItem == Items.Length) { CurrentSelectedItem = 0; }
    //        current = Items[CurrentSelectedItem].RetrieveGameObject();
    //        ScaleX = Items[CurrentSelectedItem].ScaleXVal();
    //        ScaleZ = Items[CurrentSelectedItem].ScaleZVal();
    //        Cost = Items[CurrentSelectedItem].ItemCost();
    //        MoneyReturn = Items[CurrentSelectedItem].ItemMoneyReturns();
    //    }

      
    //}


    void objectSpawning(RaycastHit hit)
    {

        if (hit.transform.gameObject.tag == "Object")
        {
            Destroy(loading);
        }
        if (hit.transform.gameObject.tag == "Ground")
        {
            if (loading == null)
            {
                loading = Instantiate(Prefab, hit.point, transform.rotation);
                loading.GetComponent<Renderer>().material.color = PlacementPossibleColour;
                center = loading.GetComponent<Renderer>().bounds.center;
            }

           
       
            if (loading.gameObject.GetComponent<SpawningCollision>().notspawn == true || (GameManager.CurrentPlaythroughStats.CheckCurrentMoney()< Cost))
            {
                loading.GetComponent<Renderer>().material.color = NoPlacementPossible;
            }
            if (loading.gameObject.GetComponent<SpawningCollision>().notspawn == false && GameManager.CurrentPlaythroughStats.CheckCurrentMoney()>=Cost)
            {
                loading.GetComponent<Renderer>().material.color = PlacementPossibleColour;
            }

            if (CurrentScaleX == 1 && CurrentScaleZ == 1)
            {
                loading.transform.localScale = new Vector3(1, 1, 1);
                loading.transform.position = new Vector3(hit.transform.gameObject.GetComponent<Renderer>().bounds.center.x, (hit.point.y + center.y), (hit.transform.gameObject.GetComponent<Renderer>().bounds.center.z));

            }


            if ((CurrentScaleX > 1 || CurrentScaleX < 0) || (CurrentScaleZ > 1 || CurrentScaleZ < 0))
            {
                loading.transform.localScale = new Vector3(ScaleX, 1, ScaleZ);
                string xposChar, zposChar;
                currentBlock = hit.transform.name;
                zposChar = currentBlock.Substring(3, 3);
                int.TryParse(zposChar, out z);
                xposChar = currentBlock.Substring(0, 3);
                int.TryParse(xposChar, out x);
                if (CurrentScaleZ > 1)
                {
                    if (z > ((worldWidth-1) - (CurrentScaleZ - 1))) { z = ((worldWidth - 1) - (CurrentScaleZ - 1)); }
                }

                if (CurrentScaleZ < 0)
                {
                    if (z + CurrentScaleZ < 0) { z = 0 - (CurrentScaleZ + 1); }
                }

                if (CurrentScaleX > 1)
                {
                    if (x > ((worldHeight - 1) - (CurrentScaleX - 1))) { x = ((worldHeight - 1) - (CurrentScaleX - 1)); }
                }

                if (CurrentScaleX < 0)
                {
                    if (x + CurrentScaleX < 0) { x = 0 - (CurrentScaleX + 1); }
                }


                if (CurrentScaleZ > 0 && CurrentScaleX > 0)
                {
                    Vector3 xpos = Grid[x, z].transform.position + (Grid[x + (CurrentScaleX - 1), z].transform.position - Grid[x, z].transform.position) / 2;
                    Vector3 zpos = Grid[x, z].transform.position + (Grid[x, z + (CurrentScaleZ - 1)].transform.position - Grid[x, z].transform.position) / 2;
                    loading.transform.position = new Vector3(xpos.x, (hit.point.y + center.y), zpos.z);
                }

                if (CurrentScaleZ < 0 && CurrentScaleX > 0)
                {

                    Vector3 xpos = Grid[x, z].transform.position + (Grid[x + (CurrentScaleX - 1), z].transform.position - Grid[x, z].transform.position) / 2;
                    Vector3 zpos = Grid[x, z].transform.position + (Grid[x, z + (CurrentScaleZ + 1)].transform.position - Grid[x, z].transform.position) / 2;
                    loading.transform.position = new Vector3(xpos.x, (hit.point.y + center.y), zpos.z);
                }

                if (CurrentScaleZ > 0 && CurrentScaleX < 0)
                {
                    Vector3 xpos = Grid[x, z].transform.position + (Grid[x + (CurrentScaleX + 1), z].transform.position - Grid[x, z].transform.position) / 2;
                    Vector3 zpos = Grid[x, z].transform.position + (Grid[x, z + (CurrentScaleZ - 1)].transform.position - Grid[x, z].transform.position) / 2;
                    loading.transform.position = new Vector3(xpos.x, (hit.point.y + center.y), zpos.z);
                }

              


            }
            if(loading.GetComponent<Renderer>().material.color == PlacementPossibleColour)
            {
             SpawnItem();
            }
            
            ChangeRotation();
            //ChangeSpawnItem();
        }
    }


 
    public Color PlacementPossibleColour, NoPlacementPossible;
    void TileSpawning(RaycastHit hit)
    {
       current = TilePrefab;
       Cost = 5;

        if (hit.transform.gameObject.tag == "Ground")
        {
            if (loading == null)
            {
                loading = Instantiate(Prefab, hit.point, transform.rotation);
                loading.GetComponent<Renderer>().material.color = PlacementPossibleColour;
                center = loading.GetComponent<Renderer>().bounds.center;
            }

            if (CurrentScaleX == 1 && CurrentScaleZ == 1)
            {
                loading.transform.localScale = new Vector3(1, 1, 1);
                loading.transform.position = new Vector3(hit.transform.gameObject.GetComponent<Renderer>().bounds.center.x, (hit.point.y + center.y), (hit.transform.gameObject.GetComponent<Renderer>().bounds.center.z));

            }

            if(hit.transform.gameObject.GetComponent<TileInformation>().BeingUsed==false && (GameManager.CurrentPlaythroughStats.CheckCurrentMoney() >= Cost))
            {

                loading.gameObject.GetComponent<Renderer>().material.color = PlacementPossibleColour;
            }

            if (hit.transform.gameObject.GetComponent<TileInformation>().BeingUsed == true || (GameManager.CurrentPlaythroughStats.CheckCurrentMoney() < Cost))
            {
                loading.gameObject.GetComponent<Renderer>().material.color = NoPlacementPossible;
            }

            loading.transform.localScale = new Vector3(ScaleX, 1, ScaleZ);
                string xposChar, zposChar;
                currentBlock = hit.transform.name;
                zposChar = currentBlock.Substring(3, 3);
                int.TryParse(zposChar, out z);
                xposChar = currentBlock.Substring(0, 3);
                int.TryParse(xposChar, out x);
               

                if (CurrentScaleZ > 0 && CurrentScaleX > 0)
                {
                    Vector3 xpos = Grid[x, z].transform.position + (Grid[x + (CurrentScaleX - 1), z].transform.position - Grid[x, z].transform.position) / 2;
                    Vector3 zpos = Grid[x, z].transform.position + (Grid[x, z + (CurrentScaleZ - 1)].transform.position - Grid[x, z].transform.position) / 2;
                    loading.transform.position = new Vector3(xpos.x, (hit.point.y + center.y), zpos.z);
                }
            if(loading.gameObject.GetComponent<Renderer>().material.color == PlacementPossibleColour)
            {
                TileSpawn(hit);
            }
           
            
            //ChangeSpawnItem();
        }
    }

    float PlaneXScale, PlaneZScale;

    void TileSpawn(RaycastHit hit)
    {
        PlaneXScale = 0.1f;
        PlaneZScale = 0.1f;
        if (Input.GetButton("Fire1"))
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(rotationX, loading.transform.eulerAngles.y, rotationZ));
            var temp = Instantiate(current, new Vector3(loading.transform.position.x, GridPlane.transform.position.y + 0.02f, loading.transform.position.z), rotation);
            temp.name = "Tile";
            temp.transform.localScale = new Vector3(PlaneXScale, 0.1f, PlaneZScale);
            temp.gameObject.transform.parent = TileParent.transform;
            hit.transform.gameObject.GetComponent<TileInformation>().BeingUsed = true;
            GameManager.CurrentPlaythroughStats.Purchase(Cost);

        }




    }


    public float buttonHeld;
    Color destroyingItemColour;
    Color originalColour = Color.white;
    public GameObject DestroyBarUI;
    private GameObject CurrentDestroyUI;
    private Image DestroyFillImage;
    private Text DestroyPercentage;
    void DestroyItems(RaycastHit hit)
    {
        if (hit.transform.gameObject.tag == "Object")
        {
           
            if (Input.GetButton("Fire1"))
            {
                Cursor.visible = false;
                if (originalColour == Color.white)
                {
                    originalColour = hit.transform.gameObject.GetComponent<Renderer>().material.color;


                    Vector3 CurrentOffset = new Vector3(hit.transform.position.x, hit.point.y, hit.transform.position.z);
                    CurrentDestroyUI = Instantiate(DestroyBarUI, CurrentOffset, Quaternion.Euler(new Vector3(90,0,0)));
                    CurrentDestroyUI.transform.position += (Vector3.up * 1.2f);
                    DestroyFillImage = CurrentDestroyUI.transform.Find("FillAmount").GetComponent<Image>();
                    DestroyPercentage = CurrentDestroyUI.transform.Find("CountDown").GetComponent<Text>();
                }
                buttonHeld += Time.deltaTime;
                destroyingItemColour = Color.Lerp(PlacementPossibleColour, NoPlacementPossible, (buttonHeld/2));
                hit.transform.gameObject.GetComponent<Renderer>().material.color = destroyingItemColour;

                DestroyFillImage.fillAmount = (buttonHeld /2);
                DestroyPercentage.text = ((buttonHeld / 2) * 100).ToString("00") + "%";
                if (buttonHeld > 2)
                {
                    Destroy(hit.transform.gameObject);
                    Destroy(CurrentDestroyUI);
                }
            }


            if (!(Input.GetButton("Fire1")) && buttonHeld > 0)
            {
                if (buttonHeld < 2)
                {
                    hit.transform.gameObject.GetComponent<Renderer>().material.color = originalColour;
                }
                buttonHeld = 0;
                originalColour = Color.white;
                Destroy(CurrentDestroyUI);
                Cursor.visible = true;
            }
        }


        if (!(Input.GetButton("Fire1")) && buttonHeld > 0)
        {
            buttonHeld = 0;
            originalColour = Color.white;
            Destroy(CurrentDestroyUI);
            Cursor.visible = true;
        }
    }

    void MainBlock()
    {
        SetRotation();
        RemoveGridView();
       
        if (GridView)
        {
            UIManagement();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            int layerTrigger = 1 << LayerMask.NameToLayer("Temp");
            layerTrigger = ~layerTrigger;
            if (Physics.Raycast(ray, out hit, 1000f, layerTrigger))
            {
                if(!(EventSystem.current.IsPointerOverGameObject()))
                {
                    if (TerrainView == false && DMode == false) { objectSpawning(hit); }
                    if (TerrainView == true && DMode == false) { TileSpawning(hit); }
                    if (DMode == true) { DestroyItems(hit); }
                }
                else
                {
                    Destroy(loading);
                }
                   

            }
            else if(!(Physics.Raycast(ray, out hit, 1000f, layerTrigger)))
            {
                Destroy(loading);
            }


            if ((!(ItemSelected.IsButtonSelected())) && TerrainView==false && DMode==false)
            {
                Destroy(loading);
            }
            


        }
    }

    void Update()
    {
        MainBlock();
    }
}
