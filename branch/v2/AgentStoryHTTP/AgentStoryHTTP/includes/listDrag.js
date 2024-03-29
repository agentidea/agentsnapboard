﻿    var gTotalStories = 0;

    
    function loadStoriesList( stories )
    {
        var attachPoint = document.getElementById("divBodyAttachPoint");
    
        var totalStories = stories.length;
        
        var i = 0;
        for(;i<totalStories;i++)
        {
            loadStory( stories[i],attachPoint);
        }
    
    }
    

    function loadStory(story, attachPoint)
    {
       
        gTotalStories++;
        var lenList = story.pages.length;
        
        var lblStory = document.createTextNode( "Drag and Drop to Order Pages for Story - '" + TheUte().decode64( story.title ) + "' ( Click save when you are done )");
        
        var divStoryContainer = document.createElement("DIV");
        divStoryContainer.appendChild(lblStory);
        
        var spacerDiv = document.createElement("DIV");
        spacerDiv.style.height = 25;
        divStoryContainer.appendChild(spacerDiv);
        
        var ulMain = document.createElement("UL");
        ulMain.id = "ul" + gTotalStories;
        ulMain.title = "storyID_" + story.id
        
        //register as a drop target
        new YAHOO.util.DDTarget(ulMain.id);
        
        ulMain.className = "draglist";

        var kk = 0;
        for(;kk < lenList;kk++)
        {
            var tmpItem = document.createElement("li");
            tmpItem.className = "list"+gTotalStories;
            
            
            var tmpKey = "li" + gTotalStories + "_" + kk;
            tmpItem.id = tmpKey;
            
            //register as a draggable item.
            
            new YAHOO.example.DDList(tmpKey);
            
            //tmpItem.value = listStruc.listItems[kk].val;
            
            var storyTitle = TheUte().decode64( story.pages[kk].title );
            var txtListItem = document.createTextNode(storyTitle);
            tmpItem.appendChild(txtListItem);
            
            //tmpItem.title = story.pages[kk].GUID + "_" + story.pages[kk].id;
            tmpItem.title = story.pages[kk].id;
            
            ulMain.appendChild(tmpItem);
        }

        divStoryContainer.appendChild(ulMain);
        attachPoint.appendChild(divStoryContainer);
        
    }
    

(function() {

var Dom = YAHOO.util.Dom;
var Event = YAHOO.util.Event;
var DDM = YAHOO.util.DragDropMgr;

//////////////////////////////////////////////////////////////////////////////
// example app
//////////////////////////////////////////////////////////////////////////////
YAHOO.example.DDApp = {
    init: function() {

        loadStoriesList( storiesMeta.stories );

        Event.on("showButton", "click", this.showOrder);
        Event.on("switchButton", "click", this.switchStyles);
    },

    showOrder: function() {
        var parseList = function(ul, title) {
            var items = ul.getElementsByTagName("li");
            var out = "";
            for (i=0;i<items.length;i=i+1) {
                out += items[i].title + "|";
            }

            return out;
        };

       
        var k = 1;
        var output = "output:: ";
        
        for(;k<gTotalStories + 1;k++)
        {
        
            var ulTmp = Dom.get("ul" + k);
            var storyID = ulTmp.title.split('_')[1];
            var pageMap = parseList(ulTmp,"");
            
            //output = output + "\n" +  " " + storyID + " :: " + parseList( ulTmp, "L:" + k);
            
            var OrderPages = newMacro("OrderPages");
            addParam( OrderPages,"StoryID",storyID     );
            addParam( OrderPages,"PageMap",pageMap );
            
            //alert( serializeMacroForRequest( OrderPages ) );
            
           processRequest( OrderPages );
        
        }
     
        

    },

    switchStyles: function() {
        Dom.get("ul1").className = "draglist_alt";
        Dom.get("ul2").className = "draglist_alt";
    }
};

//////////////////////////////////////////////////////////////////////////////
// custom drag and drop implementation
//////////////////////////////////////////////////////////////////////////////

YAHOO.example.DDList = function(id, sGroup, config) {

    YAHOO.example.DDList.superclass.constructor.call(this, id, sGroup, config);

    this.logger = this.logger || YAHOO;
    var el = this.getDragEl();
    Dom.setStyle(el, "opacity", 0.67); // The proxy is slightly transparent

    this.goingUp = false;
    this.lastY = 0;
};

YAHOO.extend(YAHOO.example.DDList, YAHOO.util.DDProxy, {

    startDrag: function(x, y) {
        this.logger.log(this.id + " startDrag");

        // make the proxy look like the source element
        var dragEl = this.getDragEl();
        var clickEl = this.getEl();
        Dom.setStyle(clickEl, "visibility", "hidden");

        dragEl.innerHTML = clickEl.innerHTML;

        Dom.setStyle(dragEl, "color", Dom.getStyle(clickEl, "color"));
        Dom.setStyle(dragEl, "backgroundColor", Dom.getStyle(clickEl, "backgroundColor"));
        Dom.setStyle(dragEl, "border", "2px solid gray");
    },

    endDrag: function(e) {

        var srcEl = this.getEl();
        var proxy = this.getDragEl();

        // Show the proxy element and animate it to the src element's location
        Dom.setStyle(proxy, "visibility", "");
        var a = new YAHOO.util.Motion( 
            proxy, { 
                points: { 
                    to: Dom.getXY(srcEl)
                }
            }, 
            0.2, 
            YAHOO.util.Easing.easeOut 
        )
        var proxyid = proxy.id;
        var thisid = this.id;

        // Hide the proxy and show the source element when finished with the animation
        a.onComplete.subscribe(function() {
                Dom.setStyle(proxyid, "visibility", "hidden");
                Dom.setStyle(thisid, "visibility", "");
                
                //end drag after animate();
                //alert("fin dragging ... ");
                
                
                
            });
        
                       
        a.animate();
    },

    onDragDrop: function(e, id) {

        // If there is one drop interaction, the li was dropped either on the list,
        // or it was dropped on the current location of the source element.
        if (DDM.interactionInfo.drop.length === 1) {

            // The position of the cursor at the time of the drop (YAHOO.util.Point)
            var pt = DDM.interactionInfo.point; 

            // The region occupied by the source element at the time of the drop
            var region = DDM.interactionInfo.sourceRegion; 

            // Check to see if we are over the source element's location.  We will
            // append to the bottom of the list once we are sure it was a drop in
            // the negative space (the area of the list without any list items)
            if (!region.intersect(pt))
            {
                var destEl = Dom.get(id);
                var destDD = DDM.getDDById(id);
                destEl.appendChild(this.getEl());
                destDD.isEmpty = false;
                DDM.refreshCache();
            }

        }
    },

    onDrag: function(e) {

        // Keep track of the direction of the drag for use during onDragOver
        var y = Event.getPageY(e);

        if (y < this.lastY) {
            this.goingUp = true;
        } else if (y > this.lastY) {
            this.goingUp = false;
        }

        this.lastY = y;
    },

    onDragOver: function(e, id) {
    
        var srcEl = this.getEl();
        var destEl = Dom.get(id);

        // We are only concerned with list items, we ignore the dragover
        // notifications for the list.
        if (destEl.nodeName.toLowerCase() == "li") {
            var orig_p = srcEl.parentNode;
            var p = destEl.parentNode;

            if (this.goingUp) {
                p.insertBefore(srcEl, destEl); // insert above
            } else {
                p.insertBefore(srcEl, destEl.nextSibling); // insert below
            }

            DDM.refreshCache();
        }
    }
});

Event.onDOMReady(YAHOO.example.DDApp.init, YAHOO.example.DDApp, true);

})();


