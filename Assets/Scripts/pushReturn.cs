using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pushReturn : MonoBehaviour
{
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
        Debug.Log("OnTriggerEnter");

    }

    private void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene("labEscapeRoomScene");

        Debug.Log("OnCollisionEnter");

    }

    private void OnCollisionStay(Collision collision)
    {

        Debug.Log("OnCollisionStay");

    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit");
    }


}
