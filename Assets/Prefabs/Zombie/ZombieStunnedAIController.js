#pragma strict

function OnChangeAIStrategy( strategy : String ) {
	enabled = (strategy == "Stunned");
	
	if(enabled) {
		gameObject.animation.Play("Stunned");
		
		yield WaitForSeconds(2);
		
		SendMessage("OnRecover", SendMessageOptions.DontRequireReceiver);
	}
}