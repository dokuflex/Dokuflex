// Copyright (c) Dokuflex, Co. All rights reserved.
// See License.txt in the project root for license information.


namespace Dokuflex.WinControls.UserControls
{
    using System.Windows.Forms;

    public partial class BiometricSignatureControl : UserControl
    {
        public BiometricSignatureControl()
        {
            InitializeComponent();
        }

        private void signButton_Click(object sender, System.EventArgs e)
        {
            sigPlusNET.SetTabletState(1);
        }

        private void clearButton_Click(object sender, System.EventArgs e)
        {
            sigPlusNET.ClearTablet();
        }

        private void finalizeButton_Click(object sender, System.EventArgs e)
        {
            sigPlusNET.SetTabletState(0);
            sigPlusNET.SetImageXSize(100);
            sigPlusNET.SetImageYSize(30);
            sigPlusNET.SetJustifyMode(5);
        }
    }
}
