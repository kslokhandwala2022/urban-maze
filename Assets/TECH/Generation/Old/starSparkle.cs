using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starSparkle : MonoBehaviour
{
    float maxSize = 1;
    float minSize = .5f;
    float speed = 7;

    Vector3 max;
    Vector3 min;
    bool grow = false;

    // Start is called before the first frame update
    void Start()
    {
        maxSize = Random.Range(.6f, .9f);
        minSize = Random.Range(.05f, .3f);
        speed = Random.Range(5, 10);
        max = new Vector3(maxSize, maxSize, maxSize);
        min = new Vector3(minSize, minSize, minSize);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.localScale.x >= maxSize || gameObject.transform.localScale.x <= minSize)//reverse
        {
            grow = !grow;
        }

        if (grow)
        {
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, max, speed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, min, speed * Time.deltaTime);
        }
    }
}
