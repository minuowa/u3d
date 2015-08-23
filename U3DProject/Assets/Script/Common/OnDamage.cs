using UnityEngine;
using System.Collections;

public class OnDamage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log(other.name);
    }
    void OnCollisionEnter(Collision collision)
    {
        UnityEngine.Debug.Log(collision.collider.name);
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }
}
