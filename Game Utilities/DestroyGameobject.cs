using UnityEngine;

public class DestroyGameobject : MonoBehaviour
{
    internal enum DestroyType
    {
        Distance,
        Timed
    }
    [SerializeField] DestroyType destroyType;
    internal enum Target
    {
        Player,
        Other
    }
    [SerializeField] Target target;
    public Transform TargetObject;

    [SerializeField] float maxDistance = 50;
    [SerializeField] float destroyDelay = 1;
    private void OnEnable()
    {
        if(target == Target.Player)
        {
            TargetObject = GameObject.FindWithTag("Player").transform;
        }
    }
    private void Update()
    {
        switch (destroyType)
        {
            case DestroyType.Distance:
                if (TargetObject == null) return;
                if(Vector3.Distance(transform.position,TargetObject.position) >=maxDistance)
                {
                    Destroy(gameObject);
                }
                break;
                case DestroyType.Timed:
                Destroy(gameObject, destroyDelay);
                break;
        }
    }
}
