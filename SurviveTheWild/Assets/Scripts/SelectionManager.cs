using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class SelectionManager : MonoBehaviour
{
    public GameObject interaction_info;
     TextMeshProUGUI int_text;
    public GameObject playerPos;
   
    
    // Start is called before the first frame update
    void Start()
    {
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

            if (selectTrans.GetComponent<InteractableObject>())
            {
                int_text.text = selectTrans.GetComponent<InteractableObject>().GetItemName();
                interaction_info.SetActive(true);
                
            }
            else
            {
                interaction_info.SetActive(false);
            }
        }
    }
}
