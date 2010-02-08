<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountActivation.aspx.cs" Inherits="AgentStoryHTTP.screens.AccountActivation" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Account Activation</title>
      <link href="../style/main.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <asp:Label ID="lblMsg" runat="server"></asp:Label><br />
        <br />
        Please enter activation Key:<br />
        <asp:TextBox ID="txtActivationCode" runat="server" Width="328px"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Label ID="lblTOS" runat="server" CssClass="clsTOS" Height="243px" Width="798px" style="overflow: hidden"></asp:Label><br />
        <br />
        <br />
        <br />
        <asp:CheckBox ID="chkAcceptTOS" runat="server" Text="I agree to the terms of service stated above." AutoPostBack="True" OnCheckedChanged="chkAcceptTOS_CheckedChanged" /><br />
        
        <br />
        <br />
        
        <asp:Button ID="cmdActivate" runat="server" Text="Activate Account" OnClick="cmdActivate_Click" Enabled="False" />
    </div>
    </form>
</body>
</html>