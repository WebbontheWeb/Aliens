using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipController : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootingOffset;


    public float speed;
    public float leftLimit;
    public float rightLimit;

    void FixedUpdate()
    {
        //moving ship
        float horizonalAxis = Input.GetAxis("Horizontal");
        Vector3 newPosition = transform.position + new Vector3(horizonalAxis, 0, 0) * speed;
        //keeping ship within boundries
        newPosition.x = Mathf.Clamp(newPosition.x, leftLimit, rightLimit);

        transform.position = newPosition;
    }
  

    // Update is called once per frame
    void Update()
    {

        //shooting
        if (Input.GetKeyDown(KeyCode.Space)){
            GameObject shot = Instantiate(bullet, shootingOffset.position, Quaternion.identity);
            Debug.Log("Bang!");

            Destroy(shot, 3f);
        }
    }

}

