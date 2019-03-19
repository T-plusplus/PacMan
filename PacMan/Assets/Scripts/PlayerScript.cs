using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    //public float speed;
    private Vector3 dir;
    private Rigidbody rb;
    private Vector3 dest = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dest = transform.position;
        //start right
        dir = Vector3.right;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        //movement input vector
        Vector3 mv = new Vector3(moveHoriz, 0.0f, moveVert);
        //eliminate momentum
        Debug.Log("Movement Vector before: " + mv);
        if ((mv.x < 1 && mv.x>0) || (mv.x>-1 && mv.x<0)) mv.x = 0;
        if ((mv.z < 1 && mv.z>0) || (mv.z>-1 && mv.z<0)) mv.z = 0;
        Debug.Log("Movement Vector after: " + mv);
        //if two buttons are being pressed
        if (mv.x != 0 && mv.z != 0)
        {
            Vector3 px = new Vector3(mv.x, 0, 0);
            Vector3 pz = new Vector3(0, 0, mv.z);
            //if both axes are valid movements,favor z
            if (Valid(px) && Valid(pz))
            {
                mv.x = (mv.z >= mv.x) ? (0) : (1);
                mv.z = (mv.z >= mv.x) ? (1) : (0);
            }
            //prioritize x
            else if (Valid(px))
                mv = px;
            else if (Valid(pz))
                mv = pz;
            //if neither are valid they shouldn't move in that direction anyway
        }
        Vector3 ps = transform.position;
        Debug.Log("Validity: Left-" + Valid(Vector3.left) + " Right -" + Valid(Vector3.right) + " Forward-" + Valid(Vector3.forward) + " Back-" + Valid(Vector3.back));
        //if the input direction changes, and the input is valid, change direction
        if (mv != dir && Valid(mv))
        {
            rb.velocity = speed * mv;
            dir = speed * mv;
        }
        //otherwise maintain direction
        else
            rb.velocity = dir;

        //Debug.Log(dir);
        //rb.AddForce(mv*speed);
        //Vector3 p = Vector3.MoveTowards(transform.position, dest, speed);
        //rb.MovePosition(p);
        // Check for Input if not moving
        /*if ((Vector3)transform.position == dest)
        {
            
            if (Input.GetKey(KeyCode.UpArrow) && valid(Vector3.forward))
                dest = (Vector3)transform.position + Vector3.forward;
            if (Input.GetKey(KeyCode.RightArrow) && valid(Vector3.right))
                dest = (Vector3)transform.position + new Vector3(1,0,0);
            if (Input.GetKey(KeyCode.DownArrow) && valid(Vector3.back))
                dest = (Vector3)transform.position + Vector3.back;
            if (Input.GetKey(KeyCode.LeftArrow) && valid(Vector3.left))
                dest = (Vector3)transform.position + Vector3.left;*/


            /*if (Input.GetKey(KeyCode.UpArrow) )
                dest = (Vector3)transform.position + Vector3.forward;
            if (Input.GetKey(KeyCode.RightArrow) )
                dest = (Vector3)transform.position + Vector3.right;
            if (Input.GetKey(KeyCode.DownArrow) )
                dest = (Vector3)transform.position + Vector3.back;
            if (Input.GetKey(KeyCode.LeftArrow) )
                dest = (Vector3)transform.position + Vector3.left;*/
       // }
    }
    bool Valid(Vector3 dir)
    {
        Vector3 pos = transform.position;
        return !Physics.Linecast(pos, pos + dir);
    }
}
