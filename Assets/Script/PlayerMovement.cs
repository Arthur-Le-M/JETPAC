using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public GameObject projectile;
    public ParticleSystem jetpack;
    public Collider2D teleporteurDroit;
    public Collider2D teleporteurGauche;
    public Canvas UI;

    public float speed = 4f;
    public float jumpForce = 4f;
    public int nbVie;
    public int nbVieMax = 5;
    public static int score=0;
    public int munitionMax = 15;
    public int munition;
    public float fuelJetpacMax = 5000f;
    public float fuelJetpac;

    private bool justTeleportedRight;
    private bool justTeleportedLeft;
    private bool isLoaded;
    private GameObject itemPorte;

    private Animator animPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        jetpack.Pause();
        justTeleportedRight = false;
        justTeleportedLeft = false;
        isLoaded = false;
        animPlayer = GetComponent<Animator>();

        munition = munitionMax;
        nbVie = nbVieMax;
        fuelJetpac = fuelJetpacMax;
    }

    // Update is called once per frame
    void Update()
    {   
        //Déplacement Perso
        float horizontal = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontal, 0f, 0f) * Time.deltaTime * speed;
        
        if(horizontal != 0){
            animPlayer.SetBool("isRunning", true);
        }
        else{
            animPlayer.SetBool("isRunning", false);
        }

        //Perso regarde vers où il se dirige
        if(horizontal < -0.1){
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if(horizontal > 0.1){
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        

        var em = jetpack.emission;
        //Jetpack
        if(Input.GetButton("Jump")){
            if(fuelJetpac > 0){
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce)*Time.deltaTime, ForceMode2D.Force);
                em.enabled = true;
                jetpack.Play();
                animPlayer.SetBool("isFlying", true);
                fuelJetpac -= 1f * Time.deltaTime;
            }
            
            
        }
        else if(jetpack.isPlaying == true){
            em.enabled = false;
            animPlayer.SetBool("isFlying", false);
        }
        


        //Tir
        if(Input.GetMouseButtonDown(0)){
            if(munition > 0){
                Instantiate(projectile, transform.position, transform.rotation);
                munition --;
            }
            
        }

        //Gestion d'item
        if(isLoaded == true){
            itemPorte.transform.position = transform.position;
            itemPorte.transform.position = new Vector3(itemPorte.transform.position.x, itemPorte.transform.position.y, -0.1f);
            if(Input.GetMouseButtonDown(1)){
            lacherObjetPorte();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other == teleporteurDroit && justTeleportedLeft == false){
            transform.position = new Vector3(teleporteurGauche.transform.position.x, transform.position.y, 0f);
            justTeleportedRight = true;
        }
        else if(other == teleporteurGauche && justTeleportedRight == false){
            transform.position = new Vector3(teleporteurDroit.transform.position.x, transform.position.y, 0f);
            justTeleportedLeft = true;
        }

        if(other.tag == "ennemie"){
            Destroy(other.gameObject);
            nbVie --;
            if(nbVie <= 0){
                gameOver();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other == teleporteurDroit){
            justTeleportedLeft = false;
            print("Fin Teleporte");
        }
        else if(other == teleporteurGauche){
            justTeleportedRight = false;
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "item"){
            if(other.gameObject.GetComponent<ScriptItem>().consumable == false){
                if(isLoaded == false){
                isLoaded = true;
                itemPorte = other.gameObject;
                itemPorte.GetComponent<Collider2D>().isTrigger = true;
                itemPorte.GetComponent<Rigidbody2D>().gravityScale = 0;
                itemPorte.GetComponent<Rigidbody2D>().freezeRotation = true;
                }
            }
            else{
                nbVie += other.gameObject.GetComponent<ScriptItem>().addVie;
                fuelJetpac += other.gameObject.GetComponent<ScriptItem>().addFuel;
                munition += other.gameObject.GetComponent<ScriptItem>().addMunition;
                if(fuelJetpac > fuelJetpacMax){
                    fuelJetpac = fuelJetpacMax;
                }
                if(nbVie > nbVieMax){
                    nbVie = nbVieMax;
                }
                Destroy(other.gameObject);
            }
            
        }
    }

    void lacherObjetPorte(){
        isLoaded = false;
        itemPorte.GetComponent<Collider2D>().isTrigger = false;
        itemPorte.GetComponent<Rigidbody2D>().gravityScale = 1;
        itemPorte.GetComponent<Rigidbody2D>().freezeRotation = false;
        itemPorte.transform.position = new Vector3(itemPorte.transform.position.x, itemPorte.transform.position.y, 0f);
        itemPorte.GetComponent<ScriptItem>().loaded = false;
    }

    void gameOver(){
        UI.GetComponent<UIScript>().afficherGameOver();
        Time.timeScale = 0;
    }
}
