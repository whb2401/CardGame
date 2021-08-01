using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FLH.Battle.Player
{
    public enum EmissionLifeSate
    {
        None,
        // 创建阶段
        Creat,
        // 生命周期阶段
        Keep,
        // 衰减阶段
        Attenuate
    }

    public enum FireState
    {
        Off,
        On
    }

    public class EmissionRayCtrl : BaseMonoBehaviour<EmissionRayCtrl>
    {
        public LineRenderer LineRayPrefab;
        public LineRenderer EleLightningPerfab;

        private LineRenderer LineRayInstance;
        private LineRenderer EleLightningInstance;

        public GameObject FirePrefab;
        public GameObject HitPrefab;

        private GameObject FireInstance;
        private GameObject HitInstance;

        // 发射位置
        public Transform FirePos;
        // 激光颜色
        public Color EmissionColor = Color.blue;
        // 电光颜色
        public Color EleLightColor = Color.blue;
        // 发射速度
        public float FireSpeed = 80f;
        // 生命周期
        public float LifeTime = .3f;
        // 最大到达宽度
        public float MaxRayWidth = .1f;
        // 宽度扩展速度
        public float WidthExtendSpeed = .6f;
        // 渐隐速度
        public float FadeOutSpeed = 1f;
        // 单位电光的距离
        public float EachEleLightDistance = 2f;
        // 电光左右偏移值
        public float EleLightOffse = .5f;
        // 击中伤害
        public int Damage = 121;
        // 伤害结算间隔
        public float DamageCD = .1f;
        // 冷却时间
        public float CD = 0f;

        public bool bHaveEleLight = false;

        private FireState State;
        private EmissionLifeSate LifeSate;

        private Vector3 RayCurrentPos;
        private float RayOriginWidth;
        private float RayCurrentWidth;
        private float LifeTimer;
        private float CDTimer;
        private float DamageCDTimer;
        private float RayLength;

        void Start()
        {
            State = FireState.Off;
            LifeSate = EmissionLifeSate.None;
            CDTimer = 0f;
            DamageCDTimer = 0f;
        }

        public void FireBegin()
        {
            switch (State)
            {
                // 只有在状态关闭时才可以开启激光
                case FireState.Off:
                    if (CDTimer <= 0)
                    {
                        // 实例化激光组件
                        LineRayInstance = ObjectPool.Instance.GetObj(LineRayPrefab.gameObject, FirePos).GetComponent<LineRenderer>();
                        EleLightningInstance = ObjectPool.Instance.GetObj(EleLightningPerfab.gameObject, FirePos).GetComponent<LineRenderer>();
                        //FireInstance = ObjectPool.Instance.GetObj(FirePrefab, FirePos);
                        //HitInstance = ObjectPool.Instance.GetObj(HitPrefab, FirePos);
                        // 设置状态
                        State = FireState.On;
                        LifeSate = EmissionLifeSate.Creat;
                        //HitInstance.SetActive(false);
                        // 初始化属性
                        RayCurrentPos = FirePos.position;
                        LineRayInstance.positionCount = 2;
                        RayOriginWidth = 0.01f;//LineRayInstance.startWidth;
                        //LineRayInstance.material.SetColor("_Color", EmissionColor);
                        //EleLightningInstance.material.SetColor("_Color", EleLightColor);
                        CDTimer = CD;
                    }
                    break;
            }
        }

        void FixedUpdate()
        {
            switch (State)
            {
                case FireState.On:
                    switch (LifeSate)
                    {
                        case EmissionLifeSate.Creat:
                            //ShootLine();
                            ShootLine0();
                            break;
                        case EmissionLifeSate.Keep:
                            ExtendLineWidth();
                            break;
                        case EmissionLifeSate.Attenuate:
                            CutDownRayLine();
                            break;
                    }
                    break;
                case FireState.Off:
                    CDTimer -= Time.fixedDeltaTime;

                    // no anim
                    RayCurrentWidth = RayOriginWidth;
                    break;
            }
        }

        // 生成射线
        private void ShootLine()
        {
            // 设置激光起点
            LineRayInstance.SetPosition(0, FirePos.position);
            var dt = Time.fixedDeltaTime;

            // 激光的终点按发射速度进行延伸
            RayCurrentPos += FirePos.forward * FireSpeed * dt;

            // 在激光运动过程中创建短射线用来检测碰撞
            Ray ray = new Ray(RayCurrentPos, FirePos.forward);
            RaycastHit hit;
            // 射线长度稍大于一帧的运动距离，保证不会因为运动过快而丢失
            var rayResult = Physics.Raycast(ray, out hit, 1.2f * dt * FireSpeed, 1 << LayerMask.NameToLayer(GameSetting.LAYERBOUNDARY));
            if (rayResult)
            {
                RayCurrentPos = hit.point;
                Debug.Log("RayCurrentPos:" + RayCurrentPos);
                // 向命中物体发送被击信号，被击方向为激光发射方向
                SendActorHit(hit.transform.gameObject, FirePos.forward.GetVector3XZ().normalized);

                // 激光接触到目标后自动切换至下一生命周期状态
                LifeSate = EmissionLifeSate.Keep;
                // 保存当前激光的长度
                RayLength = (RayCurrentPos - FirePos.position).magnitude;

                RayCurrentWidth = RayOriginWidth;
                //HitInstance.SetActive(true);
                // 开始计算生命周期
                LifeTimer = 0f;
            }
            // 设置当前帧终点位置
            LineRayInstance.SetPosition(1, RayCurrentPos);
        }

        private void ShootLine0()
        {
            // 设置激光起点
            LineRayInstance.SetPosition(0, FirePos.position);
            var dt = Time.fixedDeltaTime;

            // 激光的终点按发射速度进行延伸
            RayCurrentPos = FirePos.forward * 20;// 定长
            ExtendLineWidth0();

            // 在激光运动过程中创建短射线用来检测碰撞
            Ray ray = new Ray(FirePos.position, FirePos.forward);
            RaycastHit hit;
            // 射线长度稍大于一帧的运动距离，保证不会因为运动过快而丢失
            var rayResult = Physics.Raycast(ray, out hit, 100.0f, 1 << LayerMask.NameToLayer(GameSetting.LAYERBOUNDARY));
            if (rayResult)
            {
                RayCurrentPos = hit.point;
                Debug.Log("RayCurrentPos:" + RayCurrentPos);
                // 向命中物体发送被击信号，被击方向为激光发射方向
                SendActorHit(hit.transform.gameObject, FirePos.forward.GetVector3XZ().normalized);

                // 激光接触到目标后自动切换至下一生命周期状态
                // 保存当前激光的长度
                RayLength = (RayCurrentPos - FirePos.position).magnitude;

                // 开始计算生命周期
                LifeTimer = 0f;
            }
            // 设置当前帧终点位置
            LineRayInstance.SetPosition(1, RayCurrentPos);
        }

        // 发送受击信号
        private void SendActorHit(GameObject HitObject, Vector2 dir)
        {
            // 判断激光击中目标是否是指定的目标类型
        }

        private void CheckRayHit()
        {
            var offse = (RayCurrentWidth + EleLightOffse) * .5f;
            // 向量运算出左右的起始位置
            var startL = FirePos.position - FirePos.right * offse;
            var startR = FirePos.position + FirePos.right * offse;
            // 创建基于当前激光宽度的左右两条检测射线
            Ray rayL = new Ray(startL, FirePos.forward);
            Ray rayR = new Ray(startR, FirePos.forward);
            RaycastHit hitL;
            RaycastHit hitR;

            // 按当前激光长度检测,若没有碰到任何物体，则延长激光
            if (Physics.Raycast(rayL, out hitL, RayLength))
            {
                // 左右击中目标是击中方向为该角色运动前向的反方向
                var hitDir = (-hitL.transform.forward).GetVector3XZ().normalized;
                SendActorHit(hitL.transform.gameObject, hitDir);
            }

            if (Physics.Raycast(rayR, out hitR, RayLength))
            {
                var hitDir = (-hitR.transform.forward).GetVector3XZ().normalized;
                SendActorHit(hitR.transform.gameObject, hitDir);
            }

            ChangeLine();
        }

        private void ChangeLine()
        {
            RaycastHit info;
            if (Physics.Raycast(new Ray(FirePos.position, FirePos.forward), out info))
            {
                RayCurrentPos = info.point;
                SendActorHit(info.transform.gameObject, FirePos.forward.GetVector3XZ().normalized);
                RayLength = (RayCurrentPos - FirePos.position).magnitude;
                LineRayInstance.SetPosition(1, RayCurrentPos);
                CreatKeepEleLightning();
            }
        }

        // 延长激光
        private void ExtendLine()
        {
            var dt = Time.fixedDeltaTime;
            RayCurrentPos += FirePos.forward * FireSpeed * dt;

            Ray ray = new Ray(RayCurrentPos, FirePos.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1.2f * dt * FireSpeed))
            {
                RayCurrentPos = hit.point;
                SendActorHit(hit.transform.gameObject, FirePos.forward.GetVector3XZ().normalized);
                RayLength = (RayCurrentPos - FirePos.position).magnitude;
                CreatKeepEleLightning();
            }
            // 更新当前帧终点位置,延长不用再设置起点位置
            LineRayInstance.SetPosition(1, RayCurrentPos);
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
        }

        private void ExtendLineWidth()
        {
            var dt = Time.fixedDeltaTime;
            // 按速度扩展宽度直到最大宽度
            if (RayCurrentWidth < MaxRayWidth)
            {
                RayCurrentWidth += dt * WidthExtendSpeed;
                LineRayInstance.startWidth = RayCurrentWidth;
                LineRayInstance.endWidth = RayCurrentWidth;
            }
            // 每帧检测射线碰撞
            CheckRayHit();
            // 生命周期结束后切换为衰减状态
            LifeTimer += dt;
            if (LifeTimer > LifeTime)
            {
                LifeSate = EmissionLifeSate.Attenuate;
            }
            ReBuildLine();
        }

        // 刷新激光位置，用于动态旋转的发射源
        private void ReBuildLine()
        {
            LineRayInstance.SetPosition(0, FirePos.position);
            LineRayInstance.SetPosition(1, FirePos.position + FirePos.forward * RayLength);
            //HitInstance.transform.position = FirePos.position + FirePos.forward * RayLength;
            CreatKeepEleLightning();
        }

        // 生成电光
        private void CreatKeepEleLightning()
        {
            if (bHaveEleLight)
            {
                var EleLightCount = (int)(RayLength / EachEleLightDistance);
                EleLightningInstance.positionCount = EleLightCount;
                for (int i = 0; i < EleLightCount; i++)
                {
                    // 计算偏移值
                    var offse = RayCurrentWidth * .5f + EleLightOffse;
                    // 计算未偏移时每个电光的线段中轴位置
                    var eleo = FirePos.position + (RayCurrentPos - FirePos.position) * (i + 1) / EleLightCount;
                    // 在射线的左右间隔分布，按向量运算进行偏移
                    var pos = i % 2 == 0 ? eleo - offse * FirePos.right : eleo + offse * FirePos.right;
                    EleLightningInstance.SetPosition(i, pos);
                }
            }
        }

        private void CutDownRayLine()
        {
            ReBuildLine();
            var dt = Time.fixedDeltaTime;
            // 宽度衰减为零后意味着整个激光关闭完成
            if (RayCurrentWidth > 0.5)
            {
                RayCurrentWidth -= dt * FadeOutSpeed;
                LineRayInstance.startWidth = RayCurrentWidth;
                LineRayInstance.endWidth = RayCurrentWidth;
            }
            else
            {
                //FireShut();
            }
        }

        public void FireShut()
        {
            switch (State)
            {
                case FireState.On:
                    EleLightningInstance.positionCount = 0;
                    LineRayInstance.positionCount = 0;
                    LineRayInstance.startWidth = RayOriginWidth;
                    LineRayInstance.endWidth = RayOriginWidth;
                    // 回收实例化个体
                    ObjectPool.Instance.RecycleObj(LineRayInstance.gameObject);
                    ObjectPool.Instance.RecycleObj(EleLightningInstance.gameObject);
                    //ObjectPool.Instance.RecycleObj(FireInstance);
                    //ObjectPool.Instance.RecycleObj(HitInstance);
                    State = FireState.Off;
                    break;
            }
        }
    }
}
