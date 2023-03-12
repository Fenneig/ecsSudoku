using TMPro;
using UnityEngine;

namespace EcsSudoku.Views
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _numberText;

        public TextMeshProUGUI NumberText => _numberText;
    }
}