using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Components")]
   CharacterController characterController;
    Animator animator;

    [Header("Animation Variables")]
    [SerializeField] float walkThreshold=2.4f;
    float speed;
    Vector3 lastPos;
    private void OnEnable()
    {
        characterController = GetComponent<CharacterController>();
        animator= GetComponent<Animator>();
        lastPos = transform.position;
    }
    private void Update()
    {
        GetSpeed();
        PlayAnimation();
    }
    void PlayAnimation()
    {
        if (animator == null) return;
        if (speed > walkThreshold && isGrounded)
        {
            animator.SetFloat("Motion", 2);
        }
        else if (speed > 0.1f && speed < walkThreshold && isGrounded)
        {
            animator.SetFloat("Motion", 1);
        }
        else
        {
            animator.SetFloat("Motion", 0);
        }
    }
    void GetSpeed()
    {
        speed=Vector3.Distance(transform.position, lastPos)/Time.deltaTime;
        lastPos= transform.position;
    }
    bool isGrounded => characterController.isGrounded;
}
