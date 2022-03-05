using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class jailWalls : MonoBehaviour
{
    public GameObject west_brick;
    public GameObject spike_ball;

    private bool activated = false;
    private int WALL_NUM = 4;

    public ParticleSystem brickExplosionParticle;


    // Start is called before the first frame update
    void Start()
    {
        if (Application.isEditor)
        {
            activated = true;
            ActivateBreakWallsAfterTimeout();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            RevealBallInsideWall();
        }

        if (gameObject.activeInHierarchy && !activated)
        {
            activated = true;
            ActivateBreakWallsAfterTimeout();
        }
    }

    void RevealBallInsideWall()
    {
        // Remove the brick
        west_brick.SetActive(false);

        // Activate the explosion partical system event in the brick's location
        brickExplosionParticle.gameObject.SetActive(true);
        brickExplosionParticle.gameObject.transform.position = west_brick.transform.position;
        brickExplosionParticle.Play();

        // TODO: Replace ball to special ball
        // Reveal the 
        spike_ball.SetActive(true);
    }

    public void ActivateBreakWallsAfterTimeout()
    {
        StartCoroutine(BreakWallsAfterTimeout(5f));
    }

    IEnumerator BreakWallsAfterTimeout(float time)
    {
        yield return new WaitForSeconds(time);
        // Freeze walls position
        for (int i = 0; i < WALL_NUM; i++)
        {
            GameObject wall = gameObject.transform.GetChild(i).gameObject;
            wall.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            wall.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        }
        RevealBallInsideWall();
        yield return new WaitForSeconds(2);
        breakWallByBall();
    }

    public void breakWallByBall()
    {
        // Disable ball gravity
        spike_ball.GetComponent<Rigidbody>().useGravity = false;

        // Move on the X axis
        GameObject ball = spike_ball.transform.GetChild(0).gameObject;

        spike_ball.transform.position = new Vector3(spike_ball.transform.position.x - 1f,
                                            spike_ball.transform.position.y,
                                            spike_ball.transform.position.z);

        spike_ball.transform.position = new Vector3(-2.72f, 3.73f, 44.16f);

        // Enable ball gravity back
        spike_ball.GetComponent<Rigidbody>().useGravity = true;
    }
}
