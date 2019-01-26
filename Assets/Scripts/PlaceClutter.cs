using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceClutter : MonoBehaviour
{
    public GameObject leaf;
    public int clutterCount;

    public GameObject field;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer fieldGraphic = field.GetComponent<SpriteRenderer>();
        for (int i = 0; i < clutterCount; ++i)
        {
            float x = Random.Range(fieldGraphic.transform.position.x - fieldGraphic.size.x / 2.0f, fieldGraphic.transform.position.x + fieldGraphic.size.x / 2.0f);
            float y = Random.Range(fieldGraphic.transform.position.y - fieldGraphic.size.y / 2.0f, fieldGraphic.transform.position.y + fieldGraphic.size.y / 2.0f);
            Instantiate(leaf, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
