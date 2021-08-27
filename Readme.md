<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128637631/11.2.8%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E3581)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [BandMap.cs](./CS/MyTreeList/BandMap.cs) (VB: [BandMap.vb](./VB/MyTreeList/BandMap.vb))
* [Form1.cs](./CS/MyTreeList/Form1.cs) (VB: [Form1.vb](./VB/MyTreeList/Form1.vb))
* [MyColumnDraggingState.cs](./CS/MyTreeList/MyColumnDraggingState.cs) (VB: [MyColumnDraggingState.vb](./VB/MyTreeList/MyColumnDraggingState.vb))
* [MyPositionInfo.cs](./CS/MyTreeList/MyPositionInfo.cs) (VB: [MyPositionInfo.vb](./VB/MyTreeList/MyPositionInfo.vb))
* [MyResourceInfo.cs](./CS/MyTreeList/MyResourceInfo.cs) (VB: [MyResourceInfo.vb](./VB/MyTreeList/MyResourceInfo.vb))
* [MyTreeList.cs](./CS/MyTreeList/MyTreeList.cs) (VB: [MyTreeListHandler.vb](./VB/MyTreeList/MyTreeListHandler.vb))
* [MyTreeListBand.cs](./CS/MyTreeList/MyTreeListBand.cs) (VB: [MyTreeListBand.vb](./VB/MyTreeList/MyTreeListBand.vb))
* [MyTreeListBandCollection.cs](./CS/MyTreeList/MyTreeListBandCollection.cs) (VB: [MyTreeListBandCollection.vb](./VB/MyTreeList/MyTreeListBandCollection.vb))
* [MyTreeListBandInfo.cs](./CS/MyTreeList/MyTreeListBandInfo.cs) (VB: [MyTreeListBandInfo.vb](./VB/MyTreeList/MyTreeListBandInfo.vb))
* [MyTreeListHandler.cs](./CS/MyTreeList/MyTreeListHandler.cs) (VB: [MyTreeListHandler.vb](./VB/MyTreeList/MyTreeListHandler.vb))
* [MyTreeListViewInfo.cs](./CS/MyTreeList/MyTreeListViewInfo.cs) (VB: [MyTreeListViewInfo.vb](./VB/MyTreeList/MyTreeListViewInfo.vb))
* [Program.cs](./CS/MyTreeList/Program.cs) (VB: [Program.vb](./VB/MyTreeList/Program.vb))
<!-- default file list end -->
# How to group tree list columns in bands in the same manner as in AdvancedGridView


<p><strong>This feature is available out-of-the-box starting from version 14.2<strong>.</strong><strong> See <a href="https://www.devexpress.com/Support/Center/p/AS4236">TreeList - Implement bands in a manner similar to the one provided in AdvBandedGridView</a>. </strong><br /><br />For earlier versions:<br /><br /></strong></p>
<p>This example demonstrates how to create a custom tree list with the capability to create bands in the same manner as in AdvancedGridView.<br /> Note that this is <strong>not a full implementation</strong> of bands in TreeList but just an <strong>example</strong> of how it can be done. Some features like Drag&Drop of bands and Column Chooser are disabled because of differences in TreeList layout calculation algorithm which does not support bands.<br /><br /></p>

<br/>


