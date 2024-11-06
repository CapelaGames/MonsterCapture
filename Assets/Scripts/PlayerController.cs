using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    public float gravity;

    private bool isGrounded;
    private bool hasCoyoted = false;
    private float lastGroundedTime = float.NegativeInfinity;
    private float jumpInputTime = float.NegativeInfinity;

    public Rigidbody rb;
    public LayerMask groundMask;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Jump();

    }

    private void FixedUpdate()
    {
        Movement();

    }

    Vector3 currentVelocity;

    private void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical"));
        if(input.magnitude > 1) 
        {
            input.Normalize();
        }
        input *= speed * Time.deltaTime;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, new Vector3(input.x, rb.velocity.y,input.z),
                                        ref currentVelocity, 0.1f);

    }

//private bool hasCoyoted = false;
private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpInputTime = Time.time;
        }

        if(isGrounded || !hasCoyoted && (Time.time - lastGroundedTime) < 0.5f)
        {
            if((Time.time - jumpInputTime) < 0.5f)
            {
                hasCoyoted = true;
                lastGroundedTime = float.NegativeInfinity;
                jumpInputTime = float.NegativeInfinity;
                rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hasCoyoted = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        int goLayer =  1 << collision.gameObject.layer;

        if ((groundMask & goLayer) != 0)
        {
            //Debug.Log("Grounded");

            isGrounded = true;
            lastGroundedTime = Time.time;
        }
        else
        {
            isGrounded = false;
        }
    }
 
    private void OnCollisionExit(Collision collision)
    {
        int goLayer = 1 << collision.gameObject.layer;

        if ((groundMask & goLayer) != 0)
        {
            isGrounded = false;
        }
    }
 }
