using UnityEngine;

public class FallingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * (Constants.BackgroundYMvmtVel * Time.deltaTime);

        if (transform.position.y < Constants.DespawnY)
        {
            Destroy(gameObject);
        }
    }
}
