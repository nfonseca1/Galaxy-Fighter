using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float thrustSpeed = 5f;
    [SerializeField] float rotationSpeed = 45f;

    [SerializeField] Transform parent;
    [SerializeField] GameObject deathFX;
    [SerializeField] int scorePerHit = 10;
    [SerializeField] int hits = 5;

    ScoreBoard scoreBoard;

    void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, 0, 0));
        transform.Translate(new Vector3(0, -thrustSpeed * Time.deltaTime, 0), Space.Self);
    }

    private void OnParticleCollision(GameObject other)
    {
        scoreBoard.ScoreHit(scorePerHit);
        hits--;
        if(hits <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(fx, 2f);
        Destroy(gameObject);
    }
}
