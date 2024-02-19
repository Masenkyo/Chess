using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class ChessBoard : MonoBehaviour
{
    public TileBase whiteTile;
    public TileBase blackTile;
    public GameObject[] chessPiecePrefabs;
    private Tilemap tilemap;
    private Dictionary<(ChessPieceType, ChessPieceColor), GameObject> prefabDictionary;
    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
        GenerateChessBoard();
        BuildPrefabDictionary();
        PlaceChessPieces();
    }
    private void GenerateChessBoard()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                TileBase tile = (x + y) % 2 == 0 ? whiteTile : blackTile;
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
        }
    }
    private void BuildPrefabDictionary()
    {
        prefabDictionary = new Dictionary<(ChessPieceType, ChessPieceColor), GameObject>();

        foreach (GameObject prefab in chessPiecePrefabs)
        {
            ChessPiece chessPiece = prefab.GetComponent<ChessPiece>();
            if (chessPiece != null)
            {
                prefabDictionary.Add((chessPiece.pieceType, chessPiece.pieceColor), prefab);
            }
        }
    }
    private void PlaceChessPieces()
    {
        ChessPieceType[] pieceTypes = { ChessPieceType.Rook, ChessPieceType.Knight, ChessPieceType.Bishop, ChessPieceType.Queen, ChessPieceType.King, ChessPieceType.Bishop, ChessPieceType.Knight, ChessPieceType.Rook };

        for (int i = 0; i < 8; i++)
        {
            InstantiateChessPiece(new(i, 0, 0), pieceTypes[i], ChessPieceColor.White);
            InstantiateChessPiece(new(i, 7, 0), pieceTypes[i], ChessPieceColor.Black);
            InstantiateChessPiece(new(i, 1, 0), ChessPieceType.Pawn, ChessPieceColor.White);
            InstantiateChessPiece(new(i, 6, 0), ChessPieceType.Pawn, ChessPieceColor.Black);
        }
    }
    private void InstantiateChessPiece(Vector3Int position, ChessPieceType type, ChessPieceColor color)
    {
        Vector3 worldPosition = tilemap.GetCellCenterWorld(position);

        if (prefabDictionary.TryGetValue((type, color), out GameObject chessPiecePrefab))
        {
            GameObject chessPiece = Instantiate(chessPiecePrefab, worldPosition, Quaternion.identity);
            chessPiece.GetComponent<ChessPiece>().Initialize(type, color);
            chessPiece.transform.parent = tilemap.transform;
        }
    }
}
public enum ChessPieceType
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}
public enum ChessPieceColor
{
    White,
    Black
}
