//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
//
// Copyright (c) Paina Solutions. All right reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
//
//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=

namespace DokuFlex.WinForms.Common
{
    using System;
    using System.Collections;
    using System.Windows.Forms;

    public class ListViewItemReverseComparer : IComparer
    {
        private int col;

        public ListViewItemReverseComparer()
        {
            col = 0;
        }
        public ListViewItemReverseComparer(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            return  - String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        }
    }
}
