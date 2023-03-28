using UnityEngine;

namespace EcsSudoku.Services
{
    public static class Idents
    {
        public static class Colors
        {
            public static Color UnselectedCell = Color.white;
            public static Color SelectedCell = Color.gray;
            public static Color LinkedCell = Color.cyan;
            public static Color MistakeCell = Color.red;
            public static Color ButtonTextSwitchOn = Color.blue;
            public static Color ButtonTextSwitchOff = Color.black;
        }

        public static class Worlds
        {
            public static string Events = "Events";
        }
    }
}