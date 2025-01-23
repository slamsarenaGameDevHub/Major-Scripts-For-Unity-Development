using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camRot : MonoBehaviour
{
    [SerializeField] Transform camTransform;
    [SerializeField]private float sensitivity = 100f;
    public float xLook;
    public float minX;
    public float maxX;


    bool lockCursor=true;

    void Update()
    {
        LockCursor();

        float horizontalRot = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * horizontalRot);

        float verticalRot = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        xLook -= verticalRot;
        xLook = Mathf.Clamp(xLook, -minX, maxX);
        camTransform.localRotation = Quaternion.Euler(xLook, 0, 0);

    }
    void LockCursor()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            lockCursor=!lockCursor;
        }
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;   
        }
        else{
            Cursor.lockState=CursorLockMode.None;
            Cursor.visible=true;
        }
    }
}