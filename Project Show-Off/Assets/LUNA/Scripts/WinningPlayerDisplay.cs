using System;
using TMPro;
using UnityEngine;
namespace LUNA.Scripts{
    public class WinningPlayerDisplay : MonoBehaviour{
        public TextMeshProUGUI VictorTextInstance;

        private void Awake(){
            if (GameManager.instance == null) throw new Exception("GameManager not initialised!");
            VictorTextInstance.text = $"{GameManager.instance.VictorName} wins!";
        }
    }
}