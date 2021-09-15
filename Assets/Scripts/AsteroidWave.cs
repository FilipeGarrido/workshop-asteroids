using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidWave : MonoBehaviour
{
    public Asteroid prefabAsteroid;
    public int asteroidAmount = 3;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i < asteroidAmount; i++){
            Vector3 position = new Vector3(0.0f,0.0f,0.0f);
            Instantiate(prefabAsteroid, position, Quaternion.identity);
        }
    }
}
