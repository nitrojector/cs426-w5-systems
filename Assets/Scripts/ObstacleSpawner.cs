using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private float normalspawn = 0.9f;
   

    public float SpawnInterval = 5.0f;
    private float _t = 0.0f;


    public Vector2 HorizontalRange = new(-5.0f, 5.0f);
    public Vector2 sizeRqange = new(2f, 6f);

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


        float location = UnityEngine.Random.Range(HorizontalRange.x, HorizontalRange.y);

        float size = UnityEngine.Random.Range(sizeRqange.x, sizeRqange.y);

        Obstacle obstacle = Instantiate(prefab, pos, Quaternion.identity);
        obstacle.SetupObstacle(location, size);

       

        //Debug.Log("Percent: " + _normalSpawnPerc + " Vert:" + _vertCakeSpawnPerc);

        float r = UnityEngine.Random.value;
        if (r < normalspawn)
        {
        }
        else
        {
            Debug.Log("Horizontal");
            
        }
    }
}
