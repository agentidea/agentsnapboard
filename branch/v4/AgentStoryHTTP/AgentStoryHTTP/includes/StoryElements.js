 //   AgentIdea - Story story structres and Factory Methods
 

//////////////////////
//
//   Story 
//
//   begin
//
///////////////////////

function newStory(storyOpenedBy,id, title, desc)
{
    var s = new Story();
    s.StoryOpenedBy = storyOpenedBy;
    s.ID = id;
    s.Title = title;
    s.Description = desc;
    return s;
}

function Story()
{
    var _ID = -1;
    this.ID = _ID;

//    var _stateCursor = 0;
//    this.StateCursor = _stateCursor;
    
    var _title = null;
    this.Title = _title;
    
    var _storyOpenedBy = 0;
    this.StoryOpenedBy = _storyOpenedBy;
    
    var _description = null;
    this.Description = _description;
    
    var _pages = new Object;
    this.Pages = _pages;
    var _pageCount = 0;
    this.PageCount = _pageCount;
    
    var _pageElements = new Object;
    this.PageElements = _pageElements;
    var _pageElementCount = 0;
    this.PageElementCount = _pageElementCount;
    
    var _authorName = "null author";
    this.AuthorName = _authorName;
    var _authorID = -1;
    this.AuthorID = _authorID;
    
}
//////////////////////
//
//   Story 
//
//   end
//
///////////////////////
//////////////////////
//
//   Page 
//
//   begin
//
///////////////////////

function newPage(name,rows,cols)
{

    var p = new Page();
    p.Name = name;
    p.gridCols = cols;
    p.gridRows = rows;
    return p;
}

function Page()
{
    var _name = null;
    this.Name = _name;
    
    var _GUID = null;
    this.GUID = _GUID;
    
    var _id = 0;
    this.ID = _id;
    
    var _pageElementMaps = new Object;
    this.PageElementMaps = _pageElementMaps;
    var _pageElementMapCount = 0;
    this.PageElementMapCount = _pageElementMapCount;
    
    var _x = -1;
    var _y = -1;
    var _z = -1;
    
    this.gridCols = _x;  //or pixels?
    this.gridRows = _y;  //or pixels?
    this.gridZ = _z;




}
//////////////////////
//
//   Page 
//
//   end
//
///////////////////////


//////////////////////
//
//   PageElement 
//
//   begin
//
///////////////////////

function newPageElement(id,type,typeID,value,aBY)
{
    var pageElement = new PageElement();
    pageElement.ID = id;
    pageElement.Type = type;
    pageElement.TypeID = typeID;
    pageElement.Value = value;
    if(aBY != null)
        pageElement.BY = aBY;
        
    return pageElement;
}

function PageElement()
{
    var _type = null;
    this.Type = _type;
    
    var _typeID = 0;
    this.TypeID = _typeID;
    
    var _value = null;
    this.Value = _value;
    
    var _tags = null;
    this.Tags = _tags;
    
    var _id = null;
    this.ID = _id;
    
    var _by = null;
    this.BY = _by;

}

//////////////////////
//
//   PageElement 
//
//   end
//
///////////////////////

//////////////////////
//
//   PageElementMap
//
//   begin
//
///////////////////////

function newPageElementMap( pageElementID, x,y,z,aGUID )
{
    var pem = new PageElementMap();
    pem.PageElementID = pageElementID;
    pem.X = x;
    pem.Y = y;
    pem.Z = z;
    if(aGUID!=null)
        pem.GUID = aGUID;
        
    return pem;
}


function PageElementMap()
{
    
    var _pageElementID = null;
    this.PageElementID = _pageElementID;
    
    var _gridPositionX = -1;
    this.X = _gridPositionX;
    var _gridPositionY = 0;
    this.Y = _gridPositionY;  
    var _gridPositionZ = 0;
    this.Z = _gridPositionZ;
    var _guid = null;
    this.GUID = _guid;
    var _visible = true;
    this.visible = _visible;

}

//////////////////////
//
//   PageElementMap 
//
//   end
//
///////////////////////



// cell alert mechanism

var cellAlerts = new Array();

function cellAlert(val) {

    var backgroundColors = new Array();

    backgroundColors.push("#FF5252");
    //backgroundColors.push("#FF8f8f");
   /// backgroundColors.push("#FF2222");
    //backgroundColors.push("#FF3333");
    //backgroundColors.push("#FF6666");
    backgroundColors.push("#FF9999");
    backgroundColors.push("#e0e0e0");

    var colorCursor = -1;
    var _active = true;
    this.active = _active;

    var _container = document.createElement("DIV");
    this.container = _container;

    this.runNextColor = function() {
        colorCursor++;
        if (colorCursor < backgroundColors.length) {
            _container.style.backgroundColor = backgroundColors[colorCursor];
        } else {
            //end color gradient
            this.active = false;
        }

    }


    this.runNextColor();

    var txtVal = document.createTextNode(val + "");
    _container.appendChild(txtVal);

    cellAlerts.push(this);


}