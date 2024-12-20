using UnityEngine;

public class VehicleBehavior : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [HideInInspector] public bool IsRidden = false;
    void Update()
    {
        if (IsRidden)
        {
            transform.position = Player.transform.position;
        }
    }
}
