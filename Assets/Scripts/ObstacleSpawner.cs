using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private float normalspawn = 0.0f;
   

    public float SpawnInterval = 5.0f;
    private float _t = 0.0f;


    public Vector2 locationRange = new(-6.0f, 6.0f);
    public Vector2 sizeRqange = new(2f, 6f);
    public Vector2 widthRange = new(2f, 6f);


    public float VerticalSpawnOffset = 1.0f;
    public float VerticalMax = 5.0f;

    public Obstacle prefab;

    private void Update()
    {
        _t += Time.deltaTime;
        if (_t < SpawnInterval) return;
        _t = 0.0f;


        float x = 0.0f;
        float y = VerticalMax + VerticalSpawnOffset;
        Vector3 pos = new(x, y, 0.0f);


        float location = UnityEngine.Random.Range(locationRange.x, locationRange.y);
        float size = UnityEngine.Random.Range(sizeRqange.x, sizeRqange.y);

        //Debug.Log("Location: " + location + "Size: " + size);

        Obstacle obstacle = Instantiate(prefab, pos, Quaternion.identity);
       
        float r = UnityEngine.Random.value;
        if (r < normalspawn)
        {
            obstacle.SetupObstacle(size, location);
        }
        else
        {
            Debug.Log("Horizontal");
            //obstacle.SetupObstacle(size, location);

            obstacle.SetupHorizontal(size);
            //float verticicalOffset = UnityEngine.Random.

        }
    }
}
