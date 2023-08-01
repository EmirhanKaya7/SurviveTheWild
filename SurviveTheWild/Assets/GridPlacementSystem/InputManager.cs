using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask layerMask;

    private Vector3 lastPos;


    public Vector3 GetSelectedPos()
    {
        Vector3 mousePos= Input.mousePosition;
        mousePos.z =sceneCamera.nearClipPlane;
        Ray ray = sceneCamera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,100,layerMask))
        {
            lastPos = hit.point;
        }
        return lastPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
