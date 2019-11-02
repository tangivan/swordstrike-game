using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleKnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public Collider2D collider;
    private SimplePlayer simplePlayerScript;

    public GameObject hitParticles;

    public GameObject hitSound;

    Animator cameraAnim;

    public bool isOrb;


    private void Start()
    {
        cameraAnim = Camera.main.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Enemy") && collider.isTrigger)
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            Debug.Log(hit);
            collider.isTrigger = false;
            if (hit != null)
            {
                    Vector2 difference = hit.transform.position - transform.position;
                    difference = difference.normalized * thrust;
                    hit.AddForce(difference, ForceMode2D.Impulse);
                   // collision.isTrigger = false;
                    
                    collision.GetComponent<GenericEnemy>().Knock(hit, knockTime);
                
            }

        }


        if (collision.gameObject.CompareTag("Player") && collider.isTrigger)
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();

                if (hit != null)
                {
                    cameraAnim.SetTrigger("shake");


                    simplePlayerScript = collision.GetComponent<SimplePlayer>();
                if(simplePlayerScript.stagger==false)
                {
                    Instantiate(hitParticles, collision.transform.position, Quaternion.identity);
                    Instantiate(hitSound, collision.transform.position, Quaternion.identity);
                }
                    simplePlayerScript.stagger = true;
                    Vector2 difference = hit.transform.position - transform.position;
                    difference = difference.normalized * thrust;


                    hit.AddForce(difference, ForceMode2D.Impulse);
                    simplePlayerScript.Knock(hit, knockTime);
                    if (isOrb)
                    {
                        simplePlayerScript.TakeDamage(1);
                    }
                }
        }

    }


}