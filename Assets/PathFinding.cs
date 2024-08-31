using UnityEngine;
using System.Collections.Generic;

public class Pathfinding
{
    public Vector3 target;
    public List<Vector3> path;
    public int targetIndex;

    // Konstruktor
    public Pathfinding() { }

    // Konstruktor z argumentami
    public Pathfinding(int width, int height, Vector2Int startPosition, Vector2Int targetPosition, int[,] maze) { }

    // Metoda do generowania �cie�ki
    public void GeneratePath() { }

    // Metoda do znajdowania �cie�ki
    public List<Vector3> FindPath() { return null; }
}