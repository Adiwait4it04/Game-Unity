using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    public float playerspeed = 20;
    public float jumpforce = 20;
    public bool isGrounded;
    public LayerMask groundLayerMask;
    private Animator anim;
    private SpriteRenderer SpriteRenderer;
    public Transform Respawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Respawnpoint.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(x: horizontal * playerspeed, rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(x: rb.velocity.x, y: jumpforce);
        }
        if(rb.velocity.x != 0)
        {
            anim.SetBool(name: "isMoving", value: true);
        }
        else
        {
            anim.SetBool(name: "isMoving", value: false);
        }
        if(horizontal<0)
        {
            SpriteRenderer.flipX = true;
        }
        else if(horizontal>0)
        {
            SpriteRenderer.flipX = false;
        }
        isGrounded = (bool)Physics2D.Raycast(origin: (Vector2)transform.position, direction: Vector2.down, distance: 0.5f, (int)groundLayerMask);
        Debug.DrawRay(start: transform.position, dir: Vector3.down * 0.5f, Color.red);
        anim.SetBool(name: "isGrounded", isGrounded);
    }
    private void OnTriggerEnter2D(Collider2D other)
    { 
      if(other.tag=="Blink")
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "Respawn")
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = Respawnpoint.position;
    }
}