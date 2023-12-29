using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InGameUiManager : MonoBehaviour
{
    public static InGameUiManager instance { get; private set; }

    [Header("# In Game Ui")]
    public Image hpTime;
    public Image timerSprite;
    [SerializeField] Image clearPanel;
    [SerializeField] Image stopPanel;
    [SerializeField] Image[] clearStar;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void Clear(int addStar)
    {
        StartCoroutine(clear(addStar));
    }

    public IEnumerator clear(int addStar)
    {
        int a = 0;
        if (addStar >= 80) a = 3;
        else if (addStar >= 60) a = 2;
        else if (addStar >= 30) a = 1;
        else if (addStar < 30) a = 0;
        clearPanel.gameObject.SetActive(true);
        for (int i = 0; i < a; i++)
        {
            clearStar[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void Stop(bool active)
    {
        stopPanel.gameObject.SetActive(active);
        if (active) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
