using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleports : MonoBehaviour {

    // Use this for initialization
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        GameObject Movable = other.gameObject;
        Vector3 pos = Movable.transform.position;
        if (pos.x > 0)
            Movable.transform.position = new Vector3(-(pos.x - .5f), pos.y, pos.z);
        else
            Movable.transform.position = new Vector3(-(pos.x + .5f), pos.y, pos.z);
    }
}
