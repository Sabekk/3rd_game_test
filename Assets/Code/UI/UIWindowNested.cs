using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Window
{
    public class UIWindowNested : UIWindowBase
    {
        #region VARIABLES

        [SerializeField] private UIWindowNested[] subWindows;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public override void Initialize()
        {
            base.Initialize();
            for (int i = 0; i < subWindows.Length; i++)
                subWindows[i].Initialize();
        }

        public override void CleanUp()
        {
            base.CleanUp();
            for (int i = 0; i < subWindows.Length; i++)
                subWindows[i].CleanUp();
        }


        protected override void Refresh()
        {
            base.Refresh();
            for (int i = 0; i < subWindows.Length; i++)
                subWindows[i].Refresh();
        }

        protected override void AttachEvents()
        {
            base.AttachEvents();
            for (int i = 0; i < subWindows.Length; i++)
                subWindows[i].AttachEvents();
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            for (int i = 0; i < subWindows.Length; i++)
                subWindows[i].DetachEvents();
        }

        #endregion
    }
}