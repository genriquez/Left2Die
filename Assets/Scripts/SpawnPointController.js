#pragma strict

var spawnTarget : GameObject;

function Start () {
	GameController.instance.RegisterSpawnPoint(this);
}

function Spawn() {
	Instantiate(spawnTarget, transform.position, transform.rotation);
}
