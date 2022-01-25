using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemie : MonoBehaviour
{
    public GameObject player;
    public float timeBeforeDetroy; 
    public float speed;
    public int vie;
    private float currentTime;
    public List<Sprite> listeModels;
    private int indiceModels;
    public GameObject spriteEnnemie;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = timeBeforeDetroy;
        player = GameObject.Find("Player");
        indiceModels = Random.Range(0, listeModels.Count);
        GetComponentInChildren<SpriteRenderer>().sprite = listeModels[indiceModels];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1f, 0f, 0f)*Time.deltaTime*speed);

        currentTime -= 1f * Time.deltaTime;
        if(currentTime <= 0f){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "projectile"){
            vie --;
            Destroy(other.gameObject);
            if(vie <= 0){
                PlayerMovement.score += 100;
                Destroy(gameObject);
            }
            
        }
    }
}
