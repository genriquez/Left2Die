#pragma strict

static var instance : GameController;

var maxCouncurrentZombies = 20;
var startScreen : GameObject;
var player : GameObject;

private var spawnPoints : SpawnPointController[] = [];
private var zombieCount = 0;
private var playerHealthController : EntityHealthController;

function Awake () {
	instance = this;
}

function Start() {
	playerHealthController = player.GetComponent(EntityHealthController);
}

function StartGame() {
	SpawnCycle();
	
	player.GetComponent(FPSInputController).enabled = true;
	player.GetComponent(PlayerController).enabled = true;
	player.GetComponent(MouseLook).enabled = true;
	
	Destroy(startScreen);
}

function RegisterSpawnPoint( point : SpawnPointController ) {
	ArrayUtility.Add(spawnPoints, point);
}

function SpawnCycle() {
	var count = spawnPoints.length;
	
	if(count > 0 && maxCouncurrentZombies > zombieCount) {	//Spawn a new zombie
		var selectedPoint = spawnPoints[Random.RandomRange(0, count)];
		selectedPoint.Spawn();
		zombieCount++;
	}

	//Schedule next spawn
	Invoke("SpawnCycle", 0.5);	
}

function RegisterKill() {
	zombieCount--;
}

function KillPlayer() {
	var playerPosition = player.transform.position;
	
	var deathViewport = new GameObject();
	deathViewport.transform.position = playerPosition + Vector3.up * 8;
	deathViewport.transform.LookAt(player.transform);
	deathViewport.AddComponent("Camera");
	deathViewport.AddComponent("AudioListener");

	DestroyObject(player);
	player = null;
	
	CancelInvoke();
}

function OnGUI() {
	GUI.Label(Rect(0,0, 200, 100), "Zombies: " + zombieCount);
	
	if( playerHealthController.health > 0 ) {
		GUI.Label(Rect(0,Screen.height - 20, 200, 100), "Health: " + playerHealthController.health);
	} else {
		GUI.Label(Rect(0,Screen.height - 20, 300, 100), "YOU AND YOUR FRIENDS ARE DEAD");
	}
}