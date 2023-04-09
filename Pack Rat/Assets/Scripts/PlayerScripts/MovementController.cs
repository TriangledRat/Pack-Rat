using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Audio;

public class MovementController : MonoBehaviour
{
    CharacterController cc;
    Animator animator;
    [SerializeField] public float moveSpeed, jumpForce, climbingSpeed, throwMagnitude;
    [SerializeField] Transform holdingLocation;
    [SerializeField] GameObject grabAudio, pickupInRangeAudio, climbingInRangeAudio, climbingAudio, jumpAudio, throwAudio;
    float moveY, carryingSpeed, dazedSpeed, cableSpeed, jumpSpeed;
    public float storeSpeed;
    int frameDelay = 3;
    float gravity, fallingTimer, dazeTimer;
    private Vector3 moveDirection;
    private GameObject itemToCarry, carriedItem;
    RaycastHit hit;
    AudioSource pickUpAudio, pickupRangeAudio, climbRangeAudio, climbAudio, jumpingAudio, throwingAudio;

    private bool carryingItem, itemInRange, climbing, climbObjectInRange, dazed, cable;

    // Start is called before the first frame update
    void Start()
    {
        pickUpAudio = grabAudio.GetComponent<AudioSource>();
        pickupRangeAudio = pickupInRangeAudio.GetComponent<AudioSource>();
        climbRangeAudio = climbingInRangeAudio.GetComponent<AudioSource>();
        climbAudio = climbingAudio.GetComponent<AudioSource>();
        jumpingAudio = jumpAudio.GetComponent<AudioSource>();
        throwingAudio = throwAudio.GetComponent<AudioSource>();
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        dazedSpeed = moveSpeed / 4;
        carryingSpeed = moveSpeed / 2;
        cableSpeed = moveSpeed / 2;
        storeSpeed = moveSpeed;
        jumpSpeed = moveSpeed - 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale > 0)
        {
            DazeHandler();
            CableChecker();
            if (climbObjectInRange && Input.GetButtonDown("Jump") && !dazed)
            {
                climbAudio.Play();
                climbing = true;
            }

            if (climbing)
            {
                Climbing();
            }
            else
            {
                Running();
            }

            AnimationController();
            PickUp();

            if (climbing)
            {
                if (frameDelay > 0)
                {
                    frameDelay--;
                }
            }
        }
        
    }

    public void Running()
    {
        animator.SetBool("Climbing", false);
        gravity = 25.81f;


        if (!cc.isGrounded)
        {
            moveSpeed = jumpSpeed;
        }
        else if (carryingItem)
        {
            moveSpeed = carryingSpeed;
        }
        else if (dazed)
        {
            moveSpeed = dazedSpeed;
        }
        else if (cable)
        {
            moveSpeed = cableSpeed;
        }
        else
        {
            moveSpeed = storeSpeed;
        }


        moveY = moveDirection.y;
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), moveY, Input.GetAxisRaw("Vertical")) * moveSpeed;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = moveY;

        if (cc.isGrounded && Input.GetButtonDown("Jump") && !dazed && !carryingItem)
        {
            jumpingAudio.Play();
            animator.Play("Jump");
            moveDirection.y = jumpForce;           
                    
        }

        else if (!cc.isGrounded)
        {
            fallingTimer += Time.deltaTime;
            moveDirection.y -= (gravity * Time.deltaTime);
        }
        cc.Move(moveDirection * Time.deltaTime);
    }

    void Climbing()
    {
        animator.SetBool("Running", false);
        gravity = 0f;
        Vector3 climbMovement = new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
        if (climbMovement.magnitude >= .1f)
        {
            animator.SetBool("Climbing", true);
        }
        else
        {
            animator.SetBool("Climbing", false);
        }

        if (Input.GetButtonDown("Jump") && climbing && frameDelay <= 0)
        {
            animator.SetBool("Climbing", false);
            cc.Move(Vector3.back * 2);
            frameDelay = 3;
            climbing = false;
        }

        cc.Move(climbMovement * climbingSpeed * Time.deltaTime);

    }

    void PickUp()
    {
        if (itemToCarry != null && Input.GetMouseButtonDown(0) && !carryingItem && itemInRange)
        {
            pickUpAudio.Play();
            carriedItem = itemToCarry;
            carriedItem.transform.position = holdingLocation.position;
            carriedItem.GetComponent<Rigidbody>().isKinematic = true;
            carriedItem.transform.parent = holdingLocation.transform;
            carryingItem = true;
        }
        if (carryingItem && Input.GetMouseButtonUp(0))
        { 
            carriedItem.transform.parent = null;
            carriedItem.GetComponent<Rigidbody>().isKinematic = false;
            carriedItem = null;
            carryingItem = false;
        }

        else if(carryingItem && Input.GetMouseButtonDown(1))
        {
            throwingAudio.Play();
            carriedItem.transform.parent = null;
            carriedItem.GetComponent<Rigidbody>().isKinematic = false;
            carriedItem.GetComponent<Rigidbody>().AddForce(transform.forward * throwMagnitude);
            carriedItem = null;
            carryingItem = false;
        }
    }


    public void AnimationController()
    {
        if (moveDirection.x != 0 || moveDirection.z != 0 && !carryingItem && !climbing)
        {
            animator.SetBool("Running", true);
        }
        else if (moveDirection.x != 0 || moveDirection.z != 0 && carryingItem)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
        if (!carryingItem)
        {
            animator.SetBool("Walking", false);
        }

        if (dazed)
        {
            animator.SetBool("Dazed", true);
        }
        else
        {
            animator.SetBool("Dazed", false);
        }
    }

    private void DazeHandler()
    {
        if (cc.isGrounded)
        {
            if(fallingTimer >= 2f)
            {
                dazed = true;
                dazeTimer = 5;
                fallingTimer = 0;
            }
            else
            {
                fallingTimer = 0;
            }
        }

        if(dazeTimer > 0)
        {
            dazeTimer -= Time.deltaTime;
        }
        else if (dazeTimer <= 0)
        {
            dazed = false;
        }
    }

    private void CableChecker()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(transform.position, fwd, out hit, 1f))
        {
            if(hit.transform.tag == "Cablesurface")
            {
                cable = true;
            }
            else
            {
                cable = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("PickUpObject") && !carryingItem)
        {
            pickupRangeAudio.Play();
            itemToCarry = other.gameObject;
            itemInRange = true;
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("ClimbObject") && !climbing)
        {
            climbRangeAudio.Play();
            climbObjectInRange = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PickUpObject"))
        {
            itemToCarry = null;
            itemInRange = false;

        }
        if (other.gameObject.layer == LayerMask.NameToLayer("ClimbObject"))
        {
            climbObjectInRange = false;
            climbing = false;
            frameDelay = 3;
        }
    }


}
