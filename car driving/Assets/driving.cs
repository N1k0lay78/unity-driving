using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driving : MonoBehaviour
{
    // мощность мотора
    [SerializeField] float engineForce;
    // максимальный угол поворота
    [SerializeField] float maxSteerAngle;
    // сила торможения передних колёс
    [SerializeField] float forwardBreakForce;
    // сила торможения задних колёс
    [SerializeField] float rearBreakForce;

    // Какой привод (можно сделать полный)
    // передний привод
    [SerializeField] bool forwardDrive;
    // задний привод
    [SerializeField] bool rearDrive;

    // коллайдер колёс
    // передние колёса
    [SerializeField] WheelCollider wheelTL;
    [SerializeField] WheelCollider wheelTR;
    // зажние колёса
    [SerializeField] WheelCollider wheelRL;
    [SerializeField] WheelCollider wheelRR;

    // меш колёс
    // передние колёса
    [SerializeField] Transform transformTL;
    [SerializeField] Transform transformTR;
    // задние колёса
    [SerializeField] Transform transformRL;
    [SerializeField] Transform transformRR;

    void Update()
    {
        // какая сейчас мощьность двигателя
        float power = Input.GetAxis("Vertical") * engineForce;
        // какой сейчас поворот колёс
        float steer = Input.GetAxis("Horizontal") * maxSteerAngle;
        Debug.Log("power " + power);
        Debug.Log("steer " + steer);
        // поворачиваем колёса
        transformTL.Rotate(wheelTL.rpm / 60 * 360 * Time.deltaTime, 0.0f, 0.0f);
        transformTR.Rotate(wheelTR.rpm / 60 * 360 * Time.deltaTime, 0.0f, 0.0f);
        transformRL.Rotate(wheelRL.rpm / 60 * 360 * Time.deltaTime, 0.0f, 0.0f);
        transformRR.Rotate(wheelRR.rpm / 60 * 360 * Time.deltaTime, 0.0f, 0.0f);

        if (forwardDrive) {
            // если передний привод то подаём мощьность на передние колёса
            wheelTL.motorTorque = power;
            wheelTR.motorTorque = power;
        }
        if (rearDrive) {
            // если задний привод то подаём мощьность на задение колёса
            wheelRL.motorTorque = power;
            wheelRR.motorTorque = power;
        }

        // поворачиваем колёса
        wheelTL.steerAngle = steer;
        wheelTR.steerAngle = steer;

        if (Input.GetKeyDown(KeyCode.Space)) {
            // елси нажали кнопку то тормозим
            // тормозим передними колёсами
            wheelTL.brakeTorque = forwardBreakForce;
            wheelTR.brakeTorque = forwardBreakForce;
            //тормозим задними колёсами
            wheelTL.brakeTorque = rearBreakForce;
            wheelTR.brakeTorque = rearBreakForce;
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            // отпустили тормоз
            wheelTL.brakeTorque = 0;
            wheelTR.brakeTorque = 0;
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }

        Vector3 rotation;
        // считываем вращение переднего левого колеса
        rotation = transformTL.localEulerAngles;
        // поворачиваем его как и колайдер
        rotation.y = wheelTL.steerAngle;
        // применяем вращение
        transformTL.transform.localEulerAngles = rotation;

        // считываем повророт переднего правого колеса
        rotation = transformTR.localEulerAngles;
        // поворачиваем переднее правое колесо
        rotation.y = wheelTR.steerAngle;
        // применяем вращение
        transformTR.transform.localEulerAngles = rotation;
    }
}
