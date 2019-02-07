using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform spawner;
    [SerializeField] EnemyMovement enemyGroup;

    bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            EnemyMovement group = Instantiate(enemyGroup, spawner.transform.position, spawner.rotation);
            Destroy(group, 10f);
            group.thrustSpeed = 8f;
            group.rotationSpeed = 35f;
        }
    }
}
