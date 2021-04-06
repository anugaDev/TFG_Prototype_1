using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reliquary.Utils
{
    public class SetActiveOnInput : MonoBehaviour
    {
        [SerializeField] private GameObject gameObject;
        [SerializeField] private KeyCode input;

        private void Update()
        {
            if(Input.GetKeyDown(input))
                gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}

