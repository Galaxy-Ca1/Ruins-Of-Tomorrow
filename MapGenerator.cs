using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public int width = 100; // Ширина карты
    public int height = 100; // Высота карты
    public float scale = 20f; // Масштаб шума Перлина

    public Tilemap tilemap; // Tilemap для генерации
    public TileBase groundTile; // Тайл земли
    public TileBase waterTile; // Тайл воды
    public float waterThreshold = 0.4f; // Порог для воды

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float perlinValue = Mathf.PerlinNoise(x / scale, y / scale); // Генерация значения шума
                TileBase tile = perlinValue > waterThreshold ? groundTile : waterTile; // Выбор тайла

                tilemap.SetTile(new Vector3Int(x, y, 0), tile); // Установка тайла на карте
            }
        }
    }
}
