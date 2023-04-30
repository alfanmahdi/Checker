using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject checkerspiece;

    // Positions and team for each checkerspiece
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[12];
    private GameObject[] playerRed = new GameObject[12];

    private string currentPlayer = "black";

    private bool gameOver = false;

    // Start is called before the first frame update
    public void Start()
    {
        playerBlack = new GameObject[] {
            Create("black", 0, 0), Create("black", 2, 0), Create("black", 4, 0), Create("black", 6, 0),
            Create("black", 1, 1), Create("black", 3, 1), Create("black", 5, 1), Create("black", 7, 1),
            Create("black", 0, 2), Create("black", 2, 2), Create("black", 4, 2), Create("black", 6, 2)
        };
        playerRed = new GameObject[] {
            Create("red", 1, 7), Create("red", 3, 7), Create("red", 5, 7), Create("red", 7, 7),
            Create("red", 0, 6), Create("red", 2, 6), Create("red", 4, 6), Create("red", 6, 6),
            Create("red", 1, 5), Create("red", 3, 5), Create("red", 5, 5), Create("red", 7, 5)
        };

        // Set all piece positions on the position board
        for (int i = 0; i < playerRed.Length; i++)
        {
            SetPosition(playerRed[i]);
            SetPosition(playerBlack[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(checkerspiece, new Vector3(0,0,-1), Quaternion.identity);
        Checkersman cm = obj.GetComponent<Checkersman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Checkersman cm = obj.GetComponent<Checkersman>();

        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "black")
        {
            currentPlayer = "red";
        }
        else
        {
            currentPlayer = "black";
        }
    }

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game");
        }
    }
}
