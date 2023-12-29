using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LobbyUiManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [HideInInspector] public int stageLevel = 1;

    [Header("# Lobby Button Ui")]
    [SerializeField] private Button startBtn;
    [SerializeField] private Button optionBtn;
    [SerializeField] private Button exitBtn;

    [Space(10), Header("# Stage Panel Pos")]
    [SerializeField] private Button[] stageBtn;

    [Space(10), Header("# Lobby Cancel Button Ui")]
    [SerializeField] private Button optionCancelBtn;
    [SerializeField] private Button startCancelBtn;


    [Space(10), Header("# Lobby Panel")]
    [SerializeField] private RectTransform optionPanel;
    [SerializeField] private RectTransform startPanel;

    [Space(10), Header("# Lobby Panel Pos")]
    [SerializeField] private RectTransform optionPanelPos;
    [SerializeField] private RectTransform startPanelPos;

    private Animator panleAnimator;

    private void Start()
    {
        panleAnimator = panel.GetComponent<Animator>();

        for(int i = 1; i < stageBtn.Length + 1; i++)
        {
            if (i > stageLevel) stageBtn[i - 1].interactable = false;
        }

        // Lobby Button Init
        optionBtn.onClick.AddListener(() => StartCoroutine(OptionBtn()));
        optionCancelBtn.onClick.AddListener(() => StartCoroutine(OptionCancelBtn()));
        startBtn.onClick.AddListener(() => StartCoroutine(StartBtn()));
        startCancelBtn.onClick.AddListener(() => StartCoroutine(StartCancelBtn()));
    }

    private IEnumerator StartBtn()
    {
        panleAnimator.SetBool("isShow", false);
        yield return new WaitForSeconds(0.15f);
        startPanel.DOAnchorPos(startPanelPos.position, 0.5f);
    }

    private IEnumerator StartCancelBtn()
    {
        startPanel.DOAnchorPos(new Vector3(0, 2000, 0), 0.5f);
        yield return new WaitForSeconds(0.2f);
        panleAnimator.SetBool("isShow", true);
    }

    private IEnumerator OptionBtn()
    {
        panleAnimator.SetBool("isShow", false);
        yield return new WaitForSeconds(0.15f);
        optionPanel.DOAnchorPos(optionPanelPos.position, 0.5f);
    }

    private IEnumerator OptionCancelBtn()
    {
        optionPanel.DOAnchorPos(new Vector3(0, 1000, 0), 0.5f);
        yield return new WaitForSeconds(0.2f);
        panleAnimator.SetBool("isShow", true);
    }

    // -------- Stage Button --------
    public void StageBtn(int stageLevel)
    {
        SceneManager.LoadScene("Stage " + stageLevel);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
