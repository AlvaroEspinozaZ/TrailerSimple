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
    }

    private void CalibrateLegsPosition()
    {
        float limitUp = Mathf.Clamp(piernaRigth.transform.position.y - Time.deltaTime, 2, 4);
        float limitDown = Mathf.Clamp(piernaLeft.transform.position.y + Time.deltaTime, 2, 4);
        float limitMidle = Mathf.Clamp(piernaMidle.transform.position.y + Time.deltaTime, 2, 4);
        piernaMidle.transform.position = initialMidleLegPosition;
        piernaRigth.transform.position = initialRightLegPosition;
        piernaLeft.transform.position = initialLeftLegPosition;
    }


    void porMovimiento()
    {
        if (vehicle.motor != 0)
        {
            if (vehicle.steering <= 0)
            {
                // Ajustar la posición de las piernas en función del ángulo de inclinación
                float limitUp = Mathf.Clamp(piernaRigth.transform.position.y + Time.deltaTime, 2, 4);
                float limitDown = Mathf.Clamp(piernaLeft.transform.position.y - Time.deltaTime, 2, 4);
                Vector3 tmp = new Vector3(piernaRigth.transform.position.x, limitUp, piernaRigth.transform.position.z);
                Vector3 tmp1 = new Vector3(piernaLeft.transform.position.x, limitDown, piernaLeft.transform.position.z);
                piernaRigth.transform.position = tmp;
                piernaLeft.transform.position = tmp1;
            }
            else if (vehicle.steering >= 0)
            {
                // Ajustar la posición de las piernas en función del ángulo de inclinación
                float limitUp = Mathf.Clamp(piernaLeft.transform.position.y + Time.deltaTime, 2, 4);
                float limitDown = Mathf.Clamp(piernaRigth.transform.position.y - Time.deltaTime, 2, 4);
                Vector3 tmp = new Vector3(piernaLeft.transform.position.x, limitUp, piernaLeft.transform.position.z);
                Vector3 tmp1 = new Vector3(piernaRigth.transform.position.x, limitDown, piernaRigth.transform.position.z);
                piernaLeft.transform.position = tmp;
                piernaRigth.transform.position = tmp1;
            }
        }
    }

    void testing()
    {
        //float vehicleTiltAngleX = vehicle.NormalizedRotationX;
        ////Debug.Log(vehicle.NormalizedRotationX);

        //float limitMidle = Mathf.Clamp(vehicle.NormalizedRotationX * 4 + Time.deltaTime, 2f, 4f);
        //Vector3 tmp = new Vector3(piernaMidle.transform.localPosition.x, limitMidle, piernaMidle.transform.localPosition.z);
        //piernaMidle.transform.localPosition = tmp;


        float vehicleTiltAngleZ = vehicle.NormalizedRotationZ;
        Debug.Log(vehicle.NormalizedRotationZ);


        float limiteLeft = Mathf.Clamp(vehicle.NormalizedRotationZ * 4 + Time.deltaTime, 2f, 4f);
        Vector3 tmp1 = new Vector3(piernaLeft.transform.localPosition.x, limiteLeft, piernaLeft.transform.localPosition.z);
        piernaLeft.transform.localPosition = tmp1;

        float limiteRight = Mathf.Clamp(vehicle.NormalizedRotationZ * 4 - Time.deltaTime, 2f, 4f);
        Vector3 tmp2 = new Vector3(piernaRigth.transform.localPosition.x, limiteRight, piernaRigth.transform.localPosition.z);
        piernaRigth.transform.localPosition = tmp2;
        //if (vehicleTiltAngleZ > 0.5)
        //{
        //    float limiteLeft = Mathf.Clamp(vehicle.NormalizedRotationZ * 4 + Time.deltaTime, 2f, 4f);
        //    Vector3 tmp1 = new Vector3(piernaLeft.transform.localPosition.x, limiteLeft, piernaLeft.transform.localPosition.z);
        //    piernaLeft.transform.localPosition = tmp1;

        //    float limiteRight = Mathf.Clamp(vehicle.NormalizedRotationZ * 4 - Time.deltaTime, 2f, 4f);
        //    Vector3 tmp2 = new Vector3(piernaRigth.transform.localPosition.x, limiteRight, piernaRigth.transform.localPosition.z);
        //    piernaRigth.transform.localPosition = tmp2;
        //}

        //else if (vehicleTiltAngleZ < 0.5)
        //{
        //    float limiteLeft = Mathf.Clamp(vehicle.NormalizedRotationZ * 4 - Time.deltaTime, 2f, 4f);
        //    Vector3 tmp3 = new Vector3(piernaLeft.transform.localPosition.x, limiteLeft, piernaLeft.transform.localPosition.z);
        //    piernaLeft.transform.localPosition = tmp3;

        //    float limiteRight = Mathf.Clamp(vehicle.NormalizedRotationZ * 4 + Time.deltaTime, 2f, 4f);
        //    Vector3 tmp4 = new Vector3(piernaRigth.transform.localPosition.x, limiteRight, piernaRigth.transform.localPosition.z);
        //    piernaRigth.transform.localPosition = tmp4;
        //}

    }
}
