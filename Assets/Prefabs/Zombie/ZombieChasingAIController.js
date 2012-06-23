#pragma strict

var speed = 2F;
var sounds : AudioClip[];

private var movementController : CharacterController;
private var audioSource : AudioSource;

function Start() {
	movementController = GetComponent(CharacterController);
	audioSource = GetComponent(AudioSource);
}

function Update () {
	transform.LookAt(GameController.instance.player.transform);
	transform.localEulerAngles.x = 0;

	movementController.SimpleMove(transform.rotation * Vector3.forward * speed);
}

function RandomSoundPlayer() {
	audioSource.clip = sounds[Random.Range(0,sounds.length)];
	audioSource.Play();
	
	Invoke("RandomSoundPlayer", Random.Range(4,10));
}

function OnChangeAIStrategy( strategy : String ) {
	enabled = (strategy == "Chasing");
	
	if(enabled) {
		gameObject.animation.Play("Walking");
		RandomSoundPlayer();
	} else {
		CancelInvoke();
	}
}