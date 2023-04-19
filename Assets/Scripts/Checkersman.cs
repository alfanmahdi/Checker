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
            case "black": this.GetComponent<SpriteRenderer>().sprite = black; break;
            case "king_black": this.GetComponent<SpriteRenderer>().sprite = king_black; break;
            case "red": this.GetComponent<SpriteRenderer>().sprite = red; break;
            case "king_red": this.GetComponent<SpriteRenderer>().sprite = king_red; break;
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
}
