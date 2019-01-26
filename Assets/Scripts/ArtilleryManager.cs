using UnityEngine;
using System.Collections;

public class ArtilleryManager : MonoBehaviour
{
    public float spawnTime;
    public float targetTime;
    public GameObject targetSprite;
    public GameObject explosion;
    public GameObject playerObject;

    private GameObject curTarget;
    private GameObject curExplosion;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        targetPosition = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y);
        targetRotation = new Quaternion(0, 0, 0, 0);
        curTarget = (GameObject)Instantiate(targetSprite,targetPosition,targetRotation);
        Invoke("Replace", targetTime);
    }

    void Replace()
    {
        Destroy(curTarget);
        curExplosion = (GameObject)Instantiate(explosion, targetPosition, targetRotation);
        Destroy(curExplosion, 1f);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
