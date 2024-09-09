
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation; // Required for NavMeshSurface

public class Tile : MonoBehaviour
{
    public enum MapTileType { Road, Building } // Enum to define the type of tile

    public Tile[] upNeighbours;
    public Tile[] rightNeighbours;
    public Tile[] downNeighbours;
    public Tile[] leftNeighbours;

    public MapTileType mapTileType; // Property to define the type of tile (Road or Building)

    private NavMeshSurface navMeshSurface; // NavMeshSurface to be added to Road tiles

    private void Start()
    {
        // Check if the tile is a road and add a NavMeshSurface component for NavMesh baking
        //if (mapTileType == MapTileType.Road)
        //{
        //    navMeshSurface = gameObject.AddComponent<NavMeshSurface>();
        //}
    }
}
