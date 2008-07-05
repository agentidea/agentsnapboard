
function ProfileEditor()
{
    var _container = null;
    this.container = _container;
 
    var _operatorID = 0;
    this.operatorID = _operatorID;
 
    var _profileID = 0;
    this.profileID = _profileID;
    
    this.init = ProfileEditorInit;
    this.terminateUser_event = terminateUser_event;
    this.saveUser_event = saveUser_event;

}

function ProfileEditorInit(userData,operator,operatorName)
{
         
         this.operatorID = operator;
         this.profileID = userData.id;
         
         var values = new Array();
        
        values[0] = document.createTextNode("Username: ");
        values[1] = TheUte().getInputBox(userData.username,"txtUsername",null,null,"clsSubjectLine");
        
        values[4] = document.createTextNode("Story Alerts ");
        
        var notOptIDs = "0|1|2|3";
        var notOptNames = "NEVER|IMMEDIATELY|DAILY DIGEST|WEEKLY DIGEST";
        var notOptTitles = "No email notifications will be sent.|As Story changes are made, email me immediately.|Send out a daily email report of Story changes.|Send out a weekly email report of Story changes.";
        
        var selItemsNot = buildSelItems(notOptIDs,notOptNames,false,notOptTitles);
        var selWidgetNot = new selWidget("selWidgetNot");
        selWidgetNot.init(selItemsNot,null,1,"",userData.notificationFrequency *1);
        
        
        values[5] = selWidgetNot.selWidget;
        values[6] = document.createTextNode("First Name:");
        values[7] = TheUte().getInputBox(TheUte().decode64(userData.firstName),"txtFirstName",null,null,"clsSubjectLine");
        values[8] = document.createTextNode("Last Name:");
        values[9] = TheUte().getInputBox(TheUte().decode64(userData.lastName),"txtLastName",null,null,"clsSubjectLine");
        
        values[10] = document.createTextNode("Email:");
        values[11] = TheUte().getInputBox(TheUte().decode64(userData.email),"txtEmail",null,null,"clsSubjectLine");
        values[12] = document.createTextNode("Password:");
        values[13] = TheUte().getInputBox(TheUte().decode64(userData.password),"txtPassword",null,null,"clsSubjectLine");

        values[14] = document.createTextNode("Tags:");
        values[15] = TheUte().getTextArea(TheUte().decode64(userData.tags),"txtTags",null,null,"clsTagBox");
        
        values[16] = document.createTextNode("Groups:");
        
        
        
        var grps = TheUte().decode64(userData.groupAsPipe);
        values[17] = TheUte().getInputBox(grps,"txtGroups",null,null,"clsReadOnlyInputField");
        values[17].readOnly = true;
        values[18] = document.createTextNode("ID:");
        values[19] = TheUte().getInputBox(userData.id,"txtID",null,null,"clsReadOnlyInputField");
        values[19].readOnly = true;
        values[20] = document.createTextNode("Sponsor:");
        values[21] = TheUte().getInputBox(TheUte().decode64(userData.sponsorFullName),"txtSponsorFullName",null,null,"clsReadOnlyInputField");
        values[21].readOnly = true;
        values[22] = document.createTextNode("Roles:");
        values[23] = TheUte().getInputBox(userData.roles,"txtRoles",null,null,"clsReadOnlyInputField");
        values[23].readOnly = true;
        values[24] = document.createTextNode("Invite Code:");
        values[25] = TheUte().getInputBox(TheUte().decode64(userData.origInviteCode),"txtOrigInviteCode",null,null,"clsReadOnlyInputField");
        values[25].readOnly = true;       
        values[26] = document.createTextNode("Date Added:");
        values[27] = TheUte().getInputBox(TheUte().decode64(userData.dateAdded),"txtDateAdded",null,null,"clsReadOnlyInputField");
        values[27].readOnly = true;         
        values[28] = TheUte().getButton("cmdSave","save","save profile to database",this.saveUser_event,"clsButtonAction");
        values[29] = TheUte().getButton("cmdCancelMembership","cancel membership","terminate this account.",this.terminateUser_event,"clsButtonCancel");

        var oGrid2 = newGrid2("ProfileEditorMainGrid",18,2,values);
        oGrid2.init( oGrid2 );

        this.container = oGrid2.gridTable;
        

}

function saveUser_event()
{
    
    var m = new macro();
    m.name = "SaveUser";
    
    var txtFirstName = TheUte().findElement("txtFirstName","input");
    var txtLastName = TheUte().findElement("txtLastName","input");
    var txtUsername = TheUte().findElement("txtUsername","input");
    var txtPassword = TheUte().findElement("txtPassword","input");
    var txtEmail = TheUte().findElement("txtEmail","input");
    var txtRoles = TheUte().findElement("txtRoles","input");
    var txtTags = TheUte().findElement("txtTags","textarea");
    var txtInviteCode = TheUte().findElement("txtOrigInviteCode","input");
    
    var selWidgetNot = TheUte().findElement("selWidgetNot","select");
    
    //var nSelIndexValue = selWidgetNot.selectedIndex;
    var sSelValue = selWidgetNot.value;
    
    addParam(m,"notificationSchedule", sSelValue );
    addParam(m,"firstName",TheUte().encode64(txtFirstName.value));
    addParam(m,"lastName",TheUte().encode64(txtLastName.value));
    addParam(m,"username",TheUte().encode64(txtUsername.value));
    addParam(m,"password",TheUte().encode64(txtPassword.value));
    addParam(m,"email",TheUte().encode64(txtEmail.value));
    addParam(m,"roles",TheUte().encode64(txtRoles.value));
    addParam(m,"inviteCode",TheUte().encode64(txtInviteCode.value));
    addParam(m,"tags",TheUte().encode64(txtTags.value));
    addParam(m,"operatorID",oProfileEditor.operatorID);
    addParam(m,"ProfileID",oProfileEditor.profileID);
    addParam(m,"action","save_existing");
    
    processRequest( m );

}


function terminateUser_event()
{

    var res = confirm("Are you sure you want to terminate this account?");
    if(res)
    {

        var m = new macro();
        m.name = "TerminateUser";
        addParam(m,"ProfileID",oProfileEditor.profileID);
        addParam(m,"UserID",oProfileEditor.operatorID);
        processRequest( m );
    }
    else
    {
        alert("request cancelled, thanks!");
    }
}
