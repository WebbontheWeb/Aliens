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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        //shooting at player
        if(gameObject.name == "Alien 3(Clone)"){
            if(Random.Range(0, 1000) > 998.0f){
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
}
