using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.down * speed;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Destroy")
        {
            Destroy(this.gameObject);
        }
    }
}
