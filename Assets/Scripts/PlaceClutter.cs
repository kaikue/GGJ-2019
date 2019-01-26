using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceClutter : MonoBehaviour
{
    public GameObject leaf;
    public int clutterCount;

    public float topLimit;
    public float bottomLimit;
    public float leftLimit;
    public float rightLimit;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < clutterCount; ++i)
        {
            float x = Random.Range(leftLimit, rightLimit);
            float y = Random.Range(bottomLimit, topLimit);
            Instantiate(leaf, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
