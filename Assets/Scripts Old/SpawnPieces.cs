using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnPieces : MonoBehaviour
{
    public GameObject WhiteTeam;
    public GameObject BlackTeam;
    void Start()
    {
        Instantiate(BlackTeam);
        Instantiate(WhiteTeam);
    }
}
