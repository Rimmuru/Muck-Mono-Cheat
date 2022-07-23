using UnityEngine;

namespace ClassLibrary1
{
    public class Loader
    {
        public static GameObject obj;
        public static void Load()
        {
            obj = new GameObject();
            obj.AddComponent<ActualShit>();
            Object.DontDestroyOnLoad(obj);
        }
        public static void Unload() 
        {
            Object.Destroy(obj);
        }
    }

}
