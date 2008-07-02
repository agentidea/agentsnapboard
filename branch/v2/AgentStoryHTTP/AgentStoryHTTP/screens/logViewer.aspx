<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logViewer.aspx.cs" Inherits="AgentStoryHTTP.screens.logViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AgentStory - Daily Log</title>
</head>

<frameset rows='100,*' border='1' frameborder='1'>
    <frame marginheight='6' marginwidth='10' name='fauxIrc' scrolling='no' src='fauxIrc.aspx'>
    <frame marginheight='0' marginwidth='10' name='processor' scrolling='auto' src='logReader.aspx'>
</frameset>


</html>

