#pragma strict

var health = 100;

function Damage( amount : int ) {
	health -= amount;
	health = Mathf.Clamp(health, 0, 100);
	
	if(health == 0) {
		SendMessage("Die", SendMessageOptions.DontRequireReceiver);
	} else {
		SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
	}
}