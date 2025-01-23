using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  PlayerRotation : MonoBehaviour
{
    // References
    [SerializeField] private Transform cameraTransform;



    // Player settings
    [SerializeField] private float cameraSensitivity;
   
    // Touch detection
    private int rightFingerId;
    private float halfScreenWidth;

    // Camera control
    private Vector2 lookInput;
    private float cameraPitch;
    [SerializeField] float maxCamLook = 60;
    float yLook;

   
    // Start is called before the first frame update
    void Start()
    {
        // id = -1 means the finger is not being tracked
       
        rightFingerId = -1;

        // only calculate once
        halfScreenWidth = Screen.width / 2;

    
    }

    // Update is called once per frame
    void Update()
    {
        // Handles input
        GetTouchInput();


        if (rightFingerId != -1) {
            
            LookAround();
        }

      
    }

    void GetTouchInput() {
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {

            Touch t = Input.GetTouch(i);

            // Check each touch's phase
            switch (t.phase)
            {
                case TouchPhase.Began:

                   
                    if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        rightFingerId = t.fingerId;
                    }

                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                   
                    if (t.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                    }

                    break;
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    
                    break;
                case TouchPhase.Stationary:
                    // Set the look input to zero if the finger is still
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    void LookAround()
    {
        // Update the yaw (horizontal rotation)
        yLook += lookInput.x * cameraSensitivity;

        // Update the pitch (vertical rotation) and clamp it
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y * cameraSensitivity, -maxCamLook, maxCamLook);

        // Combine pitch and yaw into one Quaternion
        Quaternion targetRotation = Quaternion.Euler(cameraPitch, yLook, 0f);

        // Apply the combined rotation to the transform
        transform.localRotation = targetRotation;
    }


}
