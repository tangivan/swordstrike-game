using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{

    private SimplePlayer playerScript;
    private Vector3 targetPosition;

    public float speed;
    public int damage;

    private bool animDone;
    private Animator anim;

    public GameObject castSound;
    public GameObject hitSound;

    public GameObject hitParticles;

    private Vector3 movementVector = Vector3.zero;

    public bool soundless;
    public bool greenFireBall;

    public int offsetX, offsetY;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SimplePlayer>();
        targetPosition = playerScript.transform.position;
        if (!greenFireBall)
        {
            targetPosition.x += offsetX;
            targetPosition.y += offsetY;
        }
        anim = GetComponent<Animator>();
        animDone = false;
        if(!soundless)
          Instantiate(castSound, transform.position, Quaternion.identity);
        movementVector = (targetPosition - transform.position).normalized * 75;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetTrigger("prepare");

        if (animDone == true)
        {
            
            if (Vector2.Distance(transform.position, targetPosition) > .1f)
            {
                //transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                transform.position += movementVector * speed * Time.deltaTime;
            }
            else
            {
                Instantiate(hitSound, transform.position, Quaternion.identity);
       //         Instantiate(hitSound, transform.position, Quaternion.identity);
        //        Instantiate(hitSound, transform.position, Quaternion.identity);
       //         Instantiate(hitParticles, transform.position, Quaternion.identity);
                Destroy(gameObject);  
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript.TakeDamage(damage);
    //        Instantiate(hitSound, transform.position, Quaternion.identity);
     ///       Instantiate(hitSound, transform.position, Quaternion.identity);
         Instantiate(hitSound, transform.position, Quaternion.identity);
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if(collision.tag == "Wall")
        {
      //      Instantiate(hitSound, transform.position, Quaternion.identity);
       //     Instantiate(hitSound, transform.position, Quaternion.identity);
         Instantiate(hitSound, transform.position, Quaternion.identity);
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
       
    }


    void attackCo()
    {
      //  anim.SetTrigger("prepare");
       // yield return new WaitForSeconds(1f);
        animDone = true;
        
    }
}
