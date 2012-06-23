#pragma strict

var damage = 1F;
var sounds : AudioClip[];

private var playerHealthController : EntityHealthController;
private var audioSource : AudioSource;

function Start() {
	playerHealthController = GameController.instance.player.GetComponent(EntityHealthController);
	audioSource = GetComponent(AudioSource);
}

function Attack() {
	playerHealthController.Damage(damage);
	RandomSoundPlayer();
}

function RandomSoundPlayer() {
	audioSource.clip = sounds[Random.Range(0,sounds.length)];
	audioSource.Play();
}

function OnChangeAIStrategy( strategy : String ) {
	enabled = (strategy == "Attacking");
	
	if(enabled) {
		gameObject.animation.Play("Eating");
		InvokeRepeating("Attack", 1, 1);
	} else {
		CancelInvoke();
	}
}