using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public struct BrickConfig
{
    public float r;
    public float g;
    public float b;

    public int x;
    public int y;
    public bool[,] shape;

    [JsonIgnore]
    public Color Color
    {
        get
        {
            return new Color(r / 255, g / 255, b / 255);
        }
        set
        {
            r = value.r * 255;
            g = value.g * 255;
            b = value.b * 255;
        }
    }
    [JsonIgnore]
    public Vector2Int LocalCenter
    {
        get
        {
            return new Vector2Int(x, y);
        }
        set
        {
            x = value.x;
            y = value.y;
        }
    }

}