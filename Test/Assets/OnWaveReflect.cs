using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWaveReflect : MonoBehaviour
{
    public GameObject ripplePrefab;
    public Collider2D collider2D;

    private List<Ripple> ripples = new List<Ripple>();


    public void Start()
    {
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var rippleRing = other.GetComponent<RippleRing>();
        if (rippleRing)
        {
            var ripple = rippleRing.GetComponentInParent<Ripple>();
            
            if (!ripples.Contains(ripple) && !ripple.gameObject.CompareTag("DontCollide"))
            {
                var point = collider2D.bounds.ClosestPoint(ripple.gameObject.transform.position);
                var obj = Instantiate(ripplePrefab, point, Quaternion.identity);
                obj.tag = "DontCollide";
                ripples.Add(ripple);
            }
        }
    }
}
