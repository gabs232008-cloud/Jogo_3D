using UnityEngine;

public class ProceduralForestGenerator : MonoBehaviour
{
    [Header("Player Reference")]
    public Transform playerTransform;

    [Header("Tree Prefabs")]
    public GameObject[] commonTrees;

    [Header("Map Settings")]
    public int commonTreeCount = 150;
    public float commonMapRadius = 90f;
    public float safeZoneRadius = 10f;

    void Start()
    {
        GenerateForest();
    }

    void GenerateForest()
    {
        Vector3 playerPos = playerTransform != null ? playerTransform.position : Vector3.zero;
        int spawnedCommon = 0;
        int safetyNet = 0;

        while (spawnedCommon < commonTreeCount && safetyNet < 1000)
        {
            safetyNet++;
            if (commonTrees.Length == 0) break;

            Vector3 candidatePos = GetRandomPositionAround(playerPos, safeZoneRadius, commonMapRadius);

            if (Vector3.Distance(new Vector3(candidatePos.x, 0, candidatePos.z), new Vector3(playerPos.x, 0, playerPos.z)) > safeZoneRadius)
            {
                GameObject selectedPrefab = commonTrees[Random.Range(0, commonTrees.Length)];
                SpawnTree(selectedPrefab, candidatePos);
                spawnedCommon++;
            }
        }
    }

    Vector3 GetRandomPositionAround(Vector3 center, float minRadius, float maxRadius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Random.Range(minRadius, maxRadius);

        float x = center.x + Mathf.Cos(angle) * distance;
        float z = center.z + Mathf.Sin(angle) * distance;

        return new Vector3(x, 0f, z);
    }

    void SpawnTree(GameObject prefab, Vector3 position)
    {
        GameObject newTree = Instantiate(prefab, position, Quaternion.identity, null);
        newTree.transform.Rotate(0, Random.Range(0f, 360f), 0);
        newTree.transform.localScale = Vector3.one;
    }
}