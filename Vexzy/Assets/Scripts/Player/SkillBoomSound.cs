using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoomSound : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("ThornTrap");
            if (PlayerStatus.instance != null)
            {
                //PlayerStatus.instance.curHealth -= aDamageSkill * Time.deltaTime;
                //Debug.Log ("Skill: "+SkillBoob.instance.aDamageSkill);  
                //Debug.Log ("HP Enemy: "+PlayerStatus.instance.curHealth);       
                PlayerStatus.instance.TakeHit();     
            }
        }
    }
}
