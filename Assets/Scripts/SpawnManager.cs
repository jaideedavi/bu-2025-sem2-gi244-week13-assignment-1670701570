using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;
    public ObstacleObjectPool obstacleObjectPool;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, 2f);
    }

     void Spawn()
   {
    GameObject player = GameObject.Find("Player");

    if (player == null)
    {
        Debug.LogError("Player not found!");
        return;
    }

    PlayerController pc = player.GetComponent<PlayerController>();

    if (pc == null)
    {
        Debug.LogError("PlayerController missing!");
        return;
    }

    if (pc.gameOver)
    {
        return;
    }

    if (obstacleObjectPool == null)
    {
        Debug.LogError("ObstacleObjectPool not assigned!");
        return;
    }

     int randomType = Random.Range(0, 3);

     GameObject obstacle = obstacleObjectPool.Acquire(randomType);

       obstacle.transform.position = spawnPoint.position;
       obstacle.transform.rotation = Quaternion.identity;
   }
}