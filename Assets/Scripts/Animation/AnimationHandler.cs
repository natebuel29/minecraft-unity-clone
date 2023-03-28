using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NB
{
    public class AnimationHandler : MonoBehaviour
    {
        public Animator axeAnimator;

        public void PlayMineAnimation(bool isMining)
        {
            axeAnimator.SetBool("isMining", isMining);
        }
    }
}