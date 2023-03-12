using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //technique for making sure there isn't a null reference during runtime if you are going to use get component
public class laserController : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;

    public delegate void playerDestroyed(); //passing score of alien
    public static event playerDestroyed OnPlayerDestroyed;

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
        myRigidbody2D.velocity = Vector2.down * speed; 
    }

    //deleting bullet on hit
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.transform.name == "Ship"){
            OnPlayerDestroyed();
            Destroy(myRigidbody2D.gameObject);
        } else if(other.tag == "Wall"){
            Destroy(other.gameObject);
            Destroy(myRigidbody2D.gameObject);
        }
    }
}

