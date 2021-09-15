using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Rigidbody2D asteroidRigidbody;

    void Start()
    {
        asteroidRigidbody.velocity = new Vector2 (1.0f,0.0f);
    }

    void Update()
    {
        
    }
}
