using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("# Player Info")]
    [SerializeField] private int playerMood = 100;
    [Space(5),SerializeField] public SpriteRenderer guest;
    [SerializeField] private Sprite[] mood;
    // (idle, happy, disgusting, sad, yummy)

    [Space(10)]
    public float currentRestTime = 0;
    public float restTime = 0;

    private float gameTime = 0;

    [Space(10), Header("# Bool Var")]
    public float probability = 30f;
    public bool isGame = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        InvokeRepeating("Idle", 1, 3);
        gameObject.GetComponent<AudioSource>().Play();
        restTime = FoodManager.instance.currentFood.waitingTime;
    }

    private void Update()
    {
        if (isGame == false)
        {
            RestTime();
        }
        else
        {            
            // 시간 스프라이트 색상 변경 
            Image timerSprite = InGameUiManager.instance.timerSprite.GetComponent<Image>();
            timerSprite.color = Color.blue;
            // -----------------------
            currentRestTime -= Time.deltaTime;
            InGameUiManager.instance.hpTime.fillAmount = currentRestTime / gameTime;
            if (currentRestTime <= 0)
            {
                isGame = false;
                mooding(3);
                CameraShake.ShakeCamera(0.2f, 0.4f);
                FoodManager.instance.foodOrderIndex++;
            }
        }
    }

    // --------- Player --------------
    public void moodChange()
    {
        var currentFood = FoodManager.instance.currentFood;
        guest.enabled = false;
        guest.enabled = true;
        if (currentFood.foodCompleteness >= 8) guest.sprite = mood[4]; // 8 ~ 10
        else if (currentFood.foodCompleteness >= 5) guest.sprite = mood[1]; // 5 ~ 7
        else if (currentFood.foodCompleteness >= 2) guest.sprite = mood[3]; // 2 ~ 4
        else guest.sprite = mood[2];

        StartCoroutine(moodReturn());
    }

    public void mooding(int index)
    {
        if (guest.enabled == false)
        {
            guest.enabled = true;
            guest.sprite = mood[index];
            StartCoroutine(moodReturn());
        }  
    }

    private IEnumerator moodReturn()
    {
        yield return new WaitForSeconds(2f);
        guest.enabled = false;
        guest.sprite = mood[0];
    }

    private void Idle()
    {
        guest.gameObject.GetComponentInParent<Animator>().SetTrigger("Blink");
    }
    // -------------------------------

    public void RestTime()
    {
        foreach (Transform childObj in SpawnManager.instance.cloneParent.transform)
        {
            Destroy(childObj.gameObject);
        }

        if (currentRestTime >= restTime && FoodManager.instance.isClear != true)
        {
            // 대기시간을 다 기다렸을 떄 게임 실행
            isGame = true;
            int a = FoodManager.instance.foodOrderIndex;
            if(a == 0 || a == 4)
            { 
                currentRestTime = 30;
            }
            else if (a == 1 || a == 2)
            {
                currentRestTime = 45;
            }
            else if (a == 3)
            {
                currentRestTime = 55;
            }
            gameTime = currentRestTime;
           
            if (FoodManager.instance.foodOrderIndex == 0) return;
            FoodManager.instance.FoodInit();

            FoodManager.instance.materialIndex = 0;
            FoodManager.instance.MaterialInit();
        }
        else
        {
            // 시간 스프라이트 색상 변경 
            Image timerSprite = InGameUiManager.instance.timerSprite.GetComponent<Image>();
            timerSprite.color = Color.red;
            // -----------------------

            currentRestTime += Time.deltaTime;
            InGameUiManager.instance.hpTime.fillAmount = currentRestTime / restTime;
        }
    }
}

