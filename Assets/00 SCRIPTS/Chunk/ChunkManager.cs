using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    private static ChunkManager instance;
    public static ChunkManager Instance => instance;

    [Header(" Elements ")]
    [SerializeField] private GameObject[] levelDatas;
    private Transform finishLine;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GenerateLevel();

        finishLine = GameObject.FindWithTag(Global.FINISH_TAG).transform;
    }

    private void GenerateLevel()
    {
        int currentLevel = GetIntLevel();
        CreateLevel(levelDatas[currentLevel]);
    }

    private void CreateLevel(GameObject level)
    {
        Vector3 chunkPosition = Vector3.zero;

        GameObject chunkInstance = Instantiate(level, chunkPosition, Quaternion.identity, transform);
    }

    public float GetFinishLineZ()
    {
        return finishLine.position.z;
    }

    public int GetIntLevel() => PlayerPrefs.GetInt("level", 0);

    public int GetCountLevel() => levelDatas.Length;

}
