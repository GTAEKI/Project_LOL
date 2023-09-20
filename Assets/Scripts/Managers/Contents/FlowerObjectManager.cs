using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerObjectManager
{
    private string flowerPrefabPath;                                // 생성할 프리팹 경로
    private List<Transform> spawnPoints = new List<Transform>();    // 오브젝트 생성 위치

    public List<GameObject> SpawnObjs { private set; get; } = new List<GameObject>();       // 생성된 오브젝트 리스트

    /// <summary>
    /// 꽃 오브젝트 초기화 함수
    /// 김민섭_230919
    /// </summary>
    /// <param name="tag">오브젝트 태그</param>
    public void Init(string tag)
    {
        flowerPrefabPath = "Maps/Object/";

        switch(tag)
        {
            case "HealFlowerSpawnPoint": flowerPrefabPath += "HealFlower";  break;
            case "ExplosionFlowerSpawnPoint": flowerPrefabPath += "ExplosionFlower"; break;
        }

        GameObject[] points = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < points.Length; i++)
        {
            spawnPoints.Add(points[i].transform);
        }
    }

    /// <summary>
    /// 오브젝트 생성 함수
    /// 김민섭_230919
    /// </summary>
    public void Create()
    {
        foreach(Transform point in spawnPoints)
        {
            SpawnObjs.Add(Managers.Resource.Instantiate(flowerPrefabPath, point.position, Quaternion.identity));
        }
    }
}
