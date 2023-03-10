using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alienController : MonoBehaviour
{
    //events
    //when hitting edge
    public delegate void alienEdge();
    public static event alienEdge OnAlienEdge;

    //laser for shooting player
    public GameObject laser;

    //how much score this alien is worth
    public int score;

    //getting animator
    private Animator alienAnimator;
    private Rigidbody2D myRigidbody2D;


    //for destroying object
    public bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        alienAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();

    }

    void Update(){
        if(exploded == true){
            Destroy(myRigidbody2D.gameObject);
            Debug.Log("exploding");
        }  
            
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //shooting at player
        if(gameObject.name == "Alien 3(Clone)"){
            if(Random.Range(0, 1000) > 998.0f){
                alienAnimator.SetTrigger("shooting");
                Instantiate(laser, transform.position, Quaternion.identity);
            }
        }



 
    }
    
    void OnTriggerEnter2D(Collider2D other) 
	{   
        if(other.tag == "Edge"){
            //Debug.Log("alien trigger");
            OnAlienEdge();
        } else if(other.tag == "Wall"){
            Destroy(other.gameObject);
        }

	}

    private void OnCollisionEnter2D()
    {   
        alienAnimator.SetTrigger("exploding");
    }


}
