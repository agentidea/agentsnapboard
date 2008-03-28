

function InvitationManager()
{
    this.init = InvitationManagerInit;
    
    this.getFromPanel = getFromPanel;
    this.getToPanel = getToPanel;
    this.getInviteTemplate =getInviteTemplate;
    
    var _container = null;
    this.container = _container;
    
    var _operatorID = 0;
    this.operatorID = _operatorID;
    var _operatorName = null;
    this.operatorName = _operatorName;
    
    var _greetingTxtBox = null;
    this.greetingTxtBox = _greetingTxtBox;
    
    var _inviteSubjectArea = null;
    this.inviteSubjectArea = _inviteSubjectArea;
    
   var _inviteBodyTextArea = null;
    this.inviteBodyTextArea = _inviteBodyTextArea;
    
    var _inviteEvent = null;
    this.inviteEvent = _inviteEvent;
    
    this.getInviteEvent = getInviteEvent;
    
    
    this.getInviteLine = getInviteLine;
    this.getSubjectLine = getSubjectLine;
    this.getInviteBody = getInviteBody;
    this.getToGroups = getToGroups;
    this.getToUsers = getToUsers;
    
    this.getActionBar = getActionBar;
    this.getShouldGenEmails = getShouldGenEmails;
    
    
    this.Create_Inviation = Create_Inviation;
    
    this.selChanged = selChanged;
    
}

function InvitationManagerInit( operatorID,operatorName,userFromIDlist,userFromNameList,groupToIDlist,groupToNameList,userToIDlist,userToNameList)
{
    
     this.operatorID = operatorID;
     this.operatorName = operatorName;
     
    //load usr/group meta into sel boxes.
   var selItemsUsersFrom = buildSelItems(operatorID + "|" + userFromIDlist,operatorName + "|" + userFromNameList,false,null);
   var selItemsGroupsTo = buildSelItems("-1|" + groupToIDlist,"bm9uZQ==|" + groupToNameList,true,null);
   var selItemsUsersTo = buildSelItems("-1|" + userToIDlist,"none|" + userToNameList,false,null);
   
   
    var selWidgetUsersFrom   = new selWidget("selUsersFrom");
    var selWidgetUsersTo     = new selWidget("selUsersTo");
    var selWidgetGroupsTo    = new selWidget("selGroupsTo");
    
    selWidgetUsersFrom.init(selItemsUsersFrom,this.selChanged,1,"clsSingleSelBox");
    selWidgetUsersTo.init(selItemsUsersTo,this.selChanged,8,"clsMultiSelBox"); 
    selWidgetGroupsTo.init(selItemsGroupsTo,this.selChanged,8,"clsMultiSelBox"); 

    var values = new Array();
    values[0] = document.createTextNode("Invite users to join this club.");
    values[1] = this.getFromPanel(selWidgetUsersFrom);
    values[2] = this.getToPanel(selWidgetUsersTo,selWidgetGroupsTo);
    values[3] = this.getInviteEvent();
    values[4] = this.getInviteTemplate();
    values[5] = this.getShouldGenEmails();
    values[6] = this.getActionBar();


    var oGrid2 = newGrid2("InvitationMxMainGrid",7,1,values);
    oGrid2.init( oGrid2 );

    this.container = oGrid2.gridTable;
   
   
}

