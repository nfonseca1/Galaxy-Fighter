using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;

    MeshRenderer meshRenderer;
    BoxCollider boxCollider;

    float destroyDelay = 1.5f;

    void Start()
    {
        AddNonTriggerBoxCollider();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void AddNonTriggerBoxCollider()
    {
        boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        deathFX.SetActive(true);
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        Invoke("Destroy", destroyDelay);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
