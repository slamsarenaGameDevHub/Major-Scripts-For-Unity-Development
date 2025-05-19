using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public float Amount;
    public float smoothTime;
    public float frequecy=10;
    float currentSpeed;
    [SerializeField]CharacterController characterController;
    Vector3 bobPos=Vector3.zero;

    
    void OnEnable()
    {
        if(characterController == null)
        {
            Debug.LogError("No Character Controller Attached");
        }
    }
    void Update()
    {
        GenerateHeadBob();
    }
    void GenerateHeadBob()
    {
        currentSpeed=characterController.velocity.magnitude;
        if(Mathf.Abs(currentSpeed) >= 0.1f)
        {
            bobPos.x= Mathf.Lerp(bobPos.x,Mathf.Cos(Time.time *frequecy/2 ) * Amount*1.4f,smoothTime);
            bobPos.y= Mathf.Lerp(bobPos.y,Mathf.Sin(Time.time *frequecy) * Amount*1.6f,smoothTime);
        }
        else
        {
            bobPos=Vector3.Lerp(bobPos,Vector3.zero,smoothTime);
        }
        transform.localPosition = bobPos;
    }
}
