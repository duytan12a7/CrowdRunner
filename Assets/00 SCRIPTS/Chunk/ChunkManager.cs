using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    private static ChunkManager instance;
    public static ChunkManager Instance => instance;

    [Header(" Elements ")]
    [SerializeField] private LevelData[] levelDatas;
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

        LevelData level = levelDatas[0];
        CreateLevel(level.chunks);
    }

    private void CreateLevel(Chunk[] levelChunks)
    {
        Vector3 chunkPosition = Vector3.zero;

        for (int i = 0; i < levelChunks.Length; i++)
        {
            Chunk chunkToCreate = levelChunks[i];

            if (i > 0)
                chunkPosition.z += chunkToCreate.GetLength() / 2;

            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }

    public float GetFinishLineZ()
    {
        return finishLine.position.z;
    }

    public int GetIntLevel() => PlayerPrefs.GetInt("level", 0);

}
