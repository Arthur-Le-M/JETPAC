using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptItem : MonoBehaviour
{
    public bool consumable;
    public int addVie;
    public int addMunition;
    public int addFuel;
    public bool loaded;
    // Start is called before the first frame update
    void Start()
    {
        loaded = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            if(consumable == false){
                loaded = true;
            }
            
        }
    }
}
