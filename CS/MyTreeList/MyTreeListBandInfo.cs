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
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.ViewInfo;

namespace MyXtraTreeList {
    public class MyTreeListBandInfo : ColumnInfo {
        private MyTreeListBand fBand;

        public MyTreeListBandInfo(MyTreeListBand band)
            : base(band.BandColumn)
        //: base(new TreeListColumn())
        {
            fBand = band;
            Type = ColumnInfoType.ColumnButton;
        }

        public virtual MyTreeListBand Band {
            get { return fBand; }
        }

        public new MyTreeList TreeList {
            get { return Band.TreeList; }
        }

        public virtual void CalcBandInfo(MyTreeListViewInfo viewInfo) {
            MyTreeList TL = viewInfo.TreeList;
            Appearance.Assign(viewInfo.PaintAppearance.HeaderPanel);
            int VisiblePosition = Band.IndexToColumnHandle(0);
            int X = viewInfo.ViewRects.IndicatorBounds.Width + 1 - TL.LeftCoord;
            for (int i = 0; i < VisiblePosition; i++) {
                TreeListColumn Col = TreeList.RootBand.GetColumn(i);
                if ((Col != null) && Col.Visible) {
                    X += Col.Width;
                }
            }
            int Y = (Band.Level - 1) * viewInfo.BandHeight + viewInfo.ViewRects.ColumnPanel.Y;
            int Width = 0;
            for (int i = 0; i < Band.Width; i++) {
                TreeListColumn Col = Band.GetColumn(i);
                if ((Col != null) && Col.Visible) {
                    Width += Col.Width;
                }
            }
            int Height = viewInfo.BandHeight + 1;
            if ((Band.Children.Count == 0) && (!Band.HasColumns())) {
                Height *= viewInfo.BandMaxLevel - Band.Level;
            }
            Bounds = new Rectangle(X, Y, Width, Height);
            CaptionRect = new Rectangle(0, 0, Width, Height);
            Caption = Band.Name;
        }
    }
}
