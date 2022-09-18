using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private PlayerControls controls;
    [SerializeField] private CharacterMovement controller;
    [SerializeField] private List<Shooting> shootings;
    [SerializeField] private Avatar avatar;
    private int currentShooting = 0;
    private float speed;
    [SerializeField] private float maxDoubleClickTime = 0.2f;
    private float doubleClickTime = 0;
    [SerializeField] private float dodgeDuration = 0.3f;
    [SerializeField] private float dodgeSpeed = 30f;
    [SerializeField] private HitManager hitManager;
    [SerializeField] GameObject pauseMenu;

    private void Start()
    {
        speed = avatar.GetSpeed();
    }
    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.SwitchWeapon.performed += ctx => SwitchWeapon();
        controls.Player.Pause.performed += ctx => Pause();
        controls.Player.Dash.performed += ctx => Dash();

    }
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        shootings[currentShooting].SetActive(false);
        controls.Disable();
    }

    private void Update()
    {
        shootings[currentShooting].SetActive(controls.Player.Shoot.ReadValue<float>() == 1);
        if (doubleClickTime <= maxDoubleClickTime)
            doubleClickTime += Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if (controller.IsDashing())
            return;
        Vector2 move = controls.Player.Move.ReadValue<Vector2>();
        controller.Move(move * speed);
    }

    private void SwitchWeapon()
    {
        shootings[currentShooting].SetActive(false);
        currentShooting += 1;
        if (currentShooting >= shootings.Count)
            currentShooting = 0;
    }

    private void Dash()
    {
        if (Time.timeScale == 0)
            GameManager.Instance.ReturnToMainMenu();
        StartCoroutine(DodgeRoutine());

    }
    private IEnumerator DodgeRoutine()
    {
        StartCoroutine(hitManager.SetInvisible(dodgeDuration));
        yield return controller.Dodge(dodgeSpeed, dodgeDuration);
    }

    private void Pause()
    {
        Time.timeScale = 1 - Time.timeScale;
        if (Time.timeScale == 0)
            pauseMenu.SetActive(true);
        else
            pauseMenu.SetActive(false);
    }
}
