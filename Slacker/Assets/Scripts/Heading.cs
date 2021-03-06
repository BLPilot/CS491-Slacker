using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heading : MonoBehaviour
{
    Entity entity;
    public float desiredChangeRate = 30;
    public float turnRate = 15;

    public Vector3 directionVector;

    private float desiredHeading;
    public float heading;

    private Transform headingIndicator;

    void Awake()
    {
        headingIndicator = transform.Find("HeadingIndicator");
        entity = transform.GetComponent<Entity>();
        heading = transform.rotation.eulerAngles.y;
    }

    public void IncreaseDesiredHeading()
    {
        desiredHeading += desiredChangeRate * Time.deltaTime;
    }

    public void DecreaseDesiredHeading()
    {
        desiredHeading -= desiredChangeRate * Time.deltaTime;
    }

    void Update()
    {
        if (directionVector.magnitude > 0)
        {
            desiredHeading = Mathf.Rad2Deg * Mathf.Atan2(-directionVector.z, directionVector.x);
        }

        // keep desired heading within 0-360
        if (desiredHeading > 360)
        {
            desiredHeading = desiredHeading - 360;
        }
        else if (desiredHeading < 0)
        {
            desiredHeading = desiredHeading + 360;
        }

        if (headingIndicator)
        {
            headingIndicator.eulerAngles = new Vector3(0, desiredHeading, 0);
        }


        /*if (entity.movement.speed > 0)
        {
            if (desiredHeading <= heading)
            {
                if (Mathf.Abs(desiredHeading - heading) <= 180)
                {
                    heading -= turnRate * Time.deltaTime;
                }
                else
                {
                    heading += turnRate * Time.deltaTime;
                }
            }
            else
            {
                if (Mathf.Abs(heading - desiredHeading) <= 180)
                {
                    heading += turnRate * Time.deltaTime;
                }
                else
                {
                    heading -= turnRate * Time.deltaTime;
                }
            }
        }*/

        // keep desired heading within 0-360
        if (heading > 360)
        {
            heading = heading - 360;
        }
        else if (heading < 0)
        {
            heading = heading + 360;
        }

        transform.eulerAngles = new Vector3(0, heading, 0);

        directionVector = Vector3.zero;
    }
}