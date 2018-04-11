using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour {

    public GameObject loseText;
    public UnityEvent finishedMove;
    public GameObject[] positions;
    private int moveCounter = 0;
    public int modSize;
    private int miniSpeed = 25;
    // Use this for initialization

    private void Awake()
    {
        finishedMove = new UnityEvent();
    }
    void Start () {
        GameObject.Find("Player").GetComponent<PlayerMovemet>().onMove.AddListener(movement);
        loseText.SetActive(false);
        modSize = positions.Length;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void movement()
    {
        // code to move enemy on event
        //transform.position = new Vector3(positions[moveCounter].transform.position.x, 1.5f, positions[moveCounter].transform.position.z);
        StartCoroutine(movementAnimation(positions[moveCounter]));
        moveCounter++;
        moveCounter = moveCounter % modSize;

    }
    IEnumerator movementAnimation(GameObject goal)
    {
        float tinyStepx = (goal.transform.position.x - transform.position.x) / miniSpeed;
        float tinyStepz = (goal.transform.position.z - transform.position.z) / miniSpeed;
        transform.LookAt(new Vector3(goal.transform.position.x, transform.position.y, goal.transform.position.z));
        for (int i = 0; i < miniSpeed; i++)
        {
            yield return new WaitForSeconds(0.00033f);
            transform.position += new Vector3(tinyStepx, 0, tinyStepz);
        }
        transform.position = new Vector3(goal.transform.position.x, 0.2f, goal.transform.position.z);
        finishedMove.Invoke();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Time.timeScale = 0;
            loseText.SetActive(true);
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
        }
    }
}
