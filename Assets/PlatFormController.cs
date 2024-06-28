using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormController : MonoBehaviour
{
    [SerializeField] private VehicleController vehicle;

    [SerializeField] private GameObject piernaMidle;
    [SerializeField] private GameObject piernaRigth;
    [SerializeField] private GameObject piernaLeft;
    private Vector3 initialMidleLegPosition;
    private Vector3 initialRightLegPosition;
    private Vector3 initialLeftLegPosition;

    void Start()
    {
        // Calibrar la posición inicial de las piernas
        initialMidleLegPosition = piernaMidle.transform.position;
        initialRightLegPosition = piernaRigth.transform.position;
        initialLeftLegPosition = piernaLeft.transform.position;

        CalibrateLegsPosition();
    }

    private void Update()
    {
        //porMovimiento();
        testing();
        CalibrateLegsPosition();
    }

    private void CalibrateLegsPosition()
    {
       
    }
    void testing()
    {
        
    }


}
