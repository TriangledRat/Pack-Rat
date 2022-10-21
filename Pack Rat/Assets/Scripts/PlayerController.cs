using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed, climbSpeed;
    [SerializeField] private float jumpHeight, gravMultiplier;
    [SerializeField] private float cableSpeed;
    [SerializeField] float rotationSmoothTime;
    [SerializeField] private float offset;
    [SerializeField] private float fallingChecker = 1f;
    [SerializeField] private float dazeCooldown = 5f;
    [SerializeField] private float throwMagnitude = 100f;
    private CharacterController controller;
    private Camera cam;
    private bool groundedPlayer, itemRange, holdingItem, climbingRange, climbing;
    private float fallingTimer;
    private float normalSpeed, slowSpeed;
    private Vector3 velocity;
    private float gravity = Physics.gravity.y;
    float currentAngle;
    float currentAngleVelocity;
    float keyyield = 1f;
    private ItemChecker checker;
    private UIText uitext;
    RaycastHit hit;

    private GameObject pickedUpObject, climbedObject;
    public Transform holdingLocation;

    private string labelText;
    [SerializeField] float posX, posY;
    GUIStyle style;




    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        checker = GetComponentInChildren<ItemChecker>();
        uitext = GetComponent<UIText>();
        cam = Camera.main;
        normalSpeed = movementSpeed;
        slowSpeed = normalSpeed / 2;
        style = new GUIStyle();
        style.fontSize = 15;
        style.normal.textColor = Color.black;
        style.alignment = TextAnchor.MiddleCenter;
        

    }

    private void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, fwd, out hit, offset))
            groundedPlayer = true;            
        else
        {
            groundedPlayer = false;
        }

        if(hit.transform.tag == "Cablesurface")
        {
            movementSpeed = cableSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

    }

    // Update is called once per frame
    void Update()
    {


        //UnityEngine.Debug.Log(movementSpeed);
        if (climbingRange && Input.GetKey(KeyCode.E))
        {
            climbing = true;            
        }

        if (climbing && Input.GetKey(KeyCode.Space))
        {
            climbing = false;
        }

        if (holdingItem == true)
        {
            movementSpeed = slowSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        //gravity handling and jump
        velocity.y += gravity * gravMultiplier * Time.deltaTime;
        if (groundedPlayer && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        controller.Move(velocity * Time.deltaTime);



        //movement and rotation matching camera movement
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if (movement.magnitude >= 0.1f && climbing)
        {
            gravity = 0f;
            Vector3 climbMovement = new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
            controller.Move(climbMovement * climbSpeed * Time.deltaTime);
            
        }

        if (climbing && Input.GetKey(KeyCode.Space))
        {
            gravity = Physics.gravity.y;
            controller.Move(new Vector3(0, 0, -1));
            labelText = "";
        }

        if (movement.magnitude >= 0.1f && !climbing)
        {
            gravity = Physics.gravity.y;
            //compute rotation
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);

            //move in direction of rotation
            Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(rotatedMovement * movementSpeed * Time.deltaTime);
        }
        FallingCheck();
        CarryingItem();

        if (climbing)
        {
            labelText = "W to climb, S to descend, Space to release";
        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "ClimbingObject")
        {
            climbingRange = true;
            labelText = "Press E to climb";
            climbedObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ClimbingObject")
        {
            climbingRange = false;
            climbing = false;
            climbedObject = null;
            labelText = "";
        }
    }



    private void FallingCheck()
    {
        //checking fall time for daze and applying speed modifiers
        if (!groundedPlayer && !climbing)
        {
            fallingTimer += Time.deltaTime;
        }

        if (groundedPlayer && fallingTimer < fallingChecker)
        {
            fallingTimer = 0f;
        }

        if (groundedPlayer && fallingTimer > fallingChecker)
        {
            dazeCooldown -= Time.deltaTime;
            movementSpeed = slowSpeed;
        }

        if (dazeCooldown < 0f)
        {
            movementSpeed = normalSpeed;
            fallingTimer = 0f;
            dazeCooldown = 5f;
        }
    }

    private void CarryingItem()
    {
        if(checker.inRange && Input.GetMouseButtonDown(0))
        {
            //add a checker component and disable the component that is the trigger detection for this specific instance? will that stop overriding the other input?
            pickedUpObject = checker.itemInRange;
            checker.enabled = false;
            pickedUpObject.gameObject.GetComponent<Rigidbody>().useGravity = false;
            pickedUpObject.transform.position = holdingLocation.transform.position;
            pickedUpObject.transform.parent = GameObject.Find("HoldingLocation").transform;
            pickedUpObject.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            labelText = "Release LeftMouse to drop, RightMouse to throw";
            holdingItem = true;            
        }
        else if (holdingItem && Input.GetMouseButton(1))
        {
            pickedUpObject.transform.parent = null;
            pickedUpObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwMagnitude);
            pickedUpObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
            pickedUpObject = null;
            holdingItem = false;
            labelText = "";
            checker.enabled = true;
        }
        if (holdingItem && Input.GetMouseButtonUp(0))
        {            
            pickedUpObject.transform.parent = null;
            pickedUpObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
            pickedUpObject = null;
            holdingItem = false;
            labelText = "";
            checker.enabled = true;
        }

    }

    private void OnGUI()
    {
        Rect rect = new Rect(0, 403, Screen.width, 120);
        GUI.Label(rect, labelText, style);
        
    }

}
