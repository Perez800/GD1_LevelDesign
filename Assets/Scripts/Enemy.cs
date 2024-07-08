using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    public int MaxHP = 3;
    private int currentHP;
    public SplineContainer spline;
    float splineLenght;
    public float enemySpeed = 10f;
    float distancePercentage = 0f;
    public bool patrolMovement = true;
    public Collider patrolTrigger;

    void Start()
    {
        currentHP = MaxHP;
    }

    public void DecreaseHP(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void Awake()
    {
        if (spline != null)
        {
            splineLenght = spline.CalculateLength();
        }
    }
    private void Update()
    {
        if (patrolTrigger == null)
        {
            if (spline != null)
            {
                if (patrolMovement)
                    PatrolMove();
            }
        }
    }
    private void PatrolMove()
    {
        distancePercentage += enemySpeed * Time.deltaTime / splineLenght;
        Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
        transform.position = currentPosition;

        if (distancePercentage >= 1f)
        {
            distancePercentage = 0f;
            Destroy(this.gameObject);
        }

        Vector3 nextPosition = spline.EvaluatePosition(distancePercentage + 0.005f);

        Vector3 direction = nextPosition - currentPosition;

        transform.rotation = Quaternion.LookRotation(direction, transform.up);
    }
}
