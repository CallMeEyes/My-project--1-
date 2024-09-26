using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CheeseSticks : MonoBehaviour
{

    [SerializeField] 
        private TMP_Text _title;

        public void OnButtonClick()
        {
            _title.text = "Your new text is here";
        }
}
