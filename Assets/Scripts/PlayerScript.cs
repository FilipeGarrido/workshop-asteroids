using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;//Variável para armazenar o rigidbody do player
    private SpriteRenderer _sprite;//Variável para armazenar a sprite do player
    private CircleCollider2D _collider;//Variável para armazenar a caixa de colisão do player
    public Bullet bulletPrefab;//Variavel para criar o peojetil
    [SerializeField] private AudioSource shootSoudFX;//Varável para armazenar o som de tiro
    [SerializeField] private AudioSource shipDestoySoudFX;//Varável para armazenar o som de explosão da nave
    [SerializeField] private int maxLives = 3;//Vida máxima
    private int actualLives;//vida atual
    public float aceleration = 2.0f;//aceleração
    public float angularSpeed = 5.0f;//velocidade angular
    public float maxSpeed = 5.0f;//velocidade maxima
    private bool acelerate;//Variavel para verificar se o player está acelerando
    private int turn;//Variavel para verificar se o player esta virando

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();//Armazena o rigidbody do player
        _sprite = GetComponent<SpriteRenderer>();//Armazena a sprite do player
        _collider = GetComponent<CircleCollider2D>();//Armazena a caixa de colisão do player
        actualLives = maxLives;//Armazena o valor da vida maxima na vida atual, no inicio do jogo
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
        shipDestoySoudFX.Play();//Toca o audio da nave explodindo caso colida
        if(actualLives == 0){
            Destroy(this._sprite);//Destroi a sprite
            Destroy(this._collider);//Destroi a caixa de colisão
            Destroy(this.gameObject,2.0f);//Destroi o objeto do player depois de 2 segundos. Caso contrário, se for destruido no mesmo instante, o audio não será tocado.
        }
        else {
            actualLives --;//Diminui 1 de vida
        }
    }

    //Função de tiro
    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);//cria um projétil
        bullet.Project(this.transform.up);//Define a direção do projetil
    }
}
