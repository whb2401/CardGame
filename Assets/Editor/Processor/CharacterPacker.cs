using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class CharacterPacker
{
    public const string ADDRESSABLE_ASSET_SETTINGS_PATH = "Assets/AddressableAssetsData/AddressableAssetSettings.asset";
    public const string ADDRESSABLE_CHARACTER_GROUP_NAME = "Character Assets";

    private static bool isSelfPrefab;

    public enum AnimationType
    {
        Idle01,
        Show,
        Show_Idle,
        Win,
        Fail,
        Death,
        Attack,
        Attack_Idle,
        Run,
        Stun,
        Born,
        Hurt,
        Skill
    }

    [MenuItem("Assets/Create Character Prefab(Monster)")]
    public static void CreateCharacterPrefabWithComponent()
    {
        isSelfPrefab = false;
        CreateCharacterPrefab();
    }

    [MenuItem("Assets/Create Character Prefab")]
    public static void CreateCharacterPrefabSelf()
    {
        isSelfPrefab = true;
        CreateCharacterPrefab();
    }

    public static void CreateCharacterPrefab()
    {
        var modelPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (!IsActorModel(modelPath))
        {
            Debug.LogError("Active object is not character model.");
            return;
        }

        var fi = new FileInfo(modelPath);
        if (!fi.Exists)
        {
            Debug.LogError("Something error.");
            return;
        }

        // 模型名称
        var modelName = modelPath.Substring(modelPath.LastIndexOf("/") + 1);

        // 模型保存路径
        var modelRoot = modelPath.Replace("/" + modelName, "");
        modelRoot = modelRoot.Substring(0, modelRoot.LastIndexOf("/"));

        modelName = modelName.Substring(0, modelName.LastIndexOf('.'));
        var modelNumber = "01";

        Debug.Log(modelPath);
        Debug.Log(modelName);
        Debug.Log(modelRoot);

        AnimatorController controller;
        // 创建Animator
        {
            var animator = CreateWithType(typeof(HeroBasicAnimator), modelRoot, $"{modelName}_{modelNumber}_Controller") as HeroBasicAnimator;
            var animModelName = modelName.Replace("10", "").Replace("11", "");
            TryGetAnimationClip(modelRoot, animModelName, AnimationType.Idle01, ref animator.idleMotion);
            TryGetAnimationClip(modelRoot, animModelName, AnimationType.Death, ref animator.deathMotion);
            TryGetAnimationClip(modelRoot, animModelName, AnimationType.Fail, ref animator.failMotion);
            TryGetAnimationClip(modelRoot, animModelName, AnimationType.Win, ref animator.winMotion);
            TryGetAnimationClip(modelRoot, animModelName, AnimationType.Attack, ref animator.attackMotion);
            TryGetAnimationClip(modelRoot, animModelName, AnimationType.Skill, ref animator.skillMotion);
            TryGetAnimationClip(modelRoot, animModelName, AnimationType.Run, ref animator.runMotion);
            TryGetAnimationClip(modelRoot, animModelName, AnimationType.Show, ref animator.showMotion);
            TryGetAnimationClip(modelRoot, animModelName, AnimationType.Show_Idle, ref animator.showIdleMotion);

            EditorUtility.SetDirty(animator);

            controller = AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(animator), typeof(AnimatorController)) as AnimatorController;
            animator.GenerateInto(controller);
        }

        var avatar = AssetDatabase.LoadAssetAtPath<GameObject>(modelPath);
        if (avatar == null)
        {
            return;
        }

        avatar = GameObject.Instantiate(avatar);

        // 指定Animator Controller
        if (controller != null)
        {
            avatar.GetComponent<Animator>().runtimeAnimatorController = controller;
        }

        // 去掉Lighting
        SkinnedMeshRenderer[] skins = avatar.GetComponentsInChildren<SkinnedMeshRenderer>();
        if (skins.Length > 0)
        {
            foreach (var render in skins)
            {
                render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                render.receiveShadows = false;
            }
        }

        if (!isSelfPrefab)
        {
            var collider = avatar.AddComponent<CapsuleCollider>();
            collider.center = new Vector3(0, 1, 0);
            collider.radius = 1;
            collider.height = 2.33f;
            var rigidBody = avatar.AddComponent<Rigidbody>();
            rigidBody.isKinematic = true;
            rigidBody.interpolation = RigidbodyInterpolation.None;
            rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rigidBody.constraints = RigidbodyConstraints.FreezeRotationX |
                RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ |
                RigidbodyConstraints.FreezePositionX |
                RigidbodyConstraints.FreezePositionY |
                RigidbodyConstraints.FreezePositionZ;

            var healthManager = avatar.AddComponent<BrickHealthManager>();
            var movementCtrl = avatar.AddComponent<BrickMovementController>();
            var ctrl = avatar.AddComponent<EnemyController>();
            healthManager.ctrl = ctrl;
            ctrl.moveCtrl = movementCtrl;
            ctrl.animatorEnemy = avatar.GetComponent<Animator>();
        }

        PrefabUtility.SaveAsPrefabAsset(avatar, $"{modelRoot}/{modelName}_{modelNumber}_p.prefab", out bool success);
        if (success)
        {
            CreateAddressable($"{modelRoot}/{modelName}_{modelNumber}_p.prefab", $"Characters/{modelName}_{modelNumber}");
        }
        UnityEngine.Object.DestroyImmediate(avatar);
    }

    public static bool IsActorModel(string assetPath)
    {
        return assetPath.Contains("Characters/");
    }

    private static CSharpAnimator CreateWithType(Type type, string savePath, string saveName)
    {
        var path = string.Format("{0}/{1}.controller", savePath, saveName);

        var controller = AssetDatabase.LoadAssetAtPath(path, typeof(AnimatorController)) as AnimatorController;
        if (controller == null)
        {
            controller = AnimatorController.CreateAnimatorControllerAtPath(path);
            controller.RemoveLayer(0);

            AssetDatabase.SaveAssets();

            var animator = (CSharpAnimator)ScriptableObject.CreateInstance(type);
            animator.hideFlags |= HideFlags.HideInHierarchy;
            AssetDatabase.AddObjectToAsset(animator, controller);

            AssetDatabase.SaveAssets();
            return animator;
        }
        else
        {
            var subAssets = AssetDatabase.LoadAllAssetsAtPath(path);
            for (int i = 0; i < subAssets.Length; ++i)
            {
                if (subAssets[i] != null)
                {
                    var objType = subAssets[i].GetType();
                    if (objType == typeof(CSharpAnimator) || objType.IsSubclassOf(typeof(CSharpAnimator)))
                    {
                        return (CSharpAnimator)subAssets[i];
                    }
                }
            }

            return null;
        }
    }

    private static void TryGetAnimationClip(string rootPath, string modelName, AnimationType animationType, ref Motion motion)
    {
        string motionName;
        switch (animationType)
        {
            case AnimationType.Idle01:
                {
                    motionName = "Idle01";
                }
                break;
            case AnimationType.Run:
                {
                    motionName = "Move";

                    if (!File.Exists(string.Format("{0}/Fbx/{1}@{2}.FBX", rootPath, modelName, motionName)))
                    {
                        motionName = "Run";
                    }
                }
                break;
            case AnimationType.Skill:
                {
                    motionName = "Skill01_A";
                }
                break;
            default:
                {
                    motionName = animationType.ToString();
                }
                break;
        }

        var fbxPath = string.Format("{0}/Fbx/{1}@{2}.FBX", rootPath, modelName, motionName);
        if (!File.Exists(fbxPath))
        {
            return;
        }

        motion = AssetDatabase.LoadAssetAtPath(fbxPath, typeof(AnimationClip)) as AnimationClip;
    }

    private static void CreateAddressable(string assetPath, string address)
    {
        var setting = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>(ADDRESSABLE_ASSET_SETTINGS_PATH);
        var group = setting.FindGroup(ADDRESSABLE_CHARACTER_GROUP_NAME);

        var guid = AssetDatabase.AssetPathToGUID(assetPath);
        var entry = setting.CreateOrMoveEntry(guid, group);
        entry.address = address;
    }
}
