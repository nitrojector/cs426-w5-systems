using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public BoxCollider2D headCollider;
    public BoxCollider2D bodyCollider;

    private InputAction moveAction;

    public float velocity = 5f;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        transform.Translate(input.x * velocity * Time.deltaTime * Vector3.right);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Cake"))
        {
        }
    }
}