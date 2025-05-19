using UnityEngine;
using UnityEngine.UI;

namespace Believe.Games.Studios
{
    public class Tracker : MonoBehaviour
    {
        public Transform Target;
        [SerializeField] Image trackerImage;


        Vector2 pos;
        private void Update()
        {
            if(Target==null)
            {
                trackerImage.gameObject.SetActive(false);
                return;
            }
            else
            {
                if(!trackerImage.gameObject.activeInHierarchy)
                {
                    trackerImage.gameObject.SetActive(true);
                }
            }
                float minX = trackerImage.GetPixelAdjustedRect().width / 2;
          float maxX = Screen.width - minX;

            float minY = trackerImage.GetPixelAdjustedRect().height/2;
            float maxY = Screen.height - minY;

            Vector2 pos = Camera.main.WorldToScreenPoint(Target.position);

            if (Vector2.Dot((Target.position-transform.position),transform.forward)<0)
            {
                if(pos.x<Screen.width/2)
                {
                    pos.x = maxX;
                }
                else
                {
                    pos.x = minX;
                }
            }


            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            trackerImage.transform.position = pos;

        }
    }
}