/*

    AgentIdea - Story timer
 */
 

var secs = 30;
var timerID = null;
var timerRunning = false;
var delay = 1000;
var callbackFunction = null;
var repeat = false;
var intialSecs = 0;

function InitializeTimer(callback,aSecs,aRepeat,aDelay)
{
    callbackFunction = callback;
    repeat = aRepeat;
    secs = aSecs;
    StopTheClock();
    StartTheTimer();
    intialSecs = aSecs;
    delay = aDelay;
}
  
  
  function SetDelay(t)
  {
    delay = t;
  
  }
  
  function StopTheClock()
  { 
    if(timerRunning)
    {
         try
         {
            clearTimeout(timerID);
            //alert("cleared timer " + timerID);
         }
         catch(errrr)
         {
            alert("stop clock error" + errrr.description);
         }
     }
     
    timerRunning = false;
  }
  
  function StartTheTimer()
  { 
    if (secs==0)
    { 
         callbackFunction("END : " + secs);
         
         //Start again
         if(repeat)
         {
            InitializeTimer(callbackFunction,intialSecs,repeat,delay);
         }
         else
         {
            StopTheClock();
         }
         
         
     } 
     else
     { 
     
        secs = secs - 1;
        timerRunning = true;
        timerID = self.setTimeout("StartTheTimer()", delay);
         
        //callbackFunction("BEAT : " + secs);
      } 
      
     
      
 }