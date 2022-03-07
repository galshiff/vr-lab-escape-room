using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CauldronCollider : MonoBehaviour
{
    public UnityEvent onWin;
    public UnityEvent onFinish;
    public GameObject rocket;
    public GameObject vial;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Vial")
        {
            return;
        }

        other.gameObject.SetActive(false);
        onWin.Invoke();
        rocket.SetActive(true);
        onFinish.Invoke();
    }

    public void Solver()
    {
        if (Application.isEditor)
        {
            // Move the player to the third riddle
            GameObject.Find("OVRPlayerController").gameObject.transform.position = new Vector3(-1.54999995f ,1.09000003f,34.8600006f);
            GameObject.Find("OVRPlayerController").gameObject.transform.Rotate(0, -100f, 0, Space.Self);

            // Disable ball gravity
            vial.GetComponent<Rigidbody>().useGravity = false;

            vial.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            vial.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

            vial.transform.position = new Vector3(vial.transform.position.x + 0.6f,
                                                  vial.transform.position.y + 1f,
                                                  vial.transform.position.z - 1.5f);

            // Enable ball gravity back
            vial.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
