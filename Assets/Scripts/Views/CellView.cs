using TMPro;
using UnityEngine;

namespace EcsSudoku.Views
{
    public class CellView : MonoBehaviour
    {
        public SpriteRenderer Background; 
        public TextMeshProUGUI NumberText;
        public int Entity;
        public Notes Notes;
    }
}