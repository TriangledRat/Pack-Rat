using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float cableSpeed;
    [SerializeField] float rotationSmoothTime;
    [SerializeField] private float offset;
    [SerializeField] private float fallingChecker = 1f;
    [SerializeField] private float dazeCooldown = 5f;
    private CharacterController controller;
    private Camera cam;
    private bool groundedPlayer;
    private float fallingTimer;
    private float normalSpeed, slowSpeed;
    private Vector3 velocity;
    private float gravity = Physics.gravity.y;
    float currentAngle;
    float currentAngleVelocity;
    RaycastHit hit;

   

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        normalSpeed = movementSpeed;
        slowSpeed = normalSpeed / 2;

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
        Debug.Log(movementSpeed);
        //gravity handling and jump
        velocity.y += gravity *1.5f * Time.deltaTime;
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

        if (movement.magnitude >= 0.1f)
        {
            //compute rotation
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);

            //move in direction of rotation
            Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(rotatedMovement * movementSpeed * Time.deltaTime);
        }
        FallingCheck();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundedPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundedPlayer = false;
        }
    }

    private void FallingCheck()
    {
        //checking fall time for daze and applying speed modifiers
        if (!groundedPlayer)
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
}
