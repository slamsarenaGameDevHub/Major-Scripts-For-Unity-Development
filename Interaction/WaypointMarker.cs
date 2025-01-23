using UnityEngine.UI;
using UnityEngine;

public class WaypointMarker : MonoBehaviour
{
    public Image Marker;
    [SerializeField]Transform Target;

    // Update is called once per frame
    void Update()
    {
        Vector2 pos=Camera.main.WorldToScreenPoint(Target.position);

        float minX=Marker.GetPixelAdjustedRect().width/2;
        float maxX=Screen.width-minX;

        float minY=Marker.GetPixelAdjustedRect().height/2;
        float maxY=Screen.height-minY;

        if(Vector3.Dot((Target.position-transform.position).normalized,transform.forward)<0)
        {
           if(pos.x<Screen.width/2)
           {
                pos.x=maxX;
           }
           else{
            pos.x=minX;
           }
        }

        pos.x=Mathf.Clamp(pos.x,minX,maxX);
        pos.y=Mathf.Clamp(pos.y,minY,maxY);

        Marker.transform.position=pos;

    }
}
