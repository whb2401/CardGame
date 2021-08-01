using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FLH.Battle.Player
{
    public class EmissionRayCrossCtrl : BaseMonoBehaviour<EmissionRayCtrl>
    {
        public LineRenderer LineRayPrefab;
        public Transform StartPos;
        public Transform EndPos;

        public float MaxRayWidth = .1f;
        public float WidthExtendSpeed = .6f;

        private LineRenderer LineRayInstance;
        private float RayCurrentWidth;
        private bool isBegin;

        private void Awake()
        {
            isBegin = false;
        }

        private void OnEnable()
        {
            LineRayInstance = ObjectPool.Instance.GetObj(LineRayPrefab.gameObject, transform).GetComponent<LineRenderer>();
            LineRayInstance.positionCount = 2;
            RayCurrentWidth = 0.01f;
        }

        private void FixedUpdate()
        {
            //if (isBegin)
            if (LineRayInstance.positionCount > 0)
            {
                LineRayInstance.SetPosition(0, StartPos.position);
                ExtendLineWidth0();
                LineRayInstance.SetPosition(1, EndPos.position);
            }
        }

        private void ExtendLineWidth0()
        {
            var dt = Time.fixedDeltaTime;
            // 按速度扩展宽度直到最大宽度
            if (RayCurrentWidth < MaxRayWidth)
            {
                RayCurrentWidth += dt * WidthExtendSpeed;
                LineRayInstance.startWidth = RayCurrentWidth;
                LineRayInstance.endWidth = RayCurrentWidth;
            }
            Debug.Log($"RayCurrentWidth:{RayCurrentWidth}");
        }

        //private void OnDisable()
        //{
        //    RayCurrentWidth = 0.01f;
        //    isBegin = false;
        //}

        public void Begin()
        {
            if (LineRayInstance != null)
            {
                LineRayInstance.positionCount = 2;
            }
            //LineRayInstance = ObjectPool.Instance.GetObj(LineRayPrefab.gameObject, transform).GetComponent<LineRenderer>();
            isBegin = true;
        }

        public void ShutDown()
        {
            isBegin = false;
            RayCurrentWidth = 0.01f;
            LineRayInstance.positionCount = 0;
            LineRayInstance.startWidth = RayCurrentWidth;
            LineRayInstance.endWidth = RayCurrentWidth;
            ObjectPool.Instance.RecycleObj(LineRayInstance.gameObject);
        }
    }
}
