using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speed = 30f;

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}