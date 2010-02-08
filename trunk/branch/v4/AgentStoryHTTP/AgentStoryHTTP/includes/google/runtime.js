function MsPageControl(opt_options) {
  this.bindToPage();
}

var VS_ALL_DONE = "i'm all done";
var VS_LOOK = "more...";
var VS_SAVE = "remember this search";
var NM_LOREM = "unc quis libero. Pellentesque mauris enim, blandit a, ullamcorper tempus, elementum eget, lectus. unc quis libero. Pellentesque mauris enim, blandit a, ullamcorper tempus, elementum eget, lectus. unc quis libero. Pellentesque mauris enim, blandit a, ullamcorper tempus, elementum eget, lectus."

MsPageControl.prototype.bindToPage = function() {

  // find the key app touch points
  this.appRoot = document.getElementById("mySample");
  this.videoSearchExpressions = document.getElementById("videoSearchExpressions");
  this.somethingElse = document.getElementById("somethingElse");

  // video search expressions
  this.videoPreCanned = document.getElementById("videoPreCanned");
  this.videoDynamic = document.getElementById("videoDynamic");

  this.videoSearchControl = document.getElementById("videoSearchControl");

  this.somethingElse.innerHTML = VS_LOOK;
  this.somethingElse.onclick = methodClosure(this, MsPageControl.prototype.twidleSomethingElse, []);

  
  // set up for messaging
  //this.loadMessaging();

  // load pre-canned expressions
  this.loadPreCanned();

  // build the dynamic expression form
  removeChildren(this.videoDynamic);
  this.videoSearchForm = createForm("search-form");
  this.videoSearchInput = createTextInput("search-input");
  this.videoSearchForm.appendChild(this.videoSearchInput);

  this.videoSearchButton = createDiv(null, "search-button");
  this.videoSearchButton.innerHTML = "&nbsp;";
  this.videoSearchForm.appendChild(this.videoSearchButton);

  this.videoSaveQueryButton = createDiv(VS_SAVE, "save-query-button");
  this.videoSearchForm.appendChild(this.videoSaveQueryButton);
  this.videoDynamic.appendChild(this.videoSearchForm);

  // form to actions
  this.videoSearchButton.onclick = methodClosure(this, MsPageControl.prototype.newVideoSearch, []);
  this.videoSaveQueryButton.onclick = methodClosure(this, MsPageControl.prototype.saveQuery, []);
  this.videoSearchForm.onsubmit = methodClosure(this, MsPageControl.prototype.newVideoSearch, []);

  // build the video search control
  this.vsc = new GSearchControl();
  this.vsc.setOnKeepCallback(this, MsPageControl.prototype.keepHandler, "add this to my note");
  this.vs = new GvideoSearch();
  var options = new GsearcherOptions();
  options.setExpandMode(GSearchControl.EXPAND_MODE_OPEN);
  this.vsc.addSearcher(this.vs, options);
  this.vsc.draw(this.videoSearchControl);

  // pick a video set
  var max = cannedVideoSearchExpressions.length - 1;
  var index = Math.round(max*Math.random());
  this.newCannedVideoSearch(index);
}

MsPageControl.prototype.twidleSomethingElse = function() {
  if (this.videoSearchExpressions.className == "pre-canned") {
    this.somethingElse.innerHTML = VS_ALL_DONE;
    cssSetClass(this.videoSearchExpressions, "dynamic");
  } else {
    this.somethingElse.innerHTML = VS_LOOK;
    cssSetClass(this.videoSearchExpressions, "pre-canned");
  }
}

// load and bind pre-canned search expressions
MsPageControl.prototype.loadPreCanned = function() {
  removeChildren(this.videoPreCanned);

  for (var i=0; i < cannedVideoSearchExpressions.length; i++ ) {
    this.loadPreCannedItem(i);
  }
}

