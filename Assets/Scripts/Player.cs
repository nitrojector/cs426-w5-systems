using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BoxCollider2D headCollider;
    public BoxCollider2D bodyCollider;

    void Start()
    {
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Cake"))
        {
        }
    }
}