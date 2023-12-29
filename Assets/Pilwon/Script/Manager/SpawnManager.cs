using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance { get; private set; }

    [Header("# GameObject Var")]
    [SerializeField] private GameObject platePrefab;
    [SerializeField] public GameObject cloneParent;

    [Space(10), Header("# Pos Var")]
    [SerializeField] private Transform targetPos;
    [SerializeField] private Transform leftSpawnPos;
    [SerializeField] private Transform rightSpawnPos;

    [Space(10), Header("# Time Var")]
    [SerializeField] private float spawnTime;

    [Space(10)]
    public int currentSpawnCount = 0;
    public int SpawnCount = 0;


    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(SpawnPlate());
    }

    private IEnumerator SpawnPlate()
    {
        while (true)
        {
            if (GameManager.instance.isGame == true)
            {
                int spawnPosIndex = Random.Range(0, 2);
                Transform spawnPos = spawnPosIndex == 0 ? leftSpawnPos : rightSpawnPos;

                GameObject clone = Instantiate(platePrefab, spawnPos.position, Quaternion.identity);
                clone.transform.SetParent(cloneParent.transform, false);

                currentSpawnCount++;

                // 날라가는 스크립트 추가
                if (spawnPos == leftSpawnPos) clone.AddComponent<LeftFood>();
                else if (spawnPos == rightSpawnPos) clone.AddComponent<RightFood>();

                Plate plate = clone.GetComponent<Plate>();
                MaterialData materialData = clone.GetComponent<MaterialData>();

                plate.MoveDirection(targetPos); // 이동방향 정해주기
                FoodManager.instance.PlateInit(materialData); // 접시의 재료데이터 설정
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