MsPageControl.prototype.loadPreCannedItem = function(itemIndex) {

  var item = cannedVideoSearchExpressions[itemIndex]
  var baseClassName = "pre-canned pre-canned-";
  var label = item.query;
  if (item.label) {
    label = item.label;
  }

  // random class between 0 and 5
  var r = Math.round(5*Math.random());
  className = baseClassName + r;


  var div = createDiv(label, className);
  div.onclick = methodClosure(this, MsPageControl.prototype.newCannedVideoSearch, [itemIndex]);
  this.videoPreCanned.appendChild(div);

  item.savedClassName = className;
  item.domNode = div;
}

// save the current query
MsPageControl.prototype.saveQuery = function() {
  if (this.videoSearchInput.value && this.videoSearchInput.value != "search for a video") {
    var item = new Object();
    item.query = this.videoSearchInput.value;
    cannedVideoSearchExpressions.push(item);
    this.loadPreCannedItem(cannedVideoSearchExpressions.length-1);
    this.twidleSomethingElse();
  }
}

//
// click from the USER starts here
//


MsPageControl.prototype.newCannedVideoSearch = function(itemIndex) {
  var item = cannedVideoSearchExpressions[itemIndex];
  
  //call the item
  this.newVideoSearch(item.query);
  
  if (this.currentCannedQueryItem) {
    cssSetClass(this.currentCannedQueryItem.domNode,
                this.currentCannedQueryItem.savedClassName);
  }
  this.currentCannedQueryItem = item;
  cssSetClass(this.currentCannedQueryItem.domNode,
              this.currentCannedQueryItem.savedClassName + " pre-canned-selected");
}

//
// clear current markers and start a new search
//

MsPageControl.prototype.newVideoSearch = function(opt_query) {
  var query = "";
  
//  alert(opt_query); FIRES
  
  if (opt_query) {
    query = opt_query;
    this.videoSearchInput.value = query;
  } else if (this.videoSearchInput.value) {
    query = this.videoSearchInput.value;
  } else {
    return false;
  }
  this.vsc.execute(query);
  return false;
}

MsPageControl.prototype.keepHandler = function(result) {

    alert(result);
    
  var node = result.html.cloneNode(true);

  if ( result.GsearchResultClass ==  GvideoSearch.RESULT_CLASS ) {
    // scale down the image
    var imgs = node.getElementsByTagName("img");
    
   
    
    var imageScaler = {width:60,height:45};
    for (var imageIndex=0; imageIndex < imgs.length; imageIndex++) {
      GSearch.scaleImage(result.tbWidth, result.tbHeight, imageScaler,imgs[imageIndex]);
    }
    this.clipCount++;
    this.messageClips.appendChild(node);
  }
}

MsPageControl.prototype.loadMessaging = function() {

  // messaging sections
  this.sendMessage = document.getElementById("sendMessage");
  this.savedMessages = document.getElementById("savedMessages");
  this.newMessageArea = document.getElementById("newMessageArea");
  this.sendMessage.onclick = methodClosure(this, MsPageControl.prototype.newNote, []);

  // build the message submit form
  // build the dynamic expression form
  removeChildren(this.newMessageArea);
  removeChildren(this.savedMessages);
  this.newMessageForm = createForm("message-form");
  this.newMessageInput = createTextarea("message-input");
  this.newMessageForm.appendChild(this.newMessageInput);

  // cancel
  var div = createDiv(null, "message-buttons");
  var b = createButton("cancel", "button");
  b.onclick = methodClosure(this, MsPageControl.prototype.endNote, [false]);
  div.appendChild(b);

  // save
  var b = createButton("save", "button");
  b.onclick = methodClosure(this, MsPageControl.prototype.endNote, [true]);
  div.appendChild(b);
  this.newMessageForm.appendChild(div);

  var table = createTable("message-table");
  var row = createTableRow(table);
  var cell0 = createTableCell(row, "message-text");
  var cell1 = createTableCell(row, "message-clips");
  this.messageClips = createDiv(null, "message-clips");
  cell1.appendChild(this.messageClips);
  cell0.appendChild(this.newMessageForm);

  this.newMessageArea.appendChild(table);
}

MsPageControl.prototype.newNote = function() {
  this.clipCount = 0;
  removeChildren(this.messageClips);
  this.newMessageInput.value = NM_LOREM;
  cssSetClass(this.appRoot, "editing");
}

