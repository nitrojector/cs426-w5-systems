using System;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    public CakeType Type { get; private set; } = CakeType.Normal;

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


    void Awake()
    {
        SetType(Type);
        ActorManager.Add(gameObject);
    }

    private void OnDestroy()
    {
        ActorManager.Remove(gameObject);
    }

    void Update()
    {
    }

    public void SetType(CakeType type)
    {
        Type = type;
        transform.localScale = new Vector3(cakeDim[type].x, cakeDim[type].y, 1.0f);
    }
}