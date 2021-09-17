using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;//Variável para armazenar o rigidbody do player
    public Bullet bulletPrefab;//Variavel para criar o peojetil
    public AsteroidFX destroyFXPrefab;
    [SerializeField] private AudioSource shootSoudFX;//Varável para armazenar o som de tiro
    public float aceleration = 2.0f;//aceleração
    public float angularSpeed = 5.0f;//velocidade angular
    public float maxSpeed = 5.0f;//velocidade maxima
    private bool acelerate;//Variavel para verificar se o player está acelerando
    private int turn;//Variavel para verificar se o player esta virando
    public static System.Action playerLivesEvent = null;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();//Armazena o rigidbody do player
        this.gameObject.SetActive(false);
    }

    void Update(){
        
        acelerate = Input.GetKey(KeyCode.UpArrow);//Toda vez q o player acionar o direcional para cima, a variável acelerate armazena true;

        //Toda vez que o player aciona um dos direcionais derito ou esquerdo, a variável turn armaze um valor (-1,0 ou 1) que define o sentido da rotção
        if(Input.GetKey(KeyCode.LeftArrow)){
            turn = -1;
        }
        else if(Input.GetKey(KeyCode.RightArrow)){
            turn = 1;
        }
        else{
            turn = 0;
        }

        //Se o player pressionar barra de espaço, toca o audio de tiro e chama a função de tiro
        if(Input.GetKeyDown(KeyCode.Space)){
            shootSoudFX.Play();
            Shoot();
        }

    }
    void FixedUpdate()
    {
        if(acelerate){
            Vector3 speed = transform.up * aceleration;
            playerRigidbody.AddForce(speed);
        }
        if(turn != 0){
            playerRigidbody.AddTorque(turn * angularSpeed);
        }
        if(playerRigidbody.velocity.magnitude > maxSpeed){
            playerRigidbody.velocity = Vector2.ClampMagnitude(playerRigidbody.velocity,maxSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        
        
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.angularVelocity = 0.0f;
        Instantiate(destroyFXPrefab,playerRigidbody.position,Quaternion.identity);
        
        if(playerLivesEvent != null){
            playerLivesEvent();
        }

        for(int i=0;i<2;i++){
            this.gameObject.SetActive(false);
        }
    }

    //Função de tiro
    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);//cria um projétil
        bullet.Project(this.transform.up);//Define a direção do projetil
    }
}
