YAHOO.example2.DDOnTopLib = function(id, sGroup, config) {
    if (id) {
        this.init(id, sGroup, config);
        this.logger = this.logger || YAHOO;
    }
};


YAHOO.extend(YAHOO.example2.DDOnTopLib, YAHOO.util.DD);

/**
 * The inital z-index of the element, stored so we can restore it later
 *
 * @type int
 */
YAHOO.example2.DDOnTopLib.prototype.origZ = 0;

YAHOO.example2.DDOnTopLib.prototype.startDrag = function(x, y) {
    this.logger.log(this.id + " startDrag");

    var style = this.getEl().style;

    // store the original z-index
    this.origZ = style.zIndex;

    // The z-index needs to be set very high so the element will indeed be on top
    style.zIndex = 999;
};

var Event = YAHOO.util.Event;
var Dom = YAHOO.util.Dom;


YAHOO.example2.DDOnTopLib.prototype.endDrag = function(e) {

    alert("end drag ");

    
};


