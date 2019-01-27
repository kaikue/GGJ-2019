using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	private float SPEED = 5.0f;
    public float projectile_speed;
    public float deathTime;
    public GameObject projectile;
    public Transform bulletSpawnPoint;

    private Rigidbody2D rb;
    private float lastLook = 1; //right

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
                GameObject shot = Instantiate(projectile, bulletSpawnPoint.transform.position, Quaternion.identity);
                Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
                rb.velocity = transform.right * projectile_speed * lastLook;
                SoldierAudio a = GetComponentInChildren<SoldierAudio>();
                a.PlayGunshot();
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
            StartCoroutine(EndLevel(false));
        }
        else
        {
            //injured = true;
            SoldierAudio a = GetComponentInChildren<SoldierAudio>();
            a.PlayInjury();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Explosion"))
        {
            injured = true;
            //end game
            StartCoroutine(EndLevel(false));
        }

		if (other.gameObject.CompareTag("DestroySpot"))
		{
			//TODO: throw grenade animation
			other.GetComponentInParent<Tank>().BlowUp();
			StartCoroutine(EndLevel(true));
		}
    }

    private IEnumerator EndLevel(bool success)
    {
		if (!success)
		{
			killed = true;
            SoldierAudio a = GetComponentInChildren<SoldierAudio>();
            a.PlayDeath();
            Destroy(gameObject.GetComponent<Rigidbody2D>());
		}

        yield return new WaitForSeconds(deathTime);
        data.levelSuccess[data.level] = success;
        SceneManager.LoadScene("LetterScene");
    }
}
