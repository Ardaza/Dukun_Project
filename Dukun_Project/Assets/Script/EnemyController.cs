﻿using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform[] waypoints;
    public float idleTime = 2f;
    public float walkSpeed = 2f; // Walking speed.
    public float chaseSpeed = 4f; // Chasing speed.
    public float sightDistance = 10f;
    public AudioClip idleSound;
    public AudioClip walkingSound;
    public AudioClip chasingSound;

    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private Animator animator;
    private float idleTimer = 0f;
    private Transform player;
    private AudioSource audioSource;

    private enum EnemyState { Idle, Walk, Chase }
    private EnemyState currentState = EnemyState.Idle;

    private bool isChasingAnimation = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        SetDestinationToWaypoint();
        LockYPosition(); // Lock Y position initially.
    }

    private void Update()
    {
        // Lock X rotation and Y position
        LockXRotationAndYPosition();

        switch (currentState)
        {
            case EnemyState.Idle:
                idleTimer += Time.deltaTime;
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsChasing", false); // Ensure IsChasing is set to false in the idle state.
                PlaySound(idleSound);

                if (idleTimer >= idleTime)
                {
                    NextWaypoint();
                }

                CheckForPlayerDetection();
                break;

            case EnemyState.Walk:
                idleTimer = 0f;
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsChasing", false); // Set IsChasing to false when walking.
                PlaySound(walkingSound);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    currentState = EnemyState.Idle;
                }

                CheckForPlayerDetection();
                break;

            case EnemyState.Chase:
                idleTimer = 0f;
                agent.speed = chaseSpeed; // Set the chase speed.
                agent.SetDestination(player.position);
                isChasingAnimation = true; // Set to true in chase state.
                animator.SetBool("IsChasing", true); // Set IsChasing to true in chase state.

                // Play chasing sound.
                PlaySound(chasingSound);

                // Check if the player is out of sight and go back to the walk state.
                if (Vector3.Distance(transform.position, player.position) > sightDistance)
                {
                    currentState = EnemyState.Walk;
                    agent.speed = walkSpeed; // Restore walking speed.
                }
                break;
        }

        // Lock Y position during update to ensure constant height
        LockYPosition();
    }

    private void LockXRotationAndYPosition()
    {
        // Lock the rotation on the X-axis and the position on the Y-axis
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void LockYPosition()
    {
        // Lock the position on the Y-axis
        transform.position = new Vector3(transform.position.x, 11f, transform.position.z);
    }

    private void CheckForPlayerDetection()
    {
        RaycastHit hit;
        Vector3 playerDirection = player.position - transform.position;

        if (Physics.Raycast(transform.position, playerDirection.normalized, out hit, sightDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                currentState = EnemyState.Chase;
            }
        }
    }

    private void PlaySound(AudioClip soundClip)
    {
        if (!audioSource.isPlaying || audioSource.clip != soundClip)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }
    }

    private void NextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        SetDestinationToWaypoint();
    }

    private void SetDestinationToWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        targetPosition.y = 11f; // Ensure the waypoint is at y = 11
        agent.SetDestination(targetPosition);
        currentState = EnemyState.Walk;
        agent.speed = walkSpeed; // Set the walking speed.
        animator.enabled = true;
    }

    // Draw a green raycast line at all times and switch to red when the player is detected.
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = currentState == EnemyState.Chase ? Color.red : Color.green;
    //    Gizmos.DrawLine(transform.position, player.position);
    //}
}
