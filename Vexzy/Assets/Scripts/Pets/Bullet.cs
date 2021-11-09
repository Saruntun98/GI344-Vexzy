using UnityEngine;

public class Bullet : MonoBehaviour
{

	private Transform target;

	public float speed = 70f;
	public GameObject impactEffect;
	
    [SerializeField]
    public float aDamage = 30;
	
    public static Bullet instance;

	public void Seek(Transform _target)
	{
		target = _target;
	}

    void Awake()
    {
        instance = this;
        //maxStamina = stamina;
    }

	// Update is called once per frame
	void Update()
	{

		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);

	}

	void HitTarget()
	{
		GameObject effectIns =  (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 2f);
		
		Destroy(gameObject);
	}
}
