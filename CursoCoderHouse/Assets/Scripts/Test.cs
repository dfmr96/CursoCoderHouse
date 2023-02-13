using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float angularSpeed;
    [SerializeField] bool isShrinked;
    [SerializeField] float shrinkCooldown;
    [SerializeField] float shrinkTimer;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += -transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -angularSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, angularSpeed * Time.deltaTime, 0);
        }

        shrinkTimer += Time.deltaTime;


    }


    private void Shrink()
    {
        if (isShrinked)
        {
            transform.localScale = new Vector3(1, 3, 1);
            isShrinked = false;
            Debug.Log("Vuelve a tamaño original");
        }
        else
        {
            transform.localScale *= 0.5f;
            isShrinked = true;
            Debug.Log("Se achicó");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " posee un Trigger");
        if (other.gameObject.GetComponent<Portal>() != null)
        {
            Debug.Log("Tiene un componente Shrinker");
        }
        if (other.gameObject.CompareTag("Portal"))
        {
            Debug.Log("Entró en portal");
            if (shrinkTimer >= shrinkCooldown)
            {
                Shrink();
                shrinkTimer = 0;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name + " posee un collider");
    }

}
