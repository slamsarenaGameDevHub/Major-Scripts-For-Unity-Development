using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;




[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 2.0f;

    [Tooltip("Sprint speed of the character in m/s")]
    public float SprintSpeed = 5.335f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -15.0f;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;
    bool wasGroundedLastFrame=true;

    [Header("Camera Control")]
    [SerializeField] Transform camTransform;
    [SerializeField] float camLookAngle = 60;
    [SerializeField] float camSensitivity =5;
    float pitch;





    // player
    private float _speed;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;




    private CharacterController _controller;
    private GameObject _mainCamera;


    [Header("FootStep Clip")]
    [SerializeField] List<AudioClip> footstepClips;
    [SerializeField] AudioClip landClip;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioSource footstepSource;
    [SerializeField] AudioSource jumpSource;
    [SerializeField] float walkPlayRate = 0.55f;
    [SerializeField] float runPlayRate = 0.25f;
    float nextTimeToPlay,nextCheckTime;
    int currentFootstepClipIndex = 0;

    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main.transform.gameObject;
        }
    }

    private void OnEnable()
    {

        _controller = GetComponent<CharacterController>();

        // reset our timeouts on start
        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;
    }

    private void Update()
    {
        GroundedCheck();
    }

    private void FixedUpdate()
    {
        wasGroundedLastFrame = _controller.isGrounded;
    }
    private void GroundedCheck()
    {
        Grounded = _controller.isGrounded;
    }
    private void LateUpdate()
    {
        if (!wasGroundedLastFrame && Grounded)
        {
            jumpSource.clip = landClip;
            jumpSource.Play();
        }
    }

    public void Move(Vector2 input, bool isRun, bool isJump,Vector2 lookAround)
    {
        float targetSpeed = isRun ? SprintSpeed : MoveSpeed;

        
        if (input == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = input.magnitude >= 0.1f ? input.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }


        // normalise input direction
        Vector3 targetDirection = new Vector3(input.x, 0.0f, input.y).normalized;
        targetDirection = transform.TransformDirection(targetDirection);

        



        // move the player
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        LookAround(lookAround);
        JumpAndGravity(isJump);

        //Play Footstep Sound
        if(!wasGroundedLastFrame && Grounded)
        {
            print("Landed");
        }
        else if(_controller.velocity.magnitude>MoveSpeed && Grounded)
        {
            if(Time.time>=nextTimeToPlay)
            {
                nextTimeToPlay = Time.time + runPlayRate;
                PlayFootstepSound();
            }
        }
        else if(_controller.velocity.magnitude>=0.1f && _controller.velocity.magnitude<=MoveSpeed && Grounded)
        {
            if (Time.time >= nextTimeToPlay)
            {
                nextTimeToPlay = Time.time + walkPlayRate;
                PlayFootstepSound();
            }
        }
        
    }
    void LookAround(Vector2 look)
    {
        float xLook = look.x*camSensitivity*Time.deltaTime;
        transform.Rotate(xLook*Vector3.up);

        if (camTransform == null) return;
        float yLook=look.y*camSensitivity*Time.deltaTime;
        pitch -= yLook;
        pitch=Mathf.Clamp(pitch,-camLookAngle,camLookAngle);
        camTransform.localRotation=Quaternion.Euler(pitch,0f,0f);
    }

    private void JumpAndGravity(bool jump)
    {
        if (Grounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;


            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump
            if (jump && _jumpTimeoutDelta <= 0.0f)
            {
                jumpSource.clip = jumpClip;
                jumpSource.Play();
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);


            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }
    void PlayFootstepSound()
    {
        for (int i = 0; i < footstepClips.Count; i++)
        {
            footstepSource.clip = footstepClips[currentFootstepClipIndex];
            footstepSource.Play();
            if(currentFootstepClipIndex>=footstepClips.Count-1)
            {
                currentFootstepClipIndex = 0;
            }
            else
            {
                currentFootstepClipIndex++;
            }
        }
    }
}
