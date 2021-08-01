using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

[Serializable]
public class GyroSetUp
{
    [Tooltip("敏感度")] public float sensitivity = 15f;

    [Tooltip("最大水平移动速度")] public float maxTurnSpeed = 35f;

    [Tooltip("最大垂直傾斜角移动速度")] public float maxTilt = 35f;

    [Tooltip("位移加成速率")] public float posRate = 1.5f;
}

public class UICameraGyro : MonoBehaviour
{
    public enum MotionAxial
    {
        All = 1,// 全部轴
        None,
        x,
        y,
        z
    }

    public enum MotionMode
    {
        Position = 1,// 只是位置变化
        Rotation,
        All// 全部变化
    }

    public MotionAxial motionAxial1 = MotionAxial.y;
    public MotionAxial motionAxial2 = MotionAxial.None;
    public MotionMode motionMode = MotionMode.Rotation;// 运动模式
    public GyroSetUp setUp;
    private Vector3 _mMobileOrientation;// 手机陀螺仪变化的值
    private Vector3 _mTargetTransform;
    private Vector3 _mTargetPos;
    [FormerlySerializedAs("ReversePosition")] public Vector3 reversePosition = Vector3.one;// 基于陀螺仪方向的取反


    public GameObject objTarget;
    public GameObject objTarget3;


    // 是否支持陀螺仪
    private bool _gyroSupported;

    // 陀螺仪
    private Gyroscope gyro;

    // X轴方向移动的速度参数
    private float xSpeed = 200;

    // 移动方向的三维向量
    private Vector3 directionVec;

    // 陀螺仪x轴的取值
    private float gyrosParameter;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        _gyroSupported = SystemInfo.supportsGyroscope;

