using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Camera mainCam;
    public BoxCollider play;
    public BoxCollider quit;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.name == "Quit_Note")
                    {
                        Debug.Log("hit quit");
                        Application.Quit();
                    }

                    else if (hit.collider.name == "Play_Buttons")
                    {
                        Debug.Log("hit play");
                    }
                }
            }
        }
    }
}
