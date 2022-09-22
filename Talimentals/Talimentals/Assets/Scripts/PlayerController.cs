using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator anim;

    private float speedAddition;
    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        speedAddition = 2;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();    
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        moveSpeed = Mathf.Clamp(moveDirection.magnitude, 0.0f, 1.0f);
    }

    void Move()
    {
        rb.velocity = moveDirection * (moveSpeed + speedAddition);
    }

}
