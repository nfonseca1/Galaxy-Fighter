using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;

    MeshRenderer meshRenderer;
    Collider collider;

    float destroyDelay = 1.5f;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    private void OnParticleCollision(GameObject other)
    {
        deathFX.SetActive(true);
        meshRenderer.enabled = false;
        collider.enabled = false;
        Invoke("Destroy", destroyDelay);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
