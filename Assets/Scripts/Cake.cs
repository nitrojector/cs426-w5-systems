using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    public CakeType Type { get; private set; } = CakeType.Normal;

    public float DespawnY = -6.0f;

    public enum CakeType
    {
        Normal,
        Vertical,
        Horizontal,
    }

    public Dictionary<CakeType, Vector2> cakeDim = new()
    {
        [CakeType.Normal] = new(0.6f, 0.6f),
        [CakeType.Vertical] = new(0.3f, 1.0f),
        [CakeType.Horizontal] = new(1.0f, 0.3f)
    };

    public float velocity = 2.0f;

    void Awake()
    {
        SetType(Type);
    }

    void Update()
    {
        transform.position += Vector3.down * velocity * Time.deltaTime;

        if (transform.position.y < DespawnY)
        {
            Destroy(gameObject);
        }
    }

    public void SetType(CakeType type)
    {
        Type = type;
        transform.localScale = new Vector3(cakeDim[type].x, cakeDim[type].y, 1.0f);
    }
}