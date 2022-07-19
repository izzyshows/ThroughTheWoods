using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Animator animator;
    public int direction;
    //four directions 0,1,2,3
    //        down, left, up, right

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 dirVector;

    private void FixedUpdate() {
        if (direction == 0) dirVector = new Vector2(0.0f, -1.0f);
        if (direction == 1) dirVector = new Vector2(-1.0f, 0.0f);
        if (direction == 2) dirVector = new Vector2(0.0f, 1.0f);
        if (direction == 3) dirVector = new Vector2(1.0f, 0.0f);

            rb.velocity = dirVector.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Arrow"){
            Physics2D.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider2D>());
            return;
        }

        Vector2 newPosition = new Vector2(
            transform.position.x - dirVector.x * 0.1f,
            transform.position.y - dirVector.y * 0.1f);

            rb.MovePosition(newPosition);
            speed = 0f;
    }
}
