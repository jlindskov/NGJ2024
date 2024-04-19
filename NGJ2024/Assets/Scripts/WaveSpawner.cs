using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    //Make a method that counts the time the player held down the mousebutton 
    //and spawns a wave with the size of the time held down
    private float timeHeldDown = 0;
    private bool isHolding = false;
    public Wave wavePrefab;
    public float waveSpeed = 1;
    public float waveSize = 1;

    public LayerMask groundMask; 

    private Camera mainCamera; 
    private void Start()
    {
       mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
      //  Debug.DrawRay(ray.origin, ray.direction * 200, Color.red);
        if (Input.GetMouseButtonDown(0))
        {
            isHolding = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isHolding = false;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
           RaycastHit hitInfo;
           if (Physics.Raycast(ray, out hitInfo,1000,groundMask )) // Check if the ray hits something
           {
                SpawnWave(hitInfo);
               Debug.DrawLine(mainCamera.transform.position, hitInfo.point, Color.red);
               Debug.Log(Input.mousePosition);
           }
        }

        if (isHolding)
        {
            timeHeldDown += Time.deltaTime;
        }
    }
    
    
    private void SpawnWave(RaycastHit hitInfo)
    {
        Vector3 spawnPos = new Vector3(hitInfo.point.x, 0, 0);
        var wave = Instantiate(wavePrefab, spawnPos, Quaternion.Euler(0f, 90f, 0f));
        
        int dir = Random.Range(0, 1);
        
        if (dir == 0)
        {
            wave.direction = Wave.Direction.Left; 
        }
        else
        {
            wave.direction = Wave.Direction.Right;
        }
        
        wave.speed = waveSpeed / (timeHeldDown + 1); // Slower as time increases
        wave.WaveSize = waveSize * (timeHeldDown + 1); // Bigger as time increases
        timeHeldDown = 0;
    }
    

}
