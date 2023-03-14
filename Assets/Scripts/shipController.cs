using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipController : MonoBehaviour
{
    public GameObject bullet;
    public int bulletDelay = 15;
    private int bulletTracker = 0;
    public Transform shootingOffset;

    float horizonalAxis;
    public float speed;
    public float leftLimit;
    public float rightLimit;

    private Rigidbody2D myRigidbody2D;
    private Animator shipAnimator;

    private AudioSource audioSource;
    public AudioClip shooting;
    public AudioClip exploding;


    // Start is called before the first frame update
    void Start()
    {   
        shipAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        laserController.OnPlayerDestroyed += StartExploding;
    }

    void FixedUpdate()
    {
        //moving ship
        horizonalAxis = Input.GetAxis("Horizontal");
        Vector3 newPosition = transform.position + new Vector3(horizonalAxis, 0, 0) * speed;
        //keeping ship within boundries
        newPosition.x = Mathf.Clamp(newPosition.x, leftLimit, rightLimit);

        transform.position = newPosition;

        bulletTracker++;
    }
  

    // Update is called once per frame
    void Update()
    {

        //shooting
        if (Input.GetKeyDown(KeyCode.Space) && bulletDelay < bulletTracker){
            shipAnimator.SetTrigger("shooting");
            GameObject shot = Instantiate(bullet, shootingOffset.position, Quaternion.identity);
            //Debug.Log("Bang!");

            Destroy(shot, 3f);
            bulletTracker = 0;

            audioSource.clip = shooting;
            audioSource.Play();
        }
    }

    void StartExploding(){
        shipAnimator.SetTrigger("exploding");
        audioSource.clip = exploding;
        audioSource.Play();
        //Debug.Log("Game Over");
    }


    public void DestroyShip(){
        Destroy(myRigidbody2D.gameObject);
    }

    // private void OnCollisionEnter2D()
    // {   
    //     shipAnimator.SetTrigger("exploding");
    //     Debug.Log("ship animation");
    // }

}