MsPageControl.prototype.endNote = function(save) {
  cssSetClass(this.appRoot, "reading");

  if (save) {
    var v = "";
    if (this.newMessageInput.value) {
      v = this.newMessageInput.value;
      v = v.replace(/\n/g, "<br>");
      v = v + "<br><br>";
    }
    var messageHeader = createDiv("hey harley,", "message-header");
    var messageText = createDiv("message-text");
    messageText.innerHTML = v;

    var table = createTable("message-table");
    var row = createTableRow(table);
    var cell0 = createTableCell(row, "message-text");
    var cell1 = createTableCell(row, "message-clips");
    var messageClips = this.messageClips.cloneNode(true);
    cell1.appendChild(messageClips);
    cell0.appendChild(messageHeader);
    cell0.appendChild(messageText);

    if (this.clipCount > 0) {
    
    //alert( "found " + this.clipCount + " clips");
    
      var footerRow = createTableRow(table);
      var footerCell = createTableCell(footerRow, "message-footer");
      footerCell.colSpan = 2;
      var t = new Date();
      var str = t.getMonth()+1 + "/" + t.getFullYear();
      var footerString = "videos by Goooooogle (" + str + ")";
      var link = createLink("http://video.google.com", footerString);
      var footer = createDiv(null,"footer");
      footer.appendChild(link);
      footerCell.appendChild(footer);
    }
    this.savedMessages.appendChild(table);

  }

  this.clipCount = 0;
  removeChildren(this.messageClips);
}


/**
 * Various Static DOM Wrappers.
*/
function methodClosure(object, method, opt_argArray) {
  return function() {
    return method.apply(object, opt_argArray);
  }
}

function createDiv(opt_text, opt_className) {
  var el = document.createElement("div");
  if (opt_text) {
    el.innerHTML = opt_text;
  }
  if (opt_className) { el.className = opt_className; }
  return el;
}

function removeChildren(parent) {
  while (parent.firstChild) {
    parent.removeChild(parent.firstChild);
  }
}

function cssSetClass(el, className) {
  el.className = className;
}


function createForm(opt_className) {
  var el = document.createElement("form");
  if (opt_className) { el.className = opt_className; }
  return el;
}

function createTable(opt_className) {
  var el = document.createElement("table");
  if (opt_className) { el.className = opt_className; }
  return el;
}

function createTableRow(table) {
  var tr = table.insertRow(-1);
  return tr;
}

function createTableCell(tr, opt_className) {
  var td = tr.insertCell(-1);
  if (opt_className) { td.className = opt_className; }
  return td;
}

function createTextInput(opt_className) {
  var el = document.createElement("input");
  el.type = "text";
  if (opt_className) { el.className = opt_className; }
  return el;
}

function createLink(href, text, opt_target, opt_className) {
  var el = document.createElement("a");
  el.href = href;
  el.appendChild(document.createTextNode(text));
  if (opt_className) {
    el.className = opt_className;
  }
  if (opt_target) {
    el.target = opt_target;
  }
  return el;
}

function createButton(value, opt_className) {
  var el = document.createElement("input");
  el.type = "button";
  el.value = value;
  if (opt_className) { el.className = opt_className; }
  return el;
}

function createTextarea(opt_className) {
  var el = document.createElement("textarea");
  if (opt_className) { el.className = opt_className; }
  return el;
}



var cannedVideoSearchExpressions = [
  {
    query : "paris hilton"
  },

  {
    query : "jessica simpson"
  },

  {
    query : "bonzai pipeline"
  },

  {
    query : "jimi hendrix"
  },

  {
    label : "50cent",
    query : "50cent music"
  },

  {
    query : "kelly clarkson"
  },

  {
    query : "jessica alba"
  },

  {
    query : "bob marley"
  },

  {
    query : "dmx"
  },

  {
    query : "ferrari f1"
  },

  {
    query : "south park"
  },

  {
    query : "ferrari f430"
  },

  {
    query : "ziggy marley"
  }
];

