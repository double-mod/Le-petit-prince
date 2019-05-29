using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerSoundEffect
    {
        RUN,
        JUMP,
        DASH,
        STEP,
        RECOVER,
        ABSORBED,
        NONE,
    }

    // Config
    [SerializeField] bool DashVersion = true;

    [SerializeField] [Range(0f, 20f)] float runSpeed = 5f;
    [SerializeField] [Range(0f, 20f)] float jumpHorizonSpeed = 5f;
    [SerializeField] [Range(0f, 200f)] float airAccel = 50f;
    [SerializeField] [Range(1f, 50f)] float jumpHeight = 10f;
    [SerializeField] [Range(0f, 1f)] float jumpReleaseImpression = 0.4f;
    [SerializeField] [Range(0f, 50f)] float DashSpeed = 15f;
    [SerializeField] [Range(0f, 50f)] float DragSpd = 2.5f;
    [SerializeField] float doubleJumpHeight = 20f;
    [SerializeField] float recoveryPerFrame=25f;
    [SerializeField] AudioClip[] PlayerSE;


    [SerializeField] int energyPoint = 0;
    [SerializeField] int energyPointMax = 3;

    Vector2 aimDir = new Vector2(0f, 0f);
    [SerializeField] Vector2 extraJumpSpeed = new Vector2(0f, 0f);

    // State
    int superMode = 0;
    bool canDash = true;
    bool canAirJump = true;
    bool FlipOn = true;

    [SerializeField] bool infinitDash = false;

    // Cached component references
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    BoxCollider2D myBodyCollider2D;
    CapsuleCollider2D myFeet;
    Light myLight;
    AudioSource myAudioSource;
    // FSMSystem stateMachine;
    SpriteRenderer mySprite;
    Energy energy;

    string state;
    string stateNext;
    string preState;
    float stateTimer;
    bool stateNew;
    Vector2 prevVelocity;

    // Message then methods
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeet = GetComponent<CapsuleCollider2D>();
        myBodyCollider2D = GetComponent<BoxCollider2D>();
        myLight = GetComponentInChildren<Light>();
        mySprite = GetComponent<SpriteRenderer>();
        energy = GetComponent<Energy>();
        myAudioSource = GetComponent<AudioSource>();

        // stateMachine = GetComponent<FSMSystem>();
        // stateMachine.StateCreate("Stand", Stand);
        // stateMachine.StateCreate("Air", Air);
        // stateMachine.StateCreate("Walk", Walk);
        // stateMachine.StateCreate("Dash", Dash);
        // stateMachine.StateCreate("SuperJump", SuperJump);
        //
        // stateMachine.StateInit("Stand");
        state = "Stand";
        preState = state;
    }

    // Update is called once per frame
    void Update()
    {
        prevVelocity = myRigidbody.velocity;
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
        energyAutoRecover();
    }

    private void energyAutoRecover()
    {
        if(!TimeWatch.isNight)
        {
            energy.energyIncrease((int)(recoveryPerFrame * Time.deltaTime));
            //Debug.Log(energy.energyHave());
        }

    }


    private void LateUpdate()
    {
        if (state != stateNext && stateNext != null)
        {
            preState = state;
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
        if (TimeWatch.isNight)
        {
            if(myLight.intensity<500)
            {
                myLight.intensity++;
            }
            if (myLight.spotAngle > 40f + superMode * 40f)
            {
                myLight.spotAngle -= 50f * Time.deltaTime;
            }
            if (myLight.spotAngle < 40f + superMode * 39f)
            {
                myLight.spotAngle += 80f * Time.deltaTime;
            }
        }
        else
        {
            if(myLight.intensity>200)
            {
                myLight.intensity-=2;
            }
        }
    }

    private void Stand()
    {
        if (stateNew)
        {
            if (stateNew)
            {
                myAnimator.SetBool("Idling", true);
                playSound(PlayerSoundEffect.NONE);
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
        
        if (DashVersion)
        {
            if (CheckDash())
            {
                myAnimator.SetBool("Idling", false);
            }
        }
        if (CheckJumpAndFall())
        {
            playSound(PlayerSoundEffect.JUMP);
            myAnimator.SetBool("Idling", false);
        }
    }
    

    private void Walk()
    {
        if (stateNew)
        {
            myAnimator.SetBool("Running", true);
            playSound(PlayerSoundEffect.RUN);
        }

        if (FlipOn == false)
            runSpeed = DragSpd;
        else
            runSpeed = 7.8f;

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
        if (DashVersion)
        {
            if (CheckDash())
            {
                myAnimator.SetBool("Running", false);
            }
        }
        if (CheckJumpAndFall())
        {
            playSound(PlayerSoundEffect.JUMP);
            myAnimator.SetBool("Running", false);
        }
    }

    private void Air()
    {
        if (stateNew)
        {
            myAnimator.SetBool("Jumping", true);
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
            if (!Input.GetButton("Jump") || preState == "Dash")
            {
                playerVelocity.y *= jumpReleaseImpression;
            }
        }
        myRigidbody.velocity = playerVelocity;

        if(CheckLanding())
        {
            myAnimator.SetBool("Jumping", false);
        }
        if (DashVersion)
        {
            if(CheckDash())
            {
                myAnimator.SetBool("Jumping", false);
            }
        }
        else
        {
            DoubleJump();
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

    private void DoubleJump()
    {
        if (!canAirJump && !infinitDash) return;
        if (myRigidbody.velocity.y < 3.0f && state == "Air")
        {
            if (Input.GetButtonDown("Jump") && energy.energyUse())
            {
                this.GetComponent<StarDust>().boost();
                myLight.spotAngle = 150f;
                Vector2 jumpVelocity = new Vector2(myRigidbody.velocity.x, doubleJumpHeight);
                myRigidbody.velocity = jumpVelocity;
                canAirJump = false;
            }
        }
      
    }

    private bool CheckDash()
    {
        if (!canDash && !infinitDash)
        {
            return false;
        }

        if (Input.GetButtonDown("Dash") && energy.energyUse())
        {
            playSound(PlayerSoundEffect.DASH);

            this.GetComponent<StarDust>().boost();
            canDash = false;
            float controlH = Input.GetAxis("Horizontal"); // -1 ~ +1
            float controlV = Input.GetAxis("Vertical"); // -1 ~ +1
                
            aimDir.x = Mathf.Abs(controlH) > 0.35f ? Mathf.Sign(controlH) : 0f;
            aimDir.y = Mathf.Abs(controlV) > 0.35f ? Mathf.Sign(controlV) : 0f;
            if (aimDir == new Vector2(0,0))
            {
                aimDir.x = transform.localScale.x;
            }

            // energyPoint--;
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
            playSound(PlayerSoundEffect.STEP);
            canDash = true;
            canAirJump = true;
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
        if (state != "Walk")
            SetFlipStat(true);
        if (PlayerHasHorizontalSpeed()&&FlipOn)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x)* Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    private void playSound(PlayerSoundEffect sound)
    {
        myAudioSource.Stop();
        if(sound!=PlayerSoundEffect.NONE)   myAudioSource.PlayOneShot(PlayerSE[(int)sound]);
    }

    public void SetFlipStat(bool stat)
    {
        FlipOn = stat;
    }

    public Vector2 GetPrevVelocity()
    {
        return prevVelocity;
    }

    public string getState()
    {
        return state;
    }

    public void InfinitDash(bool value)
    {
        infinitDash = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GetComponent<EventSystem>().getEventTYpe()==EventSystem.eventType.STARRYLIGHTB)
        {
            GetComponent<Energy>().setDashInStarry(false);
        }
    }
}
