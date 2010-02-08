

    function fauxIRC()
    {
        var _container = null;
        this.container = _container;
        
        var _txtGo = null;
        this.txtGo = _txtGo;
        
        this.init = initFauxIRC;
        
        this.submit_msg = submit_msg;
        this.refresh_msg = refresh_msg;
        this.tryFocus = tryFocus;
    
    }
    
    
function initFauxIRC()
{
    
    var values = new Array();
    this.txtGo = TheUte().getTextArea("","txtIRCinput",null,null,"clsChatTextbox");
    values[0] = this.txtGo;
    values[1] = TheUte().getButton("cmdGo","send message to system log","faux chat with the system log",this.submit_msg,"clsButtonAction");
    values[2] = TheUte().getButton("cmdRefresh","refresh system log","refresh the system log",this.refresh_msg,"clsButtonAction");
   
    var oGrid2 = newGrid2("storyEditorMainGrid",1,3,values,1);
    oGrid2.init( oGrid2 );

    this.container = oGrid2.gridTable;
    
}


function submit_msg()
{
    if( oFauxIRC.txtGo.value == "")
    {
        oFauxIRC.tryFocus();
        return;
    }
    
    var msg64 = TheUte().encode64( oFauxIRC.txtGo.value );
    //PostLogMessage
    var PostLogMessage = newMacro("PostLogMessage");
    addParam( PostLogMessage,"msg",msg64);
    //alert( serializeMacroForRequest( PostLogMessage) );
    processRequest( PostLogMessage ); 
    oFauxIRC.txtGo.value = "";
    oFauxIRC.tryFocus();
   
}

function tryFocus()
{
 try
    {
        oFauxIRC.txtGo.focus();
    }
    catch(e)
    {
        //do nuttin for those who can't focus :)
    }
}
function refresh_msg()
{
    top.processor.location.href = top.processor.location.href;
}
