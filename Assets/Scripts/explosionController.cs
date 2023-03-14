using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionController : MonoBehaviour
{
    // Start is called before the first frame update
    public float length = 0.5f;
    void Start()
    {
        StartCoroutine(selfDestruct());
    }

    IEnumerator selfDestruct()
    {   
        yield return new WaitForSeconds(length);
        Destroy(gameObject);
    }
}
