//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using System.Windows;

namespace Microsoft.VisualStudio.Docker.Shared.UI.Converters
{
    /// <summary>
    /// Represents a WPF converter that converts a boolean to a Visibility value.
    /// </summary>
    public class BooleanToVisibilityConverter : GenericBooleanConverter<Visibility>
    {
        public BooleanToVisibilityConverter()
        {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }
    }
}