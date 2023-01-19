using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool gameOver;
    public bool gameTime;
    bool gameplaySegment = false;

    
    private float hozMovement;
    private float elapsedTime;
    private float startTime = 12f;

    private int livesValue;

    TimeSpan timePlaying;

    private Rigidbody2D rb;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI loseText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI instructionsText;

    public AudioSource audioSource;
    public AudioClip startSound;
    public AudioClip backgroundMusic;
    public AudioClip hitSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    public ParticleSystem explodeEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOver = false;
        gameTime = false;

        livesValue = 3;
        livesText.text = "Health: " + livesValue;
        
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = backgroundMusic;
        PlaySound(startSound);

        StartCoroutine(gamePacer());
    }

    IEnumerator gamePacer()
    {
        yield return new WaitForSeconds(2);

        titleText.enabled = false;
        instructionsText.enabled = false;

        gameplaySegment = true;

        livesText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);
        
        audioSource.time = 25f;
        audioSource.Play();
        audioSource.SetScheduledEndTime(AudioSettings.dspTime+(35f-25f));


        yield return new WaitForSeconds(10);

        gameTime = true;
        gameplaySegment = false;
        livesText.enabled = false;
        timerText.enabled = false;
        EndGame();

        //yield return new WaitForSeconds(2);
        //Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameplaySegment) 
        {
            elapsedTime = Time.time - startTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);

            string timePlayingStr = "Time: " + timePlaying.ToString("ss'.'ff");
            timerText.text = timePlayingStr;
        }

        if (audioSource.time > 35f) 
        {
            audioSource.Stop();
        }
    }

    void FixedUpdate()
    {
        hozMovement = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(hozMovement * speed, 0);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.collider.tag == "Obstacle")
        {
            if (gameOver == false)
            {
                livesValue -= 1;
                livesText.text = "Health: " + livesValue;
                
                PlaySound(hitSound);
                explodeEffect.Play();

                Destroy(collision.collider.gameObject);
                CheckLose();
            }
            else
            {
                if (gameTime == false)
                {
                    PlaySound(hitSound);
                    explodeEffect.Play();
                }

                livesText.text = "Health: 0";
                Destroy(collision.collider.gameObject);
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void CheckLose()
    {
        if (livesValue <= 0)
        {   
            gameOver = true;
        }
    }

    void EndGame() 
    {
        if (gameOver == true)
        {
            PlaySound(loseSound);
            loseText.gameObject.SetActive(true);
        }
        else
        {
            PlaySound(winSound);
            winText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        
    }
}
