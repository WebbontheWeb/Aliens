using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //technique for making sure there isn't a null reference during runtime if you are going to use get component
public class bulletController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;

    public delegate void alienDestroyed(int score); //passing score of alien
    public static event alienDestroyed OnAlienDestroyed;

    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Fire();
    }

    // Update is called once per frame
    private void Fire()
    {
        myRigidbody2D.velocity = Vector2.up * speed; 
        Debug.Log("Wwweeeeee");
    }

    //deleting bullet on hit
    private void OnCollisionEnter2D(Collision2D other)
    {   
        Destroy(other.gameObject);
        Destroy(myRigidbody2D.gameObject);

        if(other.transform.name == "Alien 1(Clone)"){
            Debug.Log("destroyed, 10");
            OnAlienDestroyed(10);
            Debug.Log("after, 10");

        } else if(other.transform.name == "Alien 2(Clone)"){
            OnAlienDestroyed(20);
        } else if(other.transform.name == "Alien 3(Clone)"){
            OnAlienDestroyed(40);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) 
	{   
        if(other.tag == "Wall"){
            Destroy(other.gameObject);
            Destroy(myRigidbody2D.gameObject);
        }

	}
}

