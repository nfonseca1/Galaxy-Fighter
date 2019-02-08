using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] bool play = false;
    [SerializeField] float speed = 5f;
    [SerializeField] float rotation = 45f;

    [SerializeField] Transform[] spawners;
    [SerializeField] EnemyMovement enemyGroup;

    bool isTriggered = false;

    private void Update()
    {
        if (play)
        {
            OnTriggerEnter(new Collider());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            foreach(var spawner in spawners)
            {
                EnemyMovement group = Instantiate(enemyGroup, spawner.transform.position, spawner.rotation);
                Destroy(group, 10f);
                group.thrustSpeed = speed;
                group.rotationSpeed = rotation;
            }
        }
    }
}
