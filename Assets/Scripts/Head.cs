using UnityEngine;

public class Head : MonoBehaviour
{
    private CakePlayer _cakePlayer;
    public BoxCollider2D bodyCollider;

    public string deathTag = "Obstacle";



    private void Start()
    {
        _cakePlayer = GetComponentInParent<CakePlayer>();
        //Collider2D thisCollider = GetComponent<Collider2D>();
        //Physics2D.IgnoreCollision(thisCollider, bodyCollider);
        //thisCollider.enabled = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      // Debug.Log("Head collided with " + other.gameObject.name);
        _cakePlayer.Interact(other);

        // if (other.gameObject.CompareTag(deathTag))
        // {
        //     _cakePlayer.Die();
        // }
    }


}