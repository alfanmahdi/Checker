using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkersman : MonoBehaviour
{
    // References
    public GameObject controller;
    public GameObject movePlate;

    // Positions
    private int xBoard = -1;
    private int yBoard = -1;

    // Variable to keep track of "black" player or "red" player
    private string player;

    // References for all the sprites that the checkerspiece can be
    public Sprite black, king_black, red, king_red;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        // Take the instantiated location and adjust the transform
        SetCoords();

        switch (this.name)
        {
            case "black": this.GetComponent<SpriteRenderer>().sprite = black; player = "black"; break;
            case "king_black": this.GetComponent<SpriteRenderer>().sprite = king_black; player = "black"; break;
            case "red": this.GetComponent<SpriteRenderer>().sprite = red; player = "red"; break;
            case "king_red": this.GetComponent<SpriteRenderer>().sprite = king_red; player = "red"; break;
        }
    }

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.88f;
        y *= 0.88f;

        x += -3.08f;
        y += -3.08f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();

            InitiateMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black":
                LineMovePlate(1, 1);
                LineMovePlate(-1, 1);
                break;
            case "red":
                LineMovePlate(1, -1);
                LineMovePlate(-1, -1);
                break;
            case"king_black":
            case"king_red":
                LineMovePlate(1, 1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                LineMovePlate(1, -1);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        if(IsKing())
        {
            while (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y) == null)
            {
                MovePlateSpawn(x, y);
                x += xIncrement;
                y += yIncrement;
            }

            if (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y).GetComponent<Checkersman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        } else
        {
            if (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y) == null)
            {
                MovePlateSpawn(x, y);
            } else if (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y).GetComponent<Checkersman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
            if (y == (player == "black" ? 7 : 0))
                {
                    PromoteToKing();
                }
        }
    }

    public bool IsKing()
    {
        switch (this.name)
        {
            case "king_black":
            case "king_red":
                return true;
            default:
                return false;
        }
    }
    
    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.88f;
        y *= 0.88f;

        //Add constants (pos 0,0)
        x += -3.08f;
        y += -3.08f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.88f;
        y *= 0.88f;

        //Add constants (pos 0,0)
        x += -3.08f;
        y += -3.08f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void PromoteToKing()
    {
        switch (this.name)
        {
            case "black": transform.name = "king_black"; this.GetComponent<SpriteRenderer>().sprite = king_black; break;
            case "red": transform.name = "king_red"; this.GetComponent<SpriteRenderer>().sprite = king_red; break;
        }
    }
}