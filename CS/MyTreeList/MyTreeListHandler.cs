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
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Handler;

namespace MyXtraTreeList {
    public class MyTreeListHandler : TreeListHandler {
        public MyTreeListHandler(MyTreeList treeList)
            : base(treeList) {
        }

        protected override TreeListControlState CreateState(TreeListState state) {
            switch (state) {
                case TreeListState.ColumnDragging:
                    return new MyColumnDraggingState(this);
            }
            return base.CreateState(state);
        }
    }
}
