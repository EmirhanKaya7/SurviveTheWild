using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemlist = new List<string>();
    GameObject itemToAdd;
    GameObject whatSlotToEquip;
    public GameObject inventoryScreenUI;
    public bool isOpen;
    public bool isFull;
    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            
            isOpen =true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            isOpen =false;
        }
    }
}
