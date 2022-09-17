using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private Vector2 rectangleUpLeftCorner;
    [SerializeField] private Vector2 rectangleWidthHeight;

    [SerializeField] private List<Shooting> shootings;
    [SerializeField] private List<float> attackDurations;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private AnimationCurve moveCurve;
    [SerializeField] private float moveTime;
    private float time = 0;
    [SerializeField] private CharacterMovement controller;
    [SerializeField] private GameObject deathEffect;
    private Vector3 from;
    private Vector3 dest;
    private List<int> nextAttacks;
    private bool attacking = false;
    private void Start()
    {
        EnergyBar healthBar = GameObject.FindGameObjectWithTag("BossHealthBar").GetComponent<EnergyBar>();
        ObjectFade healthBarFader = healthBar.GetComponent<ObjectFade>();
        from = transform.position;
        nextAttacks = new List<int>();
        for (int i = 0; i < shootings.Count; i++)
        {
            nextAttacks.Add(i);
        }
        healthBar.UpdateBar(1, false);
        StartCoroutine(healthBarFader.FadeIn());
    }
    private void Update()
    {
        if (!attacking) {
            ShuffleAttacks();
            StartCoroutine(AttackRoutine());
        }
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > moveTime)
        {
            time = 0;
            from = dest;
            RandomizeDest();
        }
        controller.Lerp(from, dest, moveCurve.Evaluate(time / moveTime));
    }
    private void RandomizeDest()
    {
        float x = Random.Range(rectangleUpLeftCorner.x, rectangleUpLeftCorner.x + rectangleWidthHeight.x);
        float y = Random.Range(rectangleUpLeftCorner.y, rectangleUpLeftCorner.y + rectangleWidthHeight.y);
        dest = new Vector3(x, y, 0);
    }

    private void ShuffleAttacks()
    {
        nextAttacks.Sort((a, b) => 1 - 2 * Random.Range(0, 2));
    }

    private IEnumerator AttackRoutine()
    {
        attacking = true;
        for (int i = 0; i< nextAttacks.Count; i++)
        {
            yield return new WaitForSeconds(timeBetweenAttacks);
            shootings[nextAttacks[i]].SetActive(true);
            yield return new WaitForSeconds(attackDurations[nextAttacks[i]]);
            shootings[nextAttacks[i]].SetActive(false);
        }
        attacking = false;
    }
}
