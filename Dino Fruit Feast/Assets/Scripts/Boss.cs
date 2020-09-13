using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    Rigidbody2D rb2d;
    public float speed;

    Animator anim;

    private Transform target;
    public Transform groundDetect;

    private float time;
    private float startTime = 2f;

    public GameObject projectile;

    public static int enemyHealth = 5;

    // Start is called before the first frame update
    void Start()
    {
        time = startTime;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, 1f);

        if (groundInfo.collider == true && Vector2.Distance(transform.position, target.position) < 7f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        } 

        else if (Vector2.Distance(transform.position, target.position) > 7f && Vector2.Distance(transform.position, target.position) < 15f)
        {
               if (time <= 0.01f)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                time = startTime;
            } else { time -= Time.deltaTime; }



        } 

        if (enemyHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Hurt());
            enemyHealth--;

        } else if (collision.gameObject.tag == "Fire")
        {
            StartCoroutine(Hurt());
            enemyHealth--;
        }
    }

    IEnumerator Hurt()
    {
        anim.SetBool("isHit", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isHit", false);
    }

}
 


