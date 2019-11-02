using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimplePlayer : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public float speed;
    public Vector3 change;
    private Rigidbody2D rb;

    private Animator anim;

    public float health;

    [HideInInspector]
    public bool stagger;

    [Header("IFrame")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D triggerCollider;
    public SpriteRenderer mySprite;

    [Header("Dash Variables")]
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    public float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public GameObject echo;

    private Vector3 faceRight;
    private Vector3 faceLeft;

    public GameObject dashSound;

    [Header("Weapons")]
    public GameObject AirSword;
    public GameObject FireSword;
    public GameObject EarthSword;
    public GameObject WaterSword;

    private Weapon currentWeapon;


    private SceneTransitions sceneTransitions;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        stagger = false;

        dashTime = startDashTime;

        faceRight = new Vector3(0, 0, 0);
        faceLeft = new Vector3(0, 180, 0);

        currentWeapon = FireSword.GetComponent<Weapon>();
        sceneTransitions = FindObjectOfType<SceneTransitions>();


    }

    private void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (stagger == false)
            UpdateAnimationAndMove();

        if (Input.GetButtonDown("Dash"))
        {
            StartCoroutine(Echo());
            StartCoroutine(DashCo());
            Instantiate(dashSound, transform.position, Quaternion.identity);
            Instantiate(dashSound, transform.position, Quaternion.identity);
            Instantiate(dashSound, transform.position, Quaternion.identity);

        }

        if (Input.GetButtonDown("AirSword") && !currentWeapon.attacking)
        {
            AirSword.SetActive(true);
            FireSword.SetActive(false);
            WaterSword.SetActive(false);
            EarthSword.SetActive(false);
            currentWeapon = AirSword.GetComponent<Weapon>(); ;
        }
        if (Input.GetButtonDown("FireSword") && !currentWeapon.attacking)
        {
            AirSword.SetActive(false);
            FireSword.SetActive(true);
            WaterSword.SetActive(false);
            EarthSword.SetActive(false);
            currentWeapon = FireSword.GetComponent<Weapon>(); ; ;
        }
        if (Input.GetButtonDown("WaterSword") && !currentWeapon.attacking)
        {
            AirSword.SetActive(false);
            FireSword.SetActive(false);
            WaterSword.SetActive(true);
            EarthSword.SetActive(false);
            currentWeapon = WaterSword.GetComponent<Weapon>(); ; ;
        }
        if (Input.GetButtonDown("EarthSword") && !currentWeapon.attacking)
        {
            AirSword.SetActive(false);
            FireSword.SetActive(false);
            WaterSword.SetActive(false);
            EarthSword.SetActive(true);
            currentWeapon = EarthSword.GetComponent<Weapon>(); ; ;
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {

            change.Normalize();
            rb.MovePosition(
                 transform.position + change * speed * Time.deltaTime
                 );


            if (change.x < 0)
            {
                transform.eulerAngles = faceLeft;
            }
            else if (change.x > 0)
            {
                transform.eulerAngles = faceRight;
            }
            anim.SetBool("isRunning", true);
            currentWeapon.isMoving = true;
        }
        else
        {
            anim.SetBool("isRunning", false);
            currentWeapon.isMoving = false;
        }
    }


    public void TakeDamage(float damageAmount)
    {
        if (triggerCollider.enabled)
        {
            health -= damageAmount;
            UpdateHealthUI(health);
        }
        if (health <= 0)
        {
            //    Destroy(gameObject);
            this.enabled = false;
            sceneTransitions.LoadScene("Lose");
        }
    }


    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));

    }


    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            stagger = false;
        }
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }
    private void Dash()
    {
        if (change.x != 0 || change.y != 0)
        {
            Vector2 dashDirection = new Vector2(change.x, change.y);
            rb.velocity = dashDirection * dashSpeed;
        }
        else
        {
            if (transform.eulerAngles == faceRight)
            {
                Vector2 dashDirection = new Vector2(1, 0);
                rb.velocity = dashDirection * dashSpeed;
            }
            else
            {
                Vector2 dashDirection = new Vector2(-1, 0);
                rb.velocity = dashDirection * dashSpeed;
            }

        }
    }

    private IEnumerator DashCo()
    {
        change.Normalize();
        stagger = true;
        Dash();

        yield return new WaitForSeconds(dashTime);
        rb.velocity = Vector2.zero;
        stagger = false;
    }

    IEnumerator Echo()
    {

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * 3;
            GameObject instance = Instantiate(echo, transform.position, Quaternion.Euler(transform.eulerAngles));
            Destroy(instance, 0.1f);
            timeBtwSpawns = startTimeBtwSpawns;

            yield return null;
        }
    }

    void UpdateHealthUI(float currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    public void Heal(float healAmount)
    {
        if (health + healAmount > 5)
        {
            health = 5;
        }
        else
        {
            health += healAmount;
        }
        UpdateHealthUI(health);

    }
}
