using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{


    public Box curBox;
    public bool boxHeld;
    private Vector3 boxOffset = new();

    private void OnTriggerStay(Collider other)
    {
        
        if(!boxHeld && other.gameObject.TryGetComponent<Box>(out Box box))
            curBox = box;

    }

    private void OnTriggerExit(Collider other)
    {
        
        if(!boxHeld)
            curBox = null;

    }

    private void FixedUpdate()
    {
        
        //if(boxHeld)
        //    curBox.transform.localPosition = boxOfset;

    }

    public void GrabBox()
    {

        if (curBox == null)
            return;

        curBox.transform.parent = transform.GetChild(0);
        boxOffset = curBox.transform.localPosition;

        //Destroy(curBox.GetComponent<Rigidbody>());
        curBox.GetComponent<Rigidbody>().isKinematic = true;
        curBox.PickUp();

        boxHeld = true;

    }

    public void ReleaseBox()
    {

        if (!boxHeld)
            return;


        //curBox.AddComponent<Rigidbody>();
        curBox.GetComponent<Rigidbody>().isKinematic = false;
        curBox.transform.parent = null;
        curBox.PutDown();
        boxHeld = false;

    }


}
