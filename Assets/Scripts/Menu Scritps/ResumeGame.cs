using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour {

    public Transform canvas;

    public void Resume()
    {
        canvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
