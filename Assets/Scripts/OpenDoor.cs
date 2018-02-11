using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{


    private Animator anim;
    public GameObject character;
    public GameObject winText;
    public float distanceToOpen = 5;
    public GameObject targetLook;
    private int characterNearbyHash = Animator.StringToHash("character_nearby");

    private int miniSpeed = 100;
    public GameObject cam;
    public Vector3 camDestPos = new Vector3(-16, 2, 27);
    Vector3 camDestRot = new Vector3(0, 0, 0);
    int coCounter = 0;
    void Start()
    {
        anim = GetComponent <Animator> ();
        winText.SetActive(false);
    }


    void Update()
    {
        float distance = Vector3.Distance(transform.position, character.transform.position);

        if (distanceToOpen >= distance)
        {
            if (coCounter == 0)
            {
                coCounter++;
                StartCoroutine(MoveCam());
            }

        }
        else
        {
            anim.SetBool(characterNearbyHash, false);
        }
    }
    void LoadNextLevel()
    {
        SceneManager.LoadScene("Level2");
    }
    void winGame()
    {
        Time.timeScale = 0;
        winText.SetActive(true);
    }
    IEnumerator MoveCam()
    {
        cam.GetComponent <CameraMovement > ().enabled = false;
        Vector3 camMiniOffsetPos = (camDestPos - cam.transform.position) / miniSpeed;
        //FIXME_VAR_TYPE camMiniOffsetRot= (camDestRot - cam.transform.rotation) / miniSpeed;
        int i = miniSpeed;
        while (i > 0)
        {
            yield return new WaitForSeconds(0.033f);
            //Debug.Log("positionB: " + cam.transform.position);
            cam.transform.position += camMiniOffsetPos;
          //  Debug.Log("direction: " + camMiniOffsetPos);
          //  Debug.Log("positionA: " + cam.transform.position);

            cam.transform.LookAt(targetLook.transform.position);
            i--;
        }

        anim.SetBool(characterNearbyHash, true);
        if (SceneManager.GetActiveScene().name == "Main")
        {
            Invoke("LoadNextLevel", 2f);
        } else
        {
            Invoke("winGame", 1f);
        }

    }
   
}