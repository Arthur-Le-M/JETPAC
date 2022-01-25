using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed;
    public float timeBeforeDestruction=2f;
    private float currentTime = 0f;
    
    void Start()
    {
        currentTime = timeBeforeDestruction;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1f*Time.deltaTime;
        if(currentTime <= 0f){
            Destroy(gameObject);
        }
        transform.position += transform.right * Time.deltaTime * speed;

    }
    

    
}
