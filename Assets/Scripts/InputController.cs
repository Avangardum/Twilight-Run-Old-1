using System;
using UnityEngine;

namespace TwilightRun
{
    public class InputController : SingletonMonoBehaviour<InputController>
    {
        public event Action Tap;

        private void Update()
        {
            if (Input.touchCount > 0 || Input.GetMouseButton(0))
                Tap?.Invoke();
        }
    }
}