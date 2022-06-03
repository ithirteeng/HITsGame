using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 4f;
    private Vector2 position;
    private Animator animator;

    private void Start()
    {
       animator = GetComponent<Animator>();
       rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        position.x = Input.GetAxisRaw("Horizontal");
        position.y = Input.GetAxisRaw("Vertical");
        
        animator.SetFloat("Horizontal", position.x);
        animator.SetFloat("Vertical", position.y);
        animator.SetFloat("Speed", position.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + position * (speed * Time.fixedDeltaTime));
    }
}
