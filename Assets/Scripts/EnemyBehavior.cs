using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public float shootVelocity = 20f;
    public float shootBufferTime = 0.5f;

    public enum EnemyState { Follow, Shoot, FollowAndShoot, PlayerIsDead }
    private EnemyState _currentState;

    private GameObject _currentTarget;
    private NavMeshAgent _agent;
    private Rigidbody _rigidbody;

    private float _lastShootTime;
    
    private bool _isInit = false;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(GameObject target)
    {
        _currentTarget = target;
        _isInit = true;
    }

	void OnEnable ()
    {
        if (_isInit) SetState(EnemyState.FollowAndShoot);
    }

    void OnDisable()
    {
        if (_isInit) _agent.ResetPath();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void Update ()
    {
        if (_isInit)
        {
            //float dist = Vector3.Distance(transform.position, _currentTarget.transform.position);

            switch (_currentState)
            {
                case EnemyState.Follow:
                    _agent.SetDestination(_currentTarget.transform.position);
                    break;
                case EnemyState.Shoot:

                    break;
                case EnemyState.FollowAndShoot:
                    _agent.SetDestination(_currentTarget.transform.position);
                    ShootAttack();
                    break;
                case EnemyState.PlayerIsDead:

                    break;
            }
        }
        
	}

    public void SetState(EnemyState newState)
    {
        switch (_currentState)
        {
            case EnemyState.Follow:
                _agent.SetDestination(_currentTarget.transform.position);
                break;
            case EnemyState.Shoot:

                break;
            case EnemyState.FollowAndShoot:
                _agent.SetDestination(_currentTarget.transform.position);
                break;
            case EnemyState.PlayerIsDead:

                break;
        }

        _currentState = newState;
    }

    public void ShootAttack()
    {
        if ((Time.time - _lastShootTime) >= shootBufferTime)
        {
            BallBehavior bb = SpawnManager.Instance.GetPooledBall();
            bb.transform.position = Vector3.up + transform.position + (transform.forward * 1.5f);
            bb.transform.rotation = transform.rotation;
            bb.Init(shootVelocity, "EnemyBall");

            _lastShootTime = Time.time;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBall")
        {
            gameObject.SetActive(false);

            collision.gameObject.SetActive(false);
        }
    }
}
