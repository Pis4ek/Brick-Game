using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBrickConfig", menuName = "Custom/Create Brick Conifg")]
public class BrickConfigScriptable : ScriptableObject
{
    [SerializeField] public string Name;

    //[SerializeField] public bool[,] BlockMap = new bool[4,4];
    public BoolMatrix2D matrix2D;
    [SerializeField] public Color BrickColor;

    [SerializeField] public Vector2Int CenterPositon;
}

[Serializable]
public class BoolMatrix2D : UnityEngine.Object
{
    public int sizeX;
    public int sizeY;
    public bool[,] BoolMatrix;
}