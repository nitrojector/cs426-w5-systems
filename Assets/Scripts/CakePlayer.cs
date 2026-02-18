using System;
using Unity.VisualScripting;
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

    public float cakeTracker = 1;
    public float verticalCakeTracker = 1;

    public float cakeMultipler = 0.5f;

    public float score = 0.0f;

    public bool actuallyDie = false;
    //todo scoring

    private void Awake()
    {
        _scale = bodyCollider.gameObject.transform.localScale.x;
        // Debug.Log("Scale:" + _scale);
       
        Physics2D.IgnoreCollision(headCollider, bodyCollider);

        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        transform.Translate(input.x * velocity * Time.deltaTime * Vector3.right);
    }

    public void Interact(Collider2D other)
    {
        //Debug.Log("interact");
        if (other.gameObject.CompareTag("Cake"))
        {
            Cake cake = other.gameObject.GetComponent<Cake>();

           // Debug.Log("Before Scale:" + _scale);

            switch (cake.Type)
            {
                case Cake.CakeType.Normal:
                    cakeTracker *= 1- cakeMultipler;
                    _scale += areaDelta;
                    break;
                case Cake.CakeType.Vertical:
                    verticalCakeTracker *= 1- cakeMultipler;
                    _ar /= 1.0f + arDelta;
                    break;
                case Cake.CakeType.Horizontal:
                    verticalCakeTracker *= 1 + cakeMultipler;
                    _ar *= 1.0f + arDelta;
                    break;
            }

           // Debug.Log("Scale:" + _scale);

            Destroy(other.gameObject);

            UpdateLocalScale();
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    public void Die()
    {
        //Todo Death
        if (actuallyDie)
        {
            Destroy(gameObject);

        }
    }

    private void UpdateLocalScale()
    {
        float area = _scale * _scale;
        var mult = Mathf.Sqrt(area / _ar);
        bodyCollider.transform.localScale = new Vector3(_ar * mult, mult, 1.0f);
    }
}