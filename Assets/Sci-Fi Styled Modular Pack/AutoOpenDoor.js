#pragma strict

private var anim : Animator;
var character : GameObject;
var distanceToOpen: float = 5;
var targetLook: GameObject;
private var characterNearbyHash: int = Animator.StringToHash("character_nearby");

private var miniSpeed: int = 100;
var cam: GameObject;
var camDestPos: Vector3 = new Vector3(-16, 2, 27);
var camDestRot: Vector3 = new Vector3(0,0,0);
var coCounter: int = 0;
function Start () 
{
    anim = GetComponent("Animator");
}


function Update () 
{
	var distance = Vector3.Distance(transform.position,character.transform.position);
	
    if (distanceToOpen >= distance) {
        if (coCounter == 0) {
            coCounter++;
            StartCoroutine("MoveCam");
        }
    	
    }else{
    	anim.SetBool(characterNearbyHash, false);
    }
}

function MoveCam() {
  //  cam.GetComponent("CameraMovement").canMoveCamera = false;
    var camMiniOffsetPos = (camDestPos - cam.transform.position) / miniSpeed;
    //var camMiniOffsetRot = (camDestRot - cam.transform.rotation) / miniSpeed;
    var i = miniSpeed;
    while (i > 0) {
        yield WaitForSeconds(0.033);
        Debug.Log("positionB: " + cam.transform.position);
        cam.transform.position += camMiniOffsetPos;
        Debug.Log("direction: " + camMiniOffsetPos);
        Debug.Log("positionA: " + cam.transform.position);

        //cam.transform.LookAt(targetLook.transform.position);
        i--;
    }
  
    anim.SetBool(characterNearbyHash, true);
    
}