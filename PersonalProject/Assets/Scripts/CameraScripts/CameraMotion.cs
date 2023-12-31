﻿using UnityEngine;


public class CameraMotion : MonoBehaviour
{
	[SerializeField] private float _normalSpeed;
	[SerializeField] private float _shiftSpeed;
	private float _currentSpeed;
	[SerializeField] private float _smoothing = 1f;
	[SerializeField] private Vector2 _range = new(100, 100);

	public GameObject player;

	[HideInInspector] public bool isCameraLockedToPlayer = false;
	[HideInInspector] public bool isCameraLockedToTarget = false;


	private Vector3 _targetPosition;
	private Vector3 _input;

	private float defaultPosY;

	private void Awake()
	{
		_targetPosition = transform.position;
		defaultPosY = transform.position.y;
		_currentSpeed = _normalSpeed;
	}

	private void HandleInput()
	{
		if ((Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d")) && isCameraLockedToPlayer)
		{
			//Target position will be last camera position.
			isCameraLockedToPlayer = false;
			_targetPosition = transform.position;
			_targetPosition.y = defaultPosY;
		}

		float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");

		Vector3 right = transform.right * x;
		Vector3 forward = transform.forward * z;

		_input = (forward + right).normalized;

	}

	private void Move()
	{
		if (!isCameraLockedToPlayer)
		{
			if (Input.GetKey("left shift"))
			{
				_currentSpeed = _shiftSpeed;
			}
			else
			{
				_currentSpeed = _normalSpeed;
			}

			Vector3 nextTargetPosition = _targetPosition + (_input * _currentSpeed * Time.unscaledDeltaTime);

			if (IsInBounds(nextTargetPosition)) _targetPosition = nextTargetPosition;

			_targetPosition.y = defaultPosY;
			transform.position = Vector3.Lerp(transform.position, _targetPosition, _smoothing * Time.unscaledDeltaTime);
		}

	}

	private bool IsInBounds(Vector3 position)
	{
		return position.x > -_range.x &&
			   position.x < _range.x &&
			   position.z > -_range.y &&
			   position.z < _range.y;
	}

	//Lock camera to player
	private void LockedToPlayer()
	{
		if (isCameraLockedToPlayer)
		{
			Vector3 playerPos = new Vector3(player.transform.position.x, defaultPosY, player.transform.position.z);
			transform.position = Vector3.Lerp(transform.position, playerPos, Time.unscaledDeltaTime * 10f);
		}

	}

	private void Update()
	{
		//Camera lock
		if (Input.GetKeyDown("space"))
		{
			if (isCameraLockedToPlayer)
			{
				//Target position will be last camera position.
				isCameraLockedToPlayer = false;
				_targetPosition = transform.position;
				_targetPosition.y = defaultPosY;
			}
			else
			{
				isCameraLockedToPlayer = true;
			}
		}

		if (!isCameraLockedToTarget) HandleInput();
		Move();
		LockedToPlayer();
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 5f);
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(_range.x * 2f, 5f, _range.y * 2f));
	}

	//Move camera to target
	public void MoveToObject(GameObject _target)
    {
		//Setting input to zero for exact target pos
		_input = Vector3.zero;
		//if camera alreay locked to player set false
		isCameraLockedToPlayer = false;
		_targetPosition = _target.transform.position;
		transform.position = Vector3.Lerp(transform.position, _targetPosition, 0.1f * Time.unscaledDeltaTime);
	}
}

//namespace CameraControl {
	
//}