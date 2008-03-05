<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabelEdit.aspx.cs" Inherits="LabelEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Label Edit Test</title>
    <link rel="stylesheet" type="text/css" href="./main.css">
    
    <script  type="text/javascript" src="./includes/PageUtils.js"></script>
    <script src="includes/grid.js" language="javascript" type="text/javascript"></script>
    <script  type="text/javascript" src="./includes/LabelEdit.js"></script>
    
    <script language=javascript>
    
        window.onload = function ()
        {
            var t1 = new LabelEdit(" val one ","id1",false,"textarea",saveCallBack);
            t1.init();
            var t2 = new LabelEdit(" val two ","id2",true,"textarea",saveCallBack);
            t2.init();
            var t3 = new LabelEdit(" val three ","id3",false,"input",saveCallBack);
            t3.init();
            
            var attachHook = document.getElementById("attachPoint");
            
            attachHook.appendChild( t1.container );
            attachHook.appendChild( t2.container );
            attachHook.appendChild( t3.container );
        
        }
        
        function saveCallBack(what)
        {
            alert("save" + what);
        }
    
    </script>
</head>
<body>
    
   
    <div id="attachPoint">
    
    </div>
   
</body>
</html>
