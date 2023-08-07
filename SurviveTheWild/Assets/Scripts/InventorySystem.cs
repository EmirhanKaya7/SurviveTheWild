using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemlist = new List<string>();
    GameObject itemToAdd;
    GameObject whatSlotToEquip;
    public GameObject inventoryScreenUI;
    public bool isOpen;
    public GameObject pickupAlert;
    public TextMeshProUGUI pickupName;
    public Image pickupImage;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        isOpen = false;
        PopulateSlotList();

    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            FirstPersonController.Instance.cameraCanMove = false;
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            if (!CraftingSystem.Instance.isOpen)
            {
                FirstPersonController.Instance.cameraCanMove = true;

                Cursor.lockState = CursorLockMode.Locked;

            }

            isOpen = false;
        }
    }



    public void AddToInventory(string itemName)
    {
        whatSlotToEquip = FindNextEmptySlot();

        itemToAdd = Instantiate(Resources.Load<GameObject>(itemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);

        itemlist.Add(itemName);

        
        TriggerPickupPopUp(itemName, itemToAdd.GetComponent<Image>().sprite);


        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();

    }


    void TriggerPickupPopUp(string itemName, Sprite itemSprite)
    {
        pickupAlert.SetActive(true);
        pickupName.text = itemName;
        pickupImage.sprite = itemSprite;
        StartCoroutine(closePopup());
    }
    IEnumerator closePopup()
    {
        yield return new WaitForSeconds(1);
        pickupAlert.SetActive(false);
    }
    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter++;
            }

        }
        if (counter == 21)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }

        }
        return new GameObject();
    }



    public void RemoveItem(string nameToRemove, int amountToRemove)
    {

        int counter = amountToRemove;
        for (var i = slotList.Count - 1; i >= 0; i--)
        {
            if (slotList[i].transform.childCount > 0)
            {
                if (slotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter != 0)
                {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    counter--;
                }
            }


        }
        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }


    public void ReCalculateList()
    {
        itemlist.Clear();
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;


                string str2 = "(Clone)";

                string result = name.Replace(str2, "");



                itemlist.Add(result);
            }
        }
    }
}
