#pragma strict

var currentStrategy = "Roaming";
var playerSensingRange = 15F;
var playerAttackingRange = 3F;

private var stunned = false;

function Start() {
	PublishChangeStrategy();
	InvokeRepeating("CheckStrategyState", 1, 1);
}

function CheckStrategyState() {
	var strategy : String;
	var player = GameController.instance.player;
	
	if(!player) {
		strategy = "Roaming";
	} else if(!stunned) {
		var vectorToPlayer = (player.transform.position - transform.position);
		var distanceToPlayer = vectorToPlayer.magnitude;
		
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

function OnHit() {
	stunned = true;
	CheckStrategyState();
}

function OnRecover() {
	stunned = false;
	CheckStrategyState();
}

function Die() {
	Destroy(gameObject);
	GameController.instance.RegisterKill();
}

function PublishChangeStrategy() {
	SendMessage("OnChangeAIStrategy", currentStrategy, SendMessageOptions.DontRequireReceiver);
}