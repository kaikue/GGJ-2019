using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public GameObject player;
    private BoxCollider2D playerBox;

    public float projectile_speed;
    public float reload_speed;
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
        playerBox = player.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO - MOVEMENT

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
