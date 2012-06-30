using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public static GameController instance;
	
	public int maxCouncurrentZombies = 20;
	public GameObject startScreen;
	public GameObject player;
	
	private SpawnPointController[] spawnPoints = new SpawnPointController[0];
	private int zombieCount = 0;
	private EntityHealthController playerHealthController;
	
	public void Awake () {
		instance = this;
	}
	
	public void Start() {
		playerHealthController = player.GetComponent<EntityHealthController>();
	}
	
	
	public void StartGame() {
		SpawnCycle();
		
		player.GetComponent<FPSInputController>().enabled = true;
		player.GetComponent<PlayerController>().enabled = true;
		player.GetComponent<MouseLook>().enabled = true;
		
		Destroy(startScreen);
	}
	
	public void RegisterSpawnPoint( SpawnPointController point ) {
		ArrayList arr = new ArrayList(spawnPoints);
		arr.Add(point);
		
		spawnPoints = (SpawnPointController[])arr.ToArray(typeof(SpawnPointController));
	}
	
	private void SpawnCycle() {
		int count = spawnPoints.Length;
		
		if(count > 0 && maxCouncurrentZombies > zombieCount) {	//Spawn a new zombie
			SpawnPointController selectedPoint = spawnPoints[Random.RandomRange(0, count)];
			selectedPoint.Spawn();
			zombieCount++;
		}
	
		//Schedule next spawn
		Invoke("SpawnCycle", 0.5F);	
	}
	
	public void RegisterKill() {
		zombieCount--;
	}
	
	public void KillPlayer() {
		Vector3 playerPosition = player.transform.position;
		
		GameObject deathViewport = new GameObject();
		deathViewport.transform.position = playerPosition + Vector3.up * 8;
		deathViewport.transform.LookAt(player.transform);
		deathViewport.AddComponent("Camera");
		deathViewport.AddComponent("AudioListener");
	
		DestroyObject(player);
		player = null;
		
		CancelInvoke();
	}
	
	public void OnGUI() {
		GUI.Label(new Rect(0,0, 200, 100), "Zombies: " + zombieCount.ToString());
		
		if( playerHealthController.health > 0 ) {
			GUI.Label(new Rect(0,Screen.height - 20, 200, 100), "Health: " + playerHealthController.health);
		} else {
			GUI.Label(new Rect(0,Screen.height - 20, 300, 100), "YOU AND YOUR FRIENDS ARE DEAD");
		}
	}
}
