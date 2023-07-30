using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance {get; set;}
    public GameObject interaction_info;
     TextMeshProUGUI int_text;
    public GameObject playerPos;
    public bool onTarget;
    public GameObject selectedObject;
    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
       onTarget = false;
        int_text = interaction_info.GetComponent<TextMeshProUGUI>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = new Ray(playerPos.transform.position, playerPos.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            var selectTrans = hit.transform;

            InteractableObject interactable = selectTrans.GetComponent<InteractableObject>();
            
            if (interactable && interactable.playerInRange)
            {
                onTarget = true;
                selectedObject = interactable.gameObject;
                int_text.text = interactable.GetItemName();
                interaction_info.SetActive(true);
                
            }
            else
            {
                onTarget = false;
                interaction_info.SetActive(false);
            }
        }
        else{
            onTarget = false;
            interaction_info.SetActive(false);
        }
    }
}
