using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class jailWalls : MonoBehaviour
{
    public GameObject west_brick;
    public GameObject west_ball;

    public bool activated = false;

    public ParticleSystem brickExplosionParticle;


    // Start is called before the first frame update
    void Start()
    {

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
            // RevealBallInsideWall();
            StartCoroutine(SetTimeout(5f));
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
        // Reveal the ball
        west_ball.SetActive(true);
    }

    IEnumerator SetTimeout(float time)
    {
        yield return new WaitForSeconds(time);
        RevealBallInsideWall();
    }
}
