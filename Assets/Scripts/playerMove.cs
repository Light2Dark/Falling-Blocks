using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float speed;
    float width;
    Collider coll;
    public event System.Action OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        float halfWinHeight = Camera.main.orthographicSize;
        float halfWinWidth = halfWinHeight * Camera.main.aspect;
        float playerWidth = this.transform.localScale.x / 2f;
        width = halfWinWidth + playerWidth;
        coll = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "Obstacle") {
            if (OnPlayerDeath != null) {
                OnPlayerDeath();
            }
            Destroy(gameObject);
        }
    }

    private void Move() {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * speed;

        this.transform.Translate(velocity * Time.deltaTime, Space.World);

        if (this.transform.position.x > width) {
            this.transform.Translate(new Vector3 (-width * 2,0,0));
        } 
        if (this.transform.position.x < -width) {
            this.transform.Translate(new Vector3 (width * 2,0,0));
        }
    }
    
}

