using UnityEngine;
using System.Collections;

public class ZombieAIDirector : MonoBehaviour {

	private string currentStrategy = "Roaming";
	
	private float playerSensingRange = 15;
	private float playerAttackingRange = 3;
	private bool stunned = false;

	public void Start () {
		PublishChangeStrategy();
		InvokeRepeating("CheckStrategyState", 1, 1);
	}

	#region "Custom Events"

	public void OnHit() {
		stunned = true;
		CheckStrategyState();
	}
	
	public void OnRecover() {
		stunned = false;
		CheckStrategyState();
	}
	
	public void Die() {
		Destroy(gameObject);
		GameController.instance.RegisterKill();
	}
	
	#endregion
	
	private void CheckStrategyState() {
		string strategy = null;
		
		if( GameController.instance.player == null ) {
			strategy = "Roaming";
		} else if(!stunned) {
			PlayerController player = GameController.instance.player.GetComponent<PlayerController>();
			
			Vector3 vectorToPlayer = (player.transform.position - transform.position);
			float distanceToPlayer = vectorToPlayer.magnitude;
			
			strategy = (distanceToPlayer > playerSensingRange ? "Roaming" : "Chasing");
			strategy = (distanceToPlayer > playerAttackingRange ? strategy : "Attacking");
		} else {
			strategy = "Stunned";
		}
		
		if( strategy != currentStrategy ) {
			currentStrategy = strategy;
			PublishChangeStrategy();
		}
	}

	private void PublishChangeStrategy() {
		this.SendMessage("OnChangeAIStrategy", currentStrategy, SendMessageOptions.DontRequireReceiver);
	}
}
