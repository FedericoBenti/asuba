﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PController2D : Player
{
    public float defaultMovementSpeed = 5f;  
    public float runMultipler = 2;
    public Rigidbody2D rb; 
    public Camera cam;
    public int extrajump;
    public int extrajumpValue;

    private bool Grounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask WhatsIsGround;
   
        
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extrajump = extrajumpValue;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Grounded = Physics2D.OverlapCircle(groundCheck.position , checkRadius , WhatsIsGround);  
       // Grounded = true;
        Movement_character();

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        base.Update();
    }
    void Movement_character()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float movementSpeed = defaultMovementSpeed;
            movementSpeed *= runMultipler;
        }
        if(Grounded == true)
        {
            extrajump = extrajumpValue;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extrajump > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
            extrajump--;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extrajump == 0 && Grounded == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        }
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f ,0f);
        transform.Translate( movement * Time.deltaTime * defaultMovementSpeed);
    }
    
}
