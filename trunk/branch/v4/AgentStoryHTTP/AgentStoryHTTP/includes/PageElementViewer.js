// JScript File


function PageElementViewer()
{

  var _container = null;
    this.container = _container;
    

    var _operatorID = 0;
    this.operatorID = _operatorID;
    
    var _toID = 0;
    this.toID = _toID;
    
    this.init = PageElementViewerInit;
    this.copy = element_copy;
    this.getPageElementContainer = getPageElementContainer;
}


function element_copy()
{
    var cmdBits = this.id.split('_');
    
    var origTxtBox = TheUte().findElement("txtContentOrig_" + cmdBits[1],"textarea");
    
    //alert(origTxtBox.value);
    
    //hack to copy to clipboard.
    try
    {
        origTxtBox.focus();
        origTxtBox.select();
        document.execCommand("Copy");
        alert("copied " + origTxtBox.value.trim().length + " bytes to the clipboard");
    
    }
    catch(e)
    {
        alert(e.message + " \r\n\r\n  your browser may not support copy to clipboard ");
    }
    
    
}


function PageElementViewerInit(operatorID,pageElementJSON)
{
   this.operatorID = operatorID;
   
   var lenPageElems = pageElementJSON.PageElements.length;
  
   var values = new Array();

   
   for(var i=0;i<lenPageElems;i++)
   {
   
     
     var tmpID = pageElementJSON.PageElements[i].id;
     
     var tmpContent = TheUte().decode64( pageElementJSON.PageElements[i].value64 );
     var tmpUserName = pageElementJSON.PageElements[i].addedByUsername;
     var tmpDateStamp  = pageElementJSON.PageElements[i].DateAdded;

     var tmpTextBox = TheUte().getTextArea(tmpContent,"txtContentOrig_" + tmpID,null,null,"clsClipBoardHackTextBox");
     values.push( tmpTextBox );     
     
     var tmpCopyButton = TheUte().getButton("cmd_" + tmpID,"opy","copy to clipboard",this.copy,"clsButtonAction");
    // values.push(tmpCopyButton);
     values.push(  this.getPageElementContainer(tmpID,tmpContent,tmpUserName,tmpDateStamp )  );

     
   }
   

        
    var oGrid2 = newGrid2("PageElementViewer",lenPageElems,3,values,1);
    oGrid2.init( oGrid2 );

    this.container = oGrid2.gridTable;
   
   
}


function getPageElementContainer(tmpContentID,tmpContent,tmpUsername,tmpDateStamp)
{
    var values = new Array();

    var divPageElementContainerTitle = document.createElement("div");
    divPageElementContainerTitle.className = "clsPageElementContainerTitle";
    divPageElementContainerTitle.appendChild( document.createTextNode( "on " + tmpDateStamp +   " - " + tmpUsername + " - " + tmpContentID ));
    values.push(divPageElementContainerTitle);

    var divPreview = document.createElement("div");
    divPreview.className = "clsPageElementPreview";
    divPreview.innerHTML = tmpContent;
    values.push( divPreview );

    var oGrid2 = newGrid2("PageElementContainer",2,1,values);
    oGrid2.init( oGrid2 );

    return oGrid2.gridTable;
}