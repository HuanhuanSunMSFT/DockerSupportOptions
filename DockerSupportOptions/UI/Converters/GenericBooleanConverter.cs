//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Microsoft.VisualStudio.Docker.Shared.UI.Converters
{
    /// <summary>
    /// Represents a WPF converter that converts a boolean to a value of the specified type.
    /// </summary>
    public abstract class GenericBooleanConverter<TTarget> : IValueConverter
    {
        /// <summary>
        /// Gets or sets the target type value that is associated with a true value.
        /// </summary>
        public TTarget TrueValue { get; set; }

        /// <summary>
        /// Gets or sets the target type value that is associated with a false value.
        /// </summary>
        public TTarget FalseValue { get; set; }

        /// <summary>
        /// If true, converts from a target type value to a Boolean, rather than from a Boolean to a target type value.
        /// </summary>
        public bool Invert { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Invert)
            {
                return ConvertTargetTypeToBoolean(value, targetType, parameter, culture);
            }

            return ConvertBooleanToTargetType(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Invert)
            {
                return ConvertBooleanToTargetType(value, targetType, parameter, culture);
            }

            return ConvertTargetTypeToBoolean(value, targetType, parameter, culture);
        }

        protected virtual object ConvertBooleanToTargetType(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? TrueValue : FalseValue;
        }

        protected virtual object ConvertTargetTypeToBoolean(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TTarget targetValue = (TTarget)value;

            if (EqualityComparer<TTarget>.Default.Equals(targetValue, TrueValue))
            {
                return true;
            }

            if (EqualityComparer<TTarget>.Default.Equals(targetValue, FalseValue))
            {
                return false;
            }

            Debug.Fail("This converter does not support converting from value " + targetValue);

            return DependencyProperty.UnsetValue;
        }
    }
}
