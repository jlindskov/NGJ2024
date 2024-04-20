using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;


public class RippleSpawner : MonoBehaviour
{
    public StudioEventEmitter musicEvent; 
    public LayerMask mask; 
    public GameObject ripplePrefab;
    public Vector3 spawnRotation = new Vector3(90, 0, 0);
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        musicEvent.Play();
    }

    //make a updateloop that when you click with your mouse it spawns a ripple
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, mask);

            // Check if the ray hits any collider in the scene
            if (hit.collider != null)
            {
                // Get the hit point
                Vector2 spawnPosition = hit.point;

                // Instantiate the prefab at the hit point
                Instantiate(ripplePrefab, spawnPosition, Quaternion.identity);
            }
        }
        
    }
    
}
