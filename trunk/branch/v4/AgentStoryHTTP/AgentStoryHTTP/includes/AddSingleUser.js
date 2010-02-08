function UserForm()
{
    var _container = null;
    this.container = _container;
    
    var _operatorID = 0;
    this.operatorID = _operatorID;
    
    this.init = UserFormInit;
    this.save_event = UserForm_save;

}


function UserForm_save()
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
   // var txtInviteCode = TheUte().findElement("txtInviteCode","input");
    
    addParam(m,"notificationSchedule", 1 );
    addParam(m,"firstName",TheUte().encode64(txtFirstName.value));
    addParam(m,"lastName",TheUte().encode64(txtLastName.value));
    addParam(m,"username",TheUte().encode64(txtUsername.value));
    addParam(m,"password",TheUte().encode64(txtPassword.value));
    addParam(m,"email",TheUte().encode64(txtEmail.value));
    addParam(m,"roles",TheUte().encode64(txtRoles.value));
    //addParam(m,"inviteCode",TheUte().encode64(txtInviteCode.value));
    addParam(m, "tags", TheUte().encode64(txtTags.value));
    addParam(m, "ProfileID", "-1");
    addParam(m,"operatorID",oUserForm.operatorID);
    addParam(m,"action","save_new");
    processRequest( m );
}


function UserFormInit(operatorID)
{
     this.operatorID = operatorID;
     
     var values = new Array();
     
    values[0] = document.createTextNode("username");
    values[1] = TheUte().getInputBox("","txtUsername",null,null,"clsInput");
    values[2] = document.createTextNode("password");
    values[3] = TheUte().getInputBox("","txtPassword",null,null,"clsInput");
    values[4] = document.createTextNode("first name");
    values[5] = TheUte().getInputBox("","txtFirstName",null,null,"clsInput");
    values[6] = document.createTextNode("last name");
    values[7] = TheUte().getInputBox("","txtLastName",null,null,"clsInput");
    values[8] = document.createTextNode("email");
    values[9] = TheUte().getInputBox("","txtEmail",null,null,"clsInput");
    values[10] = document.createTextNode("roles");
    values[11] = TheUte().getInputBox("editor","txtRoles",null,null,"clsReadOnlyInputField");
    values[11].readOnly = true;
   // values[12] = document.createTextNode("Invite Code ( enter something unique like fav color etc)");
   // values[13] = TheUte().getInputBox("","txtInviteCode",null,null,"clsInput");
    
    values.push(  document.createTextNode("Tags (optional) :") );
    values.push(  TheUte().getTextArea("","txtTags",null,null,"clsTagsTextBox") );
      
    // values.push( document.createTextNode("") ); //sponsor user id
    // values.push( document.createTextNode( this.operatorID ));
    
    
    values.push(TheUte().getButton("cmdSave","save","save this user info",this.save_event,"clsButtonAction"));
    
     
    var oGrid2 = newGrid2("UserFormMainGrid",values.length,2,values);
    oGrid2.init( oGrid2 );

    this.container = oGrid2.gridTable;

}

