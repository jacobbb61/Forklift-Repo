using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    
    private Rigidbody body;
    private Animator anim;
    private ItemGrabber grabber;
    private bool lift = false;
    private Vector2 directionMotion = new();
    public float moveSpeed = 3;
    public float turnSpeed = 30;
    public bool stopped = false;

    private void Start ()
    {

        body = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        grabber = GetComponentInChildren<ItemGrabber>();

    }

    public void Update()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            directionMotion.x = Input.GetKey(KeyCode.W) ? 1 : -1;
        else
            directionMotion.x = 0;

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            directionMotion.y = Input.GetKey(KeyCode.D) ? 1 : -1;
        else
            directionMotion.y = 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {

            ToggleLift();

        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            if(grabber.boxHeld)
                grabber.ReleaseBox();
            else
                grabber.GrabBox();
        }

    }

    public void FixedUpdate()
    {

        if (!stopped)
        {
            float gravity = body.velocity.y;
            body.velocity = transform.forward * moveSpeed * Time.deltaTime * directionMotion.x;
            body.velocity += new Vector3(0, gravity, 0);


            transform.rotation *= Quaternion.Euler(0, directionMotion.y * turnSpeed * Time.deltaTime, 0);
            
        }

    }

    public void ToggleLift()
    {

        lift = !lift;
        if(grabber.boxHeld)
        {
            if(lift)
            {
                grabber.curBox.Lift();
            }
            else
            {
                grabber.curBox.Lower();
            }
        }
        anim.SetBool("LiftUp", lift);

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.rigidbody!=null && collision.rigidbody.gameObject.TryGetComponent<Box>(out Box b) && !b.playerPushable)
        {
            collision.rigidbody.velocity = new Vector3(0,collision.rigidbody.velocity.y,0);
            /*
            collision.rigidbody.AddForce(body.velocity * body.mass * b.playerCoefficientOfRestitution);
            body.AddForce(-body.velocity * body.mass * b.playerCoefficientOfRestitution);
            */
        }
    }
}
