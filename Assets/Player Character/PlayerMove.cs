using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    enum PlayerState { Idle, Running, Airborne}
    PlayerState state;
    bool stateComplete;
    
    public float acceleration; 
    public float groundSpeed;
    public float jumpSpeed;
    [Range(0f, 1f)] 
    public float groundDecay;

    float xInput; 
    float yInput;

    public bool grounded;

    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public Animator animator; 
    void Start()
    {
        
    }

    void Update()
    {
        GetInput();
        HandleJump();

        if (stateComplete)
        {
            SelectState();
        }
        UpdateState();
    }

    void FixedUpdate()
    {
        CheckGround();
        ApplyFriction();
        MoveWithInput();
    }

    void SelectState()
    {
        stateComplete = false;

        if(grounded)
        {
            if(xInput == 0)
            {
                state = PlayerState.Idle;
                StartIdle();
            }
            else
            {
                state = PlayerState.Running;
                StartRunning();

            }
        }
        else
        {
            state = PlayerState.Airborne;
            StartAirborne();
        }
    }

    void UpdateState()
    {
        switch(state)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Running:
                UpdateRun();
                break;
            case PlayerState.Airborne:
                UpdateAirborne();
                break;
        }
    }

        void StartIdle()
        {
            animator.Play("Idle");
        }
        void StartRunning()
        {
            animator.Play("run");
        }
        void StartAirborne()
        {
            animator.Play("Jump");
        }

        void UpdateIdle()
        {
            if(xInput != 0)
            {
                stateComplete = true;
            }
        }
        void UpdateRun()
        {
            if(!grounded || xInput == 0)
            {
                stateComplete = true;
            }
        }
        void UpdateAirborne()
        {
            if(grounded)
            {
                stateComplete = true;
            }
        }

    void GetInput()
    {
       xInput = Input.GetAxis("Horizontal");
       yInput = Input.GetAxis("Vertical");
    }

    void MoveWithInput()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(body.velocity.x + increment, -groundSpeed, groundSpeed); 
            body.velocity = new Vector2(newSpeed, body.velocity.y);

            FaceDirectionInput();
        }

    }

    void FaceDirectionInput()
    {
        // change the player facing direction
        float direction = Mathf.Sign(xInput);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }

    void ApplyFriction()
    {
        if (grounded && xInput == 0 && body.velocity.y == 0)
        {
            body.velocity *= groundDecay;
        }
    }
}
