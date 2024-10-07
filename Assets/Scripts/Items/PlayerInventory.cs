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
    public ItemBase SelectedItem;
    public int CursorNo;

    public int CursorNoSelection;
    public bool isItemSelected;

    [Header("SELECTED ITEM MENU")]
    //public TextMeshProUGUI ItemNameText;
    public GameObject SelectionMenuTop;
    public TextMeshProUGUI SelectedItemInfoText;
    public Image SelectedItemImage;


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

                isItemSelected = false;           //SELECTION MENU ILE ILGILI
                SelectionMenuTop.SetActive(false);// 

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
        if (!isItemSelected)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                SetInvCursor(CursorNo + 1);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                SetInvCursor(CursorNo - 1);
            }


            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                //SelectedItem.UseItem();
                SelectionMenuTop.SetActive(true);
                isItemSelected = true;
                SetInvCursorSelection(CursorNoSelection);
                UpdateSelectionUI();
            }
        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                SetInvCursorSelection(CursorNo + 1);
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                SetInvCursorSelection(CursorNo - 1);
            }


            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                if (CursorNoSelection == 0) {
                    SelectedItem.UseItem();
                    isItemSelected = false;
                    SelectionMenuTop.SetActive(false);
                    UpdateInventoryUI();
                }
                else 
                {
                    isItemSelected = false; 
                    SelectionMenuTop.SetActive(false); 
                    UpdateInventoryUI(); 
                }
            }
        }


    }
    private void SetInvCursor(int x)
    {
        int no = Mathf.Clamp(x, 0,InventoryBars.Count-1);
        if(no < 0) { no = 0; }

        CursorNo = no;
        InvUICursor.GetComponent<RectTransform>().position = new Vector2(960 + (-5*4), 540 + ((-5 * 20) * no));
        InvUICursor.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        SelectedItem = Inventory[no];
    }
    private void SetInvCursorSelection(int x)
    {
        int no = Mathf.Clamp(x, 0, 1);

        CursorNoSelection = no;
        RectTransform CursorRect = InvUICursor.GetComponent<RectTransform>();
        CursorRect.rotation = Quaternion.Euler(0, 0, 90);

        if(no == 0) { CursorRect.position = new Vector2(960 +172, 540 + -420); } else { CursorRect.position = new Vector2(960 + 500, 540 + -420); }

    }
    private void UpdateSelectionUI()
    {
        SelectedItemInfoText.text = SelectedItem.itemInfo;

        Vector2 spriteSize = SelectedItem.itemSprite.rect.size;
        //float pixelsPerUnit = SelectedItem.itemSprite.pixelsPerUnit;
        SelectedItemImage.rectTransform.sizeDelta = spriteSize * 5;//1920x1080 e esitlemek icin  // / pixelsPerUnit;
        SelectedItemImage.sprite = SelectedItem.itemSprite;
    }
}
