using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("breakableWall") &&
            collision.collider.gameObject.CompareTag("breakableBall"))
        {
            // TODO: Replace with explosion...
            gameObject.SetActive(false); // breakable wall
            collision.gameObject.SetActive(false); // breakable ball
        }
    }
}
