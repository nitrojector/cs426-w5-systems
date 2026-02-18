using UnityEngine;

public class Body : MonoBehaviour
{
    private CakePlayer _cakePlayer;
    public string deathTag = "Obstacle";
    public BoxCollider2D headCollider;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Collider2D thisCollider= GetComponent<Collider2D>();
        //Physics2D.IgnoreCollision(thisCollider, headCollider);
        //thisCollider.enabled = true;

        _cakePlayer = GetComponentInParent<CakePlayer>();


    }

    

 
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Body collided with " + other.gameObject.name);

        if (other.gameObject.CompareTag(deathTag))
        {
            _cakePlayer.Die();
        }
    }
}
