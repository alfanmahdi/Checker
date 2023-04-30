using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    // Bord positions, not world positions
    int matrixX, matrixY;

    // false: movement, true: attacking
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            // Change to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            Destroy(cp);
        }

        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Checkersman>().GetXBoard(),
            reference.GetComponent<Checkersman>().GetYBoard());

        reference.GetComponent<Checkersman>().SetXBoard(matrixX);
        reference.GetComponent<Checkersman>().SetYBoard(matrixY);
        reference.GetComponent<Checkersman>().SetCoords();

        controller.GetComponent<Game>().SetPosition(reference);

        controller.GetComponent<Game>().NextTurn();

        reference.GetComponent<Checkersman>().DestroyMovePlates();
    }


    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }

    public void SetAttack(bool attack)
    {
        this.attack = attack;
        if (attack)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }


}
