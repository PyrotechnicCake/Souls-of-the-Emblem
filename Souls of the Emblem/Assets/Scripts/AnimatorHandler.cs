using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class AnimatorHandler : MonoBehaviour
    {
        public Animator anim;
        int vertical;
        int horizontal;
        public bool canRotate;

        public void Initialize()
        {
            anim = GetComponent<Animator>();
        }
    }
}
