﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private float SPEED = 5.0f;

	private Rigidbody2D rb;

	private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	
    private void FixedUpdate()
    {
        Vector2 velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * SPEED;
		rb.velocity = velocity;
		//rb.MovePosition(rb.position + rb.velocity * Time.deltaTime);
    }
}
