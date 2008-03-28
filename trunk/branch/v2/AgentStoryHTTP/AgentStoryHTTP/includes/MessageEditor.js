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
    
    this.init = MessageEditorInit;
    this.send_event = MessageEditor_send;
    this.saveDraft_event = MessageEditor_saveDraft;

}

function MessageEditor_send()
{
    processRequest( prepareMessage("SaveEmailAndSend") );
}

function MessageEditor_saveDraft()
{
  processRequest( prepareMessage("SaveEmailDraft") );
}

function prepareMessage(macroName)
{
    var m = new macro();
    m.name = macroName;
    
   var subject = TheUte().findElement("txtSubject","input");
   var emailBody = TheUte().findElement("txtBody","textarea");
      
   addParam(m,"operatorID",oMessageEditor.operatorID );
   addParam(m,"toID",oMessageEditor.toID );
   addParam(m,"subject",TheUte().encode64( subject.value ));
   addParam(m,"body",TheUte().encode64( emailBody.value ));
   addParam(m,"currentMessageGUID", oMessageEditor.currentMessageGUID);
   
   var chkIncludeEmail = TheUte().findElement("chkIncludeEmail","INPUT");
   
   if(chkIncludeEmail.checked)
   {
        addParam(m,"useAnon","true");
   }
   else
   {
        addParam(m,"useAnon","false");
   }
   
   return m;
}


function MessageEditorInit(emailData, operatorID,operatorName,toID,toName)
{

    this.operatorID = operatorID;
    var values = new Array();
    
    if( emailData.guid == null && toID != -1)
    {
        //new to email
        this.toID = toID;
        this.currentMessageGUID = "";
        values[0] = document.createTextNode("to: ");
        values[1] = document.createTextNode( toName );
        values[2] = document.createTextNode("from: ");
        values[3] = document.createTextNode(operatorName);
        values[4] = document.createTextNode("anon reply-to");
        values[5] = TheUte().getCheckbox("chkIncludeEmail","do not show your email address, rather use the anonymous relay agent.",false);
        values[6] = document.createTextNode("subject:");
        values[7] = TheUte().getInputBox("","txtSubject",null,null,"clsSubjectLine");
        values[8] = document.createTextNode("body:");
        values[9] = TheUte().getTextArea("","txtBody",null,null,"clsTextBox");
        
        values[12] = TheUte().getButton("cmdSend","send","send message",this.send_event,"clsButtonAction");
        values[13] = TheUte().getButton("cmdSaveDraft","save draft","save a draft copy message ( message will not be sent )",this.saveDraft_event,"clsButtonAction");
    
        
    }
    else
    {
        //existing email
        this.currentMessageGUID = emailData.guid;
        values[0] = document.createTextNode("to: ");
        values[1] = document.createTextNode( TheUte().decode64(emailData.toUsername) );
        values[2] = document.createTextNode("from: ");
        values[3] = document.createTextNode(TheUte().decode64(emailData.fromUsername));
        values[4] = document.createTextNode("anon reply-to");
        
        var bChecked = false;
        var nEmailAnon = emailData.anon*1;

        if(nEmailAnon == 1)
            bChecked = true;
            
        values[5] = TheUte().getCheckbox("chkIncludeEmail","Use Anon@Bukanator.com as 'reply to'",bChecked);
        values[6] = document.createTextNode("subject:");
        values[7] = TheUte().getInputBox(TheUte().decode64(emailData.subject),"txtSubject",null,null,"clsSubjectLine");
        values[8] = document.createTextNode("body:");
        
        
        
        if(emailData.state == 2) //sent
        {
            var divBody = document.createElement("div");
            divBody.className = "clsEmailReadOnlyPreview";
           
           /* var bodyText = document.createTextNode( TheUte().decode64(emailData.body));
            divBody.appendChild(bodyText);
            */
            divBody.innerHTML = "<pre>" + TheUte().decode64(emailData.body) + "</pre>";
            values[9] = divBody;
            
            //display no buttons
        }
        else
        {
            values[9] = TheUte().getTextArea(TheUte().decode64(emailData.body),"txtBody",null,null,"clsTextBox");
    
            values[12] = TheUte().getButton("cmdSend","send","send message",this.send_event,"clsButtonAction");
            values[13] = TheUte().getButton("cmdSaveDraft","re-save draft","re-save a draft copy message ( message will not be sent )",this.saveDraft_event,"clsButtonAction");
        }
    
    }
    

     
    var oGrid2 = newGrid2("UserFormMainGrid",10,2,values);
    oGrid2.init( oGrid2 );

    this.container = oGrid2.gridTable;

}

