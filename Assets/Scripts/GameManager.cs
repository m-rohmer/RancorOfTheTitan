using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public GameObject rainTile;		// rain prefab
	public GameObject windTile;		// wind prefab
	public GameObject growthTile;	// growth prefab
	private List<Trap> trapList;
	
	void Awake () {

		if (instance == null)
			instance = this;

		else if (instance != this)
			Destroy(gameObject);

		//DontDestroyOnLoad(gameObject);

		trapList = new List<Trap>();

		GameObject[] gameObjectLongHole = GameObject.FindGameObjectsWithTag("LongHole");
		GameObject[] gameObjectDeepHole = GameObject.FindGameObjectsWithTag("DeepHole");
		GameObject[] gameObjectVine = GameObject.FindGameObjectsWithTag("Vine");

		foreach(GameObject g in gameObjectLongHole){
			trapList.Add((Trap) g.GetComponent<Trap>());
		}
		foreach(GameObject g in gameObjectDeepHole){
			trapList.Add((Trap) g.GetComponent<Trap>());
		}
		foreach(GameObject g in gameObjectVine){
			trapList.Add((Trap) g.GetComponent<Trap>());
		}
	}

	// Create the given power at the given location
	public void CreatePower (string power, Vector3 location){

		GameObject powerChoice = null;

		switch ( power){

			case "Rain" :
				powerChoice = rainTile;
				break;

			case "Wind" :
				powerChoice = windTile;
				break;

			case "Growth" :
				powerChoice = growthTile;
				break;
		}

		Instantiate(powerChoice, location, Quaternion.identity);

		foreach(Trap trap in trapList)

			//check if the trap can be solved by the power
			if(trap.trapsSolved.Contains(power))

				//check if the power and the trap are aligned
				if (Math.Abs(location.x - trap.transform.position.x) <= trap.AcceptedDistanceHorizontal)

					//check if the power is above the trap
					if((location.y - trap.transform.position.y > 0) && (location.y - trap.transform.position.y <= trap.AcceptedDistanceVertical))

						//if every condition is ok the trap is disabled
						trap.Disable();

	}

    public void ReactiveTrap(Vector3 location)
    {

        Trap trapToEnable = null;

        foreach (Trap trap in trapList)

            //check if the power and the trap are aligned
            if (Math.Abs(location.x - trap.transform.position.x) <= trap.AcceptedDistanceHorizontal)

                //check if the power is above the trap
                if ((location.y - trap.transform.position.y > 0) && (location.y - trap.transform.position.y <= trap.AcceptedDistanceVertical))

                    if (trap.GetType() == typeof(DeepHole))
                    {
                        //if every condition is ok the trap will be enabled
                        trapToEnable = trap;
                        break;
                    }

        if (trapToEnable)
        {
            DeepHole deepHole = (DeepHole)trapToEnable;
            deepHole.Enable();
        }

    }
}

