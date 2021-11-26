using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoob : MonoBehaviour
{
    public static SkillBoob instance;
    [SerializeField] public Collider[] skill;

    [SerializeField] GameObject skillBoob;

    public bool skillboob = false;
    public float timeSkill = 10f;
    public float aDamageSkill = 100f;
    private void Awake() 
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginAttack()
    {
        foreach (var skill in skill)
        {
            skill.enabled = true;
        }
    }
    public void EndAttack()
    {
        foreach (var skill in skill)
        {
            skill.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MiniBlock")
        {
            if(skillboob=true)
            {
                Debug.Log("Power Boom!!");
                skillBoob.SetActive(true);
                BeginAttack();
                StartCoroutine(DelayHideSkills());
            }
        }
    }
    IEnumerator DelayHideSkills()
    {
        yield return new WaitForSeconds(timeSkill);
        EndAttack();
        skillBoob.SetActive(false);
        Debug.Log("Hide Skill");
        //Destroy(this.gameObject);
        //gameObject.GetComponent<SphereCollider>().enabled = true;
    }
}
