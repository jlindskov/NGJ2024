using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RippleRing : MonoBehaviour
{
    public float fadeTime = 5f;
    public Renderer spriteRenderer;

    public Ripple parentRipple; 

    private void Start()
    {
        parentRipple = GetComponentInParent<Ripple>();
        StartCoroutine(RippleEffect(fadeTime)); 
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Emitter>(out var emitter))
        {
            emitter.Emit(parentRipple);
        }
    }
    
    
    //make a corutine that makes the ripple grow and fade out
    public IEnumerator RippleEffect(float time)
    {
        yield return spriteRenderer.material.DOFade(0,time).SetEase(Ease.InQuad).WaitForCompletion();
       
    }
}
