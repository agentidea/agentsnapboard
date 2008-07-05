// web service URL endpoint
//var url = "http://localhost:1585/webservices/daemon.asmx/ProcessMacro";
var url = "http://www.agentidea.com/webservices/daemon.asmx/ProcessMacro";

var bCallBusyLock = false; 
var gBufferDisplay = 24;
var gRefreshRate = 1000;
var gDelay = 5;
var gDisableAJAXcalls = 0; 

var maxHeartbeatsBeforeRefresh = 2400;  //to prevent browser leaks ( possible )


/*

    AgentIdea core AgentIdea Core Macro Processor for for Modern Browsers
    [TESTED 
            pc:
            ie 6,7 firefox 2.0.0.5/6 Netscape Opera Safari
            
            mac:
            firefox 2.0.0.5
            (Safari buggy still - not recommended)
            */



var txSessionID = "not_set_yet";

var _logger = null;

function TheLogger()
{
    if(_logger==null)
    {
        _logger = YAHOO.widget.Logger;
        _logger.log("initializing logger Singleton Function","warn");
    }
    
    return _logger;
}


////////////////////
// GLOBAL VARIABLES
// for WEB SERVICE endpoint

var xmlHttpLocal = null;
var SYNC = false;


function xmlHttp_callback() 
{ 
    try
    {
        //readyState of 4 or 'complete' represents 
        //that data has been returned 
        if ( xmlHttpLocal.readyState == 4 || 
            xmlHttpLocal.readyState == 'complete' )
        {
            var response = xmlHttpLocal.responseText; 
            if (response.length > 0)
            {
                //alert(response);
                processResponse(response);
                
            } 
        }
    }
    catch(e)
    {
        //smell fix this?
       // alert(" callback error: " + e.description + " \r\n\r\n RESP: " + response);
       // var json = getJSONoffXML(response);
       // alert("JSON WAS " + json);
    }
    
    bCallBusyLock = false;
}

function log(s)
{
    var divLog =  TheUte().findElement("divLog","div");
    if(divLog != null)
        divLog.innerHTML = s + "<br />" + divLog.innerHTML;
}

function trace(s)
{
 var divLog =  TheUte().findElement("divLog","div");
    if(divLog != null)
        divLog.innerHTML += s;    

}

function processResponse(res)
{
     //get json response
     //$to do: understand what the error was.
     var json = getJSONoffXML(res);
     
     if(json == null)
     {
        alert("unable to process response " + res);
        //location.href = location.href;
        gDisableAJAXcalls = 1;
        return;
     }

     //eval(s);
     
    //log("response :: " + json);
     var resMacros = null;
     try
     {
        resMacros = eval('(' + json + ')');
     }
     catch(e)
     {
       //$to do: understand what the error was.
       // alert("truncated process response - eval error :: " + e.description );
       alert("JSON response was \r\n " + json);
       
       //Refresh Story
       //alert("It's possible your session has ended. \r\n\r\n Story page is been auto-refreshed.  \r\n\r\n You may need to relogin to get write and read access rights.");
       //location.href = location.href;
       alert(e.description);
        gDisableAJAXcalls = 1;
     }
     
     
     //alert("about to process " + resMacros.macros.length + " macros");
     
     var i = 0;
     for ( i = 0 ;i<resMacros.macros.length;i++)
        executeLocalCommand( resMacros.macros[i] );
     
}

function executeLocalCommand(macro)
{
    var s = "cmd" + macro.name + "(macro);";
    eval(s);
}




function processRequest(macro)
{

    if(bCallBusyLock == true || gDisableAJAXcalls == 1)
    {
         //$to do: why are some browsers re-entrant and others not????
        return;
    }
     
     
    bCallBusyLock = true;
    
    xmlHttpLocal = getXMLHTTP();
    var macroTxt = serializeMacroForRequest(macro);
   
    if (xmlHttpLocal!=null)
    {
        try
        {
           // alert("posting to " + url);
            
            //xmlHttpLocal.onreadystatechange=state_Change;
            xmlHttpLocal.open("POST",url,SYNC);
            xmlHttpLocal.setRequestHeader("Accept","text/xml");
            xmlHttpLocal.setRequestHeader("Content-Type","application/x-www-form-urlencoded");
           //xmlHttpLocal.setRequestHeader("SOAPAction","''");
           // xmlHttpLocal.setRequestHeader("Cache-Control","no-cache");

            
            //alert("before encoding  \r\n\r\n" + macroTxt);
            //log("ajax request: \r\n\r\n" + macroTxt);
            macroTxt = TheUte().URLEncode( macroTxt );
            
            
            xmlHttpLocal.send("serializedMacro=" + macroTxt);
        }
        catch(exp)
        {
            alert("XMLHTTP COM error " + exp.description);
            gDisableAJAXcalls = 1;
        }
     
    }
    else
    {
        alert("Your browser seems to be unable to support XMLHTTP");
    }
    
    
}


function getJSONoffXML(text)
{
        
    //seems like a string truncate could work equall well here
    //substring off<?xml ...?><string></string>
    //slippery, what is xml def changes!!!! unlikely?
    var jsonMid = text.substring(93,(text.length-9));
    return jsonMid;

}

function getXMLHTTP()
{

   var xmlHttp2 = null;

   if (window.ActiveXObject)
   {
            //all windows ActiveX broser shall use MSXML ( IE7 and later )
            //alert("MSXL capable");
            // ...otherwise, use the ActiveX control for IE5.x and IE6
            xmlHttp2 = GetMSXmlHttp();
            xmlHttp2.onreadystatechange = xmlHttp_callback;
  } 
  else
  if (window.XMLHttpRequest)
  {
    
        //alert(" native XMLHTTP ... ");
        // If , Mozilla, Safari, etc: Use native object
        xmlHttp2 = new XMLHttpRequest();
        xmlHttp2.onload = xmlHttp_callback;
        xmlHttp2.onerror = xmlHttp_callback;

        
    }
    else
    {
        alert("FATAL ERROR :: unrecognized xmlHTTP unsupported by this browser");
    }

    return xmlHttp2;
}

