using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerEnnemieScript : MonoBehaviour
{
    public Transform borneHaut;
    public Transform borneBas;
    private GameObject ennemie;
    public float frequenceMin;
    public float frequenceMax;
    private float currentTime;
    private Vector3 positionEnnemie;
    private float positionRandomX;
    private int indiceRandomEnnemie;
    public List<GameObject> listeEnnemie;
    public bool inverse;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = Random.Range(frequenceMin, frequenceMax);

    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1f * Time.deltaTime;
        if(currentTime <= 0f){
            positionRandomX = Random.Range(borneBas.position.y, borneHaut.position.y);
            positionEnnemie = new Vector3(transform.position.x, positionRandomX ,transform.position.y);
            indiceRandomEnnemie = Random.Range(0, listeEnnemie.Count);
            if(inverse == true){
                ennemie = Instantiate(listeEnnemie[indiceRandomEnnemie], positionEnnemie, transform.rotation);
                }
            else{
                Instantiate(listeEnnemie[indiceRandomEnnemie], positionEnnemie,transform.rotation);
            }
            
            currentTime = Random.Range(frequenceMin, frequenceMax);
        }
    }
}
