using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakableWall : MonoBehaviour
{
    public float brickSize = 0.2f;
    public int bricksNum = 5;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;

    float brickPivotDist;
    Vector3 brickPivot;

    // Start is called before the first frame update
    void Start()
    {
        //calculate pivot distance
        brickPivotDist = brickSize * bricksNum / 2;
        //use this value to create pivot vector)
        brickPivot = new Vector3(brickPivotDist, brickPivotDist, brickPivotDist);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BreakWallToPieces(new Vector3(-3, 1, 44), null);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("breakableWall") &&
            collision.collider.gameObject.CompareTag("breakableBall"))
        {
            BreakWallToPieces(collision.transform.position, collision.gameObject);
        }
    }

    public void BreakWallToPieces(Vector3 initPos, GameObject collider)
    {
        // Remove breakable wall & the collider - breakable ball
        gameObject.SetActive(false);
        if (collider) 
        { 
            collider.SetActive(false);
        }

        // create 3D bricks (by bricksNum value)
        for (int x = 0; x < bricksNum; x++)
        {
            for (int y = 0; y < bricksNum; y++)
            {
                for (int z = 0; z < bricksNum; z++)
                {
                    CreateBrick(x, y, z, initPos);
                }
            }
        }

        ExplodeWall(initPos);

    }

    void CreateBrick(int x, int y, int z, Vector3 initPos)
    {
        // Generate new cube as brick
        GameObject brick = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Calculate the brick transform
        Vector3 relativePos = new Vector3(brickSize * x, brickSize * y, brickSize * z) - brickPivot;
        brick.transform.position = initPos + relativePos;
        brick.transform.localScale = new Vector3(brickSize, -0.05f, brickSize);

        // Set the brick material as brick from the original wall
        GameObject bricksLine = gameObject.transform.GetChild(0).gameObject;
        GameObject wall_brick = bricksLine.transform.GetChild(0).gameObject;
        brick.GetComponent<Renderer>().material = wall_brick.GetComponent<Renderer>().material;

        // Set the brick rigibody and mass
        brick.AddComponent<Rigidbody>();
        brick.GetComponent<Rigidbody>().mass = brickSize;
    }

    void ExplodeWall(Vector3 explosionPos)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        // Add explosion force to all colliders in that overlap sphere
        foreach (Collider hit in colliders)
        {
            // Get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }
    }
}
