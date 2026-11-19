using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public TMP_Text finalScore;
    public TMP_Text wonLostText;
    public TMP_Text ammo;
    private GUIButtonHandler menuScript;
    private PlayerController playerControllerScript;
    private PlayerScore playerScoreScript;
    private WeaponManager weaponManagerScript;
    private int score;
    private int lives;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        weaponManagerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponManager>();
        menuScript = GameObject.FindGameObjectWithTag("Menu").GetComponent<GUIButtonHandler>();
        playerScoreScript = gameObject.GetComponent<PlayerScore>();

        menuScript.hideAllMenus();
    }

    // Update is called once per frame
    void Update()
    {
        updateAmmo();
    }

    public void loseLife()
    {
        //The playerControllerScript adjusts the remaining lives of the player
        playerControllerScript.changeLife(-1);

        //Clears on screen effects
        //clearEffects();
    }

    public void gameOver()
    {
        menuScript.showGameOver();
        showFinalScore();
    }

    //Block controller calls this whenever a block is broken
    public void updateFinalScore()
    {
        lives = playerControllerScript.getRemainingLives();
        score = playerScoreScript.getScore();

        finalScore.text = "Final Score: " + score.ToString();
    }

    private void updateAmmo()
    {
        ammo.text = "Ammo: " + weaponManagerScript.getCurAmmo().ToString() + "/" + weaponManagerScript.getMaxAmmo().ToString();
    }

    public void showFinalScore()
    {
        //Change final score to visible
        finalScore.alpha = 1f;
    }
}
