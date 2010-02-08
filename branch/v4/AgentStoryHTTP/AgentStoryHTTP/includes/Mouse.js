// JScript File

var currentHeight =0;
var currentPosition =0;
var newPosition =0;
var direction="Released";

function getDivPosition(mouse)
{
     //  Assign the global variable to 'Pressed'
     direction="Pressed";
     // Save the cursor position to its corresponding global variable
     currentPosition=mouse.clientY; 
    // Save the Current Div height to a local variable
    var divHeight=document.getElementById("firstdiv").style.height;
     // Remove the px from the value retrieved from above line of code 
    var heightNoPx=divHeight.split('p');
    currentHeight=parseInt(heightNoPx[0]);
    
    
 }
 
 function SetDivPosition(mouse)
{
  if (direction == 'Pressed')
  {
    //get new mouse position
    newPosition = mouse.clientY;
    //calculate movement in pixels
    var movePerPixels = parseInt(newPosition - currentPosition);
    //determine new height
    var divnewLocation = parseInt(currentHeight + movePerPixels);
    
    if(divnewLocation < 10)
    {
      //set the new height of the div
      document.getElementById('firstdiv').style.height = 10+'px';
    }
    else
    {
      //set the new height of the div
      document.getElementById('firstdiv').style.height = divnewLocation + 'px';
    }
  }
 }