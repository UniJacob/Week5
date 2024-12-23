using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile : KeyboardMover
{
    [SerializeField] GameManager GameManager;
    [SerializeField] Tilemap tilemap = null;
    //    [SerializeField] TileBase[] allowedTiles = null;
    [SerializeField] AllowedTiles AllowedTiles;

    [SerializeField] GameObject Boat;
    [SerializeField] GameObject Goat;
    [SerializeField] GameObject Pickaxe;
    [SerializeField] string PrincessTag = "Princess";
    bool onBoat = false;
    bool onGoat = false;
    Vector3 ScaleWhenOnBoat = new(0.5f, 0.5f, 1);
    Vector3 ScaleWhenOnGoat = new(0.75f, 0.75f, 1);
    Vector3 NormalScale;

    //[HideInInspector] public Vector2 LastDirection = new(0, 1);

    public TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }
    void Start()
    {
        NormalScale = transform.localScale;
    }

    void Update()
    {
        Vector3 newPosition = NewPosition();
        bool shouldMove = false;
        TileBase tileOnNewPosition = TileOnPosition(newPosition);

        if (onBoat)
        {
            if (AllowedTiles.ShoreTiles.Contains(tileOnNewPosition))
            {
                shouldMove = true;
                transform.localScale = NormalScale;
                onBoat = false;
                Boat.GetComponent<VehicleBehavior>().IsRidden = false;
            }
            else if (AllowedTiles.BoatTiles.Contains(tileOnNewPosition))
            {
                shouldMove = true;
            }
        }
        else if (onGoat)
        {
            if (AllowedTiles.PlayerTiles.Contains(tileOnNewPosition))
            {
                shouldMove = true;
                transform.localScale = NormalScale;
                onGoat = false;
                Goat.GetComponent<VehicleBehavior>().IsRidden = false;
            }
            else if (AllowedTiles.GoatTiles.Contains(tileOnNewPosition))
            {
                shouldMove = true;
            }
        }
        else if (AllowedTiles.PlayerTiles.Contains(tileOnNewPosition))
        {
            shouldMove = true;
        }
        else if (newPosition == Boat.transform.position)
        {
            shouldMove = true;
            transform.localScale = ScaleWhenOnBoat;
            onBoat = true;
            Boat.GetComponent<VehicleBehavior>().IsRidden = true;
        }
        else if (newPosition == Goat.transform.position)
        {
            shouldMove = true;
            transform.localScale = ScaleWhenOnGoat;
            onGoat = true;
            Goat.GetComponent<VehicleBehavior>().IsRidden = true;
        }
        if (newPosition == Pickaxe.transform.position)
        {
            GetComponent<Miner>().HasPickaxe = true;
            Pickaxe.GetComponent<VehicleBehavior>().IsRidden = true;
        }

        if (shouldMove)
        {
            transform.position = newPosition;
        }
        //else
        //{
        //    Debug.LogError("You cannot walk on " + tileOnNewPosition + "!");
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIGGER");
        if (collision.CompareTag(PrincessTag))
        {
            GameManager.youwin = true;
            return;
        }
        GameManager.gameOver = true;
    }
}
