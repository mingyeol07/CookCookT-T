using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Tracing;

public class Recipe : MonoBehaviour
{
    public static Recipe instance { get; private set; }

    [SerializeField] private Image currentMaterial;
    [SerializeField] private Image nextMaterial;
    [SerializeField] private Image endMaterial;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

    }

    private void Start()
    {
        Recipe.instance.RecipeInit();
    }

    public void RecipeInit()
    {
        FoodManager instance = FoodManager.instance;

        currentMaterial.sprite = instance.currentFood.
            foodMaterial[instance.materialIndex].materialSprite;

        if (instance.materialIndex + 1 < instance.currentFood.foodMaterial.Length)
        {
            nextMaterial.sprite = instance.currentFood.
            foodMaterial[instance.materialIndex + 1].materialSprite;
        }
        else
        {
            nextMaterial.sprite = endMaterial.sprite;
        }
    }
}
