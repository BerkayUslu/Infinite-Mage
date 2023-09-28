using UnityEditor;
using UnityEngine;

namespace snail
{
    static class SearchAssets
    {
        public static T[] SearchAssetsForScriptableObjectInstances<T>() where T : ScriptableObject
        {
            T[] result;
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            int length = guids.Length;
            if (length == 0)
            {
                Debug.LogError("No ScriptableObject instance is found");
                return null;
            }
            else
            {
                result = new T[length];
            }
            for (int i = 0; i < length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                result[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }
            return result;
        }
    }

    static class ManageGame
    {
        public static void PauseGame(bool a)
        {
            Time.timeScale = a ? 0f : 1f;
        }
    }

    static class CustomMath
    {
        public static Vector3 ElementwiseVectorMultiply(this Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }
    }

}

