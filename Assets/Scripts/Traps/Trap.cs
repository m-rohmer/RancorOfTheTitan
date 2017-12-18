using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Abstract class, mother of all kind of traps
/// </summary>
public abstract class Trap : MonoBehaviour
{

    public bool isSolved; // can Atalante cross the trap
    public int AcceptedDistanceHorizontal;
    public int AcceptedDistanceVertical;
    public List<string> trapsSolved;

    public abstract void Disable();
    public abstract void Update();

}
