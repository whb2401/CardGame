using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts
{
    public class GameSplash : MonoBehaviour
    {
        public AudioSource asIn;
        public CanvasGroup cgLogo;

        private void Start()
        {
            DontDestroyOnLoad(asIn);
        }

        private void ShowSplash()
        {
            cgLogo.DOFade(1, 0.2f).SetDelay(4f);
        }
    }
}
