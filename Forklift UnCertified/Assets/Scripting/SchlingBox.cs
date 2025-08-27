using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchlingBox : Box
{
    public bool isHeld;
    public Transform schlingPoint;
    public float distanceFromPoint;
    public float distanceTillTension;
    public float tensionModifier_Box;
    public float tensionModifier_Player;

    private Rigidbody rb;
    private LineRenderer line;
    private Rigidbody playerRb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
        playerRb = FindAnyObjectByType<PlayerMovement>().gameObject.GetComponent<Rigidbody>();
    }
    public override void Lift()
    {
        base.Lift();
    }

    public override void Lower()
    {
        base.Lower();
    }

    public override void PickUp()
    {
        base.PickUp();
        isHeld = true;
    }

    public override void PutDown()
    {
        base.PutDown();
        isHeld = false;
    }

    

    private void LateUpdate()
    {
        line.SetPosition(0,schlingPoint.position);
        line.SetPosition(1,transform.position);

        distanceFromPoint = Vector3.Distance(transform.position, schlingPoint.position);

        if (isHeld)
        {
            if (distanceFromPoint >= distanceTillTension)
            {
                Vector3 dir = schlingPoint.position - transform.position;
                playerRb.AddForce(dir * distanceFromPoint * tensionModifier_Player);
            }
            rb.velocity = Vector3.zero;
        }
        else
        {
            if(distanceFromPoint >= distanceTillTension)
            {
                Vector3 dir = schlingPoint.position - transform.position;
                rb.AddForce(dir * distanceFromPoint * tensionModifier_Box);
            }
        }

    }
}
