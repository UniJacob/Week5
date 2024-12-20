using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Tilemap GameTilemap;
    [SerializeField] Tilemap FogTilemap;
    [SerializeField] TileBase Black, Blank;
    [SerializeField] TileBase[] ObstructingTiles;
    [Tooltip("The alpha of the black tiles")]
    List<Vector3Int> ObstructingTilesPositions;
    List<Vector3Int> allTilesPositions;

    void Start()
    {
        allTilesPositions = SetupTilemapPositions(GameTilemap);
    }
    void Update()
    {
        ObstructingTilesPositions = GetObstaclesLocations(GameTilemap);

        var ppos = Player.transform.position;
        var pposInt = new Vector3Int(Mathf.FloorToInt(ppos.x), Mathf.FloorToInt(ppos.y));
        UpdateFogOfWar(pposInt, ObstructingTilesPositions, allTilesPositions);
    }
    List<Vector3Int> SetupTilemapPositions(Tilemap tm)
    {
        List<Vector3Int> allPositions = new List<Vector3Int>();
        BoundsInt bounds = tm.cellBounds;

        for (int x = bounds.xMin; x <= bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y <= bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                allPositions.Add(tilePosition);
            }
        }
        return allPositions;
    }
    List<Vector3Int> GetObstaclesLocations(Tilemap tm)
    {
        List<Vector3Int> ans = new List<Vector3Int>();
        BoundsInt bounds = tm.cellBounds;
        for (int x = bounds.xMin; x <= bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y <= bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tileOnPosition = Player.GetComponent<KeyboardMoverByTile>().TileOnPosition(tilePosition);
                if (ObstructingTiles.Contains(tileOnPosition))
                {
                    ans.Add(tilePosition);
                }
            }
        }
        return ans;
    }
    /// <summary>
    /// This method calculates the tiles between the start and end points using Bresenham’s algorithm. 
    /// </summary>
    /// <param name="start">Source of the "sight"</param>
    /// <param name="end">Position to check if in line of sight</param>
    /// <returns>A list of all the tile-locations traversed by the line.</returns>
    public List<Vector3Int> GetLineOfSight(Vector3Int start, Vector3Int end)
    {
        List<Vector3Int> tilesInLine = new List<Vector3Int>();

        int dx = Mathf.Abs(end.x - start.x);
        int dy = Mathf.Abs(end.y - start.y);
        int sx = (start.x < end.x) ? 1 : -1;
        int sy = (start.y < end.y) ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            tilesInLine.Add(start);

            if (start.x == end.x && start.y == end.y)
                break;

            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                start.x += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                start.y += sy;
            }
        }
        return tilesInLine;
    }
    public bool IsTileBehind(Vector3Int source, Vector3Int target, List<Vector3Int> obstacles)
    {
        List<Vector3Int> line = GetLineOfSight(source, target);
        foreach (var tile in line)
        {
            if (tile == target)
            {
                return false;
            }
            if (obstacles.Contains(tile))
            {
                return true;
            }
        }
        return false;
    }
    public void UpdateFogOfWar(Vector3Int playerPosition, List<Vector3Int> obstacles, List<Vector3Int> allTiles)
    {
        foreach (var tilePosition in allTiles) // allTiles could be the entire grid
        {
            bool isVisible = !IsTileBehind(playerPosition, tilePosition, obstacles);
            TileBase tileToPlace;
            if (isVisible)
            {
                tileToPlace = Blank;
            }
            else
            {
                tileToPlace = Black;
            }
            FogTilemap.SetTile(new Vector3Int(tilePosition.x, tilePosition.y, 0), tileToPlace);
        }
    }
}
