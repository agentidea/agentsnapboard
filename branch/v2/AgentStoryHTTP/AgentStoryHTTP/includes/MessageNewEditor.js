function MessageEditor()
{
    var _container = null;
    this.container = _container;
    
    var _currentMessageGUID;
    this.currentMessageGUID = _currentMessageGUID;
    
    var _operatorID = 0;
    this.operatorID = _operatorID;
    
    var _toID = 0;
    this.toID = _toID;
    
    this.init = MessageNewEditorInit;
    this.send_event = MessageEditor_send;
    this.saveDraft_event = MessageEditor_saveDraft;
    this.getBodyPanel = getBodyPanel;
    this.getGreetingPanel = getGreetingPanel;
    this.getToPanel = getToPanel;
    this.getToGroups = getToGroups;
    this.getToUsers = getToUsers;

}

function getGreetingPanel()
{
    var values = new Array();
    values[0] = TheUte().getInputBox("","txtGreeting",null,null,"");
    values[1] = document.createTextNode(" {username} ");
    var oGrid2 = newGrid2("GreetingPanel",1,2,values);
    oGrid2.init( oGrid2 );

    return oGrid2.gridTable;
 }

function getBodyPanel()
{     
    var values = new Array();
    values[0] = this.getGreetingPanel();
    values[1] = TheUte().getTextArea("","txtBody",null,null,"clsTextBox");
    
    var oGrid2 = newGrid2("BodyPanel",2,1,values);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;

}        
        
function getToPanel(selWidgetUsersTo,selWidgetGroupsTo)
{
   var values = new Array();
   values[0] = this.getToUsers(selWidgetUsersTo);
   values[1] = this.getToGroups(selWidgetGroupsTo);
   
    var oGrid2 = newGrid2("ToPanel",1,2,values);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;

}


function getToUsers(selWidgetUsersTo)
{
    var values = new Array();
   values[0] = document.createTextNode("users");
   values[1] = selWidgetUsersTo.selWidget;
   
    var oGrid2 = newGrid2("ToPanel",2,1,values);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;

}
function getToGroups(selWidgetGroupsTo)
{
    var values = new Array();
   values[0] = document.createTextNode("groups");
   values[1] = selWidgetGroupsTo.selWidget;
   
    var oGrid2 = newGrid2("ToPanel",2,1,values);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;

}

function MessageEditor_send()
{
    processRequest( prepareMessage("CreateMailAndSend") );
}

function MessageEditor_saveDraft()
{
  processRequest( prepareMessage("CreateMail") );
}

function prepareMessage(macroName)
{
   var greeting        = TheUte().findElement("txtGreeting","input");
   var subject         = TheUte().findElement("txtSubject","input");
   var bodyContent     = TheUte().findElement("txtBody","textarea");
    
   var userSelTo        = TheUte().findElement("selUsersTo","select");
   var groupsSelTo      = TheUte().findElement("selGroupsTo","select");
   
   
   var GroupIDsTo = "";
   var numSelItemsToGroup = 0;
    
    for(var i = 0;i < groupsSelTo.length;i++)
    {
        if(groupsSelTo.options[i].selected == true)
        {
            GroupIDsTo += groupsSelTo.options[i].value + "|";
            numSelItemsToGroup++;
        }
     }     
     
   var UserIDsTo = "";
   var numSelItemsToUser = 0;
    
    for(var i = 0;i < userSelTo.length;i++)
    {
        if(userSelTo.options[i].selected == true)
        {
            UserIDsTo += userSelTo.options[i].value + "|";
            numSelItemsToUser++;
        }
     }
     

   var m = new macro();
   m.name=macroName;
    
   addParam(m,"action","saveDraftOnly");
   addParam(m,"operatorID",oMessageNewEditor.operatorID );
   addParam(m,"fromID",oMessageNewEditor.operatorID );
   addParam(m,"toUserIDs", UserIDsTo);
   addParam(m,"toGroupIDs",GroupIDsTo );
   
   var greetText = TheUte().encode64( greeting.value );
   
   addParam(m,"greeting",greetText);
   addParam(m,"subject",TheUte().encode64( subject.value ));
   addParam(m,"bodyContent",TheUte().encode64( bodyContent.value ));

   var chkAnonSender = TheUte().findElement("chkAnonSender","INPUT");
   
   if(chkAnonSender.checked)
   {
        addParam(m,"useAnonSender","true");
   }
   else
   {
        addParam(m,"useAnonSender","false");
   }
   
   
   var chkReadReciept = TheUte().findElement("chkReadReciept","INPUT");
   if(chkReadReciept.checked)
   {
        addParam(m,"requestReadReciept","true");
   }
   else
   {
        addParam(m,"requestReadReciept","false");
   }
var chkAllowHtml = TheUte().findElement("chkAllowHtml","INPUT");
   if(chkReadReciept.checked)
   {
        addParam(m,"AllowHtml","true");
   }
   else
   {
        addParam(m,"AllowHtml","false");
   }   
   
var chkHighPriority = TheUte().findElement("chkHighPriority","INPUT");
   if(chkReadReciept.checked)
   {
        addParam(m,"HighPriority","true");
   }
   else
   {
        addParam(m,"HighPriority","false");
   }   
   
  // alert( serializeMacroForRequest( m ) );
   
   return m;
 }