function getFromPanel(selWidgetUsersFrom)
{
   var values = new Array();
   values[0] = document.createTextNode("From: ");
   values[1] = selWidgetUsersFrom.selWidget;
   
    var oGrid2 = newGrid2("FromPanel",1,2,values);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;

}
function getToPanel(selWidgetUsersTo,selWidgetGroupsTo)
{
   var values = new Array();
   values[0] = document.createTextNode("To:");
   values[1] = this.getToUsers(selWidgetUsersTo);
   values[2] = this.getToGroups(selWidgetGroupsTo);
   
    var oGrid2 = newGrid2("ToPanel",1,3,values);
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



function getInviteTemplate()
{
   var values = new Array();
   values[0] = this.getSubjectLine();
   values[1] = this.getInviteLine();
   values[2] = this.getInviteBody();
   
    var oGrid2 = newGrid2("ToPanel",4,1,values,1);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;
}
function getActionBar()
{
   var values = new Array();
   values[0] = TheUte().getButton("cmdCreateInvites","create invites " + this.operatorName,"create invitations",this.Create_Inviation,"clsButtonAction");
   var oGrid2 = newGrid2("InviteBodyPanel",1,2,values);
   oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;
}
function getShouldGenEmails()
{
   var values = new Array();
   values[0] = document.createTextNode("create invite emails");
   values[1] = TheUte().getCheckbox("chkCreateInviteEmails","create emails",false);
   
   var oGrid2 = newGrid2("InviteBodyPanel",1,2,values);
   oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;
}
function getInviteBody()
{
    this.inviteBodyTextArea = TheUte().getTextArea("","txtInviteBody",null,null,"clsTextBox");
   
   var values = new Array();
   values[0] = document.createTextNode("Invite body: ");
   values[1] = this.inviteBodyTextArea;
   
    var oGrid2 = newGrid2("InviteBodyPanel",1,2,values);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;
}

function getSubjectLine()
{
   var values = new Array();
   this.inviteSubjectArea = TheUte().getInputBox("","txtInviteSubject",null,null,"clsSubjectLine");
   values[0] = document.createTextNode("Invite Title: ");
   values[1] = this.inviteSubjectArea;
   
    var oGrid2 = newGrid2("ToPanel",1,2,values,0);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;
}
function getInviteEvent()
{
   var values = new Array();
   this.inviteEvent = TheUte().getInputBox("","txtInviteEvent",null,null,"clsSubjectLine");
   values[0] = document.createTextNode("Invite Event: ");
   values[1] = this.inviteEvent;
   
    var oGrid2 = newGrid2("ToPanel",1,2,values,0);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;
}

function getInviteLine()
{
   var values = new Array();
   
   this.greetingTxtBox = TheUte().getInputBox("Dear","txtGreeting",null,null,"clsSubjectLine");
   
   values[0] = document.createTextNode("Greeting: ");
   values[1] = this.greetingTxtBox;
   values[2] = document.createTextNode("<< user >>");
   
    var oGrid2 = newGrid2("ToPanel",1,3,values);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;
}

function selChanged()
{
  //  alert(this.id);
}

function Create_Inviation()
{
  //
   var greeting        = oInvitationManager.greetingTxtBox.value;
   var subject         = oInvitationManager.inviteSubjectArea.value;
   var bodyContent     = oInvitationManager.inviteBodyTextArea.value;
   var inviteEventName = oInvitationManager.inviteEvent.value;
   
   //FROM
   var userSelFrom = TheUte().findElement("selUsersFrom","select");
   var userSelTo = TheUte().findElement("selUsersTo","select");
   var groupsSelTo = TheUte().findElement("selGroupsTo","select");
   
   var UserIDsFrom = "";
   var numSelItems = 0;
    
    for(var i = 0;i < userSelFrom.length;i++)
    {
        if(userSelFrom.options[i].selected == true)
        {
            UserIDsFrom += userSelFrom.options[i].value;
            numSelItems++;
        }
     }
     
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
    m.name="CreateInvitation";
    
   addParam(m,"operatorID",oInvitationManager.operatorID );
   addParam(m,"fromID",UserIDsFrom );
   addParam(m,"toUserIDs", UserIDsTo);
   addParam(m,"toGroupIDs",GroupIDsTo );

   addParam(m,"greeting",TheUte().encode64( greeting ));
   addParam(m,"subject",TheUte().encode64( subject ));
   addParam(m,"bodyContent",TheUte().encode64( bodyContent ));
   addParam(m,"inviteEventName",TheUte().encode64( inviteEventName ));
   

   var chkCreateInviteEmails = TheUte().findElement("chkCreateInviteEmails","INPUT");
   
   if(chkCreateInviteEmails.checked)
   {
        addParam(m,"CreateInviteEmails","true");
   }
   else
   {
        addParam(m,"CreateInviteEmails","false");
   }
   //alert( serializeMacroForRequest( m ) );
   processRequest(m);
 }