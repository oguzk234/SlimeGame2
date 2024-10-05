using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public static int PixelSizeInUI = 5;
    //private int InvBarSpace = 20;  //bar=18 +2 Space  //GEREK YOK VERTICAL LAYOUT GROUP ILE HALLETTIM

    public List<ItemBase> Inventory = new List<ItemBase>();
    public List<InventoryBar> InventoryBars = new List<InventoryBar>();

    public static bool isInvOpen;
    public GameObject InventoryTopObj;
    public GameObject InventoryBarPrefab;
    public Transform InventoryMenuBase;

    public GameObject InvUICursor;
    public int CursorNo;
    public ItemBase SelectedItem;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(DialogManager.Instance.isOnDialogue == true) { return; }



            if (!isInvOpen)
            {
                isInvOpen = true;
                PlayerMove.Instance.isMoveInputGetting = false;
                InventoryTopObj.SetActive(true);
                UpdateInventoryUI();
            }
            else
            {
                isInvOpen = false;
                PlayerMove.Instance.isMoveInputGetting = true;
                InventoryTopObj.SetActive(false);
            }

        }

        if (isInvOpen && InventoryBars.Count != 0)
        {
            InventoryControls();
        }
    }

    public void UpdateInventoryUI()
    {
        if (Inventory.Count == 0) { InvUICursor.SetActive(false); } else { InvUICursor.SetActive(true); }
        InventoryBars = new List<InventoryBar>();
        //if (Inventory.Count == 0) { return; }

        foreach (GameObject go in OguzLib.SubObject.GetAllSubGameObjects(InventoryMenuBase.gameObject))
        {
            Destroy(go);
        }

        foreach(ItemBase item in Inventory)
        {
            GameObject barObj = Instantiate(InventoryBarPrefab, InventoryMenuBase);
            InventoryBar bar = barObj.GetComponent<InventoryBar>();

            bar.Initialize(item);
            InventoryBars.Add(bar);
        }


        if(Inventory.Count != 0) { SetInvCursor(CursorNo); }   ////BUNU USTE ATINCA HATA OLUYO

    }

    private void InventoryControls()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetInvCursor(CursorNo+1);
        }
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetInvCursor(CursorNo - 1);
        }


        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SelectedItem.UseItem();
        }
    }
    private void SetInvCursor(int x)
    {
        int no = Mathf.Clamp(x, 0,InventoryBars.Count-1);
        if(no < 0) { no = 0; }

        CursorNo = no;
        InvUICursor.GetComponent<RectTransform>().position = new Vector2(960 + (-5*4), 540 + ((-5 * 20) * no));
        SelectedItem = Inventory[no];
    }
}
