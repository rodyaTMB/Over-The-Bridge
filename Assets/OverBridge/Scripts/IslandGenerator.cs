using UnityEngine;
using System.Collections.Generic;

public class IslandGenerator : MonoBehaviour
{
     public static IslandGenerator Instance;
    
    [Header("Island Settings")]
    public GameObject islandPrefab;
    public int poolSize = 10;
    public float minDistance = 3f;
    public float maxDistance = 8f;
    public float minWidth = 1f;
    public float maxWidth = 3f;
    
    [Header("Generation")]
    public Transform generationPoint;
    public float destroyPointX = -10f;
    
    private Queue<GameObject> islandPool = new Queue<GameObject>();
    private List<GameObject> activeIslands = new List<GameObject>();
    private Vector3 lastIslandPosition;
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        InitializePool();
        GenerateInitialIslands();
    }
    
    void Update()
    {
        // Уничтожаем острова, которые ушли за левую границу
        if (activeIslands.Count > 0 && activeIslands[0].transform.position.x < destroyPointX)
        {
            ReturnIslandToPool(activeIslands[0]);
        }
        
        // Генерируем новые острова при необходимости
        if (activeIslands.Count < poolSize / 2)
        {
            GenerateNewIsland();
        }
    }
    
    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject island = Instantiate(islandPrefab);
            island.SetActive(false);
            islandPool.Enqueue(island);
        }
    }
    
    void GenerateInitialIslands()
    {
        lastIslandPosition = Vector3.zero;
        
        // Создаем стартовый остров
        GameObject startIsland = GetIslandFromPool();
        startIsland.transform.position = lastIslandPosition;
        startIsland.transform.localScale = new Vector3(2f, 1f, 1f);
        
        // Генерируем несколько начальных островов
        for (int i = 0; i < 5; i++)
        {
            GenerateNewIsland();
        }
    }
    
    void GenerateNewIsland()
    {
        GameObject island = GetIslandFromPool();
        
        float distance = Random.Range(minDistance, maxDistance);
        float width = Random.Range(minWidth, maxWidth);
        
        lastIslandPosition.x += distance;
        island.transform.position = lastIslandPosition;
        island.transform.localScale = new Vector3(width, 1f, 1f);
        
        activeIslands.Add(island);
    }
    
    GameObject GetIslandFromPool()
    {
        if (islandPool.Count > 0)
        {
            GameObject island = islandPool.Dequeue();
            island.SetActive(true);
            return island;
        }
        else
        {
            // Если пул пуст, создаем новый остров
            GameObject island = Instantiate(islandPrefab);
            return island;
        }
    }
    
    void ReturnIslandToPool(GameObject island)
    {
        island.SetActive(false);
        activeIslands.Remove(island);
        islandPool.Enqueue(island);
    }
    
    public bool IsIslandAtPosition(Vector2 position)
    {
        foreach (GameObject island in activeIslands)
        {
            Collider2D collider = island.GetComponent<Collider2D>();
            if (collider.bounds.Contains(position))
            {
                return true;
            }
        }
        return false;
    }
    
    public void MoveToNextIsland()
    {
        // Смещаем все острова влево
        float moveDistance = activeIslands[1].transform.position.x;
        
        foreach (GameObject island in activeIslands)
        {
            island.transform.position -= new Vector3(moveDistance, 0, 0);
        }
        
        lastIslandPosition.x -= moveDistance;
        
        // Перемещаем героя на стартовую позицию
        GameManager.Instance.hero.transform.position = Vector3.zero;
        
        // Сбрасываем мост
        GameManager.Instance.bridge.ResetBridge();
        
        // Генерируем новые острова
        GenerateNewIsland();
    }
}
