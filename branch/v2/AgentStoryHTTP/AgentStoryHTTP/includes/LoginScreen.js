/*

    AgentIdea - Story login page
    Copyright AgentIdea 2008
    
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


function LoginScreen(aAttachPoint,aKlubName,loginCallBack)
{
   
    var _attachPoint = aAttachPoint;
    var _klubName = aKlubName;
    var _loginCallBack = loginCallBack;

    this.init = function ()
    {
    
        var outerGridValues = new Array();
    
        //hoz toolbar
        var toolbarVals = new Array();
       
        
        //context menu or spacer for now
        var spacer = document.createElement("DIV");
        //spacer.style.height = "500px";
        spacer.style.width = "300px";
         toolbarVals.push( spacer );
        
        
        //login or user information
        var _lw = new LoginWidget(_loginCallBack);
        //toolbarVals.push( _lw.init( "login" ) );
       
        
         //var oToolBar = newGrid2("toolBar", 1 ,2,toolbarVals);
        //oToolBar.init( oToolBar );

        //outerGridValues.push( oToolBar.gridTable );
        outerGridValues.push( _lw.init( "login" ) );
        
        var oOuterGrid = newGrid2("outerGrid", 1 ,2,outerGridValues);
        oOuterGrid.init( oOuterGrid );
        
        _attachPoint.appendChild( oOuterGrid.gridTable);    
        
        
        
    }
}