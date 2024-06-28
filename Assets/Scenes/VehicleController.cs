using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public WheelCollider[] frontWheelColliders;   // Ruedas delanteras (con giro)
    public WheelCollider[] rearWheelColliders;    // Ruedas traseras (solo avance)
    public Transform[] wheelMeshes;         // Array que contiene los meshes de las ruedas visuales
    public float maxMotorTorque = 1000f;    // Torque máximo del motor
    public float maxSteeringAngle = 30f;    // Ángulo máximo de giro de las ruedas
    public float brakeTorque = 2000f;       // Torque de frenado
    public float brakeDelay = 0.2f;         // Retraso del freno en segundos

    private Rigidbody rb;
    private float currentBrakeDelay = 0f;
    private bool isBraking = false;


    [Header("Datas")]
    public float motor = 0;
    public float steering = 0;

    // Ángulo de inclinación general del vehículo
    private float vehicleTiltAngleX = 0f;
    private float vehicleTiltAngleY = 0f;
    private float vehicleTiltAngleZ = 0f;

    public float VehicleTiltAngleX => vehicleTiltAngleX;
    public float VehicleTiltAngleY => vehicleTiltAngleY;
    public float VehicleTiltAngleZ => vehicleTiltAngleZ;

    private Quaternion initialRotation;
    public Vector3 initialRotations;


    public float NormalizedRotationX
    {
        get
        {
            float minAngleX = -20f;  // Ángulo mínimo en X
            float maxAngleX = 20f;   // Ángulo máximo en X
            float currentAngleX = transform.rotation.eulerAngles.x;  // Ángulo actual en X

            // Ajustar el ángulo actual para que esté dentro del rango de -20 a 20 grados
            if (currentAngleX > 180f)
            {
                currentAngleX -= 360f;
            }

            currentAngleX = Mathf.Clamp(currentAngleX, minAngleX, maxAngleX);

            // Calcular el valor normalizado inverso dentro del rango de -20 a 20 grados
            float normalizedX = Mathf.InverseLerp(maxAngleX, minAngleX, currentAngleX);

            return normalizedX;
        }
    }

    public float NormalizedRotationY
    {
        get
        {
            float normalizedY = NormalizeAngle(transform.rotation.eulerAngles.y) / 360f;
            return Mathf.Clamp01(normalizedY);
        }
    }

    public float NormalizedRotationZ
    {
        get
        {
            float minAngleZ = -20f;  // Ángulo mínimo en Z
            float maxAngleZ = 20f;   // Ángulo máximo en Z
            float currentAngleZ = transform.rotation.eulerAngles.z;  // Ángulo actual en Z

            // Ajustar el ángulo actual para que esté dentro del rango de -20 a 20 grados
            if (currentAngleZ > 180f)
            {
                currentAngleZ -= 360f;
            }

            currentAngleZ = Mathf.Clamp(currentAngleZ, minAngleZ, maxAngleZ);

            // Calcular el valor normalizado inverso dentro del rango de -20 a 20 grados
            float normalizedZ = Mathf.InverseLerp(maxAngleZ, minAngleZ, currentAngleZ);

            return normalizedZ;
        }
    }

    private float NormalizeAngle(float angle)
    {
        angle = angle % 360; // Asegurar que el ángulo esté dentro de 0 a 359 grados
        if (angle < 0)
        {
            angle += 360; // Convertir ángulos negativos a positivos
        }
        return angle;
    }














    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialRotation = transform.rotation;
        
    }

    private void Update()
    {
        // Activar el freno cuando se presiona la tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            isBraking = true;
            currentBrakeDelay = 0f;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            isBraking = false;
            currentBrakeDelay = 0f;
        }
        initialRotations = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        float xRotationNormalized = GetComponent<VehicleController>().NormalizedRotationX;
        float yRotationNormalized = GetComponent<VehicleController>().NormalizedRotationY;
        float zRotationNormalized = GetComponent<VehicleController>().NormalizedRotationZ;

        //Debug.Log($"Normalized Rotation - X: {xRotationNormalized}, Z: {zRotationNormalized}");
    }

    private void FixedUpdate()
    {
         motor = maxMotorTorque * Input.GetAxis("Vertical");
         steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        
        // Aplicar el motor y el giro a las ruedas delanteras
        foreach (WheelCollider wheel in frontWheelColliders)
        {
            if (wheel.transform.localPosition.z > 0)  // Si la rueda es motriz
            {
                wheel.motorTorque = motor;
            }
            wheel.steerAngle = steering;
        }

        // Aplicar el motor a las ruedas traseras (sin giro)
        foreach (WheelCollider wheel in rearWheelColliders)
        {
            if (wheel.transform.localPosition.z > 0)  // Si la rueda es motriz
            {
                wheel.motorTorque = motor;
            }
        }

        // Aplicar freno con retardo
        if (isBraking)
        {
            currentBrakeDelay += Time.fixedDeltaTime;

            if (currentBrakeDelay >= brakeDelay)
            {
                foreach (WheelCollider wheel in frontWheelColliders)
                {
                    wheel.brakeTorque = brakeTorque;
                }
                foreach (WheelCollider wheel in rearWheelColliders)
                {
                    wheel.brakeTorque = brakeTorque;
                }
            }
        }
        else
        {
            // Liberar el freno
            foreach (WheelCollider wheel in frontWheelColliders)
            {
                wheel.brakeTorque = 0;
            }
            foreach (WheelCollider wheel in rearWheelColliders)
            {
                wheel.brakeTorque = 0;
            }
        }

        // Actualizar la posición y rotación de los meshes visuales de las ruedas
        for (int i = 0; i < wheelMeshes.Length; i++)
        {
            Vector3 pos;
            Quaternion rot;
            if (i < frontWheelColliders.Length)
            {
                frontWheelColliders[i].GetWorldPose(out pos, out rot);
            }
            else
            {
                rearWheelColliders[i - frontWheelColliders.Length].GetWorldPose(out pos, out rot);
            }
            wheelMeshes[i].position = pos;
            wheelMeshes[i].rotation = rot;
        }
    }

}
