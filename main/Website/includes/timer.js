/*

    AgentIdea - Story timer
    Copyright AgentIdea 2007
    
    please note this is private and a copyrighted original work by Grant Steinfeld.
    
    
    
1. Registration Number:    TXu-959-906  
Title:    AgentIdea application studio for IE : version 1.0. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    AgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Author on © Application:    text of computer program: Grant Steinfeld , 1967- & Oren Kredo , 1966-. 
Special Codes:   1/C 

--------------------------------------------------------------------------------
2. Registration Number:    TXu-960-165  
Title:    AgentIdea application studio for Java : version 1.0. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    AgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Author on © Application:    program text: Grant Steinfeld , 1967-, & Oren Kredo , 1966-. 
Special Codes:   1/C 

--------------------------------------------------------------------------------
3. Registration Number:    TXu-961-904  
Title:    AgentIdea application studio for IIS : version 1.0 / authors, Grant Steinfeld, Oren Kredo. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    cAgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Special Codes:   1/C 

to verify search for Grant Steinfeld or Oren Kredo
http://www.copyright.gov/records/cohm.html

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
  
  function StopTheClock()
  { 
    if(timerRunning)
     clearTimeout(timerID);
     
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