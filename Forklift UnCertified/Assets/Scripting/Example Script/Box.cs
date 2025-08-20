using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Box : MonoBehaviour
{
    private List<Box> boxesAbove;
    private List<Box> boxesBelow;

    [SerializeField] private float weight = 1;

    private void Start()
    {
        boxesAbove = new();
        boxesBelow = new();
    }

    public float Weight
    {
        get
        {
            if (boxesAbove.Count == 0)
                return weight/Mathf.Max(boxesBelow.Count,1);
            else
            {
                float runningWeight = 0f;
                foreach(Box above in boxesAbove)
                {
                    runningWeight += above.Weight;
                }
                return (runningWeight + weight)/ Mathf.Max(boxesBelow.Count, 1);
            }
        }
    }

    public bool playerPushable = true;

    [Range(0f, 1f)] public float playerCoefficientOfRestitution = 0.2f;

    public virtual void PickUp()
    {

    }

    public virtual void PutDown()
    {

    }

    public virtual void Lift()
    {

    }

    public virtual void Lower()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.body!=null && collision.body.gameObject.TryGetComponent<Box>(out Box box))
        {
            if(collision.body.transform.position.y > transform.position.y + 0.4f)
            {
                boxesAbove.Add(box);
            }
            else if (collision.body.transform.position.y < transform.position.y - 0.4f)
            {
                boxesBelow.Add(box);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.body != null && collision.body.gameObject.TryGetComponent<Box>(out Box box))
        {
            if (collision.body.transform.position.y > transform.position.y + 0.4f)
            {
                boxesAbove.Remove(box);
            }
            else if (collision.body.transform.position.y < transform.position.y - 0.4f)
            {
                boxesBelow.Remove(box);
            }
        }
    }
}
