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

    public float limitMidle;
    public float limitTraseraIzquierda;
    public float limitTraseraDerecha;


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
    }

    private void CalibrateLegsPosition()
    {
        piernaMidle.transform.localPosition = new Vector3(piernaMidle.transform.localPosition.x, 3f, piernaMidle.transform.localPosition.z);
        piernaRigth.transform.localPosition = new Vector3(piernaRigth.transform.localPosition.x, 3f, piernaRigth.transform.localPosition.z);
        piernaLeft.transform.localPosition = new Vector3(piernaLeft.transform.localPosition.x, 3f, piernaLeft.transform.localPosition.z);
    }



    void testing()
    {
        float vehicleTiltAngleX = vehicle.NormalizedRotationX;
        float vehicleTiltAngleZ = vehicle.NormalizedRotationZ;


        limitMidle = Mathf.Clamp(3f + (vehicleTiltAngleX - 0.5f) * 4f, 2f, 4f);
        Vector3 tmpMidle = new Vector3(piernaMidle.transform.localPosition.x, limitMidle, piernaMidle.transform.localPosition.z);
        piernaMidle.transform.localPosition = tmpMidle;


       

        if (vehicleTiltAngleZ > 0.5f)
        {
            limitTraseraIzquierda = Mathf.Clamp((vehicleTiltAngleZ - 0.5f) * 4 + 3f, 2f, 4f);
            limitTraseraDerecha = Mathf.Clamp((0.5f - vehicleTiltAngleZ) * 4 + 3f, 2f, 4f);
        }
        else
        {
            limitTraseraIzquierda = Mathf.Clamp((vehicleTiltAngleZ - 0.5f) * 4 + 3f, 2f, 4f);
            limitTraseraDerecha = Mathf.Clamp((0.5f - vehicleTiltAngleZ) * 4 + 3f, 2f, 4f);
        }

        Vector3 tmpIzquierda = new Vector3(piernaLeft.transform.localPosition.x, limitTraseraIzquierda, piernaLeft.transform.localPosition.z);
        piernaLeft.transform.localPosition = tmpIzquierda;

        Vector3 tmpDerecha = new Vector3(piernaRigth.transform.localPosition.x, limitTraseraDerecha, piernaRigth.transform.localPosition.z);
        piernaRigth.transform.localPosition = tmpDerecha;



    }
}
