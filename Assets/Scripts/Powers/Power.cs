using UnityEngine;

public class Power : MonoBehaviour {

    protected float time; 
    protected float duration; //time before the power is destroy

    public virtual void Start(){

        time = 0;
        duration = 2.5f; 
    }

    public virtual void Update(){

        if(!CheckIfActive())
            DestroyImmediate(gameObject);
    }

    // Check if the power is still active
    public  bool CheckIfActive(){

        time += Time.deltaTime;

        if (time > duration)
            return false;
        
        return true;
    }
}
