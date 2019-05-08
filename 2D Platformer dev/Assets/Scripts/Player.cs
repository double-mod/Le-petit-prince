using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] [Range(0f, 20f)] float runSpeed = 5f;
    [SerializeField] [Range(0f, 20f)] float jumpHorizonSpeed = 5f;
    [SerializeField] [Range(0f, 200f)] float airAccel = 50f;
    [SerializeField] [Range(1f, 50f)] float jumpHeight = 10f;
    [SerializeField] [Range(0f, 1f)] float jumpReleaseImpression = 0.4f;
    [SerializeField] [Range(0f, 50f)] float DashSpeed = 15f;


    [SerializeField] int energyPoint = 0;
    [SerializeField] int energyPointMax = 3;

    Vector2 aimDir = new Vector2(0f, 0f);
    [SerializeField] Vector2 extraJumpSpeed = new Vector2(0f, 0f);

    // State
    int superMode = 0;

    // Cached component references
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeet;
    Light myLight;
    // FSMSystem stateMachine;
    SpriteRenderer mySprite;

    string state;
    string stateNext;
    float stateTimer;
    bool stateNew;

    // Message then methods
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
        myLight = GetComponentInChildren<Light>();
        mySprite = GetComponent<SpriteRenderer>();

        // stateMachine = GetComponent<FSMSystem>();
        // stateMachine.StateCreate("Stand", Stand);
        // stateMachine.StateCreate("Air", Air);
        // stateMachine.StateCreate("Walk", Walk);
        // stateMachine.StateCreate("Dash", Dash);
        // stateMachine.StateCreate("SuperJump", SuperJump);
        //
        // stateMachine.StateInit("Stand");
        state = "Stand";
    }

    // Update is called once per frame
    void Update()
    {

       // stateMachine.StateExecute();

        switch(state)
        {
            case "Stand":
                Stand();
                break;
            case "Air":
                Air();
                break;
            case "Walk":
                Walk();
                break;
            case "Dash":
                Dash();
                break;
            case "SuperJump":
                SuperJump();
                break;

        }
        CheckMode();
        CheckLight();

    }

    private void LateUpdate()
    {
        if (state != stateNext && stateNext != null)
        {
            state = stateNext;
            stateTimer = 0f;
            stateNew = true;
        }
        else
        {
            stateTimer += Time.deltaTime;
            stateNew = false;
        }
    }

    private void CheckMode()
    {
        if (energyPoint >= energyPointMax)
        {
            superMode = 1;
            mySprite.color = new Color(255,255,255);
        }
        if (energyPoint == 0)
        {
            superMode = 0;
            mySprite.color = new Color(0, 255, 255);
        }
    }

    private void CheckLight()
    {
        if (myLight.spotAngle > 40f + superMode * 40f)
        {
            myLight.spotAngle -= 50f * Time.deltaTime;
        }
        if (myLight.spotAngle < 40f + superMode * 39f)
        {
            myLight.spotAngle += 80f * Time.deltaTime;
        }
    }

    private void Stand()
    {
        if (stateNew)
        {
            if (stateNew)
            {
                myAnimator.SetBool("Idling", true);
            }
        }
        

        float controlThrow = Input.GetAxis("Horizontal"); 
        bool hasHorizonControl = Mathf.Abs(controlThrow) > Mathf.Epsilon;
        if (hasHorizonControl)
        {
            stateNext = "Walk";
            myAnimator.SetBool("Idling", false);

            Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;
        }
        
        if (CheckJumpAndFall() || CheckDash())
        {
            myAnimator.SetBool("Idling", false);
        }
    }
    

    private void Walk()
    {
        if (stateNew)
        {
            myAnimator.SetBool("Running", true);
        }


        float controlThrow = Input.GetAxis("Horizontal"); // -1 ~ +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        
        FlipSprite();
        
        if (!PlayerHasHorizontalSpeed())
        {

            stateNext = "Stand";
            myAnimator.SetBool("Running", false);
        }

        // check dejavu
        bool hasKeepDejavu = Mathf.Sign(myRigidbody.velocity.x) == Mathf.Sign(controlThrow);
        if (hasKeepDejavu)
        {
            // do something...
        }
        else
        {
            // slide...
        }
        if (CheckJumpAndFall() || CheckDash())
        {
            myAnimator.SetBool("Running", false);
        }
    }

    private void Air()
    {
        if (stateNew)
        {
            myAnimator.SetBool("Running", true);
        }
        
        float controlThrow = Input.GetAxis("Horizontal"); // -1 ~ +1
        float xSpeed = 0f;
        if (Mathf.Abs(controlThrow) > Mathf.Epsilon)
        {
            xSpeed = controlThrow * airAccel * Time.deltaTime + controlThrow * Mathf.Abs(myRigidbody.velocity.x);
            xSpeed = Mathf.Clamp(xSpeed, -jumpHorizonSpeed, jumpHorizonSpeed);
        }
        Vector2 playerVelocity = new Vector2(xSpeed, myRigidbody.velocity.y);

        if (myRigidbody.velocity.y > 10f)
        {
            if (!Input.GetButton("Jump"))
            {
                playerVelocity.y *= jumpReleaseImpression;
            }
        }
        myRigidbody.velocity = playerVelocity;


        if(CheckLanding() || CheckDash())
        {
            myAnimator.SetBool("Running", false);
        }
        FlipSprite();

    }

    private bool PlayerHasHorizontalSpeed()
    {
        return Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    }

    private bool CheckJumpAndFall()
    {
        if (Input.GetButtonDown("Jump"))
        {

            stateNext = "Air";
            Vector2 jumpVelocityToAdd = new Vector2(myRigidbody.velocity.x, jumpHeight);
            myRigidbody.velocity = jumpVelocityToAdd;
            return true;
        }

        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))
            && !myFeet.IsTouchingLayers(LayerMask.GetMask("unseen"))
            && !myFeet.IsTouchingLayers(LayerMask.GetMask("untouchable")))
        {
            stateNext = "Air";
            return true;
        }

        return false;
    }

    //private void DoubleJump()
    //{
    //    if (energyPoint >= 1 && myRigidbody.velocity.y < 3.0f && stateMachine.StateName == "Air")
    //    {
    //        if (Input.GetButtonDown("Jump"))
    //        {
    //            energyPoint--;
    //            myLight.spotAngle = 150f;
    //            Vector2 jumpVelocity = new Vector2(myRigidbody.velocity.x, doubleJumpHeight);
    //            myRigidbody.velocity = jumpVelocity;
    //        }
    //    }
        
    //}

    private bool CheckDash()
    {
        if (Input.GetButtonDown("Dash"))
        {
            float controlH = Input.GetAxis("Horizontal"); // -1 ~ +1
            float controlV = Input.GetAxis("Vertical"); // -1 ~ +1
                
            aimDir.x = Mathf.Abs(controlH) > 0.35f ? Mathf.Sign(controlH) : 0f;
            aimDir.y = Mathf.Abs(controlV) > 0.35f ? Mathf.Sign(controlV) : 0f;
            if (aimDir == new Vector2(0,0))
            {
                aimDir.x = transform.localScale.x;
            }

            energyPoint--;
            myLight.spotAngle = 190f;
            stateNext = "Dash";
            return true;
        }
        return false;
    }

    

    private void Dash()
    {

        Vector2 dashVelocity = aimDir.normalized * DashSpeed;
        if (Input.GetButtonDown("Jump") && (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("unseen"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("untouchable"))))
        {
            stateNext = "SuperJump";
            dashVelocity.y = jumpHeight * 0.7f;
        }
        myRigidbody.velocity = dashVelocity;

        if (stateTimer >= 0.15f)
        {
            CheckLanding();
        }
    }
    

    private void SuperJump()
    {
        if (stateNew)
        {
            myAnimator.SetBool("Jumping", true);
        }

        float controlThrow = Input.GetAxis("Horizontal"); // -1 ~ +1
        float xSpeed = 0f;
        xSpeed = controlThrow * airAccel * Time.deltaTime + myRigidbody.velocity.x;
        xSpeed = Mathf.Clamp(xSpeed, -DashSpeed * 0.85f, DashSpeed * 0.85f);
        
        Vector2 playerVelocity = new Vector2(xSpeed, myRigidbody.velocity.y);
        
        myRigidbody.velocity = playerVelocity;


        if (stateTimer > 0.2f && myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("unseen"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("untouchable"))) // check landing
        {
            if (PlayerHasHorizontalSpeed())
            {
                stateNext = "Walk";
            }
            else
            {
                stateNext = "Stand";
            }
            myAnimator.SetBool("Jumping", false);
        }
        FlipSprite();
    }

    private bool CheckLanding()
    {
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))
             || myFeet.IsTouchingLayers(LayerMask.GetMask("unseen"))
            || myFeet.IsTouchingLayers(LayerMask.GetMask("untouchable"))) // check landing
        {
            if (PlayerHasHorizontalSpeed())
            {
                stateNext = "Walk";
                return true;
            }
            else
            {
                stateNext = "Stand";
                return true;
            }
        }
        else
        {
            stateNext = "Air";
            return false;
        }
    }

    private void FlipSprite()
    {
        if (PlayerHasHorizontalSpeed())
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), transform.localScale.y);
        }
    }
}
