using UnityEngine;

public class FallingScript : MonoBehaviour
{
    public float DespawnY = -6.0f;
    public float velocity = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * velocity * Time.deltaTime;

        if (transform.position.y < DespawnY)
        {
            Destroy(gameObject);
        }
    }
}
