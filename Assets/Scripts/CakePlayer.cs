using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CakePlayer : MonoBehaviour
{
    public BoxCollider2D headCollider;
    public BoxCollider2D bodyCollider;


    private InputAction moveAction;

    public float velocity = 5f;
    public float arDelta = 0.15f;
    public float areaDelta = 0.5f;

    // TODO: The body does not start with scale of 1 in the scene
    private float _scale = 1.0f;
    private float _ar = 1.0f;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        transform.Translate(input.x * velocity * Time.deltaTime * Vector3.right);
    }

    public void Interact(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cake"))
        {
            Cake cake = other.gameObject.GetComponent<Cake>();
            switch (cake.Type)
            {
                case Cake.CakeType.Normal:
                    _scale += areaDelta;
                    break;
                case Cake.CakeType.Vertical:
                    _ar /= 1.0f + arDelta;
                    break;
                case Cake.CakeType.Horizontal:
                    _ar *= 1.0f + arDelta;
                    break;
            }

            Destroy(other.gameObject);

            UpdateLocalScale();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
    }

    private void UpdateLocalScale()
    {
        float area = _scale * _scale;
        var mult = Mathf.Sqrt(area / _ar);
        bodyCollider.transform.localScale = new Vector3(_ar * mult, mult, 1.0f);
    }
}