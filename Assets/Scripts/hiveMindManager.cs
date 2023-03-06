using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hiveMindManager : MonoBehaviour
{
    private int moveTimer = 0;
    //tracks the movement direction and how far
    public float moveVelocity = 1f;
    //for tracking speed
    private int speedometer = 0;

    //how long till the next move
    public float moveDelay = 50.0f;

    //for building waves of aliens
    public int rows = 5;
    public int columns = 11;
    private int aliens = 0;

    //actual aliens
    public GameObject alien1;
    public GameObject alien2;
    public GameObject alien3;

    //tracking score
    private int score = 0;
    private int highScore = 0;
    //score text
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;


    // Start is called before the first frame update
    void Start()
    {
        //alienController.OnAlienDestroyed += SwitchVelocity;
        alienController.OnAlienEdge += SwitchVelocity;
        bulletController.OnAlienDestroyed += WaveTracker;

        if(PlayerPrefs.HasKey("highScore")){
            highScore = PlayerPrefs.GetInt("highScore");
            updateScore(0);
        }
        

        //setting aliens to the amount of aliens
        aliens = rows * columns;
        WaveBuilder();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        moveTimer++;
        if(moveTimer >= moveDelay){
            //transform.position = new Vector3(transform.position.x + moveVelocity, transform.position.y, transform.position.z);
            transform.Translate(moveVelocity, 0.0f, 0.0f);

            //resetting to continue
            moveTimer = 0;
        }
    }

    //tracks how far along the wave is and when a new one should be spawned
    //probably also tracks score
    void WaveTracker(int scored)
    {
        aliens--;
        updateScore(scored);

        //making the delay between moves smaller
        if(aliens < (rows * columns)/2 && speedometer == 0){
            moveDelay *= 0.5f;
            speedometer++;
            Debug.Log("Speeding Up");
        } else if(aliens < (rows * columns)/4 && speedometer == 1){
            moveDelay *= 0.5f;
            speedometer++;
        }

        if(aliens == 0){
            transform.position = new Vector3(-15.0f, transform.position.y, transform.position.z);
            WaveBuilder();
        }
    }

    void SwitchVelocity()
    {   
        //Debug.Log("hive mind trigger");
        moveVelocity = moveVelocity * -1;

        //moving down
        transform.Translate(0.0f, -0.4f, 0.0f);
        //transform.position = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);

    }

    //builds waves of aliens based on rows and columns
    void WaveBuilder()
    {
        for(int row = 0; row < rows; row++){
            for(int column = 0; column < columns; column++){
                if(row < 1){
                    Instantiate(alien3, new Vector3(transform.position.x + column*2, transform.position.y - row*2, transform.position.z), Quaternion.identity, gameObject.transform);
                } else if(row >= 1 && row < 3){
                    Instantiate(alien2, new Vector3(transform.position.x + column*2, transform.position.y - row*2, transform.position.z), Quaternion.identity, gameObject.transform);
                } else{
                    Instantiate(alien1, new Vector3(transform.position.x + column*2, transform.position.y - row*2, transform.position.z), Quaternion.identity, gameObject.transform);
                }
            }
        }
    }

    void updateScore(int scored)
    {   
        score += scored;
        if(score > highScore){
            highScore = score;
        }

        //setting score
        scoreText.text = "Score: " + string.Format("{0:0000}", score);
        highScoreText.text = "High Score: " + string.Format("{0:0000}", highScore);
    }

    //saving when end of game
    void OnDestroy(){
        PlayerPrefs.SetInt("highScore", highScore);
    }
    
}
