/// Written by: Yulia Danilova
/// Creation Date: 25th of January, 2021
/// Purpose: Base class for notifying the UI abour property changes
#region ========================================================================= USING =====================================================================================
using System.ComponentModel;
#endregion

namespace MVVM.Models.Common.MVVM
{
    public class LightBaseModel : INotifyPropertyChanged
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion        

        #region ================================================================= METHODS ===================================================================================
        /// <summary>
        /// Notifies the UI about a binded property's value being changed
        /// </summary>
        /// <param name="propName">The property that had the value changed</param>
        public void Notify(string _propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
        #endregion
    }
}