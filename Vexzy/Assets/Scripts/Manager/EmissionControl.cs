using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionControl : MonoBehaviour
{
    public static EmissionControl instance;
    public Material materialPillerType1;
    public Material materialPillerType2;
    public Material materialPillerType3;

    void Awake()
    {
        instance = this;
        materialPillerType1.DisableKeyword ("_EMISSION");
        materialPillerType2.DisableKeyword ("_EMISSION");
        materialPillerType3.DisableKeyword ("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.H))
        {
            materialPillerType1.EnableKeyword ("_EMISSION");
            materialPillerType2.EnableKeyword ("_EMISSION");
            materialPillerType3.EnableKeyword ("_EMISSION");
        }
    }
}
