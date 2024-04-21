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
    public float spinDuration = 1f;
    public Ease spinEaseType= Ease.OutElastic;

    public float bounceStrength = 1; 
    public float bounceDuration = 1; 
    public float bounceElasticity = 1; 
    public void Emit(Ripple ripple)
    {
        if (!ripples.Contains(ripple))
        {
            transform.DOKill();
            transform.localScale = new Vector3(1, 1, 1); 
            RuntimeManager.PlayOneShot(onRippleEmit, transform.position);
//            Debug.Log("Emitting ripple");
            ripples.Add(ripple);
            StartCoroutine(DestroyAfterTime());
            transform.DORotate(new Vector3(0, 0, 360), spinDuration, RotateMode.FastBeyond360)
                .SetEase(spinEaseType); // Apply easing
            transform.DOPunchScale(Vector3.one * bounceStrength, bounceDuration, 1, bounceElasticity);
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
