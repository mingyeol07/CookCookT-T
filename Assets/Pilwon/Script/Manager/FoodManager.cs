 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodManager : MonoBehaviour
{
    public static FoodManager instance { get; private set; }

    public int gameHp = 100;
    public int foodCount;
    public int currentFoodCount;
    public int maxFoodCount;
    private int starPoint = 0;

    Image mainPlateFood;

    [Space(10)]
    [SerializeField] private GameObject mainPlate;
    public Food currentFood;
    public Materials currentMaterial;

    [Space(5), Header("[ --- Food DataBase --- ]")]
    public Food[] foodOrder;

    [HideInInspector] public int foodOrderIndex = 0; // 현재 요리순서
    public int materialIndex = 0; // 현재 재료순서

    public bool isClear = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        FoodInit();
        MaterialInit();
    }

    private void Start()
    {
        mainPlateFood = mainPlate.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        starPoint = 0;
    }

    private void Update()
    {
        currentFoodCount = 0;
        for (int i = 0; i < currentFood.foodMaterial.Length; i++)
        {
            currentFoodCount += currentFood.foodMaterial[i].materialCount;
        }
        mainPlateFood.fillAmount = (maxFoodCount - (float)currentFoodCount) / maxFoodCount;
    }

    // 다음 요리 설정
    public void FoodInit()
    {
        starPoint += currentFood.foodCompleteness + (maxFoodCount - currentFoodCount);
        Debug.Log(starPoint);
        if (foodOrderIndex >= foodOrder.Length)
        {
            InGameUiManager.instance.Clear(starPoint);
            GameManager.instance.isGame = false;
            isClear = true;
        }

        if (isClear != true)
        {
            CurrentFoodInit(foodOrder[foodOrderIndex].foodName, foodOrder[foodOrderIndex].foodSprite, foodOrder[foodOrderIndex].foodCompleteness,
            foodOrder[foodOrderIndex].IsComplete, foodOrder[foodOrderIndex].foodMaterial, foodOrder[foodOrderIndex].waitingTime);

            // 메인 접시 음식 스프라이트설정
            Image mainPlateFoodAlpha = mainPlate.transform.GetChild(0).GetChild(1).GetComponent<Image>();
            mainPlateFoodAlpha.sprite = currentFood.foodSprite;

            Image mainPlateFood = mainPlate.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            mainPlateFood.sprite = currentFood.foodSprite;

            maxFoodCount = 0;
            for (int i = 0; i < currentFood.foodMaterial.Length; i++)
            {
                maxFoodCount += currentFood.foodMaterial[i].materialCount;
            }
            
            currentFoodCount = maxFoodCount;
        }
    }

    // 다음 재료 설정
    public void MaterialInit()
    {
        if (materialIndex >= currentFood.foodMaterial.Length) return;

        var currentMaterial = currentFood.foodMaterial[materialIndex];
        CurrentMaterialInit(currentMaterial.materialName, currentMaterial.materialSprite, currentMaterial.materialCount, currentMaterial.IsComplete);
        Recipe.instance.RecipeInit();
    }

    // 생성된 접시 초기화
    public void PlateInit(MaterialData materialData)
    {
        // 재료선택 : 현재 음식의 재료 길이만큼 
        // 스폰매니저에서 호출됨

        if(SpawnManager.instance.currentSpawnCount >= SpawnManager.instance.SpawnCount)
        {
            Materials material = currentFood.foodMaterial[materialIndex];
            materialData.MaterialInit(material.materialName, material.materialSprite);

            SpawnManager.instance.currentSpawnCount = 0;
            SpawnManager.instance.SpawnCount = Random.Range(2, 5);
            Debug.Log("음식 실행");
        }
        else
        {
            int _materialIndex = Random.Range(0, currentFood.foodMaterial.Length);
            Materials material = currentFood.foodMaterial[_materialIndex];

            materialData.MaterialInit(material.materialName, material.materialSprite);
        }
    }

    // 현재 요리 및 재료 설정
    public void CurrentFoodInit(string foodName, Sprite foodSprite, int foodCompleteness, bool isComplete, Materials[] materials, float waitingTime)
    {
        currentFood.foodName = foodName;
        currentFood.foodSprite = foodSprite;
        currentFood.foodCompleteness = foodCompleteness;
        currentFood.IsComplete = isComplete;

        currentFood.foodMaterial = new Materials[materials.Length];

        // 재료 배열 복사
        for (int i = 0; i < materials.Length; i++)
        {
            currentFood.foodMaterial[i] = new Materials
            {
                materialName = materials[i].materialName,
                materialSprite = materials[i].materialSprite,
                materialCount = materials[i].materialCount,
                IsComplete = materials[i].IsComplete
            };
        }

        currentFood.waitingTime = waitingTime;

    }

    public void CurrentMaterialInit(string materialName, Sprite materialSprite, int materialCount, bool isComplete)
    {
        currentMaterial.materialName = materialName;
        currentMaterial.materialSprite = materialSprite;
        currentMaterial.materialCount = materialCount;
        currentMaterial.IsComplete = isComplete;
    }
}
