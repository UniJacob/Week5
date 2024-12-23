using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOver;
    [SerializeField] GameObject YouWin;
    [SerializeField] GameObject BlackGrid;
    [HideInInspector] public bool gameOver = false;
    [HideInInspector] public bool youwin = false;
    void Start()
    {

    }

    void Update()
    {
        if (youwin)
        {
            BlackGrid.SetActive(false);
            YouWin.SetActive(true);
            return;
        }
        if (gameOver)
        {
            BlackGrid.SetActive(false);
            GameOver.SetActive(true);
        }
    }
}
