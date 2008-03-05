

//
// core library portals 
// FLICKR, MY ELEMENTS, YOUTUBE
//


function YouTube_LibPort(aViewPortID)
{
           var _viewPortID = aViewPortID;
           var _container = document.createElement("div");
           var _mediaArea = document.createElement("div");
           _container.className = "";
           _container.id = "dvYouTube_LibPort";
           
           var defVal = "danielbeast";
           
           var lbl = document.createTextNode( "YouTube username: " );
           _container.appendChild(lbl);
           
           var txtUserName = TheUte().getInputBox(defVal,"txtYouTubeUserList",null,null,"clsInput","pipe delimeted list of users");
           _container.appendChild( txtUserName );
           _container.appendChild( _mediaArea );
           
           

           this.container = _container;
           
           this.display = function(attachPoint)
           {
                attachPoint.appendChild(_container);
                
                var macroLoadMediaItems = newMacro("LoadMediaItems");
                addParam( macroLoadMediaItems,"viewPortID", _viewPortID);
                addParam( macroLoadMediaItems,"UserList",txtUserName.value );
                addParam( macroLoadMediaItems,"ContentProviderName", "YouTube");
                processRequest( macroLoadMediaItems );
           
           }
           
           this.refresh = function()
           {
               var macroLoadMediaItems = newMacro("LoadMediaItems");
                addParam( macroLoadMediaItems,"viewPortID", _viewPortID);
                addParam( macroLoadMediaItems,"UserList",txtUserName.value );
                addParam( macroLoadMediaItems,"ContentProviderName", "YouTube");
                processRequest( macroLoadMediaItems );
           
           }
           
           this.updateMedia = function(mediaItems)
           {
            
            var attach = _mediaArea;
            TheUte().removeChildren(attach);
    
                    var lenMediaUserSets = mediaItems.envelope.length;
                    var k = 0;
                    var values = new Array();
                    
                    var hasItems = true;

                    for(k=0;k<lenMediaUserSets;k++)
                    {

                        var lenMediaItems = mediaItems.envelope[k].mediaItems.length;
                        
                        if(lenMediaItems == 0 )
                        {
                             hasItems = false;
                             alert("no data found for this search");
                             return;
                        }
                        
                        var i = 0;
                        for(i=0;i<lenMediaItems; i++)
                        {
                            var tmpDiv = document.createElement("DIV");
                            var tmpID = TheUte().decode64( mediaItems.envelope[k].mediaItems[i].id );
                            var author = TheUte().decode64( mediaItems.envelope[k].mediaItems[i].userCode );
                            var title = TheUte().decode64( mediaItems.envelope[k].mediaItems[i].title);
                            var tmpEmbed = getYouTubeVideoTemplate(tmpID,300,280);
                            
                            tmpDiv.innerHTML = tmpEmbed;
                            

                            var tmpDivTitle = document.createElement("DIV");
                            var tmpDivAuthor = document.createElement("DIV");
                            tmpDivAuthor.className="clsAuthor";
                            tmpDivAuthor.appendChild( document.createTextNode( author ) );
                            tmpDivTitle.appendChild(tmpDivAuthor);
                            tmpDivTitle.appendChild( document.createTextNode( title ) );
                            values.push ( tmpDivTitle );
                            
                            
                            //make tmpDiv draggable
                            tmpDiv.id = "tmpLibDiv_" + storyView.getTmpGUID();
                            tmpDiv.className = "clsDragLibDiv";
                            
                           var dd = null;
                           
                           try
                           {
                                dd = new YAHOO.example.DDOnTop(tmpDiv);


                                // var dd = new YAHOO.util.DDProxy(tmpDiv);
                                dd.setHandleElId(tmpDiv.id);

                                dd.setXConstraint(1000, 1000, 25);
                                dd.setYConstraint(1000, 1000, 25);

                            }
                            catch(exp)
                            {
                                alert(exp.description);
                            }

                            
                            
                            
                            
                            values.push( tmpDiv );
                            //values.push( TheUte().getButton("cmdCopy~" + tmpID , "copy > " , "create a page element",this.palette_copy,"clsButtonAction"));

                            //values.push ( TheUte().getTextArea(tmpEmbed,"txt~" + tmpID,null,null,"clsTxtEmbed") );

                        }
                        
                     }
                    
                    
                    if(hasItems)
                    {
                        var oGrid2 = newGrid2("fetchUserControl",values.length*3,1,values,0);
                        oGrid2.init( oGrid2 );
                        
                        attach.appendChild( oGrid2.gridTable );
                    }
           
           
           }

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



// Filter by tags?
function MyElements_LibPort(aViewPortID)
{
           var _viewPortID = aViewPortID;
           var _container = document.createElement("div");
           var _mediaArea = document.createElement("div");
           _container.className = "";
           _container.id = "dvMyElements_LibPort";
           
           var defVal = "tree";
           
           var lbl = document.createTextNode( "Club Username: " );
           _container.appendChild(lbl);
           
           var txtUserName = TheUte().getInputBox(defVal,"txtMyElementsUserList",null,null,"clsInput","pipe delimeted list of club users");
           _container.appendChild( txtUserName );
           _container.appendChild( _mediaArea );
           
           

           this.container = _container;
           
           this.display = function(attachPoint)
           {
                attachPoint.appendChild(_container);
               

                var macroLoadMediaItems = newMacro("LoadMediaItems");
                addParam( macroLoadMediaItems,"viewPortID", _viewPortID);
                addParam( macroLoadMediaItems,"UserList",txtUserName.value );
                addParam( macroLoadMediaItems,"ContentProviderName", "myelements");
                processRequest( macroLoadMediaItems );
                
           
           }
            
           this.refresh = function()
           {
                 var macroLoadMediaItems = newMacro("LoadMediaItems");
                addParam( macroLoadMediaItems,"viewPortID", _viewPortID);
                addParam( macroLoadMediaItems,"UserList",txtUserName.value );
                addParam( macroLoadMediaItems,"ContentProviderName", "myelements");
                processRequest( macroLoadMediaItems );
           
           }
           
            this.updateMedia = function(mediaItems)
           {
        var attach = _mediaArea;
        TheUte().removeChildren(attach);

                var lenMediaUserSets = mediaItems.envelope.length;
                var k = 0;
                var values = new Array();
                
                var hasItems = true;

                for(k=0;k<lenMediaUserSets;k++)
                {

                    var lenMediaItems = mediaItems.envelope[k].mediaItems.length;
                    if(lenMediaItems == 0 )
                    {
                         hasItems = false;
                         alert("no data found for this search");
                         return;
                    }

                    
                    var i = 0;
                    for(i=0;i<lenMediaItems; i++)
                    {
                        var tmpDiv = document.createElement("DIV");
                        var tmpID = TheUte().decode64( mediaItems.envelope[k].mediaItems[i].id );
                        var author = TheUte().decode64( mediaItems.envelope[k].mediaItems[i].userCode );
                        var title = TheUte().decode64( mediaItems.envelope[k].mediaItems[i].title);
                        var tmpEmbed = TheUte().decode64( mediaItems.envelope[k].mediaItems[i].embedHTML);
                        
                        tmpDiv.innerHTML = tmpEmbed;
                        

                        var tmpDivTitle = document.createElement("DIV");
                        var tmpDivAuthor = document.createElement("DIV");
                        tmpDivAuthor.className="clsAuthor";
                        tmpDivAuthor.appendChild( document.createTextNode( author ) );
                        tmpDivTitle.appendChild(tmpDivAuthor);
                        tmpDivTitle.appendChild( document.createTextNode( title ) );
                        values.push ( tmpDivTitle );
                        
                        
                        //make tmpDiv draggable
                        tmpDiv.id = "tmpLibDiv_" + storyView.getTmpGUID();
                        tmpDiv.className = "clsDragLibDiv";
                        
                       var dd = null;
                       
                       try
                       {
                            dd = new YAHOO.example.DDOnTop(tmpDiv);
                            dd.setHandleElId(tmpDiv.id);
                            dd.setXConstraint(1000, 1000, 25);
                            dd.setYConstraint(1000, 1000, 25);

                        }
                        catch(exp)
                        {
                            alert(exp.description);
                        }

                        
                        
                        
                        
                        values.push( tmpDiv );
                        //values.push( TheUte().getButton("cmdCopy~" + tmpID , "copy > " , "create a page element",this.palette_copy,"clsButtonAction"));
                        //values.push ( TheUte().getTextArea(tmpEmbed,"txt~" + tmpID,null,null,"clsTxtEmbed") );

                    }
                    
                 }
                
                
                if(hasItems)
                {
                    var oGrid2 = newGrid2("fetchUserControl",values.length*3,1,values,0);
                    oGrid2.init( oGrid2 );
                    
                    attach.appendChild( oGrid2.gridTable );
                }
           
           
           }
           
}

function Flickr_LibPort(aViewPortID)
{
           var _viewPortID = aViewPortID;
           var _container = document.createElement("div");
           var _mediaArea = document.createElement("div");
           _container.className = "";
           _container.id = "dvFlickr_LibPort";
           
           var defVal = "bukanator";
           
           var lbl = document.createTextNode( "Tags: " );
           _container.appendChild(lbl);
           
           var txtUserName = TheUte().getInputBox(defVal,"txtMyElementsUserList",null,null,"clsInput","photo tags");
           _container.appendChild( txtUserName );
           _container.appendChild( _mediaArea );
           
           

           this.container = _container;
           
           this.display = function(attachPoint)
           {
                attachPoint.appendChild(_container);
               

                var macroLoadMediaItems = newMacro("LoadMediaItems");
                addParam( macroLoadMediaItems,"viewPortID", _viewPortID);
                addParam( macroLoadMediaItems,"UserList",txtUserName.value );
                addParam( macroLoadMediaItems,"ContentProviderName", "flickr");
                processRequest( macroLoadMediaItems );
                
           
           }
            
           this.refresh = function()
           {
                 var macroLoadMediaItems = newMacro("LoadMediaItems");
                addParam( macroLoadMediaItems,"viewPortID", _viewPortID);
                addParam( macroLoadMediaItems,"UserList",txtUserName.value );
                addParam( macroLoadMediaItems,"ContentProviderName", "flickr");
                processRequest( macroLoadMediaItems );
           
           }
           
            this.updateMedia = function(mediaItems)
           {
        var attach = _mediaArea;
        TheUte().removeChildren(attach);

                var lenMediaUserSets = mediaItems.envelope.length;
                var k = 0;
                var values = new Array();
                
                var hasItems = true;

                for(k=0;k<lenMediaUserSets;k++)
                {

                    var lenMediaItems = mediaItems.envelope[k].mediaItems.length;
                    if(lenMediaItems == 0 )
                    {
                         hasItems = false;
                         alert("no data found for this search");
                         return;
                    }

                    
                    var i = 0;
                    for(i=0;i<lenMediaItems; i++)
                    {
                        var tmpDiv = document.createElement("DIV");
                        var tmpID = TheUte().decode64( mediaItems.envelope[k].mediaItems[i].id );
                        var author = TheUte().decode64( mediaItems.envelope[k].mediaItems[i].userCode );
                        var title = TheUte().decode64( mediaItems.envelope[k].mediaItems[i].title);
                        var tmpEmbed = TheUte().decode64( mediaItems.envelope[k].mediaItems[i].embedHTML);
                        
                        tmpDiv.innerHTML = tmpEmbed;
                        

                        var tmpDivTitle = document.createElement("DIV");
                        var tmpDivAuthor = document.createElement("DIV");
                        tmpDivAuthor.className="clsAuthor";
                        tmpDivAuthor.appendChild( document.createTextNode( author ) );
                        tmpDivTitle.appendChild(tmpDivAuthor);
                        tmpDivTitle.appendChild( document.createTextNode( title ) );
                        values.push ( tmpDivTitle );
                        
                        
                        //make tmpDiv draggable
                        tmpDiv.id = "tmpLibDiv_" + storyView.getTmpGUID();
                        tmpDiv.className = "clsDragLibDiv";
                        
                       var dd = null;
                       
                       try
                       {
                            dd = new YAHOO.example.DDOnTop(tmpDiv);
                            dd.setHandleElId(tmpDiv.id);
                            dd.setXConstraint(1000, 1000, 25);
                            dd.setYConstraint(1000, 1000, 25);

                        }
                        catch(exp)
                        {
                            alert(exp.description);
                        }

                        
                        
                        
                        
                        values.push( tmpDiv );
                        //values.push( TheUte().getButton("cmdCopy~" + tmpID , "copy > " , "create a page element",this.palette_copy,"clsButtonAction"));
                        //values.push ( TheUte().getTextArea(tmpEmbed,"txt~" + tmpID,null,null,"clsTxtEmbed") );

                    }
                    
                 }
                
                
                if(hasItems)
                {
                    var oGrid2 = newGrid2("fetchUserControl",values.length*3,1,values,0);
                    oGrid2.init( oGrid2 );
                    
                    attach.appendChild( oGrid2.gridTable );
                }
           
           
           }
           
}


