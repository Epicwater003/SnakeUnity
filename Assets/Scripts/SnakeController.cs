using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class SnakeController : MonoBehaviour
{
    [SerializeField] public float MoveSpeed = 2f;
    [SerializeField] public float SteerSpeed = 180;
    [SerializeField] public float BodySpeed = 2f;
    [SerializeField] public int Gap = 30;
    [SerializeField] public float MaxSteerAngle = 45;

    private float SteerAngle = 0;
    private int Score = 0;
    private int HiScore = 0;
    public event Action<int> ScoreIncreased;
    public event Action<bool> GameEnd;

    public Tilemap map;
    private Transform[] tiles;
    private List<Vector3> floorTilesCords = new List<Vector3>();

    [SerializeField] public GameObject BodyPrefab;
    [SerializeField] public GameObject ApplePrefab;
    [SerializeField] public GameObject AppleParticlePrefab;
    
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

    void Start()
    {
        // Загружаем рекорд очков для данного уровня
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "Score"))
        {
            HiScore = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Score");
        }
       
        GrowSnake();

        tiles = map.GetComponentsInChildren<Transform>();
        foreach (var tile in tiles)
        {
            if (tile.gameObject.tag == "Floor")
            {
                floorTilesCords.Add(tile.gameObject.transform.position);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        
    }
    
    void Update()
    {

        // Перемещение
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        // Поворот     
        float steerDirection = Input.GetAxis("Horizontal");

        // Защита от поворота в стену
        SteerAngle = Vector3.Angle(transform.forward, BodyParts[0].transform.forward);
        if (Physics.Raycast(transform.position, Quaternion.Euler(0, -45, 0) * transform.right, 1.1f, 1 << 6))
        {
            steerDirection = -1;
        }
        if (Physics.Raycast(transform.position, Quaternion.Euler(0, 45, 0) * -transform.right, 1, 1 << 6))
        {
            steerDirection = 1;
        }
        
        // Ограничение на угол поворота
        if (Math.Abs(SteerAngle) < MaxSteerAngle)
        {
            transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);
        }
        else if (SteerAngle > 0)
        {
            SteerAngle = MaxSteerAngle;
        }
        else if (SteerAngle < 0)
        {
            SteerAngle = -MaxSteerAngle;
        }
        
        // Сохраняем позицию головы
        PositionsHistory.Insert(0, transform.position);

        // Движение тела
        int index = 0;
        foreach (var body in BodyParts)
        {
            // Находим точку для части тела в истории
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            // Перемешаем часть тела к следующей точке
            Vector3 moveDirection = point - body.transform.position;
            // teleport
            //Debug.Log(Vector3.Magnitude(point - body.transform.position));
            /*if (Vector3.Magnitude(point - body.transform.position) > 1.5f)
            {
                Debug.Log("Teleport Body: " + index);
                if (index == 0)
                {
                    SteerAngle = 0;
                }
                body.transform.position = point;
            }*/
            
            
            
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            body.transform.LookAt(point);

            index++;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Apple")
        {
            Instantiate(AppleParticlePrefab, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            AudioManager.Instance.PlaySound(AudioManager.Instance.SnakeEatSound);
            Score++;
            ScoreIncreased?.Invoke(Score);

            if (Score > HiScore)
            {
                HiScore = Score;
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Score", HiScore);
            }

            GrowSnake();

            // Создаем на поле новое яблоко
            Vector3 spawnPoint = new Vector3();           
            while (true)
            {
               
                spawnPoint = floorTilesCords[UnityEngine.Random.Range(0, floorTilesCords.Count)];
                if (Vector3.Magnitude(spawnPoint - transform.position) > 5)
                {
                    break;
                }
            }
            spawnPoint.y += 1.5f;
            Instantiate(ApplePrefab, spawnPoint, Quaternion.identity);

        }
        else if (other.gameObject.tag == "Obstacle" || (other.gameObject.tag == "Snake Body" && BodyParts.Count > 1))
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.SnakeBreakSound);
            GameEnd?.Invoke(true);
        }

    }
    private void GrowSnake()
    {
        Vector3 point = this.transform.position;
        if (BodyParts.Count != 0) { 
            point = PositionsHistory[Mathf.Clamp((BodyParts.Count + 1) * Gap, 0, PositionsHistory.Count - 1)];
        }

        GameObject body = Instantiate(BodyPrefab, point, Quaternion.identity);
        body.transform.position = point;
        BodyParts.Add(body);
    }
}