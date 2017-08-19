<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:Button ID="btnCreateTable" runat="server" OnClick="btnCreateTable_Click" Text="Create a new Azure table" Width="300px" Font-Bold="True" />
        <br />
        <br />
        <asp:Button ID="btnAddTableEntry" runat="server" OnClick="btnAddTableEntry_Click" Text="Add an entry to the Azure table" Width="300px" Font-Bold="True" />
        <p>
            <asp:Button ID="btnAddTableBatch" runat="server" OnClick="btnAddTableBatch_Click" Text="Add a batch to the Azure table" Width="300px" Font-Bold="True" />
        </p>
        <asp:Button ID="btnRetrieveTableData" runat="server" OnClick="btnRetrieveTableData_Click" Text="Retrieve data from the Azure table" Width="300px" Font-Bold="True" />
        <asp:TextBox ID="TextBox1" runat="server" Height="65px" style="margin-top: 0px; margin-left: 17px;" Width="600px" TextMode="MultiLine" Font-Names="Courier New" Font-Size="Small"></asp:TextBox>
        <p>
            <asp:Button ID="btnCreateBlobContainer" runat="server" OnClick="btnCreateBlobContainer_Click" Text="Create a new Azure blob container" Width="300px" Font-Bold="True" />
        </p>
        <p>
            <asp:Button ID="btnUploadToBlob" runat="server" OnClick="btnUploadToBlob_Click" Text="Upload data to the Azure blob container" Width="300px" Font-Bold="True" />
        </p>
        <asp:Button ID="btnListBlobContent" runat="server" OnClick="btnListBlobContent_Click" Text="List content of the Azure blob container" Width="300px" Font-Bold="True" />
        <asp:TextBox ID="TextBox2" runat="server" Height="65px" style="margin-left: 18px; margin-top: 0px" TextMode="MultiLine" Width="600px" Font-Names="Courier New" Font-Size="Small"></asp:TextBox>
        <p>
            <asp:TextBox ID="TextBox3" runat="server" AutoPostBack="True" BackColor="#CCFFFF" BorderStyle="Solid" BorderWidth="3px" Font-Names="Courier New" Font-Size="Small" Height="60px" TextMode="MultiLine" Width="920px"></asp:TextBox>
        </p>
    </form>
</body>
</html>
