﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.HandleHit();
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            EnemyActions ea = collision.gameObject.GetComponent<EnemyActions>();
            ea.Kill();
        }
        Destroy(gameObject);
    }
}
