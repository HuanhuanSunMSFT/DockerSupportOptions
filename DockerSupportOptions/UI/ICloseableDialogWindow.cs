//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

namespace Microsoft.VisualStudio.Docker.Shared.UI
{
    public interface ICloseableDialogWindow
    {
        /// <summary>
        /// Sets the result to be returned by the dialog and closes it.
        /// </summary>
        void Close(bool result);
    }
}
