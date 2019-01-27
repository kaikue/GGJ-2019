using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

	public float shootDistance;
	public float shootTime;
	public float shotSpeed;
	private bool destroyed = false;

	public Transform bulletSpawnPoint;
	public GameObject bulletPrefab;
	public GameObject explosionPrefab;

	private GameObject player;

    private void Start()
    {
		player = GameObject.FindGameObjectWithTag("Player");
		StartCoroutine(TargetPlayer());
    }
	
	private IEnumerator TargetPlayer()
    {
		if (!destroyed)
		{

			float playerDist = Vector2.Distance(transform.position, player.transform.position);
			if (playerDist < shootDistance)
			{
				Shoot();
			}

			yield return new WaitForSeconds(shootTime);
			yield return TargetPlayer();
		}
	}

	private void Shoot()
	{
		Vector2 direction = (player.transform.position - bulletSpawnPoint.transform.position).normalized;

		GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.velocity = direction * shotSpeed;
	}

	public void BlowUp()
	{
		Instantiate(explosionPrefab, transform);
		destroyed = true;
	}
}
