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
        float limitMidle = Mathf.Clamp(piernaLeft.transform.position.y + Time.deltaTime, 2, 4);
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
        float vehicleTiltAngleX = vehicle.initialRotations.x;
        float vehicleTiltAngleY = vehicle.initialRotations.y;
        float vehicleTiltAngleZ = vehicle.initialRotations.z;

        if (vehicleTiltAngleX <= -0.01)
        {
            // Inclinación hacia adelante (piernaMidle hacia abajo)           
            float limitMidle = Mathf.Clamp(piernaMidle.transform.position.y - Time.deltaTime, 2, 4);
            Vector3 tmp2 = new Vector3(piernaMidle.transform.position.x, limitMidle, piernaMidle.transform.position.z);
            piernaMidle.transform.position = tmp2;

            // Piernas laterales hacia arriba
            float limitUp = Mathf.Clamp(piernaRigth.transform.position.y + Time.deltaTime, 2, 4);
            float limitDown = Mathf.Clamp(piernaLeft.transform.position.y - Time.deltaTime, 2, 4);
            Vector3 tmp = new Vector3(piernaRigth.transform.position.x, limitUp, piernaRigth.transform.position.z);
            Vector3 tmp1 = new Vector3(piernaLeft.transform.position.x, limitDown, piernaLeft.transform.position.z);
            piernaRigth.transform.position = tmp;
            piernaLeft.transform.position = tmp1;
        }
        else if (vehicleTiltAngleX > 0.01)
        {
            // Inclinación hacia adelante (piernaMidle hacia abajo)           
            float limitMidle = Mathf.Clamp(piernaMidle.transform.position.y + Time.deltaTime, 2, 4);
            Vector3 tmp2 = new Vector3(piernaMidle.transform.position.x, limitMidle, piernaMidle.transform.position.z);
            piernaMidle.transform.position = tmp2;

            // Piernas laterales hacia abajo
            float limitUp = Mathf.Clamp(piernaLeft.transform.position.y + Time.deltaTime, 2, 4);
            float limitDown = Mathf.Clamp(piernaRigth.transform.position.y - Time.deltaTime, 2, 4);
            Vector3 tmp = new Vector3(piernaLeft.transform.position.x, limitUp, piernaLeft.transform.position.z);
            Vector3 tmp1 = new Vector3(piernaRigth.transform.position.x, limitDown, piernaRigth.transform.position.z);
            piernaLeft.transform.position = tmp;
            piernaRigth.transform.position = tmp1;
        }

        // Piernas en dirección Y (izquierda/derecha)
        if (vehicleTiltAngleZ <= -0.01)
        {

            float limitUp = Mathf.Clamp(piernaLeft.transform.position.y - Time.deltaTime, 2, 4);
            float limitDown = Mathf.Clamp(piernaRigth.transform.position.y + Time.deltaTime, 2, 4);
            Vector3 tmp = new Vector3(piernaLeft.transform.position.x, limitUp, piernaLeft.transform.position.z);
            Vector3 tmp1 = new Vector3(piernaRigth.transform.position.x, limitDown, piernaRigth.transform.position.z);
            piernaLeft.transform.position = tmp;
            piernaRigth.transform.position = tmp1;
        }
        else if (vehicleTiltAngleZ > 0.01)
        {

            float limitUp = Mathf.Clamp(piernaRigth.transform.position.y - Time.deltaTime, 2, 4);
            float limitDown = Mathf.Clamp(piernaLeft.transform.position.y + Time.deltaTime, 2, 4);
            Vector3 tmp = new Vector3(piernaRigth.transform.position.x, limitUp, piernaRigth.transform.position.z);
            Vector3 tmp1 = new Vector3(piernaLeft.transform.position.x, limitDown, piernaLeft.transform.position.z);
            piernaRigth.transform.position = tmp;
            piernaLeft.transform.position = tmp1;
        }
    }
}
