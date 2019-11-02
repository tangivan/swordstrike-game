using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public GameObject orbitAround;
    public float speed; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(orbitAround.transform.position, Vector3.forward, speed * Time.deltaTime);
        transform.rotation = Quaternion.identity;
    }

    
}
