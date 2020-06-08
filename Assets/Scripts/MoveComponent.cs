using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float objectDistance = -40f;
    [SerializeField] private float despawnDistance = -110f;
    private bool canSpawnGround = true;
    private HealthComponent health;
    private GameObject player;
    private EnemyController enemy;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (GetComponent<HealthComponent>() != null){
            health = GetComponent<HealthComponent>(); 
        }

        if (GetComponent<EnemyController>() != null)
        {
            enemy = GetComponent<EnemyController>();
        }
    }


    private void Update()
    {
        if(enemy != null && !enemy.targettingPlayer)
        {
            transform.position += -transform.forward * speed * Time.deltaTime;
        }
        else if(enemy != null && enemy.targettingPlayer)
        {
            transform.position += Vector3.zero;
        }
        else
        {
            transform.position += -transform.forward * speed * Time.deltaTime;
        }

        if(transform.position.z < player.transform.position.z -10f && enemy != null)
        {
            health.ResetHealth();
            gameObject.SetActive(false);
        }
        

        if (transform.position.z <= objectDistance && transform.tag == "Ground" && canSpawnGround)
        {
            ObjectSpawner.instance.SpawnGround();
            canSpawnGround = false;
        }
        if(transform.position.z <= despawnDistance)
        {
            canSpawnGround = true;
            gameObject.SetActive(false);
        }
    }
}
