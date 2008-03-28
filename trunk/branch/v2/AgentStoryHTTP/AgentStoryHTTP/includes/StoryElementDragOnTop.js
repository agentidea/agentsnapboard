// from Yahoo
/* Copyright (c) 2006 Yahoo! Inc. All rights reserved. */

/**
 * @class a DragDrop implementation that moves the object as it is being dragged,
 * and keeps the object being dragged on top.  This is a subclass of DD rather
 * than DragDrop, and inherits the implementation of most of the event listeners
 * from that class.
 *
 * @extends YAHOO.util.DD
 * @constructor
 * @param {String} id the id of the linked element
 * @param {String} sGroup the group of related DragDrop items
 */
YAHOO.example.DDOnTop = function(id, sGroup, config) {
    if (id) {
        this.init(id, sGroup, config);
        this.logger = this.logger || YAHOO;
    }
};

// YAHOO.example.DDOnTop.prototype = new YAHOO.util.DD();
YAHOO.extend(YAHOO.example.DDOnTop, YAHOO.util.DD);

/**
 * The inital z-index AND xyz of the element, stored so we may restore it later
 *
 * @type int
 */
YAHOO.example.DDOnTop.prototype.origZ = 0;
YAHOO.example.DDOnTop.prototype.origX = 0;
YAHOO.example.DDOnTop.prototype.origY = 0;


YAHOO.example.DDOnTop.prototype.startDrag = function(x, y) {
    

    var srcEl = this.getEl();
    var elX = Dom.getX( srcEl );
    var elY = Dom.getY( srcEl );
    var style = srcEl.style;

    // store the original z-index
    this.origZ = style.zIndex;
    
    // store the original x,y points
    this.origX = elX;
    this.origY = elY;

    // The z-index needs to be set very high so the element will indeed be on top
    style.zIndex = 9999;
    
    this.logger.log(srcEl.id + " startDrag " + this.origX + "." + this.origY,"warn");
    
    
    
};

var Event = YAHOO.util.Event;
var Dom = YAHOO.util.Dom;


YAHOO.example.DDOnTop.prototype.endDrag = function(e) {

    var srcEl = this.getEl();
    var elX = Dom.getX( srcEl );
    var elY = Dom.getY( srcEl );
    
    
    var srcElid = srcEl.id;
    
    
    
    if( srcElid.indexOf("tmpLibDiv_") != -1 )
    {
        //
        // was a library element
        //
        //this.logger.log("EoD library element shoo shoo back!!" + srcElid, "error");
        //this.logger.log(srcEl.id + " startDrag " + this.origX + "." + this.origY,"warn");
  
  
        //
        // get the content by way of innerHTML
        //
        var content = srcEl.innerHTML;
  
//  
//        var coord = storyView.getOffsetCoord();
//        //
//        // create a new page element
//        //
//        var newWidget = addNewWidget( coord.x,coord.y,9,-1,storyView.getTmpGUID(),"UNSAVED lib drop",false );
//        newWidget.updateSrcText( content );
//        newWidget.updateViewHTML(newWidget.id);  
//        newWidget.showProps();
        
        storyView.dropElemIn(content, storyView.getOffsetCoord(),storyView.getTmpGUID(),"UNSAVED copy from library");
        

  
        //
        // try to palce element back?
        //
        //this.getEl().style.left = this.origX;
        //this.getEl().style.top = this.origY;
        
        
        //
        // hide the library element
        //
        this.getEl().style.display = "none";
        
        
        
    }
    else
    if( srcElid.indexOf("widget_") != -1 )
    {
        //
        // was a page element
        //
    
        var containerID = srcEl.childNodes.item(0).id;

        var pemGUID = srcEl.childNodes.item(0).getAttribute("guid");
        
        if(pemGUID != null)
        {
            this.logger.log("E.D. " + containerID + " GUID: " + pemGUID,"warn");

            var sev = storyView.sevByGuid( pemGUID );
            if(sev != null)
            {
                this.logger.log(" from " + sev.getX() + " x " + sev.getY(),"warn");
                this.logger.log(" to " + elX + " x " + elY,"warn");

                sev.setCoords(elX,elY);
                sev.reportInfo();
                
                //update system of move?
                storyView.sevEndDrag( sev );
            }
            else
            {
                alert("lost sev on endDrag" + pemGUID);
            }
            
            
        }
     }
     else
     {
        alert("unrecognized drag candidate " + srcElid);
     }
   
    // restore the original z-index
    this.getEl().style.zIndex = this.origZ;
    
};


