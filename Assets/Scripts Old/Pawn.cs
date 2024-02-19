using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pawn : Piece
{
    public List<Vector3> result = new();
    public bool drag;
    void Start()
    {
         Pieces.Add(this);
    }
    void Update()
    {
        dragging();
        if (drag) GetMovementOptions();

    }
    public List<Vector3> GetMovementOptions()
    {
        
        result.Add(transform.position + new Vector3(0, 1));

        if (transform.position.y == 0)
            result.Add(transform.position + new Vector3(0, 2));

        if (Pieces.Where(c => c.team == teams.black).Any(c => c.transform.position == transform.position + new Vector3(-1, 1)))
            result.Add(transform.position + new Vector3(-1, 1));

        if (Pieces.Where(c => c.team == teams.black).Any(c => c.transform.position == transform.position + new Vector3(1, 1)))
            result.Add(transform.position + new Vector3(1, 1));

        result = result.Where(c => Coords.tilecheck(c, this)).ToList();
        return result;
    }
    void dragging()
    {
        if (drag) transform.position = Coords.mousepos;
        if (Input.GetMouseButtonDown(0) && Coords.mousepos == transform.position && drag == false)
        {
            Coords.originalPos = Coords.mousepos;
            drag = true;
        }
        else if (Input.GetMouseButtonUp(0) && drag == true)
        {
            if (Coords.tilecheck(Coords.mousepos, this) == true)
                gameObject.transform.position = Coords.mousepos;
            else gameObject.transform.position = Coords.originalPos;
            drag = false;
        }
    }
}
