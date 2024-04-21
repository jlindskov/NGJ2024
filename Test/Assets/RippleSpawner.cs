using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RippleSpawner : MonoBehaviour
{
    public StudioEventEmitter noiseEvent;
    public StudioEventEmitter musicEvent; 
    
    public LayerMask mask; 
    public GameObject ripplePrefab;
    public Vector3 spawnRotation = new Vector3(90, 0, 0);
    private Camera camera;
    public GameObject titleScreen;
    
    [Header("Title Screen Animation Durations")]
    public float titleScreenRemoveDuration = 1f; 
    public Ease titleScreenRemoveEase = Ease.OutElastic;
    
    [Header("Emitter Spawn Durations")]
    public float emitterSpawnDuration = 1f; 
    public Ease emitterSpawnEase = Ease.OutElastic;
    
    
    private bool gameStarted = false;

    private void Start()
    {
        camera = Camera.main;
        StartCoroutine(StartGame());
    }
    
    private bool inputReceived = false;
    public IEnumerator StartGame()
    {

        noiseEvent.Play();
        var emitters = FindObjectsOfType<Emitter>();

        foreach (var emitter in emitters)
        {
            emitter.gameObject.SetActive(false);
        }
        
        titleScreen.SetActive(true);
        
        while (!inputReceived)
        {
            // Check for input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space key pressed!");
                inputReceived = true;
            }
            yield return null;
        }

        titleScreen.GetComponent<TitleFader>().StopFadingLoop();
        titleScreen.GetComponent<Renderer>().material.DOFade(0, titleScreenRemoveDuration -0.2f)
            .SetEase(titleScreenRemoveEase);
        yield return titleScreen.transform.DOScale(4, titleScreenRemoveDuration).SetEase(titleScreenRemoveEase).WaitForCompletion();
        titleScreen.SetActive(false);
        
        musicEvent.Play();
        foreach (var emitter in emitters)
        {
            emitter.gameObject.SetActive(true);
            emitter.transform.DORotate(new Vector3(0, 0, 360), emitterSpawnDuration + 0.5f, RotateMode.FastBeyond360)
                .SetEase(Ease.OutElastic); // Apply easing
            
            yield return emitter.transform.DOScale(0, emitterSpawnDuration).From().SetEase(emitterSpawnEase).WaitForCompletion();
        }
        
    
        
        yield return new WaitForSeconds(0.5f);
        gameStarted = true;
    }

    private void OnDestroy()
    {
        musicEvent.Stop();
        noiseEvent.Stop();
    }

    //make a updateloop that when you click with your mouse it spawns a ripple
    void Update()
    {
       if (!gameStarted) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
       
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
