using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public GameObject player;
    public GameObject fusee;
    private Text scoreText;
    private Text essenceEnCours;
    private int essenceTotal;
    private Image essenceProgressBar;
    private Image vieProgressBar;
    private Image fuelJetpackProgressBar;
    private Text textMunition;
    private int vieMax;
    private float playerFuelMax;
    public GameObject panelGameOver;
    public GameObject panelPause;
    public Text textScoreGameOver;
    public Text textLevelGameOver;

    private bool isPaused;
    void Start()
    {
        isPaused = false;
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        essenceEnCours = GameObject.Find("Essence").GetComponent<Text>();
        essenceTotal = ScriptFusee.tailleReservoir;
        essenceProgressBar = GameObject.Find("essenceFuseeFilled").GetComponent<Image>();
        fuelJetpackProgressBar = GameObject.Find("Fuel").GetComponent<Image>();
        vieProgressBar = GameObject.Find("vieFilled").GetComponent<Image>();
        textMunition = GameObject.Find("Munition").GetComponent<Text>();

        vieMax = player.GetComponent<PlayerMovement>().nbVie;
        playerFuelMax = player.GetComponent<PlayerMovement>().fuelJetpacMax;

    }

    // Update is called once per frame
    void Update()
    {
        essenceEnCours.text = "Level : " + essenceTotal.ToString();
        scoreText.text = "Score : " + PlayerMovement.score.ToString();
        textMunition.text = player.GetComponent<PlayerMovement>().munition.ToString();

        fuelJetpackProgressBar.fillAmount = ((player.GetComponent<PlayerMovement>().fuelJetpac*100)/playerFuelMax) / 100;
        essenceProgressBar.fillAmount = (float)fusee.GetComponent<ScriptFusee>().essenceEnCours / ScriptFusee.tailleReservoir;
        vieProgressBar.fillAmount = (float)player.GetComponent<PlayerMovement>().nbVie / vieMax;

        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused == true){
                resume();
            }
            else{
                pause();
            }
        }
    }

    public void afficherGameOver(){
        textScoreGameOver.text = "SCORE : " + PlayerMovement.score.ToString();
        textLevelGameOver.text = "LEVEL : " + essenceTotal.ToString();
        panelGameOver.SetActive(true);
    }

    public void retryButton(){
        PlayerMovement.score = 0;
        ScriptFusee.tailleReservoir = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void quitButton(){
        SceneManager.LoadScene("MainMenu");
    }

    public void pause(){
        Time.timeScale = 0;
        panelPause.SetActive(true);
        isPaused = true;
    }
    public void resume(){
        Time.timeScale = 1;
        panelPause.SetActive(false);
        isPaused = false;
    }

}
