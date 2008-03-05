<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageEditor3.aspx.cs" Inherits="PageEditor3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head> 

<title>temptitle</title> 

<meta http-equiv="content-type" content="text/html; charset=UTF-8" /> 

<script type="text/javascript"> 
<!-- 
//global variables used to track status 
var curHeight=0 
var curPos=0 
var newPos=0 
var mouseStatus='up' 

//this function gets the original div height 
function setPos(e){ 
//for handling events in ie vs. w3c 
curevent=(typeof event=='undefined'?e:event) 
//sets mouse flag as down 
mouseStatus='down' 
//gets position of click 
curPos=curevent.clientY 
//accepts height of the div 
tempHeight=document.getElementById('mydiv').style.height 
//these lines split the height value from the 'px' units 
heightArray=tempHeight.split('p') 
curHeight=parseInt(heightArray[0]) 
} 

//this changes the height of the div while the mouse button is depressed 
function getPos(e){ 
if(mouseStatus=='down'){ 
curevent=(typeof event=='undefined'?e:event) 
//get new mouse position 
newPos=curevent.clientY 
//calculate movement in pixels 
var pxMove=parseInt(newPos-curPos) 
//determine new height 
var newHeight=parseInt(curHeight+pxMove) 
//conditional to set minimum height to 5 
newHeight=(newHeight<5?5:newHeight) 
//set the new height of the div 
document.getElementById('mydiv').style.height=newHeight+'px' 
} 
} 

//--> 
</script> 

<style type="text/css"> 

body {height:100%;} 
/*now relatively positioned and no longer contains #statusbar and has overflow set to auto to allow scrolling*/ 
#mydiv{ 
position:relative; 
top:0px; 
left:0px; 
width:250px; 
border: 1px solid #808080; 
overflow: auto; 
} 

/*now relatively positioned 
also note the font-size:0px hack to force ie to do what it should--display the div with height of 5px*/ 
#statusbar{ 
cursor: s-resize; 
position:relative; 
display:block; 
background-color: #c0c0c0; 
font-size: 0px; 
margin:0; 
height:5px; 
border: 1px solid #808080; 
padding:0; 
width: 250px; 
} 
</style> 

</head> 

<body onmousemove="getPos(event)" onmouseup="mouseStatus='up'"> 
<!--note that mydiv does not contain statusbar. You might want to wrap both in a container div//--> 
<div id="mydiv" style="height: 250px;"> 
<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas aliquam massa sed dui. Aliquam ornare metus a purus. Nullam pellentesque, tellus at viverra pellentesque, lorem dolor placerat lectus, vitae suscipit augue lorem eu est. Phasellus laoreet. Mauris tempus. Sed malesuada suscipit lacus. Fusce enim magna, pharetra sed, tincidunt at, gravida auctor, neque. Curabitur quis felis eu libero commodo sollicitudin.</p> 
</div> 
<div onmousedown="setPos(event)" id="statusbar">div> 
</body> 
</html> 

