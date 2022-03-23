using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up * (.2f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {

            //other.GetComponent<Wealth>().updateSpeed();
            Destroy(this.gameObject);
        
        }
    }

}
