
//
// Example custom portal ... GOOGLE SEARCH API
//



function Google_LibPort(aViewPortID)
{
           var _viewPortID = aViewPortID;
           var _container = document.createElement("div");
           _container.className = "";
           _container.id = "dvGoogle_LibPort";
           var lbl = document.createTextNode( "google search term: " );
           _container.appendChild(lbl);
           this.container = _container;
           
           this.display = function(attachPoint)
           {
            attachPoint.appendChild(_container);
           
           }
           
            
           this.refresh = function()
           {
                alert(" refreshing A CUSTOM Google SEARCH API HOOK ...");
           
           }
}