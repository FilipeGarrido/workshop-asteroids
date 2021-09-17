using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidWave : MonoBehaviour
{
    public Asteroid asteroidPrefab;//Variável para criar um novo asteroide
    public float trajectoryVariance = 15.0f ; //angulo limite de variação
    private float spawnDistance; //Distancia do centro
    public float spawnRate = 3.0f;//Numero de repetições do spawn
    public float spawnTime = 4.0f;//Tempo entre repetições
    public int spawnAmount = 1;//Quantidade de asteroids a serem criados
    public float padding = 0.5f;
    void Awake()
    {
        this.gameObject.SetActive(false);
    }
    void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnTime, this.spawnRate);//Vai repetir o metodo de Spawn, a cada "spawnRate" segundos, por "spawnRate" vezes
    }

    //Fução de spawn dos asteroids
    private void Spawn()
    {
        Camera _cam = CameraGameplay._instance.myCam; //Recebe a camera do cenário atual
        
        var maxX = _cam.orthographicSize * _cam.aspect;//Recebe a largura da camera
        var maxY = _cam.orthographicSize;//Recebe a altura da camera

        float leftLimit = -maxX;//limite da esquerda
        float rightLimit = maxX;//limite da direita
        float upLimit = maxY;//limite superior
        float downLimit = - maxY;//limite inferior

        spawnDistance = Mathf.Max(maxX,maxY);

        for(int i = 0; i<this.spawnAmount ; i++){
           
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance; //Direção de spawn
            
            //Verifica se o spawn está dentro dos limites da camera;
            if (spawnDirection.x < leftLimit){
                spawnDirection.x = leftLimit - padding;
            }
            if (spawnDirection.x > rightLimit){
                spawnDirection.x = rightLimit + padding;
            }
            if (spawnDirection.y < downLimit){
                spawnDirection.y = downLimit - padding;
            }
            if (spawnDirection.y > upLimit){
                spawnDirection.y = upLimit + padding;
            }
            Vector3 spawnPoint = this.transform.position + spawnDirection; //Posição inicial

            float variance = Random.Range(-this.trajectoryVariance,this.trajectoryVariance);//variação do angulo
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward); //angulo inicial

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);//Cria um asteróide
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);//Define um tamanho aleatório entre o tamanho máximo e mínimo
            asteroid.SetTrajectory(rotation * -spawnDirection);//Define a trajetória conforme o angulo inicial, no sentido contrário ao do ponto inicial
        }
    }
}
