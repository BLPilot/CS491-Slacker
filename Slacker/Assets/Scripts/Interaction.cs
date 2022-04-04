using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public string objectType;

    public bool active;

    public GameObject interactible;

    public bool inTrigger;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        inTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger && (Input.GetButtonUp("Interact") || Input.GetButtonUp("Fire1") ) )
        {
            Debug.Log("Interacting");
            Interact();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            inTrigger = false;
        }
    }

    void Interact()
    {
        if (active)
        {
            var rotationVector = interactible.transform.rotation.eulerAngles;
            Debug.Log(rotationVector);
            rotationVector.y += 90;
            Debug.Log(rotationVector);
            interactible.transform.rotation = Quaternion.Euler(rotationVector);
            interactible.layer = LayerMask.NameToLayer("Wall");
        }
        else
        {
            var rotationVector = interactible.transform.rotation.eulerAngles;
            Debug.Log(rotationVector);
            rotationVector.y -= 90;
            Debug.Log(rotationVector);
            interactible.transform.rotation = Quaternion.Euler(rotationVector);
            interactible.layer = LayerMask.NameToLayer("Wall");
        }
        active = !active;

    }
}
