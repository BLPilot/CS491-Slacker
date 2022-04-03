using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public string objectType;

    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.name == "Player" && Input.GetButtonDown("Interact"))
        {
            Debug.Log("Player opens door");
            Interact();
        }
    }

    void Interact()
    {
        if (active)
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.y += 90;
            transform.rotation = Quaternion.Euler(rotationVector);
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.y -= 90;
            transform.rotation = Quaternion.Euler(rotationVector);
            gameObject.layer = LayerMask.NameToLayer("Wall");
        }
        active = !active;

    }
}
