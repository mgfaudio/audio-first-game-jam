using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float playerVel = 10f;
    public float gravity = 9.81f;
    public float rotateSpeed = 50f;
    public float rotateLimit = 50f;
    public float jumpForce = 3f;
    public float groundDist = 0.1f;
    public float yVelocity = 0f;

    //public LayerMask groundMask;
    //public BoxCollider playerCol;
    //public Transform groundCheck;
    //public Transform rifleParent;
    public Camera playerCamera;

    //private bool isGrounded = true;
    //private float currentPitch = 0;
    private float xAxisClamp = 0;

    // Start is called before the first frame update
    void Start()
    {
        //playerCol = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        MovePlayer();
        RotatePlayer();
        //GravityPull();
        PitchCamera();
    }

    void MovePlayer()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            transform.position += transform.right * Time.deltaTime * playerVel * Input.GetAxis("Horizontal");
        }

        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            transform.position += transform.forward * Time.deltaTime * playerVel * Input.GetAxis("Vertical");
        }

        if (Input.GetKeyDown("space"))
        {
            yVelocity = Mathf.Sqrt(jumpForce * -2 * -gravity);
        }

        transform.position += transform.up * yVelocity * Time.deltaTime;
    }

    void RotatePlayer()
    {
        float yaw = rotateSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
        transform.Rotate(0, yaw, 0);
    }

    /*void GravityPull()
    {
        if (!IsGrounded())
        {
            yVelocity += -gravity * Time.deltaTime;
        }
        else if (IsGrounded())
        {
            yVelocity = 0f;
        }
    }*/

    void PitchCamera()
    {
        float cameraPitch = 1f * Input.GetAxis("Mouse Y");

        Vector3 cameraRotation = playerCamera.transform.rotation.eulerAngles;
        //Vector3 rifleRotation = rifleParent.transform.rotation.eulerAngles;

        cameraRotation.x -= cameraPitch;
        //rifleRotation.x -= cameraPitch;
        xAxisClamp -= cameraPitch;

        if (xAxisClamp > 70)
        {
            xAxisClamp = 70;
            cameraRotation.x = 70;
            //rifleRotation.x = 70;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            cameraRotation.x = -90;
            //rifleRotation.x = -90;
        }

        playerCamera.transform.rotation = Quaternion.Euler(cameraRotation);
        //rifleParent.transform.rotation = Quaternion.Euler(rifleRotation);
    }

    /*private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
    }*/
}
