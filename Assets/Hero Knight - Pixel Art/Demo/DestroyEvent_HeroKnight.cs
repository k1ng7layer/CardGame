﻿using UnityEngine;

namespace Hero_Knight___Pixel_Art.Demo
{
    public class DestroyEvent_HeroKnight : MonoBehaviour
    {
        // Destroy particles when animation has finished playing. 
        // destroyEvent() is called as an event in animations.
        public void destroyEvent()
        {
            Destroy(gameObject);
        }
    }
}
