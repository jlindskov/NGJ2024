using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    public int rippleCount = 10;
    public GameObject rippleRingPrefab;
    public float stoneSpawn = 0.4f;
    public Ease stoneSpawnEase = Ease.InElastic;
    public Vector3 stoneSpawnSize = new Vector3(0.5f, 0.5f, 0.5f);
    public float spawnTime = 1f; 
    public Ease spawnEase = Ease.OutElastic;
    private Renderer rend; 
    public CircleCollider2D circleCollider2D;

    private float endRadius; 
    void Start()
    {
        StartCoroutine(DestroyAfterTime());
        rend = GetComponent<Renderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        endRadius = circleCollider2D.radius;
    }

 


    IEnumerator DestroyAfterTime()
    {
        Vector3 previousScale = Vector3.zero;
        float previousAlpha = 1;
        float previousRadius = 0;
        
        var stone = Instantiate(rippleRingPrefab, transform.position, Quaternion.identity,this.transform);
        stone.transform.localScale = stoneSpawnSize;
        
        yield return stone.transform.DOScale(0, stoneSpawn).SetEase(stoneSpawnEase).WaitForCompletion();
        
        
        for (int i = 1; i <= rippleCount; i++)
        {
            var ripple = Instantiate(rippleRingPrefab, transform.position, Quaternion.identity,this.transform);
            ripple.transform.localScale = previousScale;
            var rippleRenderer = ripple.GetComponent<Renderer>();
            rippleRenderer.material.color = new Color(0,0,0,previousAlpha);


            rippleRenderer.material.DOFade(1 - ((float)(i - 1)/rippleCount), spawnTime).SetEase(spawnEase).WaitForCompletion();
         
            yield return ripple.transform.DOScale((float)i/rippleCount, spawnTime).SetEase(spawnEase).WaitForCompletion();
            previousScale = ripple.transform.localScale;
            previousAlpha = 1 - ((float)(i - 1)/rippleCount);
        }

        yield return new WaitForSeconds(5f); 
       // yield return transform.DOScale(0, 1).SetEase(spawnEase).WaitForCompletion();
        
        Destroy(gameObject);
     
    }

}
