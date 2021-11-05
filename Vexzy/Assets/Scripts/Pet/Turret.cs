using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Turret : MonoBehaviour
{

	[Header("Unity Setup Fields")]
	public string enemyTag = "Enemy";
	
	[Header("Pet Fields")]
	public float range = 15f;
	public float fireRate = 1f;
	private float fireCountdown = 0f;
	public Transform partToRotate;
	public float turnSpeed = 10f;

	public GameObject bulletPrefab;
	public Transform firePoint;

	[Header("Look At Objects")]
	[SerializeField] private Transform playerTransform;
	[SerializeField] private Transform turrentTransform;
	[SerializeField] private Transform nearestTarget;
	
	[Header("Target List")] 
	[SerializeField] private List<GameObject> targetList = new List<GameObject>();
	

	void Start () 
	{
		InvokeRepeating("FindTarget", 0f, 0.5f);
	}
	
	void FindTarget()
	{
		targetList = GameObject.FindGameObjectsWithTag(enemyTag).ToList();

		float shortestDistance = Mathf.Infinity;
		float distanceToEnemy = 0f;

		GameObject nearestEnemy = null;

		foreach (GameObject enemy in targetList)
		{
			distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance >= 0.00f && shortestDistance <= range)
		{
			nearestTarget = nearestEnemy.transform;
		}
		else
		{
			nearestTarget = playerTransform;
		}
	}

	void Update()
	{
		/*if (target == null)
			return;

		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);*/

		if (targetList.Count > 0)
		{
			FindNearestObjects();
			CheckObjectsInRange();
		}
		else
		{
			nearestTarget = playerTransform;
		}
		
		
		if (nearestTarget != null)
		{
			LookAtTarget();
		}
		
		if (fireCountdown <= 0f)
		{
			if (nearestTarget != null)
			{
				if (!nearestTarget.gameObject.name.Contains("Player"))
				{
					Shoot();
				}
				fireCountdown = 1f / fireRate;
			}
		}

		fireCountdown -= Time.deltaTime;
	}

	private void CheckObjectsInRange()
	{
		if (targetList.Count > 0)
		{
			for (int i = 0; i < targetList.Count; i++)
			{
				var dst = Vector3.Distance(turrentTransform.transform.position, targetList[i].transform.position);
				if (dst > range)
				{
					targetList.RemoveAt(i);
				}
			}
		}
		
	}
	
	void LookAtTarget()
	{
		if (nearestTarget == null)
		{
			return;
		}

		Vector3 dir = nearestTarget.transform.position - turrentTransform.transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	void FindNearestObjects()
	{
		float shortestDistance = Mathf.Infinity;
		GameObject nearestObject = null;

		foreach (GameObject obj in targetList)
		{
			float distanceToEnemy = Vector3.Distance(turrentTransform.transform.position, obj.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestObject = obj;
			}
		}

		if (nearestObject != null && shortestDistance <= range)
		{
			nearestTarget = nearestObject.transform;
		}
		else
		{
			nearestTarget = null;
		}
	}
	
	void Shoot()
	{
		GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGo.GetComponent<Bullet>();

		if (bullet != null)
		{
			bullet.Seek(nearestTarget);
		}
			
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			playerTransform = other.transform;
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
		
		//Gizmos.DrawLine(transform.position,new Vector3(0,0,4));
	}
	
}