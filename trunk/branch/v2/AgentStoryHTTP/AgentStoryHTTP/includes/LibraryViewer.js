// LibraryViewer


// callback into library viewer Library Port


function LibraryViewer()
{

    var _container = document.createElement("DIV");
    var _libPortals = new Array();
    
    
    _container.className = "clsLibViewer";
    this.container = _container;
    
    var supportedLibs = {"names":[{"id":"frLibPort","name":"Flickr","command":"Flickr_LibPort"},{"id":"meLibPort","name":"My Elements","command":"MyElements_LibPort"},{"id":"ytLibPort","name":"You Tube","command":"YouTube_LibPort"},{"id":"geLibPort","name":"Google","command":"Google_LibPort"}]};

    this.init = function ()
    {
    
       var numLibs = supportedLibs.names.length;
       for(var i = 0;i<numLibs;i++)
       {
          var libPort = new LibraryPort(supportedLibs.names[i]);
          _container.appendChild(libPort.container);
          _libPortals.push(libPort);
       
       }
    }
    
    this.getLibPort = function(id)
    {
        
        
        var retLibPort = null;
        for(var i = 0;i<_libPortals.length;i++)
        {
            var tmpLibPort = _libPortals[i];
            if(tmpLibPort.getID() == id)
            {
               retLibPort = tmpLibPort;
               break; 
            }
        }
        
        return retLibPort;
    }
}



function LibraryPort(def)
{

           var _container = document.createElement("div");
           _container.className = "clsLibPort";
           var specPort = null;
           
           var _state = 0;
           
           var tmpID = def.id;
           this.getID = function() { return tmpID;}
           
           var _name  = def.name;
           
           var tmpLibDiv = document.createElement("div");
           tmpLibDiv.className = "clsLibrarySectionHeader";
           tmpLibDiv.id = "libSecHead_" + tmpID;
           
           var lbl = document.createTextNode( _name );
           tmpLibDiv.appendChild(lbl);
           
           var tmpLibDivBod = document.createElement("div");
           tmpLibDivBod.className = "clsLibrarySectionBod";
           tmpLibDivBod.style.display = "none";
           tmpLibDivBod.id = "libSecBod_" + tmpID;
           
           var _dvRefresh = document.createElement("DIV");
           _dvRefresh.className = "clsRefresh";
           _dvRefresh.innerHTML = "R";
           
           _dvRefresh.onclick = function()
           {
                specPort.refresh();
           }
           
           this.updateMedia = function(mediaItems)
           {
                specPort.updateMedia(mediaItems);
           }
           
            var values = new Array();
            values[0] = tmpLibDiv;
            values[1] = _dvRefresh;
            var hdrGrid = newGrid2("hdrGrid",1,2,values);
            hdrGrid.init( hdrGrid );
            //return ;
           
           
           tmpLibDiv.onclick = function()
           {
                if(tmpLibDivBod.style.display == "none")
                {
                    tmpLibDivBod.style.display = "block";
                    if(_state == 0)
                    {
                        _state =1;
                        
                        //load effective lib port palette

                        eval(" specPort = new " + def.command + "(tmpID);");
                        specPort.display(tmpLibDivBod);   
                        
                    }
    
                }
                else
                {
                    tmpLibDivBod.style.display = "none";
                }
           
           }
           
           tmpLibDiv.ondblclick = function(ev)
            {
                ev = ev || window.event;
                ev.cancelBubble = true;  
            }
           
           _container.appendChild( hdrGrid.gridTable );
           _container.appendChild( tmpLibDivBod );
           this.container = _container;
}



