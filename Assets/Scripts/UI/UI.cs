using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TMP_Text tutorialText;
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public TMP_Text gameText;
    public AudioSource gameMusic;
    public AudioSource gameOverMusic;
    public AudioSource gameStartMusic;
    public PlayerScript playerPrefab;
    public AsteroidWave spawnerPrefab;
    public Asteroid asteroidPrefab;
    public Background _background;
    public int score;
    public int lives = 3;
    public float initialRespawnTime = 3.0f;
    public float respawnTime = 1.0f;
    public float invulnTime = 3.0f;
    public int tutoTime = 100;
    private bool gameOver = false;
    private bool gameStart = true;
    void Awake()
    {    
        tutorialText.text = "";
        scoreText.text = "";
        livesText.text = "";
        gameText.text = "";
        gameMusic.Play();
        Asteroid.asteroidDestoyEvent += AsteroidDestroy;
        PlayerScript.playerLivesEvent += PlayerLives;
    }
    void OnDestroy(){
       Asteroid.asteroidDestoyEvent -= AsteroidDestroy;
       PlayerScript.playerLivesEvent -= PlayerLives;
    }
    void AsteroidDestroy(){
       score += 10;
       ScoreUpdate();
    }

    void ScoreUpdate()
    {
       scoreText.text = "Score: " + score.ToString();
     }
    void PlayerLives(){
        
        lives --;
        
        if(lives > 0){
            LivesUpdate();
        }else{
            GameOver();
        }
       
    }
    void LivesUpdate(){
        livesText.text = "Lives: " + lives.ToString();
        Invoke(nameof(Respawn) , this.respawnTime);
    }

    void Respawn(){
        this.playerPrefab.transform.position = Vector3.zero;
        this.playerPrefab.gameObject.layer = LayerMask.NameToLayer("Invulnerability");
        this.playerPrefab.gameObject.SetActive(true);
        respawnTime = 1.0f;
        Invoke(nameof(TurnOnCollision),invulnTime);
    }    

    void TurnOnCollision(){
         this.playerPrefab.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    void GameOver(){
        gameOverMusic.Play();
        livesText.text = "";
        gameText.text = "Game Over \r\n\n Press Enter to Play Again \r\n\n Press ESC to exit";
        gameOver = true;
        _background.backgroundRigidbody.position = Vector3.zero;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }  
        if(gameStart){
            gameText.text = "Press Enter to Start \r\n\n Press ESC to Exit";
            tutorialText.text = "Move with the up key, turn with the left and right keys and shoot with the spacebar";
            if(Input.GetKeyDown(KeyCode.Return)){
                this.spawnerPrefab.gameObject.SetActive(true);
                gameText.text ="";
                gameStartMusic.Play();
                respawnTime = initialRespawnTime;
                ScoreUpdate();
                LivesUpdate();
                gameStart = false;
            }
        }else{
            tutorialText.text = "";
        } 
        if(gameOver){
            if(Input.GetKeyDown(KeyCode.Return)){
                lives = 3;
                score = 0;
                gameText.text = "";
                gameStartMusic.Play();
                respawnTime = initialRespawnTime;
                ScoreUpdate();
                LivesUpdate();
                gameOver = false;
            }
        }
    }
}
