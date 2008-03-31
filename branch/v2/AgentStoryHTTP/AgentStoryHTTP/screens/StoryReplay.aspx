<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoryReplay.aspx.cs" Inherits="AgentStoryHTTP.screens.StoryReplay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%= PageTitle %></title>
</head>

    <script  type="text/javascript" src="../includes/PageUtils.js"></script>
    <script src="../includes/StoryElements.js" language="javascript" type="text/javascript"></script>
    <script  type="text/javascript" src="../includes/timer.js"></script>



<body>

 <script type="text/javascript" language="JavaScript">
 
    var story = null;
    var MS_DELAY = 500;
   
    window.onload = function()
    {
        var storyJSON = "<%= oStory.GetStoryJSON2() %>";
        eval("story = " + storyJSON + ";");

       if(bProceed)
       {
            InitializeTimer(callback,1,true,MS_DELAY);
            var replaySpeed = TheUte().getInputBox(MS_DELAY,"txtUpdateSpeed",null,blurHandle,"enter update speed here");
            this.dvControls.appendChild(replaySpeed);
       }
       else
       {
            alert("story not visible by you");
       }


    }
    
    function blurHandle()
    {
        var oUpdateSpeed = document.getElementById("txtUpdateSpeed");
        try
        {
            if(oUpdateSpeed != null)
                SetDelay( oUpdateSpeed.value * 1 );
       }
       catch(e)
       {
        //do nothing
       }
    
    }
    
    
    var i = 0;
    var loops = 0;
    var maxLoops = 2;
    
    function callback(s)
    {
        if(loops>maxLoops)
        {
            StopTheClock();
           // alert("time up");
           //does not stop!
            return;
        }
        
        if(i >= story.PageElements.length)
        {
             i = 0; //reset
             loops++;
        }
        
        dvMain.innerHTML = TheUte().decode64( story.PageElements[i].Value );
        dvCounter.innerHTML = i + " / " + story.PageElements.length;
        i++;    
        
        
    }
    
    
    </script>
    
    <form id="form1" runat="server">
    <div id='dvControls'></div>
    <div id='dvCounter' style="font-family:Verdana;font-size:xx-small;"></div>
    <div id='dvMain'></div>
    
    
    </div>
    </form>
</body>
</html>
