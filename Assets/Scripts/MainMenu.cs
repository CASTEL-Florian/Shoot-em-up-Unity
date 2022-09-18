using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int numberOfLevels = 1;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float timeBeforePlayButtonActive = 1.1f;
    [SerializeField] private Background background;
    [SerializeField] private TextMeshProUGUI chaosText;
    [SerializeField] private FadeAudioSource audioFader;
    [SerializeField] private AudioSource clickAudio;
    private bool starting = false;
    private float time = 0;

    private int levelSelected = 0;
    private Fader fader;
    private bool chaosMode = false;
    private PlayerControls controls;
    private bool lastFrameDirection = false;
    
    private void Awake()
    {
        controls = new PlayerControls();
        controls.MainMenu.Start.performed += ctx => LoadScene();
        controls.MainMenu.ChaosMode.performed += ctx => ToggleChaosMode();
        controls.MainMenu.ChangeLevel.performed += ctx => ChangeLevelButton(ctx.ReadValue<float>());

    }

    private void OnEnable()
    {
        controls.MainMenu.Enable();
    }

    private void OnDisable()
    {
        controls.MainMenu.Disable();
    }
    private void Start()
    {
        Time.timeScale = 1;
        fader = FindObjectOfType<Fader>();
        StartCoroutine(fader.FadeIn(1));
        UpdateScore();
    }
    public void LoadScene()
    {
        if (starting || time < timeBeforePlayButtonActive)
            return;
        starting = true;
        StartCoroutine(fader.TransitionToScene(levelSelected + 1 + (chaosMode? numberOfLevels : 0)));
        StartCoroutine(audioFader.StartFade(1, 0));
    }
    private void Update()
    {
        time += Time.deltaTime;
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeLevelSelected(int change)
    {
        levelSelected += change;
        if (levelSelected == 0)
            leftArrow.SetActive(false);
        else
            leftArrow.SetActive(true);
        if (levelSelected == numberOfLevels - 1)
            rightArrow.SetActive(false);
        else
            rightArrow.SetActive(true);
        levelText.text = (chaosMode? "Chaos " : "Level ") + (levelSelected + 1).ToString();
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (PlayerPrefs.HasKey((levelSelected + 1 + (chaosMode ? numberOfLevels : 0)).ToString() + "score"))
        {
            scoreText.text = "Best score : " + PlayerPrefs.GetInt((levelSelected + 1 + (chaosMode ? numberOfLevels : 0)).ToString() + "score").ToString();
        }
        else
            scoreText.text = "Best score : 0";
    }

    private void PreviousLevel()
    {
        if (levelSelected == 0)
            return;
        ChangeLevelSelected(-1);
        clickAudio.Play();
    }
    private void NextLevel()
    {
        if (levelSelected == numberOfLevels - 1)
            return;
        ChangeLevelSelected(1);
        clickAudio.Play();
    }

    private void ChangeLevelButton(float value)
    {
        if (value > 0)
            NextLevel();
        if (value < 0)
            PreviousLevel();
    }
    public void ToggleChaosMode()
    {
        clickAudio.Play();
        chaosMode = !chaosMode;
        background.ToggleDoubleSpeed();
        levelText.text = (chaosMode ? "Chaos " : "Level ") + (levelSelected + 1).ToString();
        if (chaosMode)
            chaosText.text = "Chaos mode : ON";
        else
            chaosText.text = "Chaos mode : OFF";
        UpdateScore();
    }
}
