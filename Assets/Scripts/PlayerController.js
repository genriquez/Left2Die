#pragma strict

var weapon : GameObject;
var gunshot : AudioClip;

private var isReloading = false;
private var audioSource : AudioSource;


function Start() {
	audioSource = GetComponent(AudioSource);
	audioSource.clip = gunshot;
	
	UpdateWeaponState();
}

function Update() {

	if( !isReloading && Input.GetMouseButton(0) ) {
		StartReloading();
		audioSource.Play();

		var direction = transform.TransformDirection(Vector3.forward);
		var hit : RaycastHit;
		
		if( Physics.Raycast(transform.position, direction, hit) ) {
			var healthController = hit.transform.GetComponent(EntityHealthController);
			
			if( healthController != null ) {
				healthController.Damage(Random.RandomRange(15,35));
			}
			
			
			if( hit.collider.rigidbody != null ) {
				hit.collider.rigidbody.AddForce(transform.rotation * Vector3.forward);
			}
		}
		
	}
}

function StartReloading() {
	isReloading = true;
	UpdateWeaponState();

	Invoke("FinishReloading", 2);
}

function FinishReloading() {
	isReloading = false;
	UpdateWeaponState();
}

function UpdateWeaponState() {
	weapon.animation.Play(isReloading?"Disabled":"Idle");
}

function Die() {
	GameController.instance.KillPlayer();
}