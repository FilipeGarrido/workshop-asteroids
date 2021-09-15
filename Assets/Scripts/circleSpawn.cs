using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circleSpawn : MonoBehaviour
{
    public GameObject prefabCircle;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Instantiate(prefabCircle, transform.position , Quaternion.identity);
        }
    }
}
