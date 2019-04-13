using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject healthBarPrefab = null;
    public float healthBarOffsetX = -2.5f;
    public float healthBarOffsetY = 1.0f;

    public GameObject rangedPainObject = null;
    public float rangedAttackSpeed = 0.6f;

    public float meleeAttackDamage = 0.0f;
    public float meleeAttackSpeed = 0.6f;

    public float hitPoints = 100.0f;
    public float attackDistance = 2.1f;
    public float speed;

    private float currentHitPoints;
    public float hideHealthBarTime = 2.0f;
    private float lastHitTakenTimer = 0.0f;

    private GameObject player1;
    private GameObject player2;
    private GameObject healthBar = null;

    private bool hasTarget = false;
    private Vector3 targetPosition = new Vector3();
    private float targetDistance = float.MaxValue;

    private float iWillHitObstacleSpeedFactor = 1.0f;
    private const float maxAffraidOfObstacleDistance = 10.0f;

    private float rangedAttackTimer = 0.0f;
    private float meleeAttackTimer = 0.6f;

    public void Hit(float damage)
    {
        currentHitPoints -= damage;
        lastHitTakenTimer = 0.0f;
    }

    void updateHealth()
    {
        lastHitTakenTimer += Time.deltaTime;

        if (currentHitPoints < 0)
        {
            Destroy(gameObject);
            Destroy(healthBar);
        }

        if (healthBar != null)
        {
            if (lastHitTakenTimer > hideHealthBarTime)
            {
                healthBar.SetActive(false);
            }
            else
            {
                healthBar.SetActive(true);
            }

            var newPosition = new Vector3(transform.position.x + healthBarOffsetX, transform.position.y + healthBarOffsetY, transform.position.z);
            healthBar.transform.position = newPosition;

            healthBar.GetComponent<HealthBarBehaviour>().SetTotal(hitPoints);
            healthBar.GetComponent<HealthBarBehaviour>().SetLeft(currentHitPoints);
        }
    }

    void updateMeleeAttack()
    {
        if (meleeAttackTimer > 0.0f)
        {
            meleeAttackTimer -= Time.deltaTime;
        }
    }
    void updateTarget()
    {
        Vector3 targetPosition1 = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 targetPosition2 = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

        if (player1 != null)
        {
            hasTarget = true;
            targetPosition1 = player1.transform.position;
        }

        if (player2 != null)
        {
            hasTarget = true;
            targetPosition2 = player2.transform.position;
        }

        if (hasTarget)
        {
            var player1Distance = Vector3.Distance(transform.position, targetPosition1);
            var player2Distance = Vector3.Distance(transform.position, targetPosition2);

            if (player2Distance < player1Distance)
            {
                targetPosition = targetPosition2;
                targetDistance = player2Distance;
            }
            else
            {
                targetPosition = targetPosition1;
                targetDistance = player1Distance;
            }
        }
    }

    void updateTransform()
    {
        if (hasTarget)
        {
            Vector3 directionVector = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);
            SetLook(targetPosition, directionVector);
            if (GetComponent<Rigidbody2D>().velocity.magnitude < 0.5)
                Move(directionVector);
        }
    }

    void updateRangedAttack()
    {
        if (hasTarget && rangedPainObject)
        {
            rangedAttackTimer += Time.deltaTime;
            if(rangedAttackTimer > rangedAttackSpeed && targetDistance < attackDistance)
            {
                Vector3 directionVector = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);

                rangedAttackTimer = 0.0f;

                GameObject gameObject = Instantiate(rangedPainObject);
                gameObject.transform.position = transform.position;
                gameObject.transform.rotation = transform.rotation;

                var beam = gameObject.GetComponent<RangedEnemyPainController>();
                beam.direction = directionVector;
                beam.SetSelf(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Player" && meleeAttackTimer <= 0.0f)
        {
            collision.gameObject.GetComponent<PlayerController>().Hit((int)meleeAttackDamage);
            meleeAttackTimer = meleeAttackSpeed;
        }
    }

    void SetLook(Vector3 targetPosition, Vector3 directionVector)
    {
        float angle = Vector2.Angle(Vector2.up, directionVector);
        transform.eulerAngles = new Vector3(0, 0, targetPosition.x > transform.position.x ? -angle + 90 : angle + 90);
    }

    void Move(Vector3 directionVector)
    {
        directionVector = directionVector.normalized * speed * Time.deltaTime * iWillHitObstacleSpeedFactor;
        transform.position = new Vector3(transform.position.x + directionVector.x, transform.position.y + directionVector.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");

        currentHitPoints = hitPoints;
        if (healthBar == null)
        {
            healthBar = Instantiate(healthBarPrefab);
            healthBar.transform.position = transform.position;

            healthBar.GetComponent<HealthBarBehaviour>().SetTotal(hitPoints);
            healthBar.GetComponent<HealthBarBehaviour>().SetLeft(currentHitPoints);

            healthBar.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        bool obstacleOnTheWay = false;
        Vector3 directionVector = new Vector3(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y, transform.position.z);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionVector);
        foreach(var hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject.tag == "Obstacle")
            {
                obstacleOnTheWay = true;
                var distance = Vector3.Distance(hit.collider.transform.position, transform.position);
                if (distance < maxAffraidOfObstacleDistance)
                {
                    iWillHitObstacleSpeedFactor = distance / maxAffraidOfObstacleDistance;
                }
            }
        }

        if(!obstacleOnTheWay)
        {
            iWillHitObstacleSpeedFactor = 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateHealth();
        updateTarget();
        updateTransform();
        updateRangedAttack();
        updateMeleeAttack();
    }
}
