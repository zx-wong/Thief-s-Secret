using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AiManager : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Animator aiAnimator;

    public FirstPersonController firstPersonController ;
    public AiSensor aiSensor;

    public GameObject player;

    AudioSource audio;
    public AudioClip taserS;

    float timer;
    int sec = 10;

    public bool doPatrol = false;
    public bool doLook = false;
    public bool startTimer = false;
    public bool isCaught = false;
    public bool reachPlayer = false;
    public bool shouldStop = false;
    public bool drawGun = false;
    public bool canShoot = false;

    public GameObject[] waypointSetA;
    public GameObject[] waypointSetB;
    public GameObject[] waypointSetC;

    [SerializeField] int targetIndex = 0;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        aiAnimator = GetComponent<Animator>();
        aiSensor = GetComponent<AiSensor>();

        player = GameObject.FindGameObjectWithTag("Player");
        firstPersonController = player.GetComponent<FirstPersonController>();

        waypointSetA = GameObject.FindGameObjectsWithTag("WaypointA");
        waypointSetB = GameObject.FindGameObjectsWithTag("WaypointB");
        waypointSetC = GameObject.FindGameObjectsWithTag("WaypointC");
        audio = GetComponent<AudioSource>();
        doPatrol = true;

        timer = Time.time;
        sec = 10;

        if (gameObject.name == "AiClone 1")
        {
            target = waypointSetA[targetIndex].transform;
            navMeshAgent.destination = waypointSetA[targetIndex].transform.position;
        }

        if (gameObject.name == "AiClone 2")
        {
            target = waypointSetB[targetIndex].transform;
            navMeshAgent.destination = waypointSetB[targetIndex].transform.position;
        }

        if (gameObject.name == "AiClone 3")
        {
            target = waypointSetC[targetIndex].transform;
            navMeshAgent.destination = waypointSetC[targetIndex].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(navMeshAgent.remainingDistance);
        //Debug.Log(targetIndex);

        if (waypointSetA.Length == 0)
        {
            Debug.Log("No Waypoint for A");
            waypointSetA = GameObject.FindGameObjectsWithTag("WaypointA");
        }

        if (waypointSetB.Length == 0)
        {
            Debug.Log("No Waypoint for B");
            waypointSetB = GameObject.FindGameObjectsWithTag("WaypointB");
        }

        if (waypointSetC.Length == 0)
        {
            Debug.Log("No Waypoint for C");
            waypointSetC = GameObject.FindGameObjectsWithTag("WaypointC");
        }

        if (drawGun)
        {
            Debug.Log("Drawing Gun");
            aiAnimator.SetBool("drawGun", true);

            if (this.aiAnimator.GetCurrentAnimatorStateInfo(0).IsName("drawGun"))
            {
                drawGun = false;
                canShoot = true;
            }
        }

        if (canShoot)
        {
            Debug.Log("Shoot Player");
            aiAnimator.SetTrigger("shoot");
            audio.clip=taserS;
            audio.Play();
            aiAnimator.SetBool("drawGun", false);



            if (this.aiAnimator.GetCurrentAnimatorStateInfo(0).IsName("shoot"))
            {
                drawGun = false;
                canShoot = false;
                firstPersonController.isDead = true;
                firstPersonController.isDead = true;
            }
        }

        //Debug.Log(doPatrol);
        if (!doPatrol)
        {
            Debug.Log("Go Chase");
            ChasePlayer();
        }
        else
        {
            if (!startTimer)
            {
                Patrol();
            }
        }

        if (startTimer)
        {
            Timer();
        }

        if (shouldStop)
        {
            Stop();
        }

        if (doLook)
        {
            Stop();
        }
        else
        {
            doPatrol = true;
        }
    }

    void Patrol()
    {
        Move();

        if (aiSensor.sawPlayer)
        {
            //Debug.Log("See You");
            doPatrol = false;
            //Debug.Log("Patrol: "+ doPatrol);
        }
        else
        {
            doPatrol = true;

            if (gameObject.name == "AiClone 1")
            {
                navMeshAgent.destination = waypointSetA[targetIndex].transform.position;
            }

            if (gameObject.name == "AiClone 2")
            {
                navMeshAgent.destination = waypointSetB[targetIndex].transform.position;
            }

            if (gameObject.name == "AiClone 3")
            {
                navMeshAgent.destination = waypointSetC[targetIndex].transform.position;
            }

            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                startTimer = true;
                Stop();
            }
        }
    }

    void NextPoint()
    {
        if (gameObject.name == "AiClone 1")
        {
            targetIndex = (targetIndex + 1) % waypointSetA.Length;
            navMeshAgent.destination = waypointSetA[targetIndex].transform.position;
        }

        if (gameObject.name == "AiClone 2")
        {
            targetIndex = (targetIndex + 1) % waypointSetB.Length;
            navMeshAgent.destination = waypointSetB[targetIndex].transform.position;
        }

        if (gameObject.name == "AiClone 3")
        {
            targetIndex = (targetIndex + 1) % waypointSetC.Length;
            navMeshAgent.destination = waypointSetC[targetIndex].transform.position;
        }
    }

    void ChasePlayer()
    {
        if (!reachPlayer)
        {
            Debug.Log("Chasing Player!");
            Run();
            navMeshAgent.destination = player.transform.position;

            if (!aiSensor.sawPlayer)
            {
                doPatrol = true;
            }
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (!aiSensor.sawPlayer)
            {
                Stop();
                doPatrol = true;
                Move();

                if (gameObject.name == "AiClone 1")
                {
                    navMeshAgent.destination = waypointSetA[targetIndex].transform.position;
                }

                if (gameObject.name == "AiClone 2")
                {
                    navMeshAgent.destination = waypointSetB[targetIndex].transform.position;
                }

                if (gameObject.name == "AiClone 3")
                {
                    navMeshAgent.destination = waypointSetC[targetIndex].transform.position;
                }
            }
        }
    }

    void Stop()
    {
        //Debug.Log("Ai Stopped!");

        aiAnimator.SetBool("isWalk", false);
        aiAnimator.SetBool("isRun", false);

        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0.0f;
    }

    void Move()
    {
        aiAnimator.SetBool("isWalk", true);
        aiAnimator.SetBool("isRun", false);

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = 1.0f;
    }

    void Run()
    {
        aiAnimator.SetBool("isWalk", true);
        aiAnimator.SetBool("isRun", true);

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = 3.0f;
    }

    void Timer()
    {
        Stop();

        if (Time.time > timer + 1)
        {
            timer = Time.time;
            sec--;

            if (sec < 0)
            {
                sec = 10;
                startTimer = false;
                Move();
                NextPoint();
            }
        }
    }
}
