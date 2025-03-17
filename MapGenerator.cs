using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public int width = 100; // ������ �����
    public int height = 100; // ������ �����
    public float scale = 20f; // ������� ���� �������

    public Tilemap tilemap; // Tilemap ��� ���������
    public TileBase groundTile; // ���� �����
    public TileBase waterTile; // ���� ����
    public float waterThreshold = 0.4f; // ����� ��� ����

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
                float perlinValue = Mathf.PerlinNoise(x / scale, y / scale); // ��������� �������� ����
                TileBase tile = perlinValue > waterThreshold ? groundTile : waterTile; // ����� �����

                tilemap.SetTile(new Vector3Int(x, y, 0), tile); // ��������� ����� �� �����
            }
        }
    }
}
