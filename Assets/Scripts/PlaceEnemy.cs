using System.Collections;
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

    public float topLimit;
    public float bottomLimit;
    public float leftLimit;
    public float rightLimit;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemyCount; ++i)
        {
            float x = Random.Range(leftLimit, rightLimit);
            float y = Random.Range(bottomLimit, topLimit);
            GameObject newEnemy = Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity);
            EnemyActions actionsScript = newEnemy.GetComponent<EnemyActions>();
            actionsScript.player = player;
            actionsScript.projectile = projectile;
            actionsScript.projectile_speed = projectile_speed;
            actionsScript.reload_speed = reload_speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
