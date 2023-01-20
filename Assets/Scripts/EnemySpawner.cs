using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject spawningArea; 
    [SerializeField] GameObject dude;
    [SerializeField] GameObject rangedDude;
    [SerializeField] GameObject brute;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject lair;
    [SerializeField] Transform player;
    [SerializeField] Transform enemies;
    [SerializeField] int maxNoEnemeis = 100;
    
    int _i = 0;

    [SerializeField] float timeRemaining = 300;
    [SerializeField] bool timerIsRunning = false;
    [SerializeField] TextMeshProUGUI timerDisplay;
    [SerializeField] bool bossPlanet = false; 

    
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
                DisplayTime(timeRemaining);
                timerIsRunning = false;
            }
        }
    }

    void StartSpawning()
    {
        if(bossPlanet)
            SpawnBossPlanet();
        else
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
                PlanetEnd();
                break;
        }
        
        Invoke(nameof(Spawn), 1f);
    }
    
    void SpawnBossPlanet()
    {
        _i += 1;

        if(_i == 61)
        {
            SpawnX(boss, 1, 2);
        }
        
        if(enemies.childCount > maxNoEnemeis)
        {
            Invoke(nameof(SpawnBossPlanet), 1f);
            return;
        }

        switch (_i)
        {
            case <= 60:
                SpawnX(dude, 2, 7);
                break;
            default:
                if(enemies.childCount == 0)
                    lair.SetActive(false);
                break;
        }
        
        Invoke(nameof(SpawnBossPlanet), 1f);
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
            
            RaycastHit hit;
            Ray ray = new Ray(newPos, Vector3.down);
            
            Vector3 targetPoint;
            
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
                newPos.y = targetPoint.y + 3;
            }

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
