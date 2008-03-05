<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoryPageView.aspx.cs" Inherits="StoryPageView" %>

    
    <!--
    
    
    /*

    AgentIdea PageView 
    Copyright AgentIdea 2007
    
    please note this is private and a copyrighted original work by Grant Steinfeld.
    
    
    
1. Registration Number:    TXu-959-906  
Title:    AgentIdea application studio for IE : version 1.0. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    AgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Author on © Application:    text of computer program: Grant Steinfeld , 1967- & Oren Kredo , 1966-. 
Special Codes:   1/C 

--------------------------------------------------------------------------------
2. Registration Number:    TXu-960-165  
Title:    AgentIdea application studio for Java : version 1.0. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    AgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Author on © Application:    program text: Grant Steinfeld , 1967-, & Oren Kredo , 1966-. 
Special Codes:   1/C 

--------------------------------------------------------------------------------
3. Registration Number:    TXu-961-904  
Title:    AgentIdea application studio for IIS : version 1.0 / authors, Grant Steinfeld, Oren Kredo. 
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    cAgentIdea, LLC 
Created:    2000 

Registered:    10Jul00

Special Codes:   1/C 
--------------------------------------------------------------------------------
4. Registration Number:    ( awaiting filing ) ( expected 27Sept07 )
Title:    AgentIdea - AgentStory
Description:    Computer program. 
Note:    Printout only deposited. 
Claimant:    AgentIdea, LLC 
Created:    2006 

Filed:    27Aug07

to verify search for Grant Steinfeld or Oren Kredo
http://www.copyright.gov/records/cohm.html

*/
 
    -->
    
    


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
<title>Using Drag and Drop to Reorder a List</title>

<style type="text/css">
/*margin and padding on body element
  can introduce errors in determining
  element position and are not recommended;
  we turn them off as a foundation for YUI
  CSS treatments. */
body {
	margin:0;
	padding:0;
}
</style>

<link rel="stylesheet" type="text/css" href="<%= IncludeMaster %>/build/fonts/fonts-min.css?_yuiversion=2.3.1" />
<script type="text/javascript" src="<%= IncludeMaster %>/build/utilities/utilities.js?_yuiversion=2.3.1"></script>


<!--begin custom header content for this example-->

<style type="text/css">

div.workarea { padding:10px; float:left }

ul.draglist { 
    position: relative;
    width: 200px; 
    height:240px;
    background: #f7f7f7;
    border: 1px solid gray;
    list-style: none;
    margin:0;
    padding:0;
}

ul.draglist li {
    margin: 1px;
    cursor: move; 
}

ul.draglist_alt { 
    position: relative;
    width: 200px; 
    list-style: none;
    margin:0;
    padding:0;
    /*
       The bottom padding provides the cushion that makes the empty 
       list targetable.  Alternatively, we could leave the padding 
       off by default, adding it when we detect that the list is empty.
    */
    padding-bottom:20px;
}

ul.draglist_alt li {
    margin: 1px;
    cursor: move; 
}


li.list1 {
    background-color: #D1E6EC;
    border:1px solid #7EA6B2;
}

li.list2 {
    background-color: #D8D4E2;
    border:1px solid #6B4C86;
}

#user_actions { float: right; }

</style>


<!--end custom header content for this example-->

</head>

<body class="yui-skin-sam">
<!--BEGIN SOURCE CODE FOR EXAMPLE =============================== -->


<div class="workarea" id="divAttachList">
  
</div>

<div class="workarea">
  <h3>List 2</h3>
  <ul id="ul2" class="draglist">
    <li class="list2" id="li2_1">list 2, item 1</li>
    <li class="list2" id="li2_2">list 2, item 2</li>

    <li class="list2" id="li2_3">list 2, item 3</li>
  </ul>
</div>

<div id="user_actions">
  <input type="button" id="showButton" value="Show Current Order" />
  <input type="button" id="switchButton" value="Remove List Background" />
</div>

<script type="text/javascript">

(function() {

var Dom = YAHOO.util.Dom;
var Event = YAHOO.util.Event;
var DDM = YAHOO.util.DragDropMgr;

//////////////////////////////////////////////////////////////////////////////
// example app
//////////////////////////////////////////////////////////////////////////////
YAHOO.example.DDApp = {
    init: function() {
    
    
    var listStruc = {"listItems":[{"name":"apples","val":11,"seq":1},{"name":"pears","val":21,"seq":3},{"name":"grapes","val":119,"seq":11}]};
    //var listStruc = null;
    
    //build the lists dynamically.
    
    
    
        var rows=3,cols=2,i,j;
        for (i=1;i<cols+1;i=i+1) {
            new YAHOO.util.DDTarget("ul"+i);
        }

        for (i=1;i<cols+1;i=i+1) {
            for (j=1;j<rows+1;j=j+1) {
                new YAHOO.example.DDList("li" + i + "_" + j);
                //             
            }
        }

        Event.on("showButton", "click", this.showOrder);
        Event.on("switchButton", "click", this.switchStyles);
    },

    showOrder: function() {
        var parseList = function(ul, title) {
            var items = ul.getElementsByTagName("li");
            var out = title + ": ";
            for (i=0;i<items.length;i=i+1) {
                out += items[i].id + " ";
            }
            return out;
        };

        var ul1=Dom.get("ul1"), ul2=Dom.get("ul2");
        alert(parseList(ul1, "List 1") + "\n" + parseList(ul2, "List 2"));

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
                alert("fin dragging ... ");
                
                
                
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


</script>

<!--END SOURCE CODE FOR EXAMPLE =============================== -->


<!--MyBlogLog instrumentation-->
<script type="text/javascript" src="http://track2.mybloglog.com/js/jsserv.php?mblID=2007020704011645"></script>

</body>
</html>

<!-- spaceId: 792404078 -->
<!-- VER-311 -->
<script language=javascript>
if(window.yzq_p==null)document.write("<scr"+"ipt language=javascript src=http://l.yimg.com/us.js.yimg.com/lib/bc/bc_2.0.4.js></scr"+"ipt>");
</script><script language=javascript>
if(window.yzq_p)yzq_p('P=EZdGIEJe5GIqpF3vRb6P3JYNRbUOVUcM5wMABmg9&T=13sdt14tr%2fX%3d1192027907%2fE%3d792404078%2fR%3ddev_net%2fK%3d5%2fV%3d1.1%2fW%3dJ%2fY%3dYAHOO%2fF%3d798834108%2fS%3d1%2fJ%3d3BAB49D1');
if(window.yzq_s)yzq_s();
</script><noscript><img width=1 height=1 alt="" src="http://us.bc.yahoo.com/b?P=EZdGIEJe5GIqpF3vRb6P3JYNRbUOVUcM5wMABmg9&T=142cagv27%2fX%3d1192027907%2fE%3d792404078%2fR%3ddev_net%2fK%3d5%2fV%3d3.1%2fW%3dJ%2fY%3dYAHOO%2fF%3d3612121176%2fQ%3d-1%2fS%3d1%2fJ%3d3BAB49D1"></noscript>
<!-- com2.devnet.scd.yahoo.com compressed/chunked Wed Oct 10 07:51:47 PDT 2007 -->

