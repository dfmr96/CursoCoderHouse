using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Looking, Moving
}
public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyState state;
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] float angularSpeed;
    [SerializeField] float distanceToStop;
    private void Update()
    {
        switch (state)
        {
            case EnemyState.Looking:
                RotateEnemy();
                break;
            case EnemyState.Moving:
                RotateEnemy();
                if (distanceToStop < CheckDistance())
                {
                    MoveEnemy();
                }
                break;
        }

    }

    private void RotateEnemy()
    {
        Quaternion rot = Quaternion.LookRotation((player.transform.position - transform.position));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, angularSpeed * Time.deltaTime);
    }

    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private float CheckDistance()
    {
        float distance = (player.transform.position - transform.position).magnitude;
        return distance;
    }
}
