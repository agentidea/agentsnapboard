function MessageManager()
{
    var _container = null;
    this.container = _container;
    
    var _operatorID = 0;
    this.operatorID = _operatorID;
    
    var _selectedItems = new uniqueList("selectedItems");
    this.SelectedItems = _selectedItems;
    
    this.init = MessageManagerInit;
    
    this.getMessageToolbar  =  getMessageToolbar;
    this.getMessagesDrafts  =  getMessagesDrafts;
    this.getMessagesInbox   =  getMessagesInbox; 
    this.getMessagesOutbox  =  getMessagesOutbox;
    this.getMessagesSent    =  getMessagesSent; 
    
    this.getMessages = getMessages; 
    
    this.message_new    = message_new;    
    this.message_delete = message_delete; 
    this.message_send   = message_send;   
    this.message_sendAll= message_sendAll;     
    
    this.chkAll_clicked = chkAll_clicked;      
    
    this.chkSingle_clicked = chkSingle_clicked;
    

}

function MessageManagerInit(myMessageMetaJSON, operatorID,operatorName)
{
    var values = new Array();
    values[0] = this.getMessageToolbar (); 
    values[1] = this.getMessagesDrafts (myMessageMetaJSON);
    values[2] = this.getMessagesInbox  (myMessageMetaJSON);
    values[3] = this.getMessagesOutbox (myMessageMetaJSON);
    values[4] = this.getMessagesSent   (myMessageMetaJSON);
    
    
    
    var oGrid2 = newGrid2("MessageManagerMainGrid",5,1,values);
    oGrid2.init( oGrid2 );

    this.container = oGrid2.gridTable;

}



function getMessageToolbar()
{

    var values = new Array();
    values[0] = TheUte().getButton("cmdNewEmail","New","New message",                       this.message_new        ,"clsButtonAction");
    values[1] = TheUte().getButton("cmdDelteEmail","Delete","Delete selected message(s)",   this.message_delete     ,"clsButtonAction");
    values[2] = TheUte().getButton("cmdSendEmail","Send","Send selected message(s)",        this.message_send       ,"clsButtonAction");
   // values[3] = TheUte().getButton("cmdSendAllEmail","Send All","Send all unsent messages", this.message_sendAll    ,"clsButtonAction");

    var oGrid2 = newGrid2("MessageToolbar",1,4,values);
    oGrid2.init( oGrid2 );
    return oGrid2.gridTable;

}

function prepSubHeaderDiv(txtNode)

{
    var divID = document.createElement("div");
    divID.className = "clsMessageBoxSubHeader";
    
    divID.appendChild(txtNode);
    return divID;
}

function getMessages(messages,who,grouping)
{
    var values = new Array();
    
    if( messages.length > 0 )
    {
        values[0] = prepSubHeaderDiv(document.createTextNode("id") );
        values[1] = prepSubHeaderDiv(document.createTextNode(who));
        values[2] = prepSubHeaderDiv(document.createTextNode("date"));
        values[3] = prepSubHeaderDiv(document.createTextNode("subject"));

        var i = 0;
        var counter = 6;
        var bunchOfIDs = "";
        var who = "";
        
        for(i=0;i<messages.length ;i++ )
        {
            bunchOfIDs +=  messages[i].id  + "|";
                
            values[counter] = document.createTextNode( messages[i].id );
            counter++;
            
            if(grouping == "inbox")
            {
                who = messages[i].reply_to;
            }
            else
            {
                who = messages[i].to;
            }
            values[counter] = document.createTextNode( TheUte().decode64( who ));
            counter++;
            
            values[counter] = document.createTextNode( TheUte().decode64( messages[i].dateTime));
            counter++;
            
            
            var tmpLink = document.createElement("A");
            tmpLink.href = "SendEmail.aspx?emailGuid=" + messages[i].guid;
            
            var tmpSubject = TheUte().decode64( messages[i].subject);
            
            //alert(tmpSubject.trim().length);
            var sSubject = "(none)";
            
            if(tmpSubject.trim().length > 1)
            {
                sSubject = tmpSubject;
            
            }
            
            var tmpSubject = document.createTextNode(sSubject);
            tmpLink.appendChild(tmpSubject);
            
            values[counter] = tmpLink;
            counter++;
            
            values[counter] = TheUte().getCheckbox("chk_" + grouping + "_" + messages[i].id,"",false, this.chkSingle_clicked);
            counter++;
            
           
            var tmpStat = null;
            
            
            var statMsg = TheUte().decode64( messages[i].status);
            var divStat = document.createElement("DIV");
            divStat.title = statMsg;
            divStat.className = "clsEmailMessageStatusIcon";
            
            var statIcon = document.createTextNode("!");
            //alert("|" + statMsg + "|");
            
            if(messages[i].status.length > 0 )
            {
                divStat.appendChild(statIcon);
                values[counter] = divStat;
            }
            
            
            
            counter++;
            
            
       
        }
        
        values[4] = TheUte().getCheckbox("chkAll_" + grouping + "_" + bunchOfIDs,"",false, this.chkAll_clicked );
    
        values[5] = prepSubHeaderDiv(document.createTextNode("status"));

       
         
    }
    var oGrid2 = newGrid2("MessageGrid", messages.length + 1 ,6,values);
    oGrid2.init( oGrid2 );
    return oGrid2.gridTable;

}

