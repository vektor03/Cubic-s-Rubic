<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cubics_Rubic._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Cubic&#39;s Rubic</h1>
        <p class="lead">
            &nbsp;</p>
        <asp:Panel ID="Panel1" runat="server" Width="1289px">
            <asp:Image ID="Image1" runat="server" Height="500px" ImageUrl="Content/temp.png" Width="500px" />
            <asp:Button ID="FrontBtn" runat="server" Height="67px" OnClick="FrontBtn_Click" Text="Front" Width="120px" />
            <asp:Button ID="BackBtn" runat="server" Height="67px" OnClick="BackBtn_Click" Text="Back" Width="120px" />
            <asp:Button ID="RightBtn" runat="server" Height="67px" OnClick="RightBtn_Click" Text="Right" Width="120px" />
            <asp:Button ID="LeftBtn" runat="server" Height="67px" OnClick="LeftBtn_Click" Text="Left" Width="120px" />
            <asp:Button ID="TopBtn" runat="server" Height="67px" OnClick="TopBtn_Click" Text="Top" Width="120px" />
            <asp:Button ID="DownBtn" runat="server" Height="67px" OnClick="DownBtn_Click" Text="Down" Width="120px" />
        </asp:Panel>
    </div>

</asp:Content>
