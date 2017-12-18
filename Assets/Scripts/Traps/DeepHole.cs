using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepHole : Trap {
	
	public void Awake () {
		
		isSolved = false;

		AcceptedDistanceHorizontal = 1;
		AcceptedDistanceVertical = 5;

		trapsSolved = new List<string>(){"Wind"};
	}
	
	override public void Update () {}

	override public void Disable(){
		isSolved = true;
	}

	public void Enable(){
		isSolved = false;
	}
}
