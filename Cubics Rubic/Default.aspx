<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cubics_Rubic._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Cubic&#39;s Rubic</h1>
        <p class="lead">
            &nbsp;</p>
        <asp:Panel ID="Panel1" runat="server" Width="1019px" Height="506px">
            <asp:Image ID="Image1" runat="server" Height="500px" ImageUrl="Content/temp.png" Width="500px" ImageAlign="Left" />
            <asp:ListBox ID="ListBox1" runat="server" Height="248px" Width="502px"></asp:ListBox>
            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="False" Interval="500">
            </asp:Timer>
            <br />
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server">
                <asp:Button ID="ShuffleBtn" runat="server" Height="67px" OnClick="ShuffleBtn_Click" Text="Shuffle" Width="120px" />
                <asp:Button ID="SolveBtn" runat="server" Height="67px" OnClick="SolveBtn_Click" Text="Solve" Width="120px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="MoveBtn" runat="server" Height="67px" OnClick="MoveBtn_Click" Text="Do Move" Width="120px" />
        </asp:Panel>
    </div>

</asp:Content>