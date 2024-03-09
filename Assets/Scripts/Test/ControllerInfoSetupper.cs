using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInfoSetupper : MonoBehaviour
{

    [SerializeField] private string InfoName = "";

    [SerializeField] private string InfoData = "";

    [SerializeField] private Sprite? InfoIcon;

    [SerializeField] private ControllerInfo Controller;
    // Start is called before the first frame update
    void Start()
    {
        Controller.InfoName = InfoName;
        Controller.InfoData = InfoData;
        Controller.InfoIcon = InfoIcon;
    }
}
