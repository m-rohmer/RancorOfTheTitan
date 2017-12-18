using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour {

    private Transform player;       // Reference to the player's transform.
    private Vector3 initPlayerPos;  // Initial position of Atalante
    private Vector3 diffPos;        // Difference between Atalante's starting position and background's
    private float coefX = 10f;        // Background's degrees of offset (for X and Y axes)
    private float coefY = 1f;

    // Use this for initialization
    void Awake () {
        // Setting up the reference.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initPlayerPos = player.position;
        diffPos = transform.position - player.position;
    }

    void FixedUpdate()
    {
        TrackPlayer();
    }

    void TrackPlayer()
    {
        Vector3 offset = (initPlayerPos - player.position);
        offset.x /= coefX;
        offset.y /= coefY;
        Vector3 target = diffPos - (transform.position - player.position) + offset;

        // Set the camera's position to the target position with the same z component.
        transform.Translate(target);
    }
}
