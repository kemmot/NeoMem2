using System;
using System.Windows.Forms;

namespace NeoMem2.Gui
{
    public static class DragEventArgsExtensions
    {
        #region Effects

        public static bool IsCopyAllowed(this DragEventArgs args)
        {
            return IsEffectAllowed(args, DragDropEffects.Copy);
        }

        public static bool IsMoveAllowed(this DragEventArgs args)
        {
            return IsEffectAllowed(args, DragDropEffects.Move);
        }

        public static bool IsEffectAllowed(this DragEventArgs args, DragDropEffects effect)
        {
            return (args.AllowedEffect & effect) == effect;
        }

        #endregion

        #region Key states

        public static bool IsControlKeyPressed(this DragEventArgs args)
        {
            return IsKeyPressed(args, KeyStates.ControlKey);
        }

        private static bool IsKeyPressed(DragEventArgs args, KeyStates key)
        {
            return IsKeyPressed(args, (int)key);
        }

        private static bool IsKeyPressed(DragEventArgs args, int keyCode)
        {
            return (args.KeyState & keyCode) == keyCode;
        }
        
        [Flags]
        enum KeyStates
        {
            LeftMouseButton = 1,
            RightMouseButton = 2,
            ShiftKey = 4,
            ControlKey = 8,
            MiddleMouseButton = 16,
            AltKey = 32

        }
        #endregion
    }
}
