<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Cubics_Rubic._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Cubic&#39;s Rubic</h1>
        <p class="lead">
            &nbsp;</p>
        <asp:Panel ID="Panel1" runat="server" Width="476px" Height="300px">
            <asp:Image ID="Image1" runat="server" Height="300px" ImageUrl="Content/temp.png" Width="300px" ImageAlign="Left" />
            <asp:ListBox ID="ListBox1" runat="server" Height="142px" Width="171px"></asp:ListBox>
            <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="False" Interval="500">
            </asp:Timer>
            <br />
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" Width="482px">
                <asp:Button ID="ShuffleBtn" runat="server" Height="67px" OnClick="ShuffleBtn_Click" Text="Shuffle" Width="120px" />
                <asp:Button ID="SolveBtn" runat="server" Height="67px" OnClick="SolveBtn_Click" Text="Solve" Width="120px" />
                <asp:Button ID="MoveBtn" runat="server" Height="67px" OnClick="MoveBtn_Click" Text="Do Move" Width="120px" />
        </asp:Panel>
    </div>

</asp:Content>