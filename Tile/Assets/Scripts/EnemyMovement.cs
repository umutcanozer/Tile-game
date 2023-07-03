using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody2D enemyRgb2d;
    [SerializeField] float moveSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        enemyRgb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRgb2d.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Platform") return;
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
        
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
    }


}
