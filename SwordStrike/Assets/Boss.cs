using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss : GenericEnemy
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float stopDistance;
    private float attackTime;
    private float OrbAttackTime;

    public GameObject shieldBoss;
    public GameObject trueForm;

    [Header("Rotating Orbs")]
    public GameObject fireOrb;
    public GameObject windOrb;
    public GameObject earthOrb;
    public GameObject waterOrb;

    [Header("Orb Attacks")]
    public GameObject earthOrbClone;
    public GameObject fireOrbClone;
    public GameObject windOrbClone;
    public GameObject waterOrbClone;


    public GameObject fireBall;
    public Transform shotPoint;

    private Vector3 targetPosition;
    private GameObject originElement;

    private GameObject orbAttack;

    [Header("Sounds")]
    public GameObject orbAttackSound;

    private List<GameObject> orbList = new List<GameObject>();
    private bool attacking;
    private bool orbPhaseEnded;
    private Slider HealthBar;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        HealthBar = FindObjectOfType<Slider>();
        HealthBar.maxValue = health;
        HealthBar.value = health;
       
     

        orbList.Add(fireOrb);
        orbList.Add(waterOrb);
        orbList.Add(windOrb);
        orbList.Add(earthOrb);
        attacking = false;
        orbPhaseEnded = false;
        GetComponent<BoxCollider2D>().isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        HealthBar.value = health;
       
        if (Time.time >= attackTime && !attacking)
        {
            attacking = true;
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            targetPosition = new Vector2(randomX, randomY);
         
            attackTime = Time.time + timeBetweenAttacks;
            
            StartCoroutine(TeleportAttackCo());
          
            Instantiate(deathEffect, shieldBoss.transform.position, Quaternion.identity);
            
         

        }

        if (Time.time >= OrbAttackTime && !attacking && !orbPhaseEnded && Vector2.Distance(transform.position, player.position) < stopDistance)
        {
            attacking = true;
            OrbAttackTime = Time.time + 5;
            orbManager();
            if (orbList.Count != 0)
            {
                if (orbList[0] != null)
                {
                    for (int i = 0; i < orbList.Count; i++)
                    {
                        GameObject temp = orbList[i];
                        int randomIndex = Random.Range(i, orbList.Count);
                        orbList[i] = orbList[randomIndex];
                        orbList[randomIndex] = temp;
                    }



                    StartCoroutine(OrbAttack(orbList[0]));
                }
            }
        }
    }


    private void disableRender()
    {
        shieldBoss.GetComponent<Renderer>().enabled = false;
        fireOrb.GetComponent<Renderer>().enabled = false;
        windOrb.GetComponent<Renderer>().enabled = false;
        waterOrb.GetComponent<Renderer>().enabled = false;
        earthOrb.GetComponent<Renderer>().enabled = false;

        fireOrb.GetComponent<Orb>().disableRenders();
        windOrb.GetComponent<Orb>().disableRenders();
        waterOrb.GetComponent<Orb>().disableRenders();
        earthOrb.GetComponent<Orb>().disableRenders();


    }

    private void enableRender()
    {
        shieldBoss.GetComponent<Renderer>().enabled = true;
        fireOrb.GetComponent<Renderer>().enabled = true;
        windOrb.GetComponent<Renderer>().enabled = true;
        waterOrb.GetComponent<Renderer>().enabled = true;
        earthOrb.GetComponent<Renderer>().enabled = true;

        fireOrb.GetComponent<Orb>().enableRenders();
        windOrb.GetComponent<Orb>().enableRenders();
        waterOrb.GetComponent<Orb>().enableRenders();
        earthOrb.GetComponent<Orb>().enableRenders();

    }

    private IEnumerator TeleportAttackCo()
    {
        disableRender();

        Instantiate(deathEffect, shieldBoss.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        transform.position = targetPosition;
        RangedAttack();
        enableRender();
        attacking = false;
    }

    public void RangedAttack()
    {
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotation;

        Instantiate(fireBall, shotPoint.position, shotPoint.rotation);
     }


    IEnumerator OrbAttack(GameObject originElement) 
    {
        if(originElement==earthOrb)
        {
            earthOrb.GetComponent<Renderer>().enabled = false;
            orbAttack = new GameObject();
            orbAttack = Instantiate(earthOrbClone, earthOrb.transform.position, Quaternion.identity);
            originElement = earthOrb;
        }
        else if(originElement == waterOrb)
        {
            waterOrb.GetComponent<Renderer>().enabled = false;
            orbAttack = new GameObject();
            orbAttack = Instantiate(waterOrbClone, waterOrb.transform.position, Quaternion.identity);
            originElement = waterOrb;
        }
        else if(originElement == fireOrb)
        {
            fireOrb.GetComponent<Renderer>().enabled = false;
            orbAttack = new GameObject();
            orbAttack = Instantiate(fireOrbClone, fireOrb.transform.position, Quaternion.identity);
            originElement = fireOrb;
        }
        else if(originElement == windOrb)
        {
            windOrb.GetComponent<Renderer>().enabled = false;
            orbAttack = new GameObject();
            orbAttack = Instantiate(windOrbClone, windOrb.transform.position, Quaternion.identity);
            originElement = windOrb;
        }

       // yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(0.6f);

        Vector3 target = player.transform.position;
       Vector3 savePosition = orbAttack.transform.position;
        float percent = 0;
        Instantiate(orbAttackSound, originElement.transform.position, Quaternion.identity);
 
        while (percent <= 1)
        {
            percent += Time.deltaTime * 1;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;

            orbAttack.GetComponent<CharacterFollower>().enabled = false;
            orbAttack.transform.position = Vector2.Lerp(savePosition, target, formula);
           savePosition = originElement.transform.position;


            yield return null;
        }
        

        originElement.GetComponent<Renderer>().enabled = true;
        Destroy(orbAttack);
        attacking = false;
    }

    void orbManager()
    {
        if(fireOrb.gameObject.activeInHierarchy==false)
        {
            orbList.Remove(fireOrb);
        }
        if(waterOrb.gameObject.activeInHierarchy==false)
        {
            orbList.Remove(waterOrb);
        }
        if(windOrb.gameObject.activeInHierarchy==false)
        {
            orbList.Remove(windOrb);
        }
        if(earthOrb.gameObject.activeInHierarchy==false)
        {
            orbList.Remove(earthOrb);
        }

        if(orbList.Count == 0)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            shieldBoss.gameObject.SetActive(false);
            trueForm.gameObject.SetActive(true);
            orbPhaseEnded = true;
            attacking = false;
        }
    }

    /*
    public override void TakeDamage(float damageAmount, int wepElement)
    {
        switch (element)
        {
            case 1:
                health = health - elementalBonus(damageAmount, wepElement);
                break;
            case 2:
                health = health - elementalBonus(damageAmount, wepElement);
                break;
            case 3:
                health = health - elementalBonus(damageAmount, wepElement);
                break;
            default:
                health = health - elementalBonus(damageAmount, wepElement);
                break;
        }

        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);

            HealthBar.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        HealthBar.GetComponent<Slider>().value = health;
    }
    */
}
