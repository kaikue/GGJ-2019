using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public GameObject player;
    private BoxCollider2D playerBox;

    public float projectile_speed;
    public float reload_speed;
    public GameObject projectile;
    private bool gunLoaded = true;
    private bool alive = true;

    private float lastLook = -1;    //start left towards where player starts
    private const float SPRITE_SIZE_OFFSET = 1.5f; //TEMP
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

        if(alive && gunLoaded && playerBox.bounds.Contains(
            new Vector3(playerBox.bounds.center.x, transform.position.y, playerBox.bounds.center.z)))
        {
            //shoot
            GameObject shot = Instantiate(projectile, new Vector2(transform.position.x + lastLook * SPRITE_SIZE_OFFSET, transform.position.y), Quaternion.identity);
            Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * projectile_speed * lastLook;
            Debug.Log("velocity=" + rb.velocity);
            gunLoaded = false;
            StartCoroutine(Reload());
        }
        
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
