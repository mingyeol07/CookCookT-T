using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.ParticleSystem;

public enum PlateType { CurrentMaterial, Material }

public class Plate : MonoBehaviour
{
    public PlateType plateType;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject badEffect;
    [SerializeField] private GameObject goodEffect;

    [Header("# Plate Move Var")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 moveDir;

    private MaterialData materialData;
    private SpriteRenderer plateSprite;
    private SpriteRenderer sprite;

    private void Start()
    {
        plateSprite = GetComponentInChildren<SpriteRenderer>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        materialData = GetComponent<MaterialData>();
        PlateTypeInit();

        sprite.sprite = materialData.materialSprite;
    }

    private void Update()
    {
        if (materialData.materialName == FoodManager.instance.currentMaterial.materialName)
        {
            print("실행 : 0");
            sprite.color = new Color(255, 255, 255, 255);
        }

        else if (materialData.materialName != FoodManager.instance.currentMaterial.materialName)
        {
            print("실행 : 1");
            plateSprite.color = new Color32(63, 82, 154, 255);
            //sprite.color = new Color32(137, 90, 90, 255);
        }

        Move();
    }

    // 접시 충돌처리
    private void ProcessPlate()
    {
        FoodManager foodManager = FoodManager.instance;
        var foodMaterial = foodManager.currentFood.foodMaterial[foodManager.materialIndex];

        if (materialData.materialName == foodManager.currentMaterial.materialName)
        {
            Instantiate(goodEffect, new Vector3(0, -2, 0), Quaternion.identity);

            foodMaterial.materialCount--;

            // 만약 재료를 다 먹었다면 그 재료는 완료
            if (foodMaterial.materialCount <= 0)
            {
                foodMaterial.IsComplete = true;
                // 재료 레시피 설정
                bool allComplete = foodManager.currentFood.foodMaterial.All(material => material.IsComplete == true);
                if (allComplete == true) foodManager.currentFood.IsComplete = true;
            }
        }
        else
        {
            Instantiate(badEffect, new Vector3(0, -2, 0), Quaternion.identity);
            GameManager.instance.mooding(3);
            GameManager.instance.guest.transform.GetComponentInParent<Animator>().SetTrigger("Hand");
            CameraShake.ShakeCamera(0.1f, 0.1f);
            if (foodManager.currentFood.foodCompleteness > 0)
            {
                foodManager.currentFood.foodCompleteness--;
            }
        }
    }

    private void PlateTypeInit()
    {
        FoodManager foodManager = FoodManager.instance;

        // 현재 요리재료의 아이디와 같다면
        if (materialData.materialName == foodManager.currentMaterial.materialName) plateType = PlateType.CurrentMaterial;
        else plateType = PlateType.Material;
    }

    // ------ Move Method ------
    private void Move()
    {
        if (GameManager.instance.isGame == true)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
    }

    public void MoveDirection(Transform targetPos)
    {
        moveDir = (targetPos.position - transform.position).normalized;
    }
    // --------------------------

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.CompareTag("MainPlate"))
        {
            if (FoodManager.instance.currentFood.IsComplete == false)
            {
                ProcessPlate();
                Destroy(gameObject);
            }
            else Destroy(gameObject);
        }
    }

    public void InstantEffect()
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
    }
}

