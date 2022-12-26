using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class AiSensor : MonoBehaviour
{
    public AiManager aiManager;
    public FirstPersonController firstPersonController;
    public GameObject player;

    public float hearRadius;
    public float visionRadius;
    [Range(0, 360)] public float visionAngle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool sawPlayer;
    public bool hearPlayer;

    // Start is called before the first frame update
    void Start()
    {
        aiManager = GetComponent<AiManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        firstPersonController = player.GetComponent<FirstPersonController>();

        StartCoroutine(VisionRoutine());
        StartCoroutine(HearRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator HearRoutine()
    {
        float delay = 0.05f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return true;
            HearCheck();
        }
    }

    IEnumerator VisionRoutine()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);

        while (true)
        {
            yield return true;
            VisionCheck();
        }
    }

    void VisionCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, visionRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < visionAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    //Debug.Log("I SAW YOU");
                    sawPlayer = true;
                    //firstPersonController.isDead = true;
                }
                else
                {
                    sawPlayer = false;
                }
            }
            else
            {
                sawPlayer = false;
            }
        }
        else if (sawPlayer)
        {
            sawPlayer = false;
        }
    }

    void HearCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, hearRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            float hearDistance = Vector3.Distance(transform.position, player.transform.position);

            if (!Physics.Raycast(transform.position, directionToTarget, hearDistance, obstructionMask))
            {
                if (firstPersonController.isWalking)
                {
                    hearPlayer = true;
                    FaceTarget(target.transform.position);

                }
                
                if (firstPersonController.isRunning)
                {
                    hearPlayer = true;
                    FaceTarget(target.transform.position);
                }
                
                if (firstPersonController.isCrouching)
                {
                    hearPlayer = false;
                }
                
                if (firstPersonController.isSneaking)
                {
                    hearPlayer = false;
                }
            }
            else
            {
                hearPlayer = false;
            }
        }
    }

    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.05f);
    }

   
}
