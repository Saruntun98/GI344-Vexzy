using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoom : MonoBehaviour
{
    public int aDamageSkill = 1;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("ThornTrap");
            if (PlayerStatus.instance != null)
            {
                PlayerStatus.instance.curHealth -= aDamageSkill * Time.deltaTime;
                //Debug.Log ("Skill: "+SkillBoob.instance.aDamageSkill);  
                //Debug.Log ("HP Enemy: "+PlayerStatus.instance.curHealth);                
            }
        }
    }
}
