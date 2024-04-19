using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public enum Direction
    {
        Left = 0,
        Right = 1
    }

    private Vector2 xLimit = new Vector2(-100, 100);
    public float speed = 1;
    public Direction direction = Direction.Right;
    public float WaveSize = 1;
    public BoxCollider collider;

    public float drainSpeed = 5;

    public bool dirty = false; 
    private void Start()
    {
        collider = GetComponent<BoxCollider>(); 
    }
    
    void Update()
    {
        dirty = false; 
        transform.localScale = new Vector3(200, 1, WaveSize);
        var newPos = new Vector3(transform.position.x,transform.position.y, transform.position.z);
        
        if (newPos.x - (transform.localScale.z /2) <= xLimit.x)
        {
            direction = Direction.Right;
        }
        else if (newPos.x + (transform.localScale.z /2) >= xLimit.y)
        {
            direction = Direction.Left;
        }

        if (direction == Direction.Right)
        {
            newPos.x += speed * Time.deltaTime;  
        }
        else
        {
            newPos.x -= speed * Time.deltaTime;  
        }
        
        transform.position = newPos; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Wave>(out var otherWave))
        {
            
            bool shouldDestroy = WaveSize == otherWave.WaveSize;
            
            if (direction != otherWave.direction)
            {
                if (otherWave.WaveSize == WaveSize && !dirty)
                {
                    Destroy(gameObject);
                    Destroy(otherWave.gameObject);
                }
                else
                {
                    dirty = true;
                    otherWave.dirty = true;
                    var totalWaveSize = WaveSize + otherWave.WaveSize;
                    WaveSize = totalWaveSize / 2;
                    otherWave.WaveSize = totalWaveSize / 2;
                }
              
                // WaveSize -= otherWave.WaveSize;
                //
                // if (shouldDestroy)
                // {
                //     Destroy(gameObject); // Destroy the other wave
                // }
            }
            else
            {
                if (WaveSize > otherWave.WaveSize)
                {
                    WaveSize += otherWave.WaveSize;
                    Destroy(otherWave.gameObject); // Destroy the other wa
                }
                
            }
        }
    }
}
