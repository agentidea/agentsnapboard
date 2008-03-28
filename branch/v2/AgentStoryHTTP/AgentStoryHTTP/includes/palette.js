
function palette(aContentProviderName,asUserList)
{
    var _contentProviderName = aContentProviderName;
    this.contentProviderName = _contentProviderName;
    

    
    var _container = null;
    this.container = _container;
    
    var _userList = asUserList;
    this.userList = _userList;

    var _divLoadMediaItemsAttachPoint;
    this.divLoadMediaItemsAttachPoint = _divLoadMediaItemsAttachPoint;
    
    this.loadMediaItems = loadMediaItems;
    this.displayMediaItems = displayMediaItems;
    this.getPaletteHeader = getPaletteHeader;
    this.getFetchUserControl = getFetchUserControl;
    this.fetch_user_media = fetch_user_media;

    this.init = initPalette;
    this.palette_copy = palette_copy;

}

function palette_copy()
{
    var bits = this.id.split('~');
    var tmpID = bits[1];
    var txtBoxToCopy = TheUte().findElement("txt~" + tmpID,"textarea");

  
  oStoryEditor.ThePageElementEditor.msgBox.value = txtBoxToCopy.value;
  oStoryEditor.ThePageElementEditor.changeMediaOptions(3); //set type to video
    
    
    
}
function initPalette()
{
    if(this.userList != null)
    {
        this.loadMediaItems( this.userList );
    }
    
    this.divLoadMediaItemsAttachPoint = document.createElement("DIV");
    
    this.divLoadMediaItemsAttachPoint.className = "clsMediaItemsAttachPoint";
    
    var values = new Array();
    values[0] = this.getPaletteHeader();
    values[1] = this.divLoadMediaItemsAttachPoint;
    var oGrid2 = newGrid2("paletteMainGrid",2,1,values);
    oGrid2.init( oGrid2 );
    
    this.container = oGrid2.gridTable;

}

function getPaletteHeader()
{
    var values = new Array();
    var divPaletteHeader = document.createElement("DIV");
    divPaletteHeader.className = "clsPaletteHeader";
    divPaletteHeader.appendChild( document.createTextNode( this.contentProviderName ));
    values[0] = divPaletteHeader;
    values[1] = this.getFetchUserControl();
    var oGrid2 = newGrid2("paletteHeader",2,1,values);
    oGrid2.init( oGrid2 );
    return oGrid2.gridTable;


}

function getFetchUserControl()
{
    var values = new Array();
    var divUserName = document.createElement("DIV");
    divUserName.className = "clsUserName";
    divUserName.appendChild( document.createTextNode( "user" ));
    values[0] = divUserName;
    values[1] = TheUte().getInputBox("","txtUserNameList",null,null,"clsInputBox","username OR username | username | username ");
    values[2] = TheUte().getButton("cmdFetchUserMedia","fetch videos","get user(s) media",this.fetch_user_media,"clsButtonAction");
    
    
    var oGrid2 = newGrid2("fetchUserControl",1,4,values);
    oGrid2.init( oGrid2 );
    return oGrid2.gridTable;


}

function fetch_user_media ()
{

    var txtUserNameList = TheUte().findElement("txtUserNameList","input");
    var userNames = txtUserNameList.value.trim();
    if(userNames.length==0)
    {
        alert("please supply one or more usernames");
        return;
    }
    
   var contentProvname =  oStoryEditor.MainPalette.contentProviderName;
   oStoryEditor.MainPalette.loadMediaItems ( userNames ,contentProvname  );
    
}

function loadMediaItems( userList , contentProviderName )
{
        var macroLoadMediaItems = newMacro("LoadMediaItems");
        addParam( macroLoadMediaItems,"UserList",userList );
        addParam( macroLoadMediaItems,"ContentProviderName", contentProviderName);
        processRequest( macroLoadMediaItems );
}

function displayMediaItems(mediaItems)
{
    var attach = oStoryEditor.MainPalette.divLoadMediaItemsAttachPoint;
    TheUte().removeChildren(attach);
    
    var lenMediaUserSets = mediaItems.envelope.length;
    var k = 0;
    var values = new Array();
    
    for(k=0;k<lenMediaUserSets;k++)
    {

        var lenMediaItems = mediaItems.envelope[k].mediaItems.length;
        var i = 0;
        for(i=0;i<lenMediaItems; i++)
        {
            var tmpDiv = document.createElement("DIV");
            var tmpID = TheUte().decode64( mediaItems.envelope[k].mediaItems[i].id );
            var author = TheUte().decode64( mediaItems.envelope[k].mediaItems[i].userCode );
            var title = TheUte().decode64(   mediaItems.envelope[k].mediaItems[i].title);
            var tmpEmbed = getYouTubeVideoTemplate(tmpID,400,380);
            
            tmpDiv.innerHTML = tmpEmbed;
            values.push( TheUte().getButton("cmdCopy~" + tmpID , "copy" , "",this.palette_copy,"clsButtonAction"));
            values.push( tmpDiv );
            

            var tmpDivTitle = document.createElement("DIV");
            tmpDivTitle.appendChild( document.createTextNode( title ) );
            
             var tmpDivAuthor = document.createElement("DIV");
             tmpDivAuthor.className="clsAuthor";
            tmpDivAuthor.appendChild( document.createTextNode( author ) );
            tmpDivTitle.appendChild(tmpDivAuthor);
            values.push ( tmpDivTitle );

            values.push ( TheUte().getTextArea(tmpEmbed,"txt~" + tmpID,null,null,"clsTxtEmbed") );

        }
        
     }
    
    var oGrid2 = newGrid2("fetchUserControl",values.length/4,4,values,1);
    oGrid2.init( oGrid2 );
    
    attach.appendChild( oGrid2.gridTable );
    
    
    

}


function getYouTubeVideoTemplate(mediaID,height,width)
{
    var urlToMedia = "http://www.youtube.com/v/" + mediaID;
    var s = "<object ";
    s += "height=\"";
    s += height;
    s += "\" ";
    s += "width=\"";
    s += width;
    s += "\"";
    s += ">";
    s += "<param name=\"movie\" value=\"";
    s += urlToMedia;
    s += "\"></param> ";
    s += "<param name=\"wmode\" value=\"";
    s += "transparent";
    s += "\"></param> ";
    s += "<embed src=\"";
    s += urlToMedia;
    s += "\"";
    s += " type=\"application/x-shockwave-flash\" wmode=\"transparent\" >";
    s += "</embed>";
    s += "</object>";
    return s;
}


function mediaItem()
{
    var _id = null;
    this.id = _id;
    
     var _name = null;
    this.name = _name;
    
    var _title = null;
    this.title = _title;

}

