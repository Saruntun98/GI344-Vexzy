using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCam : MonoBehaviour
{
    public GameObject myPrefabCam;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(myPrefabCam, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(GameManager.instance.isPlayerExist)
        {
            Instantiate(myPrefabCam, new Vector3(0, 0, 0), Quaternion.identity);
        }*/
    }
}