        if (_gyroSupported)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            switch (Screen.orientation)
            {
                case ScreenOrientation.LandscapeLeft:
                    directionVec = new Vector3(1, 0, 0);
                    break;
                case ScreenOrientation.LandscapeRight:
                    directionVec = new Vector3(-1, 0, 0);
                    break;
            }
        }
    }

    private void LateUpdate()
    {
        // 位置随着陀螺仪重力感应的X轴变化而变化
        if (!_gyroSupported) return;

        gyrosParameter = gyro.gravity.x;
        if (objTarget3 != null)
        {
            objTarget3.transform.localPosition = gyrosParameter * xSpeed * directionVec;
        }

        TryIt();
    }

    private void TryIt()
    {
        _mMobileOrientation = Input.acceleration;
        
        switch (motionAxial1)
        {
            // 不操作任何轴
            case MotionAxial.None when motionAxial2 == MotionAxial.None:
                return;
            // X轴
            case MotionAxial.x when motionAxial2 == MotionAxial.None:
                _mTargetTransform.x = Mathf.Lerp(_mTargetTransform.x, _mMobileOrientation.y * setUp.maxTilt * reversePosition.x, 0.2f);
                break;
            // Y轴
            case MotionAxial.y when motionAxial2 == MotionAxial.None:
                _mTargetTransform.y = Mathf.Lerp(_mTargetTransform.y, -_mMobileOrientation.x * setUp.maxTurnSpeed * reversePosition.y, 0.2f);
                break;
            // Z轴
            case MotionAxial.z when motionAxial2 == MotionAxial.None:
                _mTargetTransform.z = Mathf.Lerp(_mTargetTransform.z, -_mMobileOrientation.z * setUp.maxTilt * reversePosition.z, 0.2f);
                break;
            // X和Y轴
            case MotionAxial.x when motionAxial2 == MotionAxial.y:
                _mTargetTransform.y = Mathf.Lerp(_mTargetTransform.y, -_mMobileOrientation.x * setUp.maxTurnSpeed * reversePosition.y, 0.2f);
                _mTargetTransform.x = Mathf.Lerp(_mTargetTransform.x, _mMobileOrientation.y * setUp.maxTilt * reversePosition.x, 0.2f);
                break;
            // Y和X轴
            case MotionAxial.y when motionAxial2 == MotionAxial.x:
                _mTargetTransform.y = Mathf.Lerp(_mTargetTransform.y, -_mMobileOrientation.x * setUp.maxTurnSpeed * reversePosition.y, 0.2f);
                _mTargetTransform.x = Mathf.Lerp(_mTargetTransform.x, _mMobileOrientation.y * setUp.maxTilt * reversePosition.x, 0.2f);
                break;
            // X和Z轴
            case MotionAxial.x when motionAxial2 == MotionAxial.z:
                _mTargetTransform.x = Mathf.Lerp(_mTargetTransform.x, _mMobileOrientation.y * setUp.maxTilt * reversePosition.x, 0.2f);
                _mTargetTransform.z = Mathf.Lerp(_mTargetTransform.z, -_mMobileOrientation.z * setUp.maxTilt * reversePosition.z, 0.2f);
                break;
            // Z和X轴
            case MotionAxial.z when motionAxial2 == MotionAxial.x:
                _mTargetTransform.x = Mathf.Lerp(_mTargetTransform.x, _mMobileOrientation.y * setUp.maxTilt * reversePosition.x, 0.2f);
                _mTargetTransform.z = Mathf.Lerp(_mTargetTransform.z, -_mMobileOrientation.z * setUp.maxTilt * reversePosition.z, 0.2f);
                break;
            // Y和Z轴
            case MotionAxial.y when motionAxial2 == MotionAxial.z:
                _mTargetTransform.y = Mathf.Lerp(_mTargetTransform.y, -_mMobileOrientation.x * setUp.maxTurnSpeed * reversePosition.y, 0.2f);
                _mTargetTransform.z = Mathf.Lerp(_mTargetTransform.z, -_mMobileOrientation.z * setUp.maxTilt * reversePosition.z, 0.2f);
                break;
            // Z和Y轴
            case MotionAxial.z when motionAxial2 == MotionAxial.y:
                _mTargetTransform.y = Mathf.Lerp(_mTargetTransform.y, -_mMobileOrientation.x * setUp.maxTurnSpeed * reversePosition.y, 0.2f);
                _mTargetTransform.z = Mathf.Lerp(_mTargetTransform.z, -_mMobileOrientation.z * setUp.maxTilt * reversePosition.z, 0.2f);
                break;
            // 所有轴向都运动
            case MotionAxial.All when motionAxial2 == MotionAxial.All:
                _mTargetTransform.y = Mathf.Lerp(_mTargetTransform.y, -_mMobileOrientation.x * setUp.maxTurnSpeed * reversePosition.y, 0.2f);
                _mTargetTransform.x = Mathf.Lerp(_mTargetTransform.x, _mMobileOrientation.y * setUp.maxTilt * reversePosition.x, 0.2f);
                _mTargetTransform.z = Mathf.Lerp(_mTargetTransform.z, _mMobileOrientation.z * setUp.maxTilt * reversePosition.z, 0.2f);
                break;
        }

        _mTargetPos.x = _mTargetTransform.y;
        _mTargetPos.y = -_mTargetTransform.x;
        _mTargetPos.z = _mTargetTransform.z;

        if (motionMode == MotionMode.Position)
        {
            objTarget.transform.localPosition = Vector3.Lerp(objTarget.transform.localPosition, _mTargetPos * setUp.posRate, Time.deltaTime * setUp.sensitivity);
        }
        else if (motionMode == MotionMode.Rotation)
        {
            objTarget.transform.localRotation = Quaternion.Lerp(objTarget.transform.localRotation, Quaternion.Euler(_mTargetTransform), Time.deltaTime * setUp.sensitivity);
        }
        else
        {
            objTarget.transform.localPosition = Vector3.Lerp(objTarget.transform.localPosition, _mTargetPos * setUp.posRate, Time.deltaTime * setUp.sensitivity);
            objTarget.transform.localRotation = Quaternion.Lerp(objTarget.transform.localRotation, Quaternion.Euler(_mTargetTransform), Time.deltaTime * setUp.sensitivity);
        }
    }
}