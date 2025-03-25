using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public int width = 100;
    public int height = 100;
    public float noiseScale = 10f;
    public float riverNoiseScale = 15f; // Размер волн для рек
    public float lakeThreshold = 0.7f; // Порог для озёр

    public Tilemap tilemap; // Карта земли
    public Tilemap objectTilemap; // Отдельный Tilemap для деревьев, кустов и камней
    public TileBase grassTile, sandTile, jungleTile, snowTile;
    public TileBase waterTile; // Вода (реки и озёра)

    public GameObject[] trees;
    public GameObject[] bushes;
    public GameObject[] rocks;

    void Start()
    {
        GenerateMap();
        GenerateObjects();
    }

    void GenerateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float noiseValue = Mathf.PerlinNoise(x / noiseScale, y / noiseScale);
                float riverNoise = Mathf.PerlinNoise(x / riverNoiseScale, y / riverNoiseScale);

                TileBase selectedTile;

                // Генерация рек и озёр
                if (riverNoise > lakeThreshold)
                {
                    selectedTile = waterTile; // Озеро
                }
                else if (riverNoise > 0.5f && Random.value < 0.02f)
                {
                    selectedTile = waterTile; // Река
                }
                else
                {
                    selectedTile = GetTileForBiome(noiseValue);
                }

                tilemap.SetTile(new Vector3Int(x, y, 0), selectedTile);
            }
        }
    }

    TileBase GetTileForBiome(float noiseValue)
    {
        if (noiseValue < 0.25f) return snowTile;
        else if (noiseValue < 0.5f) return jungleTile;
        else if (noiseValue < 0.75f) return sandTile;
        else return grassTile;
    }

    void GenerateObjects()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileBase currentTile = tilemap.GetTile(new Vector3Int(x, y, 0));

                if (currentTile == waterTile) continue; // Никакие ресурсы не спавним в воде

                if (currentTile == grassTile) // Деревья на траве
                {
                    if (Random.value < 0.1f)
                    {
                        Instantiate(trees[Random.Range(0, trees.Length)], new Vector3(x, y, -0.1f), Quaternion.identity);
                    }
                }
                else if (currentTile == jungleTile) // Кусты в джунглях
                {
                    if (Random.value < 0.15f)
                    {
                        Instantiate(bushes[Random.Range(0, bushes.Length)], new Vector3(x, y, -0.1f), Quaternion.identity);
                    }
                }
                else if (currentTile == sandTile) // Камни в пустыне
                {
                    if (Random.value < 0.05f)
                    {
                        Instantiate(rocks[Random.Range(0, rocks.Length)], new Vector3(x, y, -0.1f), Quaternion.identity);
                    }
                }
                else if (currentTile == snowTile) // Камни в снегу
                {
                    if (Random.value < 0.08f)
                    {
                        Instantiate(rocks[Random.Range(0, rocks.Length)], new Vector3(x, y, -0.1f), Quaternion.identity);
                    }
                }
            }
        }
    }
}
