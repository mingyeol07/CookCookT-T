using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialData : MonoBehaviour
{
    public string materialName;
    public Sprite materialSprite;

    public void MaterialInit(string materialName, Sprite materialSprite)
    {
        this.materialName = materialName;
        this.materialSprite = materialSprite;
    }
}
