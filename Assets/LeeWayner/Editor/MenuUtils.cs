using System;
using UnityEditor;
using UnityEngine;

namespace Assets.LeeWayner.Utils
{
    class MenuUtils
    {      

        [MenuItem("Utils/Delete All PlayerPrefs")]
        static public void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
