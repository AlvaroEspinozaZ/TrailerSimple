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
        Debug.Log(initialRotations);
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
