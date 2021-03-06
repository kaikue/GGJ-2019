﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceEnemy : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public int enemyCount;
    public float projectile_speed;
    public float reload_speed;
    public GameObject projectile;
    public float decomposeTime;

    public GameObject field;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer fieldGraphic = field.GetComponent<SpriteRenderer>();
        for (int i = 0; i < enemyCount; ++i)
        {
            float x = Random.Range(fieldGraphic.transform.position.x - fieldGraphic.size.x / 2.0f, fieldGraphic.transform.position.x + fieldGraphic.size.x / 2.0f);
            float y = Random.Range(fieldGraphic.transform.position.y - fieldGraphic.size.y / 2.0f, fieldGraphic.transform.position.y + fieldGraphic.size.y / 2.0f);
            GameObject newEnemy = Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity);
            EnemyActions actionsScript = newEnemy.GetComponent<EnemyActions>();
            actionsScript.player = player;
            actionsScript.projectile = projectile;
            actionsScript.projectile_speed = projectile_speed;
            actionsScript.reload_speed = reload_speed;
            actionsScript.decomposeTime = decomposeTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
