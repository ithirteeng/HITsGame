using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 4f;
    private Vector2 movement;
    private Animator animator;
    public SavedPosition position;

    private void Start()
    {
       animator = GetComponent<Animator>();
       rb = GetComponent<Rigidbody2D>();
       transform.position = position.initialValue;
    }

    void Update()
    {
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (speed * Time.fixedDeltaTime));
    }

    private void OnApplicationQuit()
    {
        Vector3 update;
        update.x = 6.477f;
        update.y = -1.3f;
        update.z = 0;
        position.initialValue = update;
    }
}
