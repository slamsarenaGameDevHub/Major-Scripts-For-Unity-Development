using UnityEngine;

namespace Believe.Games.Studios
{
    public class HeadBob : MonoBehaviour
    {
        CharacterController controller;

        [Header("Head bob Variables")]
        [Range(0.001f,1f)]
        [SerializeField] float amount=0.1f;
        [SerializeField] float frequency=10;
        [SerializeField] float smoothTime=0.4f;
        [SerializeField] float walkThreshold = 3;
       
        private void OnEnable()
        {
            controller = GetComponentInParent<CharacterController>();
        }
        private void Update()
        {
            GenerateHeadBob();
        }
        void GenerateHeadBob()
        {
            Vector3 bob = Vector3.zero;

            if(Mathf.Abs(controller.velocity.magnitude)>=walkThreshold)
            {
                bob.x = Mathf.Lerp(bob.x, Mathf.Cos(Time.time * frequency / 2) * amount  * 1.4f, smoothTime * Time.deltaTime);
                bob.y = Mathf.Lerp(bob.y, Mathf.Sin(Time.time * frequency) * amount * 1.6f, smoothTime * Time.deltaTime);
                transform.localPosition = bob;
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero,smoothTime);
            }
        }
    }

}