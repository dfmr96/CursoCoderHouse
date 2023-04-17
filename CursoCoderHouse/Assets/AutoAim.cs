using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public static AutoAim instance;
    public List<GameObject> nearestEnemies;
    public GameObject nearestEnemy;
    public float radius;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            nearestEnemies.Add(other.gameObject);

            if (nearestEnemy == null)
            {
                nearestEnemy = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            nearestEnemies.Remove(other.gameObject);
        }

        if (other.gameObject == nearestEnemy)
        {
            nearestEnemy = null;
        }
    }

    public void GetNearestEnemy()
    {

        if (nearestEnemy == null) return;

        for (int i = 0; i < nearestEnemies.Count; i++)
        {
            if (Vector3.Distance(nearestEnemies[i].transform.position, transform.position) < Vector3.Distance(nearestEnemy.transform.position, transform.position))
            {
                nearestEnemy = nearestEnemies[i];
            }
        }
    }

    public void AimToNearestEnemy(Transform playerTransform)
    {
        GetNearestEnemy();

        if (nearestEnemy == null) return;

        Vector3 enemyDir = new Vector3(nearestEnemy.transform.position.x - playerTransform.position.x, 0, nearestEnemy.transform.position.z - playerTransform.position.z).normalized;
        Quaternion rot = Quaternion.LookRotation(enemyDir, Vector3.up);
        playerTransform.rotation = rot;
    }

    public void RemoveFromList (GameObject gameObject)
    {
        if (gameObject == nearestEnemy) nearestEnemy = null;
        nearestEnemies.Remove(gameObject);

    }
}
