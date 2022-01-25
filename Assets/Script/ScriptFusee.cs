using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptFusee : MonoBehaviour
{

    public GameObject player;
    private bool inTrigger;
    private GameObject itemLoaded;
    public static int tailleReservoir;
    public int essenceEnCours;

    
    void Start()
    {
        essenceEnCours = 0;
        inTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inTrigger == true){
            if(itemLoaded.GetComponent<ScriptItem>().loaded ==false){
                essenceEnCours ++;
                PlayerMovement.score +=200;
                Destroy(itemLoaded);
                inTrigger = false;

            }
        }

        if(essenceEnCours == tailleReservoir){
            gameObject.GetComponent<Animator>().SetBool("fueled", true);
            tailleReservoir ++;
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "item"){
            if(other.gameObject.GetComponent<ScriptItem>().consumable == false){
                inTrigger = true;
                itemLoaded = other.gameObject;
                if(other.gameObject.GetComponent<ScriptItem>().loaded == false){
                    essenceEnCours ++;
                    PlayerMovement.score +=200;
                    Destroy(itemLoaded);
                    inTrigger = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "item"){
            inTrigger = false;
        }
    }
}
