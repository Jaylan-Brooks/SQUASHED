using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1
using UnityEngine.AI;

//2
[RequireComponent(typeof(NavMeshAgent))]
public class Pumpkinhead : MonoBehaviour
{
    public Animator animator;
	//3
	/// <summary>
	/// The transform this enemy will chase.
	/// </summary>
	public Transform target;
	//4
	/// <summary>
	/// The agent that controls this object's navigation
	/// </summary>
	public NavMeshAgent agent;

	//8
	// the distance at which the enemy will stop following
	public float minPlayerDistance;
	// the distance at which the enemy will continue following
	public float maxPlayerDistance;

	public GameObject deathPop;

	public int health = 3;



	//a1
	/// <summary>
	/// All of the possible states the enemy can be in
	/// </summary>
	public enum EnemyStates { Idle, Chase, Attack, Dead };
	private EnemyStates _currentState;
	//a2
	/// <summary>
	/// The current state of the enemy;
	/// </summary>
	public EnemyStates currentState
	//a7
	{
		get { return _currentState; }
		set
		{
			if (value == _currentState)
				return;
			if (value == EnemyStates.Idle)
				animator.SetInteger("State", 0);
            else if (value == EnemyStates.Chase)
			{
                animator.SetInteger("State", 1);
            }
			else if (value == EnemyStates.Attack)
			{
                animator.SetInteger("State", 2);
            }

			// set the current state
			_currentState = value;
		}
	}

	//a5
	[Header("Detection Settings")]
	/// <summary>
	/// layers that block the enemy's vision
	/// </summary>
	public LayerMask visionBlockers;
	/// <summary>
	/// a transform showing where the enemy's eyes are
	/// </summary>
	public Transform eyes;
	/// <summary>
	/// maximum distance where the enemy can see the player
	/// </summary>
	public float visionRange;
	public float fieldOfView;
	/// <summary>
	/// The tag objects have when they are a part of the player
	/// </summary>
	public string playerTag;

	private GameObject player;

	//a6
	[Header("Enemy Settings")]
	/// <summary>
	/// how often the enemy can attack
	/// </summary>
	public float attackCooldown;
	/// <summary>
	/// the counter for attackCooldown
	/// </summary>
	private float attackCooldownCounter = 0;

	// Start is called before the first frame update
	void Start()
	{
		//5
		// get the navmeshagent component on this object
		agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
		/* OLD, NON-STATE MACHINE CODE
		// we usually shouldn't put a lot of stuff in update, but we'll be putting it here

		//6
		// if there is a target
		if (target != null)
		{
			//9
			if (Vector3.Distance(target.position, this.transform.position) <= minPlayerDistance)
				agent.isStopped = true;
			else if (Vector3.Distance(target.position, this.transform.position) > maxPlayerDistance)
				agent.isStopped = false;

			//7
			// move towards the target
			agent.destination = target.position;
		}*/

		//a4
		// New State Machine code
		// Based on the current state, execute the proper code
		switch(currentState)
		{
			case EnemyStates.Idle:
				Idle();
				break;
			case EnemyStates.Chase:
				Chase();
				break;
			case EnemyStates.Attack:
				Attack();
				break;
			case EnemyStates.Dead:
				Dead();
				break;
			default:
				Debug.Log("State not recognized");
				break;
		}
	}

	// State Machines

	//a8
	/// <summary>
	/// Tells us whether the target object is visible to this enemy.
	/// </summary>
	/// <returns>True if a raycast casted from the eyes hits a collider with the tag "player".</returns>
	bool IsTargetVisible()
	{
		if (player != null){
			if(Vector3.Distance(transform.position, player.transform.position) <= visionRange){
				Vector3 targetDirection = player.transform.position - transform.position;
				float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
				if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView){
					Ray ray = new Ray(transform.position, targetDirection);
					Debug.DrawRay(ray.origin, ray.direction * visionRange);
					RaycastHit hitInfo = new RaycastHit();
					if(Physics.Raycast(ray,out hitInfo, visionRange)){
						if (hitInfo.transform.gameObject == player){
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	//a3
	private void Idle()
	{

		if (health < 3){
			AudioManager.main.Play("Chuckle");
            currentState = EnemyStates.Chase;
			health = 3;
			return;
        }
		//a9
		//Debug.Log(IsTargetVisible());

		//a10 ^ comment out the debug
		if (IsTargetVisible())
		{
			AudioManager.main.Play("Chuckle");
			currentState = EnemyStates.Chase;
			return;
		}

		//a12
	}

	private void Chase()
	{
		if (health <= 0){
            currentState = EnemyStates.Dead;
			return;
        }
		//a14
		if (Vector3.Distance(target.position, this.transform.position) <= minPlayerDistance)
		{
			agent.isStopped = true;
			currentState = EnemyStates.Attack;
			return;
		}
		// move towards the target
		agent.destination = target.position;
	}

	private void Attack() 
	{
		if (health <= 0){
            currentState = EnemyStates.Dead;
			return;
        }
		//a15
		if (Vector3.Distance(target.position, this.transform.position) > maxPlayerDistance)
		{
			agent.isStopped = false;
			attackCooldownCounter = 0;
			currentState = EnemyStates.Chase;
			return;
		}
		
		//a16
		attackCooldownCounter += Time.deltaTime;

		//a17
		if(attackCooldownCounter >= attackCooldown)
		{
			Health.farmer.Damage();
			attackCooldownCounter = 0;
		}
	}

	private void Dead()
	{
		GameObject burst = Instantiate(deathPop, GetComponent<Transform>().position, Quaternion.identity);
		AudioManager.main.Play("Pumpkin Guts");
		Destroy(gameObject);
        return;
	}

	public void Damage(){
        health--;
    }

	public void Headshot(){
        health = 0;
    }
}
