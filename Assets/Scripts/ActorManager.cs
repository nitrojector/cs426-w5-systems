using System.Collections.Generic;
using UnityEngine;

public static class ActorManager
{
    private static List<GameObject> _objects = new();

    public static void Add(GameObject obj)
    {
        _objects.Add(obj);
    }

    public static void Remove(GameObject obj)
    {
        _objects.Remove(obj);
    }

    public static void DestroyAll()
    {
        foreach (var obj in _objects)
        {
            if (obj != null)
            {
                Object.Destroy(obj);
            }
        }
    }
}