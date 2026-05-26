using Unity.VisualScripting;
using UnityEngine;

public class LogicaPies : MonoBehaviour
{
    public LogicaKobu logicakobu;

    void OnTriggerStay(Collider other)
    {
        logicakobu.puedoSaltar = true;
    }

    void OnTriggerExit(Collider other)
    {
        logicakobu.puedoSaltar = false;
    }
}
