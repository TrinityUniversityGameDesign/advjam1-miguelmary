using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerMovemet : MonoBehaviour {

    public UnityEvent onMove;

    public GameObject position;
    public float speed;
    private Rigidbody rb;
    private bool canMove = true;
    private int miniSpeed = 25;

    private void Awake()
    {
        onMove = new UnityEvent();
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        GameObject.Find("Enemy").GetComponent<EnemyMovement>().finishedMove.AddListener(allowMovement);
    }
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && position.GetComponent<GraphStructure>().left)
            {
                position = position.GetComponent<GraphStructure>().left;
                StartCoroutine(movementAnimation(position));
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && position.GetComponent<GraphStructure>().right)
            {
                position = position.GetComponent<GraphStructure>().right;
                StartCoroutine(movementAnimation(position));
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && position.GetComponent<GraphStructure>().up)
            {
                position = position.GetComponent<GraphStructure>().up;
                StartCoroutine(movementAnimation(position));
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && position.GetComponent<GraphStructure>().down)
            {

                position = position.GetComponent<GraphStructure>().down;
                StartCoroutine(movementAnimation(position));
            }
            if (Input.GetKeyDown(KeyCode.Space) && position.GetComponent<GraphStructure>().telePort)
            {
                position = position.GetComponent<GraphStructure>().telePort;
                transform.position = new Vector3(position.transform.position.x, 0.2f, position.transform.position.z);
                onMove.Invoke();
            }
        }
       // transform.position = new Vector3(position.transform.position.x,1.5f, position.transform.position.z);
        // Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        // rb.AddForce(movement * speed);
    }

    IEnumerator movementAnimation(GameObject goal)
    {
        canMove = false;
        transform.LookAt(new Vector3(goal.transform.position.x, transform.position.y, goal.transform.position.z));
        float tinyStepx = (goal.transform.position.x - transform.position.x)/miniSpeed;
        float tinyStepz = (goal.transform.position.z - transform.position.z)/miniSpeed;
        for (int i=0; i<miniSpeed; i++)
        {
            yield return new WaitForSeconds(0.00033f);
            transform.position += new Vector3(tinyStepx, 0, tinyStepz);
        }
        transform.position = new Vector3(goal.transform.position.x, 0.2f, goal.transform.position.z);
        onMove.Invoke();
        
    }

    void allowMovement()
    {
        canMove = true;
    }
}
