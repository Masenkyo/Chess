using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public ChessPieceType pieceType;
    public ChessPieceColor pieceColor;

    public void Initialize(ChessPieceType type, ChessPieceColor color)
    {
        pieceType = type;
        pieceColor = color;
    }
}
