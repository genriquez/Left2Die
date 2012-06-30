using UnityEngine;
using System.Collections;

public class SpawnPointController : MonoBehaviour {

	public GameObject spawnTarget;
	
	public void Start () {
		GameController.instance.RegisterSpawnPoint(this);
	}
	
	public void Spawn() {
		Instantiate(spawnTarget, transform.position, transform.rotation);
	}
}
