using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.WSA;

public class Button : MonoBehaviour
{

    public GameObject activatableObject;

    public float requiredWeight = 1f;
    private float currentWeight = 0;

    public float squashSpeed = 0.5f;
    public float minimumDistance = 0.5f;

    public bool weighed = false;
    private float timer = 1.1f;
    public float startY;
    public float EndY;
    private void OnCollisionStay(Collision collision)
    {
        
        if(collision.gameObject.TryGetComponent<Box>(out Box box))
        {

            if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(box.transform.position.x, box.transform.position.z)) < minimumDistance)
            {
                currentWeight = box.Weight;

                if (!weighed)
                {
                    weighed = true;
                    timer = timer >= 1 ? 0 : 1 - timer;
                }
 
            }
            else
            {
                currentWeight = 0;

                if (weighed)
                {
                    weighed = false;
                    timer = timer >= 1 ? 0 : 1 - timer;
                }
            }

        }

    }

    private void OnCollisionExit(Collision collision)
    {

        if (weighed)
        {
            currentWeight = 0;
            weighed = false;
            timer = timer >= 1 ? 0 : 1 - timer;
        }

    }

    private void FixedUpdate()
    {
        
        if(weighed && timer < 1)
        {

            timer += Time.deltaTime * squashSpeed;

            float weightedEnd = startY - EndY;
            weightedEnd *= 1 - (currentWeight / requiredWeight);

            transform.localPosition = Vector3.Lerp(new Vector3(0, startY, 0), new Vector3(0, weightedEnd + EndY, 0), timer);

            if (timer > 1 && activatableObject.TryGetComponent<IActivatable>(out IActivatable triggeredElement))
            {
                triggeredElement.Activate();
            }

        }
        else if( !weighed && timer < 1)
        {

            timer += Time.deltaTime * squashSpeed;

            float weightedEnd = startY - EndY;
            weightedEnd *= 1 - (currentWeight / requiredWeight);

            transform.localPosition = Vector3.Lerp(new Vector3(0, weightedEnd + EndY, 0), new Vector3(0, startY, 0), timer);


            if (timer > 1 && activatableObject.TryGetComponent(out IActivatable triggeredElement))
                triggeredElement.Deactivate();

        }


    }
}
