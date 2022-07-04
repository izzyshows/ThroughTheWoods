﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucindaController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Animator animator;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Vector2 moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));
            rb.velocity = moveInput.normalized * speed;
            animator.SetFloat("Speed", rb.velocity.magnitude);
    }
}
