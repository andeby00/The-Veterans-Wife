using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject area; 
    [SerializeField] GameObject dude; 
    [SerializeField] GameObject brute;
    [SerializeField] Transform player;
    
    List<GameObject> _currentEnemies = new List<GameObject>();
    int _i = 0;
    Vector3 _localScale;
    
    [SerializeField] float timeRemaining = 305;
    [SerializeField] bool timerIsRunning = false;
    [SerializeField] TextMeshProUGUI timerDisplay;

    
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(StartSpawning), 5);
        _localScale = area.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            PlanetEnd();

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);                
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                PlanetEnd();
            }
        }

    }

    void StartSpawning()
    {
        Spawn();
        timerIsRunning = true;
        //Invoke(nameof(PlanetEnd), timeRemaining);
    }
    
    void DisplayTime(float timeToDisplay)
    {
        timerDisplay.SetText(string.Format("{0:00}:{1:00}", Mathf.FloorToInt(timeToDisplay / 60), Mathf.FloorToInt(timeToDisplay % 60)));
    }

    void Spawn()
    {
        _i += 1;

        if(_currentEnemies.Count > 100)
        {
            Invoke(nameof(Spawn), 1f);
            return;
        }
        
        switch (_i)
        {
            case <= 60:
                SpawnX(dude, 2, 4);
                break;
            case <= 120:
                SpawnX(dude, 2, 4);
                SpawnX(brute, 1, 2);
                break;
            case <= 180:
                SpawnX(dude, 3, 6);
                SpawnX(brute, 1, 2);
                break;
            case <= 240:
                SpawnX(dude, 4, 7);
                SpawnX(brute, 1, 3);
                break;
            case <= 300:
                SpawnX(dude, 5, 10);
                SpawnX(brute, 2, 6);
                break;
            default:
                break;
        }
        
        Invoke(nameof(Spawn), 1f);
    }

    void SpawnX(GameObject x, int minInc, int maxEx)
    {
        var UPPER = Random.Range(minInc, maxEx);
        
        for (int j = 0; j < UPPER; j++)
        {
            var newPos = new Vector3(
                Random.Range(-_localScale.x, _localScale.x),
                area.transform.position.y,
                Random.Range(-_localScale.z, _localScale.z)
            );
            
            if((player.position - newPos).sqrMagnitude < 500)
                continue;
            
            var currentEnemy = Instantiate(x, newPos, Quaternion.identity );
            currentEnemy.GetComponent<EnemyAI>().player = player;
            
            _currentEnemies.Add(currentEnemy);
        }
    }

    void PlanetEnd()
    {
        player.GetComponent<PlayerInventory>().SavePlayer();
        SceneManager.LoadSceneAsync("Space");
    }
}
