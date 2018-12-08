using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointTrigs : MonoBehaviour
{
    /// <summary>
    /// Reference to the empty GameObject holding the ghosts.
    /// </summary>
    private GameObject ghostRef;
    /// <summary>
    /// When an attempt to update the dirction of a ghost fails, this will be used to try again.
    /// </summary>
    private GhostScript gscrRef;
    /// <summary>
    /// Reference to the last vector we tried to change the direction to(see gscrRef).
    /// </summary>
    private Vector3 vRef;
    /// <summary>
    /// Boolean representing whether the last attempt to update the direction was a success or not.
    /// </summary>
    bool ghostsuccess = true;
    private readonly Vector3[] v3arr = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
    // Use this for initialization
    void Awake()
    {
        ghostRef = GameObject.Find("Ghosts");

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        /*if (gscrRef != null)
        {
            Vector3 backwards;// = gscrRef.GetBackwardsDir();
            for (int i = 0; i < v3arr.Length; i++)
            {
                if (v3arr[i] != backwards && !ghostsuccess)
                {
                    ghostsuccess = gscrRef.UpdateDir(v3arr[i]);
                }
            }
            //should never enter unless we eventually fail
        }*/

    }
    private void OnTriggerEnter(Collider other)
    {
        /*//Debug.Log("entered");
        GameObject ghost = other.gameObject;
        //Debug.Log(ghost.transform.parent);
        if (ghost.transform.parent == ghostRef.transform)
        {
            //Debug.Log("Success");
            //get instance of the ghost script
            GhostScript gscr = ghost.GetComponent<GhostScript>();

            //TODO: use if elses to set the dir.
            
            
            ghostsuccess = gscr.UpdateDir(v3arr[(int)System.Math.Ceiling(Random.Range(-1.0f, 3.0f))]);
            if (!ghostsuccess)
            {
                gscrRef = gscr;
                //vRef = Vector3.back;//need to change
                //TODO could still use this to prevent it from going the other way.
            }
        }*/

    }
    /*private void OnTriggerExit(Collider other)
    {
        ghostsuccess = true;
        gscrRef = null;
    }*/
}
