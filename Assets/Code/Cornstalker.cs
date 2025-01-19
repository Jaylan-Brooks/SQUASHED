using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1
using UnityEngine.AI;

//2
[RequireComponent(typeof(NavMeshAgent))]
public class Cornstalker : MonoBehaviour
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

    public int health = 5;
	public GameObject[] telepoints;


	//a1
	/// <summary>
	/// All of the possible states the enemy can be in
	/// </summary>
	public enum EnemyStates { Chase, Attack, Teleport };
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
			if (value == EnemyStates.Chase)
				animator.SetInteger("State", 0);
            else if (value == EnemyStates.Attack)
			{
                animator.SetInteger("State", 1);
            }
            else if (value == EnemyStates.Teleport)
			{
                animator.SetInteger("State", 0);
            }

			// set the current state
			_currentState = value;
		}
	}

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
			case EnemyStates.Chase:
				Chase();
				break;
			case EnemyStates.Attack:
				Attack();
				break;
            case EnemyStates.Teleport:
				Teleport();
				break;
			default:
				Debug.Log("State not recognized");
				break;
		}
	}

	// State Machines

	//a3

	private void Chase()
	{
		//a14
        if (health <= 0){
            currentState = EnemyStates.Teleport;
			return;
        }
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
            currentState = EnemyStates.Teleport;
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
			Health.farmer.BigDamage();
			attackCooldownCounter = 0;
		}
	}

    private void Teleport()
	{
		AudioManager.main.Play("Scream");
        this.transform.position = telepoints[RandomPoint()].transform.position;
		health = 5;
		currentState = EnemyStates.Chase;
		return;
	}

	public void Damage(){
        health--;
    }

	public int RandomPoint(){
		int index = Mathf.FloorToInt(Random.value * 40f);
		if (index == 40){
			index = 39;
		}
		return index;
    }

	public void Die()
	{
		AudioManager.main.Play("Scream");
		Destroy(gameObject);
        return;
	}
}
