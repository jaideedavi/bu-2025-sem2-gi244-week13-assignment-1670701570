using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public GameObject obstacleBarrelPrefab;
    public GameObject obstacleBarrierPrefab;
    public GameObject obstacleStoneWallPrefab;
    public int poolSize = 10;

    private List<GameObject> obstacleBarrelPool;
    private List<GameObject> obstacleBarrierPool;
    private List<GameObject> obstacleStoneWallPool;

    void Awake()
    {
        obstacleBarrelPool = new List<GameObject>();
        obstacleBarrierPool = new List<GameObject>();
        obstacleStoneWallPool = new List<GameObject>();

        // Type 0 = Barrel
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(obstacleBarrelPrefab);
            obj.SetActive(false);
            obstacleBarrelPool.Add(obj);
        }

        // Type 1 = Barrier
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(obstacleBarrierPrefab);
            obj.SetActive(false);
            obstacleBarrierPool.Add(obj);
        }

        // Type 2 = Stone Wall
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(obstacleStoneWallPrefab);
            obj.SetActive(false);
            obstacleStoneWallPool.Add(obj);
        }
    }

    public GameObject Acquire(int obstacleType)
    {
        List<GameObject> selectedPool = null;
        GameObject selectedPrefab = null;

        // Select correct pool + prefab
        if (obstacleType == 0)
        {
            selectedPool = obstacleBarrelPool;
            selectedPrefab = obstacleBarrelPrefab;
        }
        else if (obstacleType == 1)
        {
            selectedPool = obstacleBarrierPool;
            selectedPrefab = obstacleBarrierPrefab;
        }
        else if (obstacleType == 2)
        {
            selectedPool = obstacleStoneWallPool;
            selectedPrefab = obstacleStoneWallPrefab;
        }

        // Find inactive object in pool
        foreach (GameObject obj in selectedPool)
        {
            // skip destroyed object
            if (obj == null)
            {
                continue;
            }

            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // If no inactive object, create new one
        GameObject newObj = Instantiate(selectedPrefab);
        newObj.SetActive(true);
        selectedPool.Add(newObj);

        return newObj;
    }

    public void Release(GameObject obstacle, int obstacleType)
    {
        if (obstacle == null)
        {
            return;
        }

        // IMPORTANT:
        // Do NOT Destroy()
        // Just disable and reuse later
        obstacle.SetActive(false);

        // Ensure correct pool return
        if (obstacleType == 0)
        {
            if (!obstacleBarrelPool.Contains(obstacle))
            {
                obstacleBarrelPool.Add(obstacle);
            }
        }
        else if (obstacleType == 1)
        {
            if (!obstacleBarrierPool.Contains(obstacle))
            {
                obstacleBarrierPool.Add(obstacle);
            }
        }
        else if (obstacleType == 2)
        {
            if (!obstacleStoneWallPool.Contains(obstacle))
            {
                obstacleStoneWallPool.Add(obstacle);
            }
        }
    }
}