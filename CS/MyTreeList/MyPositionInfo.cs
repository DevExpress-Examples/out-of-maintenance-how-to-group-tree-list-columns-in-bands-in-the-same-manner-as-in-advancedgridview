// Developer Express Code Central Example:
// How to create a custom TreeList that allows you to group tree list columns in bands in the same manner as in AdvancedGridView
// 
// This example demonstrates how to create a custom tree list with the capability
// to create bands in the same manner as in AdvancedGridView.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3581
using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.ViewInfo;

namespace MyXtraTreeList {
    public class MyPositionInfo : PositionInfo {
        private ColumnInfo fInfo;

        public MyPositionInfo()
            : base() {
            fInfo = null;
        }

        public void Assign(MyPositionInfo pos) {
            base.Assign(pos);
            fInfo = pos.Info;
        }

        public void Assign(int newIndex, Rectangle newBounds, bool valid, ColumnInfo info) {
            base.Assign(newIndex, newBounds, valid);
            fInfo = info;
        }

        public ColumnInfo Info {
            get { return fInfo; }
        }
    }
}
