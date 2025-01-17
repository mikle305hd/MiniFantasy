﻿using System;
using Additional.Constants;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Infrastructure.Services
{
    public class StandaloneInputService : InputService
    {
        public StandaloneInputService(IObjectsProvider objectsProvider) : base(objectsProvider)
        {
            if (!Application.isEditor) 
                BlockCursor();
        }

        public override Vector2 GetAxis() =>
            new(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));

        public override bool IsAttackInvoked()
        {
            if (IsUiPressed())
                return false;
            
            return Math.Abs(Input.GetAxis(Fire) - 1.0f) < Constants.Epsilon;
        }

        private static bool IsUiPressed() =>
            Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject();

        private static void BlockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}