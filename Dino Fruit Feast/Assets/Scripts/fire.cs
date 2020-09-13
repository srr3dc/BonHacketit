using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    public float speed;

    private Transform enemy;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Boss").transform;

        target = new Vector2(enemy.position.x, enemy.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }

    private void OnColliderEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        { Destroy(gameObject); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        { Boss.enemyHealth--; 
        Destroy(gameObject); }
    }
}
