using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSense = 100f;

    float xRot = 0f;
    float yRot = 0f;

    private void Start() {
       Cursor.lockState = CursorLockMode.Locked;
       
    }
    private void Update() {
         float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;


        xRot = mouseY;
        xRot = Mathf.Clamp(xRot,-90f,90f);

        yRot += mouseX;

        transform.localRotation = Quaternion.Euler(xRot, yRot,0f);
    }
}
