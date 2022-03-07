using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CauldronCollider : MonoBehaviour
{
    public UnityEvent onWin;
    public UnityEvent onFinish;
    public GameObject rocket;


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
}
