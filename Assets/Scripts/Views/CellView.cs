using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EcsSudoku.Views
{
    public class CellView : MonoBehaviour
    {
        public TextMeshProUGUI NumberText;
        public Image Background;
        [NonSerialized] public int Entity;
    }
}