using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;
    CharacterController instance;

    public AudioSource jumpSound;
    public AudioSource fruitCatch;
    public AudioSource ouch;

    public float jumpForce;
    public int sprintSpeed;
    public int regSpeed;
    private int moveSpeed;
    private float canbeHurt = 0f;
    public Vector3 movement;

    public GameObject fire;

    public static int fruitCount;
    public static int health = 5;

    public bool isGrounded = false;
    public bool isSprinting = false;

    void OnAwake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Sprint();

        if (isSprinting == true)
        {
            moveSpeed = sprintSpeed;
            anim.SetBool("isSprint", true);
        }
        else { moveSpeed = regSpeed; }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Speed", movement.sqrMagnitude);
        movement.Normalize();

        if (isSprinting == true)
        {
            moveSpeed = sprintSpeed;
        }
        else { moveSpeed = regSpeed; }

        Jump();
        transform.position += movement * moveSpeed * Time.deltaTime;

        if(canbeHurt > 0f)
        {
            canbeHurt -= Time.deltaTime;
        }

        Fire();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
            jumpSound.Play();
        }
        else { anim.SetBool("isJump", false); }
    }

    void Sprint()
    {
        if (Input.GetButton("Shift"))
        {
            anim.SetBool("isSprint", true);
            isSprinting = true;
        }
        else
        {
            anim.SetBool("isSprint", false);
            isSprinting = false;
        }
    }

    private void OnTriggerExit2D(Collider2D fruit)
    {
        if (fruit.gameObject.tag == "fruit")
        {
            fruitCount++;
            fruitCatch.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && canbeHurt < 0.01f)
        {
            health--;
            Debug.Log(health);
            StartCoroutine(Hurt());
            canbeHurt = 3.0f;
            ouch.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && canbeHurt <0.01f)
        {
            health--;
            Debug.Log(health);
            StartCoroutine(Hurt());
            canbeHurt = 3.0f;
            ouch.Play();
        } else if (collision.gameObject.tag == "Boss" && canbeHurt < 0.01f)
            {
                health--;
                Debug.Log(health);
                StartCoroutine(Hurt());
                canbeHurt = 3.0f;
                ouch.Play();
            }
        }

    IEnumerator Hurt()
    {
        anim.SetBool("isHurt", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isHurt", false);
    }

    public void Fire()
    {
        if (Input.GetButtonDown("z"))
        {
            anim.SetBool("isFight", true);
            Instantiate(fire, transform.position, Quaternion.identity);
  
            }
            else
            { anim.SetBool("isFight", false); }
        }
    }



