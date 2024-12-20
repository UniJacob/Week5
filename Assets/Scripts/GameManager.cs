using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOver;
    [HideInInspector] public bool gameOver = false;
    void Start()
    {

    }

    void Update()
    {
        if (gameOver)
        {
            GameOver.SetActive(true);
        }
    }
}
