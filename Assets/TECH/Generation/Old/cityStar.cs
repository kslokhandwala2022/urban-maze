using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cityStar : MonoBehaviour
{
    public bool pulse;
    bool grow = false;
    public float size = .3f;
    public float speed = .5f;
    Vector3 max;
    Vector3 min;
    // Start is called before the first frame update
    void Start()
    {
        //TogglePulse();
    }

    // Update is called once per frame
    void Update()
    {

        if (pulse)
        {
            if (gameObject.transform.localScale.x >= max.x || gameObject.transform.localScale.x <= min.x)//reverse
            {
                grow = !grow;
                Debug.Log("switch");
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

    public void TogglePulse()
    {
        max = gameObject.transform.localScale  + new Vector3(size, size, 0);
        min = gameObject.transform.localScale  - new Vector3(size, size, 0);
        pulse = !pulse;
    }
}


