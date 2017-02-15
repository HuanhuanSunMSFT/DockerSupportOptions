//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

namespace Microsoft.VisualStudio.Docker.Shared.UI
{
    public class CloseableDialogViewModel : ObservableObject
    {
        public ICloseableDialogWindow DialogWindow { get; set; }
    }
}
