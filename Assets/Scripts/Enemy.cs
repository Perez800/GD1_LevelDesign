using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public int MaxHP = 3;
    private int currentHP;
    public SplineContainer spline;
    float splineLenght;
    public float enemySpeed = 10f;
    float distancePercentage = 0f;
    public bool patrolMovement = true;
    public Collider patrolTrigger;
    public bool aggroed = false;
    public float loseAggro = 30f;
    public float inAttackRange = 2f;
    public float aggroedSpeed = 20f;

    void Start()
    {
        currentHP = MaxHP;
    }

    public void DecreaseHP(int dmg)
    {
        currentHP -= dmg;
        print(currentHP);
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
        if (aggroed == true)
        {
            AggroedMove();
        }
        if (aggroed == false)
        {
            if ((patrolTrigger == null) && (spline != null))
            {
                if (patrolMovement)
                {
                    PatrolMove();
                }
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
    private void AggroedMove()
    {
        transform.LookAt(player.transform.position);

        if (Vector3.Distance(transform.position, player.transform.position) >= inAttackRange)
        {

            transform.position += transform.forward * aggroedSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, player.transform.position) >= loseAggro)
            {
                aggroed = false;
            }

        }
    }
}
