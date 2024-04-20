using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    public EventReference onRippleEmit;
    List<Ripple> ripples = new List<Ripple>();
    public GameObject ripplePrefab;
    public float maxRadius = 20;
    public float emitterLength; 
    public Ease spawnEase = Ease.Linear;
    public void Emit(Ripple ripple)
    {
        if (!ripples.Contains(ripple))
        {
            RuntimeManager.PlayOneShot(onRippleEmit, transform.position);
            Debug.Log("Emitting ripple");
            ripples.Add(ripple);
            StartCoroutine(DestroyAfterTime());
        }
       
    }
    
    IEnumerator DestroyAfterTime()
    {
        var ripple = Instantiate(ripplePrefab, transform.position, Quaternion.identity,this.transform);
        var rend = ripple.GetComponent<Renderer>();
        rend.material.DOFade(0, emitterLength).SetEase(spawnEase);
        yield return ripple.transform.DOScale(maxRadius, emitterLength).SetEase(spawnEase).WaitForCompletion();
        Destroy(ripple);
    }
}
