using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public string objectType;

    public bool active;

    public GameObject interactible;

    public bool inTrigger;

    public float timeToUnlock;
    public float doorCooldownTime;

    public float nextUnlockTimer = 0;
    public float doorCooldownTimer = 0;

    bool cooldownActive = false;

    public GameObject lockIcon;
    public GameObject unlockedIcon;
    public GameObject cooldownIcon;

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
            //Debug.Log("Interacting");
            Interact();
        }

        if (nextUnlockTimer > 0) { nextUnlockTimer -= Time.deltaTime; };
        if (doorCooldownTimer > 0) { doorCooldownTimer -= Time.deltaTime; };

        if (active && doorCooldownTimer <= 0 && nextUnlockTimer <= 0)
        {
            active = false;
            doorCooldownTimer = doorCooldownTime;

            var rotationVector = interactible.transform.rotation.eulerAngles;
            Debug.Log("cooldown started");
            rotationVector.y -= 90;
            //Debug.Log(rotationVector);
            interactible.transform.rotation = Quaternion.Euler(rotationVector);
            interactible.layer = LayerMask.NameToLayer("Default");

            // Show the icons
            lockIcon.SetActive(false);
            cooldownIcon.SetActive(true);
        }
        else if (!active && doorCooldownTimer <= 0 && nextUnlockTimer <= 0)
        {
            cooldownIcon.SetActive(false);
            unlockedIcon.SetActive(true);
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
        /*
        if (active)
        {
            var rotationVector = interactible.transform.rotation.eulerAngles;
            //Debug.Log("ACTIVE -> INACTIVE");
            rotationVector.y += 90;
            //Debug.Log(rotationVector);
            interactible.transform.rotation = Quaternion.Euler(rotationVector);
            interactible.layer = LayerMask.NameToLayer("Wall");
        }
        else
        {
            var rotationVector = interactible.transform.rotation.eulerAngles;
            //Debug.Log(rotationVector);
            rotationVector.y -= 90;
            //Debug.Log(rotationVector);
            interactible.transform.rotation = Quaternion.Euler(rotationVector);
            interactible.layer = LayerMask.NameToLayer("Wall");
        }
        active = !active;
        */

        if (nextUnlockTimer <= 0 && doorCooldownTimer <= 0)
        {
            // Lock the door

            var rotationVector = interactible.transform.rotation.eulerAngles;
            Debug.Log("LETSA GO");
            rotationVector.y += 90;
            //Debug.Log(rotationVector);
            interactible.transform.rotation = Quaternion.Euler(rotationVector);
            interactible.layer = LayerMask.NameToLayer("Wall");

            // Show the lock Icon
            unlockedIcon.SetActive(false);
            lockIcon.SetActive(true);

            nextUnlockTimer = timeToUnlock;

            active = true;
        }

    }

    void DoorCooldown()
    {
        
    }
}