function getMessagesOutbox(myMessageMetaJSON)
{

    var values = new Array();
    var divTxt = document.createTextNode("Outbox (" + myMessageMetaJSON.outbox_messages.length + ")");
    var divHead = document.createElement("DIV");
    divHead.className = "clsMessageBoxHeader";
    divHead.appendChild (divTxt);
    values[0] = divHead;
    values[1] = this.getMessages( myMessageMetaJSON.outbox_messages ,"to", "outbox");

    var oGrid2 = newGrid2("OutboxOuterGrid",2,1,values);
    oGrid2.init( oGrid2 );
    return oGrid2.gridTable;
}
function getMessagesSent(myMessageMetaJSON)
{

    var values = new Array();
    var divTxt = document.createTextNode("Sent (" + myMessageMetaJSON.sent_messages.length + ")");
    var divHead = document.createElement("DIV");
    divHead.className = "clsMessageBoxHeader";
    divHead.appendChild (divTxt);
    values[0] = divHead;
    values[1] = this.getMessages( myMessageMetaJSON.sent_messages ,"to","sent");

    var oGrid2 = newGrid2("SentOuterGrid",2,1,values);
    oGrid2.init( oGrid2 );
    return oGrid2.gridTable;
}
 
function getMessagesInbox(myMessageMetaJSON)
{

    var values = new Array();
    var divTxt = document.createTextNode("Inbox (" + myMessageMetaJSON.inbox_messages.length + ")");
    var divHead = document.createElement("DIV");
    divHead.className = "clsMessageBoxHeader";
    divHead.appendChild (divTxt);
    values[0] = divHead;
    values[1] = this.getMessages( myMessageMetaJSON.inbox_messages ,"from","inbox");

    var oGrid2 = newGrid2("InboxOuterGrid",2,1,values)
    oGrid2.init( oGrid2 );
    return oGrid2.gridTable;
}

function getMessagesDrafts(myMessageMetaJSON)
{

    var values = new Array();
    var divTxt = document.createTextNode("Drafts (" + myMessageMetaJSON.draft_messages.length + ")");
    var divHead = document.createElement("DIV");
    divHead.className = "clsMessageBoxHeader";
    divHead.appendChild (divTxt);
    values[0] = divHead;
    values[1] = this.getMessages( myMessageMetaJSON.draft_messages ,"to","drafts");

    var oGrid2 = newGrid2("DraftOuterGrid",2,1,values);
    oGrid2.init( oGrid2 );
    return oGrid2.gridTable;
}


function chkSingle_clicked()
{
    
    if(this.checked)
    {
        oMessageManager.SelectedItems.add(this.id,null);
    }
    else
    {
        oMessageManager.SelectedItems.remove(this.id);
    }
    
  

}

