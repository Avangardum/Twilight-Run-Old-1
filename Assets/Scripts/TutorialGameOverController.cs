using System;
using UnityEngine;

namespace TwilightRun
{
    public class TutorialGameOverController : SingletonMonoBehaviour<TutorialGameOverController>
    {
        public void GameOver()
        {
            SceneLoader.Instance.ReloadScene();
        }
    }
}
