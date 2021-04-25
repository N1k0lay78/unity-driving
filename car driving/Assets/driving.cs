using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driving : MonoBehaviour
{
    // �������� ������
    [SerializeField] float engineForce;
    // ������������ ���� ��������
    [SerializeField] float maxSteerAngle;
    // ���� ���������� �������� ����
    [SerializeField] float forwardBreakForce;
    // ���� ���������� ������ ����
    [SerializeField] float rearBreakForce;

    // ����� ������ (����� ������� ������)
    // �������� ������
    [SerializeField] bool forwardDrive;
    // ������ ������
    [SerializeField] bool rearDrive;

    // ��������� ����
    // �������� �����
    [SerializeField] WheelCollider wheelTL;
    [SerializeField] WheelCollider wheelTR;
    // ������ �����
    [SerializeField] WheelCollider wheelRL;
    [SerializeField] WheelCollider wheelRR;

    // ��� ����
    // �������� �����
    [SerializeField] Transform transformTL;
    [SerializeField] Transform transformTR;
    // ������ �����
    [SerializeField] Transform transformRL;
    [SerializeField] Transform transformRR;

    void Update()
    {
        // ����� ������ ��������� ���������
        float power = Input.GetAxis("Vertical") * engineForce;
        // ����� ������ ������� ����
        float steer = Input.GetAxis("Horizontal") * maxSteerAngle;
        Debug.Log("power " + power);
        Debug.Log("steer " + steer);
        // ������������ �����
        transformTL.Rotate(wheelTL.rpm / 60 * 360 * Time.deltaTime, 0.0f, 0.0f);
        transformTR.Rotate(wheelTR.rpm / 60 * 360 * Time.deltaTime, 0.0f, 0.0f);
        transformRL.Rotate(wheelRL.rpm / 60 * 360 * Time.deltaTime, 0.0f, 0.0f);
        transformRR.Rotate(wheelRR.rpm / 60 * 360 * Time.deltaTime, 0.0f, 0.0f);

        if (forwardDrive) {
            // ���� �������� ������ �� ����� ��������� �� �������� �����
            wheelTL.motorTorque = power;
            wheelTR.motorTorque = power;
        }
        if (rearDrive) {
            // ���� ������ ������ �� ����� ��������� �� ������� �����
            wheelRL.motorTorque = power;
            wheelRR.motorTorque = power;
        }

        // ������������ �����
        wheelTL.steerAngle = steer;
        wheelTR.steerAngle = steer;

        if (Input.GetKeyDown(KeyCode.Space)) {
            // ���� ������ ������ �� ��������
            // �������� ��������� �������
            wheelTL.brakeTorque = forwardBreakForce;
            wheelTR.brakeTorque = forwardBreakForce;
            //�������� ������� �������
            wheelTL.brakeTorque = rearBreakForce;
            wheelTR.brakeTorque = rearBreakForce;
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            // ��������� ������
            wheelTL.brakeTorque = 0;
            wheelTR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }

        Vector3 rotation;
        // ��������� �������� ��������� ������ ������
        rotation = transformTL.localEulerAngles;
        // ������������ ��� ��� � ��������
        rotation.y = wheelTL.steerAngle;
        // ��������� ��������
        transformTL.transform.localEulerAngles = rotation;

        // ��������� �������� ��������� ������� ������
        rotation = transformTR.localEulerAngles;
        // ������������ �������� ������ ������
        rotation.y = wheelTR.steerAngle;
        // ��������� ��������
        transformTR.transform.localEulerAngles = rotation;
    }
}
