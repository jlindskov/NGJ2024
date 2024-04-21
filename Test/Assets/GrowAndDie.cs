using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GrowAndDie : MonoBehaviour
{
    public float duration = 1f; 
    public Vector3 size = new Vector3(1.5f, 1.5f, 1.5f);

    private void OnEnable()
    {
        StartCoroutine(GrowAndDieEffect());
    }


    public IEnumerator GrowAndDieEffect()
    {
        gameObject.GetComponent<Renderer>().material.DOFade(0,duration).SetEase(Ease.Linear);
        yield return transform.DOScale(size, duration).SetEase(Ease.Linear).WaitForCompletion();
        Destroy(gameObject);
    }

   
}
