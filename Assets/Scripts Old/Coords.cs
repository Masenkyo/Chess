using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Coords : MonoBehaviour
{
    public static Vector3 originalPos;
    public static Camera cam => Camera.main.GetComponent<Camera>();
    public static Vector3 mousepos => new Vector3((Mathf.Floor(cam.ScreenToWorldPoint(Input.mousePosition).x)) + 0.5f, (Mathf.Floor(cam.ScreenToWorldPoint(Input.mousePosition).y)) + 0.5f, 0);
    public static Tilemap tiles;
    public Tilemap tiles2;
    void Start()
    {
        tiles = tiles2;
    }
    public static bool tilecheck(Vector3 c, Piece pa)
    {
        if (!tiles.HasTile(tiles.WorldToCell(c))) return false;
        foreach(Pawn p in Piece.Pieces)
        {
            if (p.drag) continue;
            if (p.transform.position == c) return false;
        }
        if (pa.team == Piece.teams.white) if (c.y < originalPos.y) return false;
        if (pa.team == Piece.teams.black) if (c.y > originalPos.y) return false;

        return true;
    }
}