function CreateXmlHttp(clsid)
{
    var xmlHttp = null;
    try
    {
        xmlHttp = new ActiveXObject(clsid);
        lastclsid = clsid;
        return xmlHttp;
    }
    catch(e) 
    {}

}
function GetMSXmlHttp()
{
//"Msxml2.DOMDocument.3.0","Msxml2.DOMDocument.6.0",
    var xmlHttp = null;
    var clsids = ["Msxml2.XMLHTTP.6.0",
    "Msxml2.XMLHTTP.4.0",
    "Msxml2.XMLHTTP.3.0",
    "MSXML2.XMLHTTP" ];
    
    for(var i=0; i<clsids.length && xmlHttp == null; i++)
    {
        xmlHttp = CreateXmlHttp(clsids[i]);
    }
    
    if(xmlHttp == null) alert("unable to create MSXML.XMLHTTP");
    
    return xmlHttp;
}






function getParameterVal(key,macro)
{
    var res = null;
    
    for( var i =0;i<macro.paramCount;i++)
    {
        if(macro.parameters[i].name == key)
        {
            res = macro.parameters[i].value;
            break;
        }
    }
    
    return res;
}

function assembleMacro(name)
{
    log("about to assemble " + name);
    var m = newMacro(name);
  
    //check through form elements to see if there are indeed macro elements bound to the DOM
    var unFiltered = document.getElementsByTagName('*');
    var s = "";
    var tagName = null;
    var tag = null;
    var parameterCount = 0;
    
    for(var i=0;i<unFiltered.length;i++)
    {
        tag = unFiltered[i];
        tagName = tag.tagName;

        if(tagName == "INPUT" || tagName == "TEXTAREA")
        {
            if(tag.id != null && tag.id.indexOf("m_") != -1)
            {
                addParam(m,tag.id.split('_')[1],tag.value);
                parameterCount++;
                
                //alert("INPUT Parameter name :: " + p.name + " :: val " + p.value );
            }
        }
    }
    m.paramCount = parameterCount;
    return m;
}

function serializeMacroForRequest(passedMacro)
{

    /*
    <?xml version="1.0" encoding="utf-8"?>
<Macro xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
parameterCount="1" xmlns="http://tempuri.org/">
  <Parameters>
    <Parameter>
      <Val>hello </Val>
      <Name>msg</Name>
    </Parameter>
  </Parameters>
  <Name>DisplayAlert</Name>
</Macro>

    */
    var s = "";
    
    s += "<Macro ";
    s += " xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' ";
    s += " xmlns:xsd='http://www.w3.org/2001/XMLSchema' parameterCount='"+passedMacro.parameters.length+"'>";
    
    s += "<Name>";
    s += passedMacro.name
    s += "</Name>";
    s += "<UserCurrentTxID>";
    s += passedMacro.UserCurrentTxID
    s += "</UserCurrentTxID>";    
    
    
    s += "<Parameters>";
    for(var i = 0;i<passedMacro.parameters.length;i++)
    {
        s+= "<Parameter>";
        s+= "<Name>";
        s += passedMacro.parameters[i].name;
        s+= "</Name>";
        s+= "<Val>";
        s += passedMacro.parameters[i].value;
        s+= "</Val>";
        s+= "</Parameter>";
    
    }
    s += "</Parameters>";
    s += "</Macro>";

    return s;
}



function newMacro(name)
{
    var m = new macro();
    m.name = name;
    if( typeof gUserCurrentTxID != "undefined")  //HORRIBLE dependency from realtime updating idea
    {
        if(gUserCurrentTxID != null)
        {
        
            m.UserCurrentTxID = gUserCurrentTxID;
        }
     }
    return m;
}

function addParam( m, name, val )
{
    var param = new parameter();
    param.name = name;
    param.value = val;
    
    m.parameters.push(param);
}

function macro()
{
   var _name = null;
   this.name = _name;
   var _paramCount = 0;
   this.paramCount = _paramCount;
   var _parameters = new Array();
   this.parameters = _parameters;
   var _UserCurrentTxID = "not_set_yet";
   this.UserCurrentTxID = _UserCurrentTxID;

}

function parameter()
{
   var _name = null;
   this.name = _name;
   var _val = null;
   this.value = _val;
}



// some core functions for logging etc

var statusBarRef = null;
var historyDivRef = null;
var xyRef = null;

/*
function status(msg)
{
    if(statusBarRef == null)
        statusBarRef = TheUte().findElement("divStatusBar","DIV");
        
        statusBarRef.innerHTML = msg;
}
*/
function status(msg)
{

    history(msg);
}


function logMsg(msg)
{
    var logDiv = document.getElementById("LogDiv");
    if(logDiv != null)
    {
        logDiv.style.display="block";
        logDiv.innerHTML = "<p>" + msg + "</p>" + logDiv.innerHTML;
    
    }

}

function history(msg)
{
    if(historyDivRef == null)
        historyDivRef = TheUte().findElement("historyDiv","DIV");
        
        historyDivRef.innerHTML = msg + "<br>" + historyDivRef.innerHTML;
}

function xy(coord)
{
   if(xyRef == null)
        xyRef = TheUte().findElement("divXYlabel","DIV");
    
    xyRef.innerHTML = "x:" + coord.x + " y:" + coord.y; 
}