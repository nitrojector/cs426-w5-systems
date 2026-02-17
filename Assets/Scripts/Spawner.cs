using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Cake Prefab;
    public CakePlayer player;

    public Vector2 HorizontalRange = new(-9.0f, 9.0f);

    public float VerticalSpawnOffset = 1.0f;
    public float VerticalMax = 5.0f;

    private float _normalSpawnPercStart = 0.3f;
    private float _vertCakeSpawnPercStart = 0.5f;

    private float _normalSpawnPerc = 0.3f;
    private float _vertCakeSpawnPerc = 0.5f;

    public float SpawnInterval = 1.0f;
    private float _t = 0.0f;

    private void Update()
    {
        _t += Time.deltaTime;
        if (_t < SpawnInterval) return;
        _t = 0.0f;

        float x = UnityEngine.Random.Range(HorizontalRange.x, HorizontalRange.y);
        float y = VerticalMax + VerticalSpawnOffset;
        Vector3 pos = new(x, y, 0.0f);

        Cake cake = Instantiate(Prefab, pos, Quaternion.identity);
        _normalSpawnPerc = _normalSpawnPercStart * player.cakeTracker;
        _vertCakeSpawnPerc = _vertCakeSpawnPercStart * player.verticalCakeTracker;

        //Debug.Log("Percent: " + _normalSpawnPerc + " Vert:" + _vertCakeSpawnPerc);

        float r = UnityEngine.Random.value;
        if (r < _normalSpawnPerc)
        {
            cake.SetType(Cake.CakeType.Normal);
        }
        else
        {
            var d2 = _normalSpawnPerc + _vertCakeSpawnPerc * (1.0f - _normalSpawnPerc);
            if (r < d2) // vertical
            {
                cake.SetType(Cake.CakeType.Vertical);
            }
            else // horizontal
            {
                cake.SetType(Cake.CakeType.Horizontal);
            }
        }
    }
}