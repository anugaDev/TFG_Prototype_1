﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfiguration", menuName = "Config/ItemConfiguration")]
public class ItemConfig : ScriptableObject
{
    public int InteractableLayer;
    public int NonInteractableLayer;
}
