using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallrunning : MonoBehaviour
{
    private bool isWallrunning = false;

    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float WallRunForce;
    public float WallClimbSpeed;
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    public float miniWallJumpUpForce;
    public float miniWallJumpSideForce;
    public float maxWallRunTime;
    private float wallRunTimer;
    public Slider wallRunTimeSlider;

    [Header("Input")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode upwardsRunKey = KeyCode.Q;
    public KeyCode downwardsRunKey = KeyCode.E;
    private bool upwardsRunning;
    private bool downwardsRunning;
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header("Exiting")]
    private bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    [Header("Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("References")]
    public Transform orientation;
    //private RigidbodyFirstPersonController fpc;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //fpc = GetComponent<RigidbodyFirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        StateMachine();
        //Debug.Log(rb.velocity.magnitude);
    }

    private void FixedUpdate() {
        if(isWallrunning) {
            WallRunningMovement();
        }
    }

    private void CheckForWall() {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }

    private bool AboveGround() {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine() {
        // Getting Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        upwardsRunning = Input.GetKey(upwardsRunKey);
        downwardsRunning = Input.GetKey(downwardsRunKey);

        // State 1 - Wallrunning
        if((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && !exitingWall) {
            // Perform wallrun
            if(!isWallrunning) {
                Debug.Log("You're wallrunning!");
                StartWallRun();
            }

            // Start wallrun timer
            if(wallRunTimer > 0) {
                wallRunTimer -= Time.deltaTime;
            }

            if(wallRunTimer <= 0 && isWallrunning) {
                //exitingWall = true;
                //exitWallTimer = exitWallTime;
                MiniWallJump();
            }

            DisplayWallRunTimeLeft();

            // Perform walljump
            if(Input.GetKeyDown(jumpKey)) {
                WallJump();
            }
        } else if(exitingWall) {
            if(isWallrunning) {
                StopWallRun();
            }

            if(exitWallTimer > 0) {
                exitWallTimer -= Time.deltaTime;
            }

            if(exitWallTimer <= 0) {
                exitingWall = false;
            }
        } else {
            if(isWallrunning) {
                //StopWallRun();
                MiniWallJump();
            }
        }
    }

    private void StartWallRun() {
        isWallrunning = true;
        wallRunTimeSlider.transform.localScale = new Vector3(1, 1, 1);

        wallRunTimer = maxWallRunTime;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }

    private void WallRunningMovement() {
        rb.useGravity = useGravity;

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude) {
            wallForward = -wallForward;
        }

        // Forward force
        if(rb.velocity.magnitude < 12) {
            rb.AddForce(wallForward * WallRunForce, ForceMode.Force);
        }

        // Up and down force
        if(upwardsRunning) {
            rb.velocity = new Vector3(rb.velocity.x, WallClimbSpeed, rb.velocity.z);
        }

        if(downwardsRunning) {
            rb.velocity = new Vector3(rb.velocity.x, -WallClimbSpeed, rb.velocity.z);
        }

        // Push to wall force
        if(!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0)) {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }

        // Weaken Gravity
        if(useGravity) {
            rb.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
        }
    }

    private void StopWallRun() {
        isWallrunning = false;
        rb.useGravity = true;
        wallRunTimeSlider.transform.localScale = new Vector3(0, 0, 0);
    }

    private void WallJump() {
        // Enter wall exiting state
        exitingWall = true;
        exitWallTimer = exitWallTime;

        // Create forces
        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        // Add Forces
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }

    private void MiniWallJump() {
        // Enter wall exiting state
        exitingWall = true;
        exitWallTimer = exitWallTime;

        // Create forces
        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 forceToApply = transform.up * miniWallJumpUpForce + wallNormal * miniWallJumpSideForce;

        // Add Forces
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }

    private void DisplayWallRunTimeLeft() {
        wallRunTimeSlider.value = wallRunTimer / maxWallRunTime;
    }
}
