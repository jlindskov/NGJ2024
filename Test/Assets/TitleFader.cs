using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TitleFader : MonoBehaviour
{
    public float alpha = 0.5f; 
    public float fadeDuration =1.0f;
    public Ease fadeInEase = Ease.Linear;
    public Ease fadeOutEase = Ease.Linear;
    private Renderer rendere; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        rendere = GetComponent<Renderer>(); 
        FadeLoop();
    }
    
    private bool shouldContinue = true;
    
    // Method to stop the fading loop
    public void StopFadingLoop()
    {
        rendere.material.DOKill(); 
    }
    
    void FadeLoop()
    {
        // Fade in
        if (!shouldContinue)
            return;
        rendere.material.DOFade(1.0f, fadeDuration).SetEase(fadeInEase)
            .OnComplete(() =>
            {
                // Fade out
                rendere.material.DOFade(alpha, fadeDuration).SetEase(fadeOutEase)
                    .OnComplete(() =>
                    {
                        // Restart the loop
                        FadeLoop();
                    });
            });
    }

}
