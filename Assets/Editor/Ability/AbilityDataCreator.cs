using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using FLH.Battle.Ability;

namespace FLH.Editor.Skill
{
    public class AbilityDataCreator
    {
        [MenuItem("Assets/Create/FLH/Ability Data", false, -1)]
        public static void Create()
        {
            string rootPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (!Directory.Exists(rootPath))
            {
                rootPath = Path.GetDirectoryName(rootPath);
            }

            string path = AssetDatabase.GenerateUniqueAssetPath($"{rootPath}/New Ability Data.asset");
            var data = ScriptableObject.CreateInstance<AbilityData>();

            AssetDatabase.CreateAsset(data, path);
            AssetDatabase.Refresh();
        }
    }
}
