#pragma strict

var speed = 1F;
var movementController : CharacterController;
var sounds : AudioClip[];

private var audioSource : AudioSource;

function Start() {
	movementController = GetComponent(CharacterController);
	audioSource = GetComponent(AudioSource);
	
	ChangeDirection();
	RandomSoundPlayer();
}

function Update () {
	movementController.SimpleMove(transform.rotation * Vector3.forward * speed);
}

function ChangeDirection() {
	transform.rotation = Quaternion.Euler(0, Random.RandomRange(0F, 360F), 0);
	Invoke("ChangeDirection", Random.RandomRange(5, 10));
}

function RandomSoundPlayer() {
	audioSource.clip = sounds[Random.Range(0,sounds.length)];
	audioSource.Play();
	
	Invoke("RandomSoundPlayer", Random.Range(4,10));
}

function OnChangeAIStrategy( strategy : String ) {
	enabled = (strategy == "Roaming");
	
	if(enabled) {	//Initialize direction change loop
		gameObject.animation.Play("Walking");
		ChangeDirection();
		RandomSoundPlayer();
	} else {
		CancelInvoke();
	}
}