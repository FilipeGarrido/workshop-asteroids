using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{   
    //Variável para armazenar o rigidboddy do asteroid
    private Rigidbody2D _rigidbody;

    //Variável para armazenar a sprite do asteroid
    private SpriteRenderer _sprite;

    //Variável para armazenar a caixa de colisão do asteroid
    private CircleCollider2D _collider;

    //Variável para receber o som do asteroid destruído
    [SerializeField] private AudioSource asteroidDestoySoudFX;
    public float size = 1.0f; //tamanho do asteróide (default)
    public float minSize = 0.5f; //Tamanho minimo
    public float maxSize = 2.0f;//Tamanho máximo
    public float speed = 10.0f; //Velocidade
    public float maxLifeTime = 30.0f; //Tempo em que o asteroide permanece "vivo", sem ser atingido por um projétil

    void Awake()
    {   
        _rigidbody = GetComponent<Rigidbody2D>();//Recebe o Rigidbody do asteroid
        _sprite = GetComponent<SpriteRenderer>();//Recebe a sprite do asteroid
        _collider = GetComponent<CircleCollider2D>();//Recebe a caixa de colisão do asteroid
    }
    void Start()
    {   
        //Ao spawnar, o asteroid inicia com um angulo e um tamanho aleatório
        this.transform.eulerAngles = new Vector3 (0.0f , 0.0f , Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;
        //A massa terá a mesma escala do tamanho
        _rigidbody.mass = this.size;
    }

    //Função para lançar o Asteroid ao spawnar
    public void SetTrajectory(Vector2 direction)
    {   
        _rigidbody.AddForce(direction * this.speed);

        //O Asteroid é destruido após um tempo, para não ficar muito tempo carregado no jogo, caso o player não o destrua
        Destroy (this.gameObject, this.maxLifeTime);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //Se a colisão for com um projétil, toca o audio do asteróide destruindo
        if(other.gameObject.tag == "Bullet"){
            asteroidDestoySoudFX.Play();
            //Se o tamanho do asteróide, dividido por 2 for mais ou igual a meio (tamanho mínimo), roda a função CreateSplit
            if((this.size / 2.0f ) >= 0.5f){
                CreateSplit();
                CreateSplit();
            }
        }
        Destroy(this._sprite);//Destrói a sprite, para o asteróide não aparecer
        Destroy(this._collider);//Em seguida destrói a caixa de colisão, para o player nao morrer com um asteróide invisível
        Destroy(this.gameObject,2.0f);//Depois de 2 segundos o objeto é destruído, caso contrário, se o objeto for destruido no mesmo instante que colide com o projetil, o audio não é tocado
    }

    //Função que gera 2 asteróides menores
    void CreateSplit()
    {
        Vector2 position = this.transform.position; //Armazena a posição do asteróide anterior
        position += Random.insideUnitCircle * 0.05f;//Posição inicial do novo asteróide menor
        Asteroid smallAsteroid = Instantiate(this,position,this.transform.rotation); //Cria um asteróide menor
        smallAsteroid.size = this.size * 0.5f;//Define o tamanho do asteróide menor como sendo o tamanhdo do anterior vezes meio
        smallAsteroid.SetTrajectory(Random.insideUnitCircle.normalized *this.speed);//Define uma trajetória para o novo asteróide
    }
}
