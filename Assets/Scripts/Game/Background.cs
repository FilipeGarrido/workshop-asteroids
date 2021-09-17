using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Rigidbody2D backgroundRigidbody;
    public PlayerScript _player;
    public float parallaxFX = 0.05f;

    void Awake()
    {
        backgroundRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update(){

        Vector2 speed = _player.playerRigidbody.velocity;

        backgroundRigidbody.velocity = speed * parallaxFX;

        Debug.Log(_player.playerRigidbody.position.ToString());

    }
}
