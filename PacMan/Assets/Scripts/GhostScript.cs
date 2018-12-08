using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{

    public float vel;
    private Rigidbody rb;

    private Vector3 dir;
    private Vector3 dirQ = Vector3.zero;
    private bool isSpawned;
    public enum States { scatter = 1, chase, frightened };
    private States CurState;
    private float SpawnTime;

    private readonly string Ghost1 = "Ghost1";
    private readonly string Ghost2 = "Ghost2";
    private readonly string Ghost3 = "Ghost3";
    private readonly string Ghost4 = "Ghost4";

    private readonly Vector3[] v3arr = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
    bool flag = false;
    private float FrightTime;
    public Material mat1;
    public Material mat2;
    

    GameObject RecentTrig;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isSpawned = false;
        SpawnTime = GetSpawn();
        CurState = States.scatter;
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //int numScatters = 0;
        /*
         * States:
         * - Chase: Moves toward player
         * - Scatter: moves towards the ghost's specific target tile, which is a different corner of the maze.
         * - Frightened: When they turn blue, direction is determined pseudorandomly
         * 
         * Scatter for 7 seconds, then Chase for 20 seconds.
         * Scatter for 7 seconds, then Chase for 20 seconds.
         * Scatter for 5 seconds, then Chase for 20 seconds.
         * Scatter for 5 seconds, then switch to Chase mode permanently.
         */
        //above may be used later, but for now it will all be random.
        if (!isSpawned)
        {
            dir = Vector3.zero;
            if (SpawnTime <= 0)
            {
                isSpawned = true;
                //Move ghost out of the bullpen using transform.position
                transform.position = new Vector3(transform.position.x, .5f, transform.position.z + 4);
                UpdateDir(Vector3.left);
            }
            SpawnTime -= Time.deltaTime;
        }
        else
        {
            //if we are waiting to change direction
            if(flag)
            {
                Vector3 variance = RecentTrig.gameObject.transform.position - gameObject.transform.position;
                //Debug.Log(variance);
                if (Math.Abs(variance.x) < .05 && Math.Abs(variance.z) < .05)
                {
                    bool cond = UpdateDir(v3arr[(int)System.Math.Ceiling(UnityEngine.Random.Range(-.9f, 3.0f))]);
                    while (!cond)
                    {
                        cond = UpdateDir(v3arr[(int)System.Math.Ceiling(UnityEngine.Random.Range(-.9f, 3.0f))]);
                    }
                    flag = false;
                }
            }
            rb.velocity = vel * dir;
        }
        if (CurState == States.frightened)
        {
            gameObject.GetComponent<Renderer>().material = mat2;
            if (FrightTime < 0)
            {
                CurState = States.scatter;
            }
            else
            {
                FrightTime -= Time.deltaTime;
            }
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = mat1;
        }
        if (gameObject.transform.position.y < .45f || gameObject.transform.position.y> .55f)
            gameObject.transform.position =new Vector3(gameObject.transform.position.x, .5f, gameObject.transform.position.z);
        //Debug.Log("Ghost: L " + Valid(Vector3.left) + " R " + Valid(Vector3.right) + " U " + Valid(Vector3.forward) + " D " + Valid(Vector3.back));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GTrig"))
        {
            flag = true;
            RecentTrig = other.gameObject;
        }
        else if (other.gameObject.CompareTag("GJTrig"))
        {
            flag = true;
            RecentTrig = other.gameObject;
        }


    }
    float GetSpawn()
    {

        if (name == Ghost1)
        {
            isSpawned = true;
            dir = Vector3.left;
        }
        else
        {
            float SpawnTime;
            if (name.Equals(Ghost2))
                SpawnTime = 10;
            else if (name.Equals(Ghost3))
                SpawnTime = 20;
            else
                SpawnTime = 30;

            return SpawnTime;
        }
        return 0;
    }
    private bool Valid(Vector3 dir)
    {
        Vector3 pos = transform.position;

        //set mask to these two layers
        int mask = 1 << LayerMask.NameToLayer("CoinPU");
        mask += 1 << LayerMask.NameToLayer("Ignore Raycast");
        //Debug.Log(!Physics.Linecast(pos, pos + dir, mask)+" "+dir);
        RaycastHit hit;
        bool cond = Physics.Linecast(pos, pos + dir, out hit, ~mask) && dir != -this.dir;
        if (cond)
            Debug.DrawRay(pos, dir, Color.black);
        return !cond;
    }
    public bool UpdateDir(Vector3 toDir)
    {
        //Debug.Log(Valid(toDir));
        if (Valid(toDir))
        {
            dir = toDir;
        }
        //basically will return whether or not it was successful
        return Valid(toDir);
    }
    /// <summary>
    /// Sets this ghost in to frightened state, and resets the frightened timer, and it will remain in this state until the timer is up or pacman eats him.
    /// </summary>
    public void SetFrightenedState()
    {
        CurState = States.frightened;
        FrightTime = 15f;
    }
    /// <summary>
    /// Gets the State of this ghost
    /// </summary>
    /// <returns>The state of the ghost.</returns>
    public States GetState()
    {
        return CurState;
    }
    /// <summary>
    /// Resets the ghost after being eaten. 
    /// </summary>
    public void ResetGhost()
    {
        gameObject.transform.position = new Vector3(0, .5f, 0);
        gameObject.GetComponent<Renderer>().material = mat1;
        CurState = States.scatter;
        isSpawned = false;
        SpawnTime = 10f;
        dir = Vector3.zero;
    }
    
}
