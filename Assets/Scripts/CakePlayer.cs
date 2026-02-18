using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CakePlayer : MonoBehaviour
{
    public static CakePlayer Instance { get; private set; }

    public BoxCollider2D headCollider;
    public BoxCollider2D bodyCollider;

    private InputAction moveAction;

    public TextMeshProUGUI scoreText;

    public float velocity = 5f;
    public float arDelta = 0.15f;
    public float areaDelta = 0.5f;

    private float _scale = 1.0f;
    private float _ar = 1.0f;

    public float cakeTracker = 1;
    public float verticalCakeTracker = 1;

    public float cakeMultipler = 0.5f;

    public bool actuallyDie = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        _scale = bodyCollider.gameObject.transform.localScale.x;
        // Debug.Log("Scale:" + _scale);

        Physics2D.IgnoreCollision(headCollider, bodyCollider);

        moveAction = InputSystem.actions.FindAction("Move");

        UpdateLocalScale();
    }

    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        if (Mathf.Abs(input.x) > 0) // player is strafing
        {
            transform.Translate(input.x * velocity * Time.deltaTime * Vector3.right);
            _scale -= Time.deltaTime * Constants.PlayerCalorieBurnRateStrafe;
        }
        else
        {
            _scale -= Time.deltaTime * Constants.PlayerCalorieBurnRateRegular;
        }

        // updates score display
        {
            scoreText.SetText($"Score: {GameManager.Score}\nHi: {GameManager.HiScore}");
        }

        UpdateLocalScale();
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
                    cakeTracker *= 1 - cakeMultipler;
                    _scale += areaDelta;
                    break;
                case Cake.CakeType.Vertical:
                    verticalCakeTracker *= 1 - cakeMultipler;
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
        if (actuallyDie)
        {
            ActorManager.DestroyAll();
            Reset();
        }
    }

    public void Reset()
    {
        transform.position = Vector3.zero;
        _scale = 1.0f;
        _ar = 1.0f;
        cakeTracker = 1.0f;
        verticalCakeTracker = 1.0f;

        GameManager.Score = 0;
        UpdateLocalScale();
    }

    private void UpdateLocalScale()
    {
        float area = _scale * _scale;
        var mult = Mathf.Sqrt(area / _ar);
        bodyCollider.transform.localScale = new Vector3(_ar * mult, mult, 1.0f);
    }
}