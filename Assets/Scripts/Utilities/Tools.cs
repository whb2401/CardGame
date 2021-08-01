using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Text.RegularExpressions;

public static class Tools
{
    // AES 加密的初始化向量，加密解密需设置相同的值。
    static byte[] AES_IV = Encoding.UTF8.GetBytes("0000000000000000");
    public const string lKey = "mj^co3s!YJaGj5oj9$&fT$qvD5TTsrg6u5BF^pKZ7!sN!suldx";

    #region Extension

    public static Transform DeepFind(this Transform trans, string name)
    {
        var array = trans.GetComponentsInChildren<Transform>();
        foreach (var t in array)
        {
            if (t.name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return t;
            }
        }

        return null;
    }

    public static void SetLayerOrder<T>(this GameObject obj, int layerOrder)
    {
        if (obj != null)
        {
            if (typeof(T) == typeof(SpriteRenderer))
            {
                var spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sortingOrder = layerOrder;
                }
            }
        }
    }

    public static int GetLayerOrder<T>(this GameObject obj)
    {
        int order = 0;
        if (obj != null)
        {
            if (typeof(T) == typeof(SpriteRenderer))
            {
                var spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    order = spriteRenderer.sortingOrder;
                }
            }
        }

        return order;
    }

    public static void SetVisibility(this Transform trans, bool visible)
    {
        if (trans != null)
        {
            SetVisibility(trans.gameObject, visible);
        }
    }

    public static void SetVisibility(this Text obj, bool visible)
    {
        if (obj != null)
        {
            SetVisibility(obj.gameObject, visible);
        }
    }

    public static void SetVisibility(this GameObject obj, bool visible)
    {
        if (obj != null)
        {
            obj.SetActive(visible);
        }
    }

    public static void SetParentVisibility(this GameObject obj, bool visible)
    {
        if (obj != null && obj.transform.parent != null)
        {
            obj.transform.parent.gameObject.SetActive(visible);
        }
    }

    public static void SetTextValue(this Text obj, string text)
    {
        if (obj != null)
        {
            obj.text = text;
        }
    }

    public static void SetTextValue<T>(this Text obj, T text)
    {
        if (obj != null)
        {
            obj.text = text.ToString();
        }
    }

    public static void RemoveAll(this IList list)
    {
        while (list.Count > 0)
        {
            list.RemoveAt(list.Count - 1);
        }
    }

    public static List<Transform> GetChildList(this Transform trans, bool includeInactive = false)
    {
        List<Transform> list = new List<Transform>();

        if (trans == null)
        {
            return list;
        }

        for (int i = 0; i < trans.childCount; ++i)
        {
            Transform t = trans.GetChild(i);
            if (t && includeInactive ? true : Tools.GetObjActive(t.gameObject))
            {
                list.Add(t);
            }
        }

        return list;
    }

    public static bool HasAnimParameter(this Animator animator, string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName) return true;
        }
        return false;
    }

    #region Transform

    public static void Reset(this Transform xform)
    {
        xform.localPosition = Vector3.zero;
        xform.localScale = Vector3.one;
        xform.localRotation = Quaternion.identity;
    }

    public static void SetParentSafe(this Transform transform, Transform parent)
    {
        var lp = transform.localPosition;
        var lr = transform.localRotation;
        var ls = transform.localScale;
        transform.SetParent(parent);
        transform.localPosition = lp;
        transform.localRotation = lr;
        transform.localScale = ls;
    }

    public static void ResetLocal(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static Vector3 GetVector3XZ(this Vector3 vec3)
    {
        return new Vector3(vec3.x, 0, vec3.z);
    }

    #endregion Transform

    #region Text

    public static string FLHTextTruncate(this string text, int maxLength, string endWith = "...")
    {
        if (string.IsNullOrEmpty(text))
        {
            return text + endWith;
        }

        if (text.Length > maxLength)
        {
            return text.Substring(0, maxLength).TrimEnd() + endWith;
        }

        return text;
    }

    public static string[] FLHGetTextRomanNumerals(this string text)
    {
        const string pattern = @"IX|IV|V?I{0,3}";

        var romanNum = "";
        var matches = Regex.Matches(text, pattern);
        foreach (Match item in matches)
        {
            if (!string.IsNullOrEmpty(item.Value))
            {
                romanNum = item.Value;
            }
        }

        if (string.IsNullOrEmpty(romanNum))
        {
            return new string[] { text, "" };
        }

        if (string.IsNullOrEmpty(romanNum))
        {
            return new string[] { text, "" };
        }

        return new string[] { text.Replace(romanNum, ""), romanNum };
    }

    public static string FLHReplaceChangeLine(this string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            return text.Replace("\\n", "\n");
        }
        return "";
    }

    public static string FLHReplaceChangeLine2Blank(this string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            return text.Replace("\\n", "");
        }
        return "";
    }

    public static List<string> FLHGetHelpTextByBoldSection(this string text)
    {
        const string pattern = @"(?s)\[b\].*?(?=\|)\s*";

        var result = new List<string>();
        var matches = Regex.Matches(text, pattern);
        foreach (Match item in matches)
        {
            result.Add(item.Value);
        }
        return result;
    }

    public static Dictionary<object, string> FLHGetHelpTextFormat(this string text, out string type)
    {
        const string pattern = @"(?s)\<(?<type>[^\]]+)=(?<id>[^\]]+)>(?<text>[^<]+)<end>\s*";

        type = "";
        var dicResult = new Dictionary<object, string>();
        var matches = Regex.Matches(text, pattern);
        if (matches.Count > 0)
        {
            type = matches[0].Groups["type"].Value;
            foreach (Match item in matches)
            {
                var groups = item.Groups;
                dicResult.Add(groups["id"].Value, groups["text"].Value);
            }
        }

        return dicResult;
    }

    public static Dictionary<string, string> FLHGetHelpTextSplitFormat(this string text, out string type)
    {
        const string pattern = @"(?s)\{(?<type>[^\]]+)=(?<id>[^\]]+)}(?<text>[^<]+)\s*";

        type = "";
        var dicResult = new Dictionary<string, string>();
        var texts = text.Split('|');
        foreach (var textItem in texts)
        {
            var matches = Regex.Matches(textItem, pattern);
            if (matches.Count > 0)
            {
                type = matches[0].Groups["type"].Value;
                foreach (Match item in matches)
                {
                    var groups = item.Groups;
                    dicResult.Add(groups["id"].Value, groups["text"].Value.Replace('|', ' '));
                }
            }
        }

        return dicResult;
    }

    public static string FLHFilterTheNumberInTheText(this string text)
    {
        const string pattern = @"\d+[.]?\d*%?";
        var evaluator = new MatchEvaluator(ProcessTextNumer2Italic);
        return Regex.Replace(text, pattern, evaluator);
    }

    private static string ProcessTextNumer2Italic(Match match)
    {
        return "[i]" + match.Value + "[/i]";
    }

    public static string FLHFilterSizeCodeInTheText(this string text, out int size)
    {
        const string pattern = @"\[size=([^\]]+)]";
        var result = text;
        size = 0;

        foreach (Match item in Regex.Matches(text, pattern))
        {
            result = text.Replace(item.Groups[0].Value, "");
            int.TryParse(item.Groups[1].Value, out size);
        }

        return result;
    }

    #endregion Text

    #endregion Extension

    #region Encrypt&Decrypt

    static byte[] GetKey(string pwd)
    {
        while (pwd.Length < 32)
        {
            pwd += '0';
        }
        pwd = pwd.Substring(0, 32);
        return Encoding.UTF8.GetBytes(pwd);
    }

    public static byte[] EncryptAES(string plainText, string pwd)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (string.IsNullOrEmpty(pwd))
            throw new ArgumentNullException("Key");
        if (AES_IV == null || AES_IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = GetKey(pwd);
            aesAlg.IV = AES_IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    public static string DecryptAes(byte[] cipherText, string pwd)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (string.IsNullOrEmpty(pwd))
            throw new ArgumentNullException("Key");
        if (AES_IV == null || AES_IV.Length <= 0)
            throw new ArgumentNullException("IV");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = GetKey(pwd);
            aesAlg.IV = AES_IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }

    #endregion Encrypt&Decrypt

    #region Events

    public static bool IsTouchedUI()
    {
        bool touchedUI = false;
        if (Application.isMobilePlatform)
        {
            // (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                touchedUI = true;
            }
        }
        else if (EventSystem.current.IsPointerOverGameObject())
        {
            touchedUI = true;
        }
        return touchedUI;
    }

    #endregion Events

    #region Common

    static public bool GetObjActive(GameObject go)
    {
        return go && go.activeInHierarchy;
    }

    #endregion Common

    #region DoTweenExt

    /// <summary>
    /// DOTween.Sequence延时回调
    /// </summary>
    /// <param name="delayedTimer">延时的时间</param>
    /// <param name="loopTimes">循环次数，0:不循环；负数：无限循环；正数：循环多少次</param>
    static public void Delay(float delayedTimer, ref int loopTimes, Action act)
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            if (act != null)
            {
                act.Invoke();
            }
        })
        .SetDelay(delayedTimer)
        .SetLoops(loopTimes);
    }

    #endregion DoTweenExt
}
