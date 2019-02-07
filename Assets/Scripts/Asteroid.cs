using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] ParticleSystem asteroidExplosion;
    [SerializeField] int health = 5;

    int hitCount = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3(0, 0, 10);
        transform.Rotate(rotation * Time.deltaTime);
    }

    private void OnParticleCollision(GameObject other)
    {
        print("Hit");
        hitCount++;
        if(hitCount >= health)
        {
            ParticleSystem instance = Instantiate(asteroidExplosion, transform.position, transform.rotation);
            Destroy(instance.gameObject, 5f);
            Destroy(gameObject);
        }
    }
}
