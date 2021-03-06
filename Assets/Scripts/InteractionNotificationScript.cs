﻿using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class InteractionNotificationScript : MonoBehaviour
    {

        public Boolean Interactable;
        protected GameObject InteractionNotification;
        protected MeshRenderer NotificationMeshRenderer;

        // Use this for initialization
        public void Start () {

            InteractionNotification = GameObject.Find("interactionNotification");
            NotificationMeshRenderer = InteractionNotification.GetComponent<MeshRenderer>();

            if (Interactable)
            {
                NotificationMeshRenderer.enabled = Interactable;
            }
            else
            {
                NotificationMeshRenderer.enabled = Interactable;
            }
        }
	
        // Update is called once per frame
        public void Update () {
        }
    }
}
