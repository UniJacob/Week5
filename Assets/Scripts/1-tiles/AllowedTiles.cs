using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component just keeps a list of allowed tiles.
 * Such a list is used both for pathfinding and for movement.
 */
public class AllowedTiles : MonoBehaviour  {
    public TileBase[] PlayerTiles;
    public TileBase[] BoatTiles;
    public TileBase[] ShoreTiles;
    public TileBase[] GoatTiles;
    public TileBase[] MinableTiles;

    //public bool Contains(TileBase tile) {
    //    return PlayerAllowedTiles.Contains(tile);
    //}

    public TileBase[] Get() { return PlayerTiles;  }
}
