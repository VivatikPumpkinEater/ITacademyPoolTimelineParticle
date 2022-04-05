using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineProperty : MonoBehaviour
{
    [SerializeField] private bool _destroyEnd = false;
    
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Destroy(gameObject);
        }
    }

    public void OnEnd()
    {
        if (_destroyEnd)
        {
            Destroy(gameObject);
        }
    }
}
