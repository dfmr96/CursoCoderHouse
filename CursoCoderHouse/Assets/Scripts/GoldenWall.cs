using UnityEngine;

public class GoldenWall : MonoBehaviour
{
    [SerializeField] BoxCollider areaAvailable;
    [SerializeField] float wallTimer;
    [SerializeField] Vector3 randomPos;
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            wallTimer += Time.deltaTime;

            if (wallTimer >= 2)
            {
                MoveRandom();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        wallTimer = 0;
    }
    private Vector3 GetRandomPosition()
    {
        float randomPosX = Random.Range(areaAvailable.bounds.min.x, areaAvailable.bounds.max.x);
        //float randomPosY = Random.Range(areaAvailable.bounds.min.y, areaAvailable.bounds.max.y);
        float randomPosZ = Random.Range(areaAvailable.bounds.min.z, areaAvailable.bounds.max.z);

        randomPos = new Vector3(randomPosX, transform.position.y, randomPosZ);
        return randomPos;
    }

    private float GetRandomAngle()
    {
        float randomAngle = Random.Range(0, 360);
        return randomAngle;
    }
    private void MoveRandom()
    {
        transform.position = GetRandomPosition();
        transform.rotation = Quaternion.Euler(0, GetRandomAngle(), 0);
    }
}
