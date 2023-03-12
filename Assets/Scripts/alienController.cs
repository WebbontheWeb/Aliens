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

    // Start is called before the first frame update
    void Start()
    {
        alienAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //shooting at player
        if(gameObject.tag == "Shooter"){
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

    //can be called by animation event
    public void DestroyAlien(){
        Destroy(myRigidbody2D.gameObject);
    }


}
