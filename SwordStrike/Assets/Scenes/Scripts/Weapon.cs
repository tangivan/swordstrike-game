
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Weapon : MonoBehaviour
{
    public float angleAddition;
    Vector3 target;
    Vector3 savePosition;

    [HideInInspector]
    public bool attacking;

    public GameObject origin;
    private float speed;

    public float damage;

    //private bool enemyHit;

    public Collider2D collider;

    public GameObject hitParticles;

    public GameObject hitSound;

    Animator cameraAnim;

    [Header("Element: Fire: 1 Water: 2 Air: 3 Earth:4")]
    public int element;

    public bool isMoving;

    public GameObject player;

    

    /********      AttackSpeed variables         **************/
    private float attackSpeed;
    private float bonusCount;
    private float maxAttackSpeed;
    private float baseAttackSpeed;
    private bool isBonus;
    public float increment;
    public GameObject mode1;
    public GameObject mode2;
    public GameObject mode3;
    private float threshold1;
    private float threshold2;
    private float threshold3;



    private void Start()
    {
        target = transform.position;
        attacking = false;
        speed = 300;
    //    enemyHit = false;
        cameraAnim = Camera.main.GetComponent<Animator>();
        maxAttackSpeed = 7;
        isBonus = false;
        baseAttackSpeed = 7;
        attackSpeed = baseAttackSpeed;
        threshold1 = 1;
        threshold2 = 4;
        threshold3 = 7;
        isMoving = false;


    }
    void Update()
    {


        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle + angleAddition, Vector3.forward);


        if (attacking == false)
        {
            transform.rotation = rotation;
            collider.isTrigger = false;
        }



        if (Input.GetMouseButton(0) && attacking == false)
        {
            savePosition = transform.position;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            attacking = true;
            collider.isTrigger = true;
            //StartCoroutine(SwordAttack());
            StartCoroutine(HomingSwordAttack(savePosition, target));
        }
        /* This code rotates based on movement of joystick
        if (attacking == false)
        {
            float temp = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, temp + angleAddition));
        }
        */



        //   Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //  float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //  Quaternion rotation = Quaternion.AngleAxis(angle + angleAddition, Vector3.forward);

            //  Vector2 moveVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;


            // transform.rotation = Quaternion.LookRotation(-Vector3.forward, moveVec);


            /*
            foreach (Touch touch in Input.touches)
            {




                if (touch.phase == TouchPhase.Began)
                {
                    int id = touch.fingerId;
                    if (EventSystem.current.IsPointerOverGameObject(id))
                        return;

                    if (Input.touchCount >= 1 && attacking == false)
                    {
                        //Vector2 direction = Camera.main.ScreenToWorldPoint(touch.position) - transform.position;
                      //  float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                       // Quaternion rotation = Quaternion.AngleAxis(angle + angleAddition, Vector3.forward);

                        savePosition = transform.position;
                        Vector3 target = Camera.main.ScreenToWorldPoint(touch.position);

                        target.z = transform.position.z;
                  //      transform.rotation = rotation;
                        attacking = true;
                        collider.isTrigger = true;
                        StartCoroutine(HomingSwordAttack(savePosition, target));


                    }
                }
            }*/
            /*
            if (!(touch.phase==TouchPhase.Ended) && Input.touchCount == 2 && attacking == false)
            {
                Debug.Log("here");
                Instantiate(hitSound, transform.position, Quaternion.identity);
                Touch temp = Input.GetTouch(1);
                Vector2 direction = Camera.main.ScreenToWorldPoint(temp.position) - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle + angleAddition, Vector3.forward);

                savePosition = transform.position;
                Vector3 target = Camera.main.ScreenToWorldPoint(temp.position);

                target.z = transform.position.z;
                transform.rotation = rotation;
                attacking = true;
                collider.isTrigger = true;
                StartCoroutine(HomingSwordAttack(savePosition, target));
            }
            */



            /*
            if (attackSpeed >= threshold1 && attackSpeed < threshold2)
            {

                mode1.GetComponent<SpriteRenderer>().enabled = true;
                mode2.GetComponent<SpriteRenderer>().enabled = false;
                mode3.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if(attackSpeed >=threshold2 && attackSpeed <threshold3)
            {
                mode1.GetComponent<SpriteRenderer>().enabled = true;
                mode2.GetComponent<SpriteRenderer>().enabled = true;
                mode3.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if(attackSpeed >=threshold3)
            {
                mode1.GetComponent<SpriteRenderer>().enabled = true;
                mode2.GetComponent<SpriteRenderer>().enabled = true;
                mode3.GetComponent<SpriteRenderer>().enabled = true;
            }
            */
    }



    IEnumerator HomingSwordAttack(Vector2 savePosition, Vector2 targetPosition)
    {
    
        float percent = 0;
        while (percent <= 1)
                {
                     percent += Time.deltaTime * attackSpeed;
                    float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
                    transform.position = Vector2.Lerp(origin.transform.position, targetPosition, formula);
           
            yield return null;
                   
                }
        attackSpeed = baseAttackSpeed; 
     
        attacking = false;


        /*    MOBILE CODE
         * rb.velocity = targetPosition * 100;
           yield return new WaitForSeconds(2f);
         */
    }


    IEnumerator flyBack(Collider2D collision)
    {

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(transform.position, origin.transform.position, formula);
            yield return null;
            
        }
       
      //  if (collision != null)
      //  {
    //        collision.isTrigger = true;
     //   }
        collider.isTrigger = true;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collider.isTrigger && collision.isTrigger)
        {
            /****Bonus atk speed stuff ****/
       //     if (attackSpeed <= maxAttackSpeed)
        //        attackSpeed = attackSpeed + increment;
            

            /***** Effects *****/
            cameraAnim.SetTrigger("shake");
            Instantiate(hitParticles, collision.transform.position, Quaternion.identity);
            Instantiate(hitSound, collision.transform.position, Quaternion.identity);

            StopAllCoroutines();
            StartCoroutine(flyBack(collision));
        //    enemyHit = true;
            if (attacking == true)
                collision.GetComponent<GenericEnemy>().TakeDamage(damage, element);
        }
    }

}
