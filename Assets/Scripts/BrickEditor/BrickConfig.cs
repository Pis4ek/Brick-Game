using Services.ConfigServiceComponents;
using System;
using UnityEngine;

public class BrickConfig
{
    public string Name { get; private set;}

    public bool[,] BlockMap { get; private set;}

    public Color BrickColor { get; private set;}

    public Vector2Int CenterPositon { get; private set;}

    public Texture2D BrickImage { get; private set; }

    public bool UseInGame { get; private set; }

    public BrickConfig(string name, bool[,] blockMap, Color color, Vector2Int centerPosition, Texture2D image, bool useInGame) 
    {
        Name = name;
        BlockMap = blockMap;
        BrickColor = color;
        CenterPositon = centerPosition;
        UseInGame = useInGame;
        BrickImage = image;
    }

    public BrickConfig(string name, Texture2D texture2D)
    {
        Name = name;
        BrickImage = new Texture2D(texture2D.width,texture2D.height);
        Graphics.CopyTexture(texture2D, BrickImage);
    }
}
