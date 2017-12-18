using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Power {


	public override void Start(){
		time = 0;
        duration = 1.5f; 
    }
	
    public override void Update(){

        if(!CheckIfActive()){
			GameManager.instance.ReactiveTrap(transform.position);
            DestroyImmediate(gameObject);
		}
    }
}
