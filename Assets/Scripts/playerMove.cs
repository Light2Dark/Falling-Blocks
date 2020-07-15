using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float speed;
    public float mobileSpeed;
    float widthPlusPlayer, screenWidth;
    Collider coll;
    public event System.Action OnPlayerDeath;
    Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        float halfWinHeight = Camera.main.orthographicSize;
        float halfWinWidth = halfWinHeight * Camera.main.aspect;
        screenWidth = Screen.width;
        float playerWidth = this.transform.localScale.x / 2f;
        widthPlusPlayer = halfWinWidth + playerWidth;
        coll = GetComponent<BoxCollider>();

        /*#if UNITY_IOS || UNITY_ANDROID || UNITY_WP_8_1
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                
            }
        }
        #else
        if  (Input.GetKey(KeyCode.RightArrow))
            {
                _normalizedHorizontalSpeed = 1;
            } 
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                _normalizedHorizontalSpeed = -1;
            }
        #endif
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
        // PC support
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        direction = input.normalized;

        // Mobile support
        if (Input.touchCount > 0) {

            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            
            if (touch.position.x < screenWidth && touch.position.x > screenWidth/2) {
                direction = Vector3.right * mobileSpeed;
            } else if (touch.position.x > 0 && touch.position.x < screenWidth/2) {
                direction = Vector3.left * mobileSpeed;
            } 
            direction = direction.normalized;
        }

        Vector3 velocity = direction * speed;
        this.transform.Translate(velocity * Time.deltaTime, Space.World);

        if (this.transform.position.x > widthPlusPlayer) {
            this.transform.Translate(new Vector3 (-widthPlusPlayer * 2,0,0));
        } 
        if (this.transform.position.x < -widthPlusPlayer) {
            this.transform.Translate(new Vector3 (widthPlusPlayer * 2,0,0));
        }
        
    }

    private void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "Obstacle") {
            if (OnPlayerDeath != null) {
                OnPlayerDeath();
            }
            Destroy(gameObject);
        }
    }
    
}

