using Services.ConfigServiceComponents;
using System;
using UnityEngine;

public class BrickConfig : ScriptableObject, IConfig
{
    public string KeyID => "cringe";
    public Type ConfigType => typeof(BrickConfig);



}
