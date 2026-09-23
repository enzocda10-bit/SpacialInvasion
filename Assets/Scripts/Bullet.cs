using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Rigidbody rb;
    [SerializeField] private GameObject impactEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(gameObject, 5.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        SpawnImpact(collision);
        Destroy(gameObject);
    }

    private void SpawnImpact(Collision collision)
    {
        Vector3 point = transform.position;
        Vector3 normal = -transform.forward;

        if(collision.contactCount > 0)
        {
            ContactPoint contact = collision.GetContact(0);
            point = contact.point;
            normal = contact.normal;
        }

        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, normal);
        var impactEffect = Instantiate(impactEffectPrefab, point + normal * 0.02f, rotation);
        Destroy(impactEffect, 0.5f);
    }
}
