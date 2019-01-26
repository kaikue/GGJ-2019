using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	private float SPEED = 5.0f;
    public float projectile_speed;
    public GameObject projectile;

    private Rigidbody2D rb;
    private float lastLook = 1; //right
    //private const float SPRITE_SIZE_OFFSET = 20 / 1000;  //pixel count offered up by artist when "dynamically assign" didn't... work
    private const float SPRITE_SIZE_OFFSET = 0.7f;

    public bool injured = false;
    public bool killed = false;

    public _GLOBAL_GAME_DATA data;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	
    private void FixedUpdate()
    {
        if (!killed)
        {
            //MOVEMENT
            Vector2 velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * SPEED;
            rb.velocity = velocity;
            //rb.MovePosition(rb.position + rb.velocity * Time.deltaTime);

            //FIRING
            if (Input.GetAxis("Horizontal") != 0)
                lastLook = Mathf.Sign(Input.GetAxis("Horizontal"));
            //Debug.Log("Axis=" + Input.GetAxis("Horizontal").ToString() + " Last look x=" + lastLook.ToString());
            if (Input.GetKeyDown(KeyCode.Space))
            {

                GameObject shot = Instantiate(projectile, new Vector2(transform.position.x + lastLook * SPRITE_SIZE_OFFSET, transform.position.y), Quaternion.identity);
                Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
                rb.velocity = transform.right * projectile_speed * lastLook;
                //Debug.Log("velocity=" + rb.velocity);
            }
        }
    }

    public void HandleHit()
    {
        //Player was shot, deal with it
        if(injured)
        {
            //end game
            data.levelSuccess[data.level] = false;
            ++data.level;
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            injured = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Explosion"))
        {
            //end game
            data.levelSuccess[data.level] = false;
            ++data.level;
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void death()
    {
        killed = true;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
    }
}
