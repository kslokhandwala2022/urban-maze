using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.up * (.2f));
    }

    public AudioClip clip;

    public float volume=0.6f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {

            other.GetComponent<Wealth>().updateSpeed();
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
            Destroy(this.gameObject);
        
        }
    }

}
