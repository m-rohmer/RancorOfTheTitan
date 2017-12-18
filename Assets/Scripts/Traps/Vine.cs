using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : Trap {

	private int currentVineLevel;
    private int maxVineLevel = 30;
    private bool currentlyGrowing;

    private Animator anim;

    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();

        isSolved = false;
		
		AcceptedDistanceHorizontal = 2;
        AcceptedDistanceVertical = 4;

		currentVineLevel = 0;
    
        currentlyGrowing = false;

		trapsSolved = new List<string>(){"Growth"};
	}
	
	// Update is called once per frame
	override public void Update () {
		
		if(currentlyGrowing)

            GrowVine();
	}

	override public void Disable(){
        currentlyGrowing = true;
        anim.SetTrigger("isGrowing");
        transform.Translate(new Vector3(0, 3.7f, 0));
    }

	private void GrowVine(){

        currentVineLevel += 1;

        if (currentVineLevel >= maxVineLevel)
        {
            isSolved = true;
            currentlyGrowing = false;
            anim.SetTrigger("isGrown");
        }		
	}
}
