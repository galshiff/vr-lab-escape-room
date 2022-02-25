using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class jailWalls : MonoBehaviour
{
    public GameObject west_brick;
    public GameObject west_ball;
    public GameObject north_wall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            // TODO: Replace with explosion...
            west_brick.SetActive(false);
            // TODO: Replace ball to special ball
            west_ball.SetActive(true);
            // onStart.Invoke();
        }
    }
}
