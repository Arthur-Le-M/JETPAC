using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawnerScript : MonoBehaviour
{
    public float frequenceSpawn;
    public Transform borneDroite;
    public Transform borneGauche;
    public List<GameObject> listeItems;
    private float currentTime;
    private int indiceRandomItem;
    private Vector3 posRandomItem;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = frequenceSpawn;
        print(listeItems.Count);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        if(currentTime <= 0){
            posRandomItem = new Vector3(Random.Range(borneDroite.position.x, borneGauche.position.x), transform.position.y, 0f);
            indiceRandomItem = Random.Range(0, listeItems.Count);
            print(indiceRandomItem);
            Instantiate(listeItems[indiceRandomItem], posRandomItem, Quaternion.identity);
            currentTime = frequenceSpawn;
        }
    }  
}
