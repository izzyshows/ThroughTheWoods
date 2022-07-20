using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucindaController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Animator animator;
    public GameObject shot;
    private int direction;
    private float zrot;
    private Arrow arrow;
    public int lucindaState = 0; //0 no bomb, 1 bomb
    private GameObject bomb;

    private void Start() {
        direction = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Vector2 moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));
            rb.velocity = moveInput.normalized * speed;
            animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    private void Update() {

        float x, y;
        x = rb.velocity.x;
        y = rb.velocity.y;
        if (x != 0 || y != 0) {
            if (y < x) if (y < -x) direction = 0;
            if (y > x) if (y < -x) direction = 1;
            if (y > x) if (y > -x) direction = 2;
            if (y < x) if (y > -x) direction = 3;
        }

if (lucindaState == 0)
        if (Input.GetKeyDown("space")) {

            if (direction == 0) zrot = -90f;
            if (direction == 1) zrot = 180f;
            if (direction == 2) zrot = 90f;
            if (direction == 3) zrot = 0f;


            GameObject ar = Instantiate(
                shot,
                new Vector3 (
                    transform.position.x,
                    transform.position.y, 1.0f),
                    Quaternion.Euler(0,0,zrot)
                
            );

            ar.GetComponent<Arrow>().direction = direction;
            if (x != 0|| y != 0) ar.GetComponent<Arrow>().speed += speed;
        }
        if (lucindaState == 1) {
            if (Input.GetKeyDown("space")) {
                            bomb.GetComponent<Bomb>().bombState = 1;
                bomb.transform.SetParent(null);
                lucindaState = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Bomb") {
            bomb = collision.gameObject;
            bomb.transform.SetParent(gameObject.transform);
            bomb.transform.localPosition = 
                new Vector3(0.0f, 0.6f, -1.0f);

            Physics2D.IgnoreCollision(
                collision.collider,
                gameObject.GetComponent<Collider2D>()
            );
            lucindaState = 1;
        }
    }
}
