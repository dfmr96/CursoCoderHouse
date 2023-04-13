using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Idle, Chasing
}
public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyState state;
    [SerializeField] GameObject player;
    [SerializeField] float speed;
    [SerializeField] float angularSpeed;
    [SerializeField] float distanceToStop;

    private void Start()
    {
        state = EnemyState.Idle;
    }
    private void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chasing:
                if (player == null) return;
                RotateEnemy();
                MoveEnemy();
                break;
        }

    }

    private void RotateEnemy()
    {
        Vector3 faceDir = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        Quaternion rot = Quaternion.LookRotation(faceDir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, angularSpeed * Time.deltaTime);
    }

    private void MoveEnemy()
    {
        Vector3 target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private float CheckDistance()
    {
        float distance = (player.transform.position - transform.position).magnitude;
        return distance;
    }

    public void SetChasing(GameObject go)
    {
        player = go;
        state = EnemyState.Chasing;
    }

    public void StopChasing()
    {
        state = EnemyState.Idle;
    }
}
