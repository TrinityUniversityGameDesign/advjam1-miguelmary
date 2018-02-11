using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public bool canMoveCamera = true;
    public float yPos = 25;
    public float xOffest = 0;
    public float zOffest = 0;

    public GameObject target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (canMoveCamera)
        {
            transform.position = new Vector3(target.transform.position.x + xOffest, yPos, target.transform.position.z + zOffest);
        }
    }
}
