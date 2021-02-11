﻿/// Written by: Yulia Danilova
/// Creation Date: 09th of November, 2020
/// Purpose: Interface for interacting with the system clipboard memory

namespace MVVM.ViewModels.Common.Interfaces
{
    public interface IClipboard
    {
        #region ================================================================= METHODS ===================================================================================
        void SetText(string _text);
        #endregion
    }
}
