using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 dir;
    [SerializeField] int damage;

    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
