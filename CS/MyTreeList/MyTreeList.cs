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
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.ViewInfo;
using DevExpress.XtraTreeList.Handler;
using DevExpress.LookAndFeel;
using DevExpress.XtraTreeList.Menu;
using DevExpress.Utils.Menu;

namespace MyXtraTreeList {
    public class MyTreeList : TreeList {
        private MyTreeListBand fRootBand;

        public MyTreeListBandCollection fBands;

        public MyTreeList()
            : base() {
            Bands = new MyTreeListBandCollection(this);
            fRootBand = Bands[0];
            this.PopupMenuShowing += onPopupMenuShowing;
        }

        public MyTreeList(object ignore)
            : base(ignore) {
        }

        protected internal MyTreeListBand RootBand {
            get { return fRootBand; }
        }

        public MyTreeListBandCollection Bands {
            get { return fBands; }
            set {
                if (fBands != value) {
                    fBands = value;
                    fBands.UpdateColumns();
                }
            }
        }

        public new MyTreeListViewInfo ViewInfo {
            get { return base.ViewInfo as MyTreeListViewInfo; }
        }

        protected override TreeListViewInfo CreateViewInfo() {
            return new MyTreeListViewInfo(this);
        }

        protected override TreeListHandler CreateHandler() {
            return new MyTreeListHandler(this);
        }

        public bool Swap(MyTreeListBand BandA, MyTreeListBand BandB) {
            if (BandA.Parent != null) {
                return BandA.Parent.TryToSwap(BandA, BandB);
            }
            return false;
        }

        public bool Swap(MyTreeListBand Band, TreeListColumn Column) {
            if (Band.Parent != null) {
                return Band.Parent.TryToSwapBandAndColumn(Band, Column);
            }
            return false;
        }

        public bool Swap(TreeListColumn ColumnA, TreeListColumn ColumnB) {
            return RootBand.TryToSwapColumns(ColumnA, ColumnB);
        }

        internal void MyRaiseDragObjectOver(DragObjectOverEventArgs e) {
            RaiseDragObjectOver(e);
        }

        internal void MyRaiseDragObjectDrop(DragObjectDropEventArgs e) {
            RaiseDragObjectDrop(e);
        }

        public void SetBandsWidth(int Width) {
            RootBand.SetWidth(Width);
        }

        protected internal new UserLookAndFeel ElementsLookAndFeel {
            get { return base.ElementsLookAndFeel; }
        }

        protected override void ResizeColumn(int index, int byUnits, int maxPossibleWidth)
        {
            if (ViewInfo.BandLinks.ContainsKey(VisibleColumns[index]))
            {
                MyTreeListBandCollection childBands = ViewInfo.BandLinks[VisibleColumns[index]].Band.Children;
                TreeListColumn[] childColumns = ViewInfo.BandLinks[VisibleColumns[index]].Band.Columns;
                int unitsPerChild = byUnits / (childBands.Count + childColumns.Length);
                foreach (MyTreeListBand band in childBands)
                {
                    if(band.Visible)
                        ResizeColumn(band.BandColumn.VisibleIndex, unitsPerChild, maxPossibleWidth);
                }
                foreach (TreeListColumn col in childColumns)
                {
                    if(col != null && col.Visible)
                        ResizeColumn(col.VisibleIndex, unitsPerChild, maxPossibleWidth);
                }
            }
            else
                base.ResizeColumn(index, byUnits, maxPossibleWidth);
        }

        void onPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.Menu.MenuType == TreeListMenuType.Column)
            {
                foreach (DXMenuItem item in e.Menu.Items)
                {
                    if (item.Caption == "Column Chooser")
                    {
                        item.Enabled = false;
                        break;
                    }
                }
            }
        }
    }
}