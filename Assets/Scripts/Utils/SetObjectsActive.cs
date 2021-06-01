using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reliquary.Utils
{
    public class SetObjectsActive : MonoBehaviour
    {
        [SerializeField] private GameObject[] gameObjects;


        public void SetActive(bool _active)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.SetActive(_active);
            }
        }
    }
}