function MessageNewEditorInit(emailData, operatorID,operatorName,groupToIDlist,groupToNameList,userToIDlist,userToNameList)
{

    this.operatorID = operatorID;
    var values = new Array();

    var selItemsGroupsTo = buildSelItems("-1|" + groupToIDlist,"bm9uZQ==|" + groupToNameList,true,null);
    var selItemsUsersTo = buildSelItems("-1|" + userToIDlist,"none|" + userToNameList,false,null);
    var selWidgetUsersTo     = new selWidget("selUsersTo");
    var selWidgetGroupsTo    = new selWidget("selGroupsTo");
    selWidgetUsersTo.init(selItemsUsersTo,this.selChanged,8,"clsMultiSelBox"); 
    selWidgetGroupsTo.init(selItemsGroupsTo,this.selChanged,8,"clsMultiSelBox"); 

 
    if( emailData.guid == null)
    {
        //new to email
        this.currentMessageGUID = "";
        values.push( document.createTextNode("from: ") );
        values.push( document.createTextNode(operatorName) );
        values.push( document.createTextNode("to: ") );
        values.push( this.getToPanel(selWidgetUsersTo,selWidgetGroupsTo) );
        values.push( document.createTextNode("anon reply-to") );
        values.push( TheUte().getCheckbox("chkAnonSender","do not show your email address, rather use the anonymous relay agent.",false));
        values.push( document.createTextNode("request read receipt") );
        values.push( TheUte().getCheckbox("chkReadReciept","some email clients can send you a read reciept",false));
        values.push( document.createTextNode("allow html body") );
        values.push( TheUte().getCheckbox("chkAllowHtml","some email clients can display HTML body in emails, \r\n but that said, some people still set their email readers to display as text!",false));
        values.push( document.createTextNode("high priority") );
        values.push( TheUte().getCheckbox("chkHighPriority","send mail with Priority set to HIGH",false));
        
        values.push( document.createTextNode("subject:") );
        values.push( TheUte().getInputBox("","txtSubject",null,null,"clsSubjectLine") );        
        
        values.push( document.createTextNode("body:") );
        values.push( this.getBodyPanel() );
        
        values.push( TheUte().getButton("cmdSend","send","send message",this.send_event,"clsButtonAction"));
        values.push( TheUte().getButton("cmdSaveDraft","create and save draft","create new messages \r\n\r\n This button will save draft copy of the message(s) ( message(s) will not be sent )",this.saveDraft_event,"clsButtonAction"));
    
        
    }
    else
    {
        //existing email
       
    }
    

     
    var oGrid2 = newGrid2("UserFormMainGrid",12,2,values);
    oGrid2.init( oGrid2 );

    this.container = oGrid2.gridTable;

}

