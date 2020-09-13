using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    Animator anim;

    private bool movingRight = true;
    private bool isEnemy = false;

    public Transform groundDetect;

    private Vector2 target;
    private Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, 1f);
        //Debug.Log(groundInfo.collider.gameObject.tag);

        if(groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                anim.SetFloat("Horizontal", -1f);
            } else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                anim.SetFloat("Horizontal", 1f);
            }
        }

    }
}
