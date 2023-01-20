using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject spawningArea; 
    [SerializeField] GameObject dude;
    [SerializeField] GameObject rangedDude;
    [SerializeField] GameObject brute;
    [SerializeField] Transform player;
    [SerializeField] Transform enemies;
    [SerializeField] int maxNoEnemeis = 100;
    
    int _i = 0;

    [SerializeField] float timeRemaining = 300;
    [SerializeField] bool timerIsRunning = false;
    [SerializeField] TextMeshProUGUI timerDisplay;

    
    // Start is called before the first frame update
    void Start()
    {
        DisplayTime(timeRemaining);
        Invoke(nameof(StartSpawning), 5);
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
        
        if(enemies.childCount > maxNoEnemeis)
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
                SpawnX(rangedDude, 0, 2); //1 2
                break;
            case <= 180:
                SpawnX(brute, 0, 2);
                SpawnX(rangedDude, 2, 4); //1 2
                SpawnX(dude, 3, 6);
                break;
            case <= 240:
                SpawnX(brute, 0, 2);
                SpawnX(rangedDude, 2, 4); //1 2
                SpawnX(dude, 3, 7);
                break;
            case <= 300:
                SpawnX(brute, 0, 3);
                SpawnX(rangedDude, 2, 6); //1 2
                SpawnX(dude, 5, 10);
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
            var tempPos = spawningArea.transform.TransformPoint(
                Random.Range(-.5f, .5f),
                0,
                Random.Range(-.5f, .5f)
            );

            var newPos = enemies.InverseTransformPoint(tempPos);
            newPos.y = spawningArea.transform.position.y;
            
            // Enemies spawn sqrt(500) units away from the player
            if((player.position - newPos).sqrMagnitude < 1000) // virker vidst, men ved ik om det passer ift relative pos, skal være serialize field?
                continue;
            
            var currentEnemy = Instantiate(x, newPos, Quaternion.identity, enemies);
            currentEnemy.GetComponent<EnemyAI>().player = player;
        }
    }

    void PlanetEnd()
    {
        player.GetComponent<PlayerInventory>().SavePlayer();
        SceneManager.LoadScene("Space");
    }
}
