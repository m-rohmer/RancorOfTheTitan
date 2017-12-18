using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// This long Hole is a trap, rain can solve it
/// </summary>
public class LongHole : Trap
{
    int currentWaterLevel;
    int maxWaterLevel = 50;
    bool currentlyFilling;

    private Animator anim;

    public void Awake()
    {
        anim = GetComponent<Animator>();

        isSolved = false;

        currentWaterLevel = 0;
    
        currentlyFilling = false;

        AcceptedDistanceHorizontal = 2;
        AcceptedDistanceVertical = 5;

        trapsSolved = new List<string>(){"Rain"};
    }

    override public void Update(){

        if(currentlyFilling)

            FillWater();
    }

    override public void Disable(){
        currentlyFilling = true;
        anim.SetTrigger("isFilling");
    }

    private void FillWater()
    {
        currentWaterLevel += 1;

        if (currentWaterLevel >= maxWaterLevel)
        {
            isSolved = true;
            currentlyFilling = false;
            anim.SetTrigger("isFull");
        }
    }
}
