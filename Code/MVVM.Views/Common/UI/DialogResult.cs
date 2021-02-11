/// Written by: Yulia Danilova
/// Creation Date: 11th of November, 2019
/// Purpose: Attached behavior that allows returning the DialogResult of a window in MVVM
#region ========================================================================= USING =====================================================================================
using System;
using System.Windows;
#endregion

namespace MVVM.Views.Common.UI
{
    public static class DialogResultAttachedProperty
    {
        #region ============================================================ ATTACHED PROPERTIES ============================================================================ 
        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached("DialogResult", typeof(bool?), typeof(DialogResultAttachedProperty), new PropertyMetadata(DialogResultChanged));
        #endregion

        #region ================================================================= METHODS ===================================================================================
        private static void DialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
                window.DialogResult = e.NewValue as bool?;
        }

        public static void SetDialogResult(Window _target, bool? _value)
        {
            _target.SetValue(DialogResultProperty, _value);
        }
        #endregion
    }
}
