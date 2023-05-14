using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    public Transform puntoA;
    public Transform puntoB;
    public GameObject Cop;
    public Transform copSpawn;
    public bool inSight;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (!inSight)
        {
            agent.SetDestination(puntoA.transform.position);
            if (transform.position == puntoA.transform.position)
            {
                agent.SetDestination(puntoB.transform.position);
            }
            else if (transform.position == puntoB.transform.position)
            {
                agent.SetDestination(puntoA.transform.position);
            }
        }
        
    }

    void Ruta()
    {
        agent.SetDestination(puntoA.transform.position);
        if (transform.position == puntoA.transform.position)
        {
            agent.SetDestination(puntoB.transform.position);
        }
        else if (transform.position == puntoB.transform.position)
        {
            agent.SetDestination(puntoA.transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            inSight = true;
            agent.SetDestination(player.gameObject.transform.position);

        }
    }
    IEnumerator SpawnCop()
    {
        yield return new WaitForSeconds(5);
        Instantiate(Cop, copSpawn.transform.position, copSpawn.transform.rotation);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            SpawnCop();
            Destroy(gameObject);
           
        }

        if (other.tag == "PuntoA")
        {
            agent.SetDestination(puntoB.transform.position);

        }

        if (other.tag == "PuntoB")
        {
            agent.SetDestination(puntoA.transform.position);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inSight = false;
            Ruta();
        }
    }
}
