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
    private Vector2 lastDir;
    private bool lastNeutral = true;
    [SerializeField] private float dodgeDuration = 0.3f;
    [SerializeField] private float dodgeSpeed = 30f;
    [SerializeField] private HitManager hitManager;
    [SerializeField] private EnergyManager energyManager;
    private bool isDodging = false;
    [SerializeField] float dashEnergy = 0.3f;
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

    }
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
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
        if (isDodging)
            return;
        Vector2 move = controls.Player.Move.ReadValue<Vector2>();
        controller.Move(move * speed);
        if (move.magnitude == 1)
        {
            if (move == lastDir && doubleClickTime < maxDoubleClickTime && lastNeutral && !energyManager.isFullReloading())
            {
                energyManager.UseEnergy(dashEnergy);
                StartCoroutine(DodgeRoutine(move));
            }
            else
            {
                if (lastNeutral)
                    doubleClickTime = 0;
                lastDir = move;
            }
        }
        if (move.magnitude == 0)
            lastNeutral = true;
        else
            lastNeutral = false;
    }

    private void SwitchWeapon()
    {
        shootings[currentShooting].SetActive(false);
        currentShooting += 1;
        if (currentShooting >= shootings.Count)
            currentShooting = 0;
    }

  
    private IEnumerator DodgeRoutine(Vector2 move)
    {
        isDodging = true;
        StartCoroutine(hitManager.SetInvisible(dodgeDuration));
        yield return controller.Dodge(move, dodgeSpeed, dodgeDuration);
        isDodging = false;
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
