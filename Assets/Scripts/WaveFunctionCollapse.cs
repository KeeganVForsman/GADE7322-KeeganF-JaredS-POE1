using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WaveFunctionCollapse : MonoBehaviour
{
    public int dimensions;
    public Tile[] tileObjects;
    public Cell cellObj;

    public Tile backupTile;

    private int iteration;
    private Cell[,] cellGrid; // Use a 2D array for fast access

    private void Awake()
    {
        cellGrid = new Cell[dimensions, dimensions];
        InitializeGrid();
    }

    void InitializeGrid()
    {
        int centerX = dimensions / 2;
        int centerY = dimensions / 2;

        for (int x = 0; x < dimensions; x++)
        {
            for (int y = 0; y < dimensions; y++)
            {
                if ((x == centerX || x == centerX - 1) && (y == centerY || y == centerY - 1))
                {
                    continue;
                }

                Vector3 position = new Vector3(x, 0, y);
                Cell newCell = Instantiate(cellObj, position, Quaternion.identity);
                newCell.CreateCell(false, tileObjects);
                cellGrid[x, y] = newCell;
            }
        }

        StartCoroutine(CheckEntropy());
    }

    IEnumerator CheckEntropy()
    {
        List<Cell> tempGrid = new List<Cell>();
        for (int x = 0; x < dimensions; x++)
        {
            for (int y = 0; y < dimensions; y++)
            {
                var cell = cellGrid[x, y];
                if (cell != null && !cell.collapsed)
                {
                    tempGrid.Add(cell);
                }
            }
        }

        if (tempGrid.Count == 0)
        {
            yield break; // Stop further execution if all cells are collapsed or no cells left
        }

        tempGrid.Sort((a, b) => a.tileOptions.Length - b.tileOptions.Length);
        int minOptions = tempGrid[0].tileOptions.Length;
        tempGrid.RemoveAll(a => a.tileOptions.Length != minOptions);

        if (tempGrid.Count == 0)
        {
            yield break; // No valid options left
        }

        yield return new WaitForSeconds(0.000000000000000000000000000000000000000000000000000000000000000000000001f); // Increased wait time to reduce CPU load

        CollapseCell(tempGrid);
    }

    void CollapseCell(List<Cell> tempGrid)
    {
        if (tempGrid.Count == 0)
        {
            return; // No cells left to collapse
        }

        int randIndex = UnityEngine.Random.Range(0, tempGrid.Count);
        Cell cellToCollapse = tempGrid[randIndex];

        cellToCollapse.collapsed = true;
        try
        {
            Tile selectedTile = cellToCollapse.tileOptions[UnityEngine.Random.Range(0, cellToCollapse.tileOptions.Length)];
            cellToCollapse.tileOptions = new Tile[] { selectedTile };
        }
        catch
        {
            Tile selectedTile = backupTile;
            cellToCollapse.tileOptions = new Tile[] { selectedTile };
        }

        Tile foundTile = cellToCollapse.tileOptions[0];
        Instantiate(foundTile, cellToCollapse.transform.position, foundTile.transform.rotation);

        UpdateGeneration();
    }

    void UpdateGeneration()
    {
        Cell[,] newCellGrid = new Cell[dimensions, dimensions];

        int centerX = dimensions / 2;
        int centerY = dimensions / 2;

        for (int x = 0; x < dimensions; x++)
        {
            for (int y = 0; y < dimensions; y++)
            {
                if ((x == centerX || x == centerX - 1) && (y == centerY || y == centerY - 1))
                {
                    continue;
                }

                var cell = cellGrid[x, y];
                if (cell != null)
                {
                    if (cell.collapsed)
                    {
                        newCellGrid[x, y] = cell;
                    }
                    else
                    {
                        List<Tile> options = new List<Tile>(tileObjects);
                        UpdateOptionsForNeighbors(x, y, ref options);

                        Tile[] newTileList = options.ToArray();
                        cell.RecreateCell(newTileList);
                        newCellGrid[x, y] = cell;
                    }
                }
            }
        }

        cellGrid = newCellGrid;
        iteration++;

        if (iteration < dimensions * dimensions)
        {
            StartCoroutine(CheckEntropy());
        }
    }

    void UpdateOptionsForNeighbors(int x, int y, ref List<Tile> options)
    {
        Vector3[] neighbors = {
            new Vector3(x, 0, y - 1), // Up
            new Vector3(x + 1, 0, y), // Left
            new Vector3(x, 0, y + 1), // Down
            new Vector3(x - 1, 0, y)  // Right
        };

        Func<Tile, Tile[]>[] neighborSelectors = {
            tile => tile.downNeighbours,  // Up
            tile => tile.rightNeighbours, // Left
            tile => tile.upNeighbours,    // Down
            tile => tile.leftNeighbours   // Right
        };

        for (int i = 0; i < neighbors.Length; i++)
        {
            Vector3 neighborPosition = neighbors[i];
            int neighborX = (int)neighborPosition.x;
            int neighborY = (int)neighborPosition.z;
            if (IsInBounds(neighborX, neighborY) && cellGrid[neighborX, neighborY] != null)
            {
                Cell neighborCell = cellGrid[neighborX, neighborY];
                List<Tile> validOptions = GetValidOptions(neighborCell, neighborSelectors[i]);
                CheckValidity(options, validOptions);
            }
        }
    }

    bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < dimensions && y >= 0 && y < dimensions;
    }

    List<Tile> GetValidOptions(Cell cell, Func<Tile, Tile[]> neighborSelector)
    {
        HashSet<Tile> validOptions = new HashSet<Tile>();

        foreach (Tile possibleOption in cell.tileOptions)
        {
            int validOptionIndex = Array.FindIndex(tileObjects, obj => obj == possibleOption);
            var valid = neighborSelector(tileObjects[validOptionIndex]);
            validOptions.UnionWith(valid);
        }

        return validOptions.ToList();
    }

    void CheckValidity(List<Tile> optionList, List<Tile> validOptions)
    {
        optionList.RemoveAll(element => !validOptions.Contains(element));
    }
}