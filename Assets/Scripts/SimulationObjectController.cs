using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SimulationObjectController : MonoBehaviour
{
    public event Action OnDeath;
    
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float turretCooldownInSeconds = 1f;
    [SerializeField]
    private int healthPoints = 3;
    [SerializeField]
    private float invulnerabilitySecondsAfterDamage = 2f;

    private float rotationCooldownInSeconds;
    private float currentTurretCooldown;
    private float currentRotationCooldown;

    private MeshRenderer meshRenderer;
    private Collider boxCollider;
    private MeshRenderer turretRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<Collider>();
        turretRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        rotationCooldownInSeconds = UnityEngine.Random.Range(0f, 1f);
    }

    private void Update()
    {
        currentTurretCooldown += Time.deltaTime;
        currentRotationCooldown += Time.deltaTime;

        if (currentTurretCooldown >= turretCooldownInSeconds)
        {
            Shoot();
            currentTurretCooldown = 0;
        }
        
        if (currentRotationCooldown >= rotationCooldownInSeconds)
        {
            float rotationArc = UnityEngine.Random.Range(0, 360);
            transform.Rotate(0, rotationArc, 0);
            
            rotationCooldownInSeconds = UnityEngine.Random.Range(0f, 1f);
            currentRotationCooldown = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        healthPoints -= 1;

        if (healthPoints <= 0)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
        else
            StartCoroutine(DamageReceivedCoroutine());
    }
    
    private void Shoot()
    {
        var currentTransform = transform;
        GameObject.Instantiate(bulletPrefab, currentTransform.position + currentTransform.forward, currentTransform.rotation, GameManager.Instance.BulletsContainer);
    }
    
    private IEnumerator DamageReceivedCoroutine()
    {
        this.enabled = false;
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        turretRenderer.enabled = false;
        
        yield return new WaitForSeconds(invulnerabilitySecondsAfterDamage);
        
        this.enabled = true;
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
        turretRenderer.enabled = true;
    }
}
