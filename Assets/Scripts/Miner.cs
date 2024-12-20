using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Miner : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] KeyboardMoverByTile PlayerMoveScript;
    [SerializeField] AllowedTiles AllowedTiles;
    [SerializeField] InputAction MineAction;
    [SerializeField] TileBase AfterMineTile;
    [HideInInspector] public bool HasPickaxe = false;

    private void OnEnable()
    {
        MineAction.Enable();
    }

    private void OnDisable()
    {
        MineAction.Disable();
    }
    void Update()
    {
        if (HasPickaxe && MineAction.WasPerformedThisFrame())
        {
            Vector2 mineDirection = PlayerMoveScript.LastDirection;
            if (mineDirection.y != 0)
            {
                mineDirection = new(0, mineDirection.y);
            }
            Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
            Vector2 minelocation = mineDirection + position2D;

            if (AllowedTiles.MinableTiles.Contains(PlayerMoveScript.TileOnPosition(minelocation)))
            {
                Vector3Int minelocation3D = new Vector3Int(Mathf.FloorToInt(minelocation.x), Mathf.FloorToInt(minelocation.y), 0);
                tilemap.SetTile(minelocation3D, AfterMineTile);
            }
        }
    }
}
