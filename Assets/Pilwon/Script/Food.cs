using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Food
{
    public string foodName;
    public Sprite foodSprite;
    [Range(0, 10)] public int foodCompleteness = 100; // 음식완성도
    [SerializeField] private bool isComplete;

    //  ---- Property ----
    public bool IsComplete
    {
        get { return isComplete; }
        set
        {
            isComplete = value;
            if (isComplete == true)
            {
                GameManager.instance.moodChange();
                GameManager.instance.currentRestTime = 0;
                GameManager.instance.isGame = false;

                FoodManager.instance.foodOrderIndex++;
            }
        }
    }
    // --------------------
    [Space(5)] public Materials[] foodMaterial;

    [Space(5), Header("# Food Time")]
    public float waitingTime; // 대기시간
}

[System.Serializable]
public class Materials
{
    public string materialName;
    public Sprite materialSprite;
    public int materialCount;
    [SerializeField] private bool isComplete;

    //  ---- Property ----
    public bool IsComplete
    {
        get { return isComplete; }
        set
        {
            isComplete = value;
            if (isComplete == true)
            {
                if (FoodManager.instance.materialIndex >= FoodManager.instance.currentFood.foodMaterial.Length - 1) return;
                FoodManager.instance.materialIndex++;

                FoodManager.instance.MaterialInit();
            }
        }
    }
    // --------------------
}

