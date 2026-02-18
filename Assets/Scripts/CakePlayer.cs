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

    public int score = 0;

    public float velocity = 5f;
    public float arDelta = 0.15f;
    public float areaDelta = 0.5f;

    private float _scale = 1.0f;
    private float _ar = 1.0f;

    public float cakeTracker = 1;
    public float verticalCakeTracker = 1;

    public float cakeMultipler = 0.5f;

    public bool actuallyDie = false;

    public float scoreInterval = 0.25f;
    private float _t = 0.0f;

    public AudioSource eatCakeAudio;
    public AudioSource deathAudio;

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
        // check for death
        if (_scale <= Constants.PlayerDeathScale || transform.position.y < Constants.PlayerDeathY)
        {
            Die();
        }

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
            _t += Time.deltaTime;
            if (_t < scoreInterval) return;
            _t = 0.0f;
            GameManager.AdjustScore(Time.deltaTime * 100.0f);

            scoreText.SetText($"Score: {GameManager.Score:F1}\nHi-Score: {GameManager.HiScore:F1}");
        }

        Debug.Log($"scale={_scale:F3}");

        UpdateLocalScale();
    }

    private void FixedUpdate()
    {
        if (false)
        {
            var pos = transform.position;
            if (pos.y < Constants.PlayerTargetY)
            {
                pos.y = Mathf.Min(Constants.PlayerTargetY - pos.y, 0.05f);
            }

            transform.position = pos;
        }
    }


    private void PlayEatCakeSound()
    {
        if (eatCakeAudio != null && !eatCakeAudio.isPlaying)
        {
            eatCakeAudio.Play();
        }
    }

    private void PlayDeathSound()
    {
        if (deathAudio != null && !deathAudio.isPlaying)
        {
            deathAudio.Play();
        }
    }

    public void Interact(Collider2D other)
    {
        //Debug.Log("interact");
        if (other.gameObject.CompareTag("Cake"))
        {
            Cake cake = other.gameObject.GetComponent<Cake>();

            // Debug.Log("Before Scale:" + _scale);
            PlayEatCakeSound();

            switch (cake.Type)
            {
                case Cake.CakeType.Normal:
                    cakeTracker *= 1 - cakeMultipler;
                    _scale += areaDelta;
                    GameManager.AdjustScore(2.0f);
                    break;
                case Cake.CakeType.Vertical:
                    verticalCakeTracker *= 1 - cakeMultipler;
                    _ar /= 1.0f + arDelta;
                    _scale += areaDelta * Constants.SpecialCakeCalorieValuePerc;
                    GameManager.AdjustScore(1.0f);
                    break;
                case Cake.CakeType.Horizontal:
                    verticalCakeTracker *= 1 + cakeMultipler;
                    _ar *= 1.0f + arDelta;
                    _scale += areaDelta * Constants.SpecialCakeCalorieValuePerc;
                    GameManager.AdjustScore(1.0f);
                    break;
            }

            // Debug.Log("Scale:" + _scale);

            Destroy(other.gameObject);

            UpdateLocalScale();
        }
    }

    public void Die()
    {
        Debug.Log("Die");
        // if (actuallyDie)
        {
            PlayDeathSound();
            ActorManager.DestroyAll();
            Reset();
        }
    }

    public void Reset()
    {
        transform.position = new Vector3(0, -3, 0);
        _scale = 3.0f;
        _ar = 1.0f;
        cakeTracker = 1.0f;
        verticalCakeTracker = 1.0f;
        GameManager.ResetScore();
        UpdateLocalScale();
    }

    private void UpdateLocalScale()
    {
        float area = _scale * _scale;
        var mult = Mathf.Sqrt(area / _ar);
        bodyCollider.transform.localScale = new Vector3(_ar * mult, mult, 1.0f);
    }
}