function chkAll_clicked()
{
    var bits = this.id.split('_');
    
    var containerName = bits[1];
    var chkIDbit = bits[2].split('|');
    
    var i=0;
    for(i = 0;i<chkIDbit.length -1 ;i++)
    {
        var chkKey =  "chk_" + containerName + "_" + chkIDbit[i];
       // alert( chkKey );
        var tmpChk = TheUte().findElement( chkKey , "input");
        if(this.checked)
        {
            //check others
            tmpChk.checked = true;
            oMessageManager.SelectedItems.add(chkKey,null);
            
        }
        else
        {
            //uncheck others
            tmpChk.checked = false;
            
            oMessageManager.SelectedItems.remove(chkKey);
        }
        
        
    
    }
    
    
}

function message_new()
{
    var keysPiped = oMessageManager.SelectedItems.GetKeysPiped();
    
    var url = "SendNewEmail.aspx";
    if(keysPiped.trim().length > 0)
    {
        url += "?idTo=";
        url += keysPiped;
    }
        
    location.href = url;

}
function message_delete()
{
    var keysPiped = oMessageManager.SelectedItems.GetKeysPiped();
    if(keysPiped == "")
    {
        alert("Please select a message to delete");
     }
     else
     {
        //send checked messages
        var mSendMessages = new macro();
        mSendMessages.name = "DeleteMessages";
        addParam(mSendMessages,"keysPiped64",TheUte().encode64(keysPiped));
        processRequest( mSendMessages );
     }   
}
function message_send()
{
    var keysPiped = oMessageManager.SelectedItems.GetKeysPiped();
    if(keysPiped == "")
    {
        alert("Please select a message to send");
     }
     else
     {
        //send checked messages
        var mSendMessages = new macro();
        mSendMessages.name = "SendMessages";
        addParam(mSendMessages,"keysPiped64",TheUte().encode64(keysPiped));
        processRequest( mSendMessages );
     }   
        
        
}
function message_sendAll()
{
    alert(this.id);
}

// Unique list V way V :)


































//// unique list
function uniqueList(name)
{
    var _name = name;
    this.Name = _name;
    var _uniqueArray = new Array();
    this.UniqueArray = _uniqueArray;

    this.add = uniqueListAdd;
    this.exists = uniquleListExists;
    this.GetKeysPiped = uniqueListGetKeysPiped;
    this.remove = removeUniqueListItem;
    this.removeAll = removeAllFromUniqueList;
    
    
    
}

function uniqueListAdd( key,value )
{
    if( this.exists( key ) == false )
        this.UniqueArray.push( new uniqueListItem(key,value) );

}

function uniquleListExists ( key )
{
    var i = 0;
    for ( i = 0;i<this.UniqueArray.length;i++)
    {
        if( key == this.UniqueArray[i].Key )
            return true;
         
    }
    
    return false;
}
function removeAllFromUniqueList ( )
{
    var i = 0;
    for ( i = 0;i<this.UniqueArray.length;i++)
    {
        this.UniqueArray[i] = null;
    }
    
    this.UniqueArray = new Array();
}


function removeUniqueListItem ( key )
{
    if(this.exists( key ))
    {
        var arrayCopy = new Array();
         var i = 0;
        for ( i = 0;i<this.UniqueArray.length;i++)
        {
            if( key == this.UniqueArray[i].Key )
             {
                //do nothing
             
             }
             else
             {
                //copy
                arrayCopy.push( this.UniqueArray[i] );
             }
        }

        this.removeAll();
        this.UniqueArray = arrayCopy;
        
    }
}

function uniqueListGetKeysPiped ()
{
    var i = 0;
    var pipedString = "";
    
    for ( i = 0;i<this.UniqueArray.length;i++)
    {
        pipedString += this.UniqueArray[i].Key;
        pipedString += "|";
         
    }
    
    if(this.UniqueArray.length > 0)
    {
        pipedString = pipedString.substring(0,pipedString.length -1);
    }
    
    return pipedString;
}
function uniqueListItem(key,value)
{
    var _key = key;
    this.Key = _key;
    var _value = value;
    this.Value = _value;

}