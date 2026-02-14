using UnityEngine;

public class Head : MonoBehaviour
{
    private CakePlayer _cakePlayer;

    private void Start()
    {
        _cakePlayer = GetComponentInParent<CakePlayer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Head collided with " + other.gameObject.name);
        _cakePlayer.Interact(other);
    }
}