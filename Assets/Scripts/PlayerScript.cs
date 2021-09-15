using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    public Rigidbody2D bulletRigidbody;
    public float aceleration = 1.0f;
    public float angularSpeed = 5.0f;
    public float maxSpeed = 5.0f;
    public float bulletSpeed = 10.0f;

    void Start()
    {
        
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            Rigidbody2D bullet = Instantiate (bulletRigidbody,playerRigidbody.position,Quaternion.identity);
            bullet.velocity = transform.up * bulletSpeed;
        }
    }
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.UpArrow)){
            Vector3 speed = transform.up * aceleration;
            playerRigidbody.AddForce(speed , ForceMode2D.Force);
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            playerRigidbody.rotation += angularSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            playerRigidbody.rotation -= angularSpeed * Time.deltaTime;
        }
        if(playerRigidbody.velocity.magnitude > maxSpeed){
            playerRigidbody.velocity = Vector2.ClampMagnitude(playerRigidbody.velocity,maxSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        Destroy(gameObject);
    }
}
