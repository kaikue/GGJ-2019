using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public GameObject player;
    private BoxCollider2D playerBox;
	private Rigidbody2D rb;
	private SpriteRenderer sr;

    public float projectile_speed;
    public float reload_speed;
    public float run_speed;
	public float acceleration;
	public float shoot_distance;
    public GameObject projectile;
	public Transform bulletSpawnPoint;
	private bool gunLoaded = true;
    private bool alive = true;

    private float lastLook = -1;    //start left towards where player starts
    public float decomposeTime;
    
    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
        playerBox = player.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
		//TODO - MOVEMENT
		if (alive) Move();

        //FIRING
        //TODO - actually get direction based on movement

        if(alive && gunLoaded && CanTargetPlayer())
        {
            //shoot
            GameObject shot = Instantiate(projectile, bulletSpawnPoint.transform.position, Quaternion.identity);
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * projectile_speed * lastLook;
            Debug.Log("velocity=" + rb.velocity);
            gunLoaded = false;
            StartCoroutine(Reload());
        }
        
    }

	private void Move()
	{
		float playerDist = Vector2.Distance(transform.position, player.transform.position);
		if (playerDist > shoot_distance) return;

		int moveY = 0;

		if (bulletSpawnPoint.position.y > playerBox.bounds.max.y)
		{
			moveY = -1;
		}
		else if (bulletSpawnPoint.position.y < playerBox.bounds.min.y)
		{
			moveY = 1;
		}

		rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0, moveY * run_speed), acceleration);
		if (player.transform.position.x < transform.position.x)
		{
			lastLook = -1;
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		else
		{
			lastLook = 1;
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
	}

	public bool CanTargetPlayer()
	{
		RaycastHit2D raycast = Physics2D.Raycast(bulletSpawnPoint.transform.position, Vector2.right * lastLook, shoot_distance, LayerMask.GetMask("Default"));
		return raycast.collider != null && raycast.collider.gameObject.CompareTag("Player");
	}

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(reload_speed);
        gunLoaded = true;
    }

    public void Kill()
    {
        alive = false;
        //CHANGE SPRITE SET
        //SpriteRenderer s = GetComponent<SpriteRenderer>();
        //s.sprite = Resources.Load<Sprite>("Player/Injured/Playerinj_0001");
        Destroy(gameObject, decomposeTime);
    }

}
