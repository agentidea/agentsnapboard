

function newStoryEditor3( aoStoryController,dvAttachPoint )
{
    
    var storyEditor = new StoryEditor3();
    storyEditor.init(  aoStoryController , dvAttachPoint );
    return storyEditor;
}

function StoryEditor3()
{

    
    var _storyController = null;
    this.StoryController = _storyController;
    
    var _container = null;
    this.container = _container;
    
    var _zIndex = 0;
    this.zIndexCursor = _zIndex;
    
    
    this.init = StoryEditor3Init;
    this.getStoryMetaInfoPanel = getStoryMetaInfoPanel;
    this.getPalette = getPalette;
    this.getStage = getStage;
    this.getStatusBar = getStatusBar;
    
    this.renderCurrentPage = renderCurrentPage;
    

    this.addMedia = addMedia;
    this.addMedia2 = addMedia2;
    this.getWidget = getWidget;
    this.getWidgetPos = getWidgetPos;
    this.getXYlabel = getXYlabel;
    
    var _widgetCount = 0;
    this.widgetCount = _widgetCount;
    
    var _colorIndex = 0;
    this.colorIndex = _colorIndex;
    

}



function addMedia2(name , x,y,z,w,h,value)
{

    if( value == null) value = "<div></div>";
    
    var dvMainStage = TheUte().findElement("divMainStage","DIV");
    var dvMediaWidget = this.getWidget(this,name, x, y, z, w, h, value );
  
 
    drag.createSimpleGroup(dvMediaWidget);
 
    //alert(drag);
    dvMainStage.appendChild( dvMediaWidget );
}

function addMedia(name )
{
    var dvMainStage = TheUte().findElement("divMainStage","DIV");
    
    var x = 0;
    var y = 0;
    
    if( mousePosREF != null )
    {
        x = mousePosREF.x;
        y = mousePosREF.y;
        history( "mouse is at " + x + " x " + y);
    }
    
    var dvMediaWidget = oStoryEditor3.getWidget(oStoryEditor3,name,x,y );
  
    drag.createSimpleGroup(dvMediaWidget)
    dvMainStage.appendChild( dvMediaWidget );
}


function getWidgetPos( x, y, z, w, h )
{

    var widgetShadow = document.createElement("DIV");
    //widgetShadow.className = "box";

    var color = '#ff0000';
    var cssTextString = 'float:left; visibility:visible; width:'+w+'px;height:'+h+'px; background-color:'+color+';';
   //cssTextString += 'position:absolute;';
    cssTextString += ' top:' + y ;
    cssTextString += 'px;';
    cssTextString += ' left:' + x;
    cssTextString += 'px;';
    cssTextString += ' z-index:' + (z) + ";";
    
    history( cssTextString );
   
    widgetShadow.style.cssText = cssTextString;
    
    //widgetShadow.style.top = y;
    //widgetShadow.style.left = x;
    
    widgetShadow.setAttribute("title",x + ' x ' + y);
    

    return widgetShadow;
        
    
}


function getWidget( editor, name, x, y,z, w, h, value )
{

    if( x == null) x = 0;
    if( y == null) y = 0;
    if( z == null) z = 0;
    if( w == null || w == 0 ) w = 250;
    if( h == null || h ==0 ) h = 100;
    if( name == null) name = "no name";
    if( value == null) value = "<div> intentionally no value</div>";
    
   
  
    var widgetShadow = null;
    
    editor.widgetCount++;

    var dvNameLabel = document.createElement("DIV");
       
    var dragHandle = document.createElement("DIV");
    dragHandle.className = "clsDragHandle";
    
    widgetShadow = editor.getWidgetPos(x,y,z,w,h);

    var values = new Array();
    var txtNode = document.createTextNode( name );
    dvNameLabel.appendChild ( txtNode );

    var innerDiv =  document.createElement("DIV");
    innerDiv.innerHTML = value;

    values.push (  dvNameLabel );
    values.push ( innerDiv );
    
    widgetShadow.id="divWidgetShadow_" + name;

    var oGrid2 = newGrid2("widget",2,1,values,1);
    oGrid2.init( oGrid2 );
   
    widgetShadow.appendChild( oGrid2.gridTable );

    return widgetShadow;
 
 
}



function renderCurrentPage(aoStoryController)
{



    var page = aoStoryController.GetPage( aoStoryController.CurrentPageCursor );

    var pageElementMapCount = page.PageElementMapCount;
    var i = 0;
    for(;i<pageElementMapCount;i++)
    {
    
        var s = "var pem = page.PageElementMaps.pageElementMap_" + i + ";";
        eval(s);
        
       // alert("[" +  pem.X + " , " + pem.Y + " , " + pem.Z + "] " + pem.PageElementID );
        
        var pageElement = aoStoryController.FindPageElement( aoStoryController.CurrentStory,page,pem.PageElementID );
        
        var pageElementValue = TheUte().decode64( pageElement.Value );
        this.addMedia2( pageElement.Type, pem.X, pem.Y, pem.Z , 300,200, pageElementValue );
        
    
    }
    
    
    
    
     

}

function StoryEditor3Init(aoStoryController,attachPoint)
{

    

     this.StoryController = aoStoryController;
    
     var values2 = new Array();
     values2.push ( this.getStatusBar());
     values2.push ( this.getXYlabel());
    
    
    var values = new Array();
    var stage = this.getStage();
   
   
    history("stage loaded w x h :: " + stage.style.width + " x " + stage.style.height + " @left - " + stage.style.left);
    values.push (  stage );
    values.push ( this.getStoryMetaInfoPanel( this.StoryController ));
    values.push (  this.getPalette( this.StoryController ) );

    var oGrid  = newGrid2("statArea",1,2,values2,0);
    oGrid.init( oGrid );
    values.push (  oGrid.gridTable );


    var oGrid2 = newGrid2("storyEditorMainGrid",4,1,values,1);
    oGrid2.init( oGrid2 );

    this.container = oGrid2.gridTable;
    this.container.id = "oGrid2";
    attachPoint.appendChild( this.container );

    this.renderCurrentPage( aoStoryController );

}


function getStage()
{
    var dvStage = document.createElement("DIV");
    //dvStage.className = "clsMainStage";
    
    var cssTextString = "";
    cssTextString += "background-color:#999999;";
    cssTextString += "color:#ffffff;";
    cssTextString += "width:1000px;";
    cssTextString += "height:500px;";
    cssTextString += "visibility:visible;";
    cssTextString += "position:relative; ";
    
    dvStage.style.cssText = cssTextString;
    dvStage.id = "divMainStage";
    return dvStage;
}

function getXYlabel()
{
    var divXYlabel = document.createElement("DIV");
    divXYlabel.className = "clsXYlabel";
    divXYlabel.id = "divXYlabel";
    return divXYlabel;
}


function getStatusBar()
{
    var dvStatusBar = document.createElement("DIV");
    dvStatusBar.className = "clsStatusBar";
    dvStatusBar.id = "divStatusBar";
    return dvStatusBar;
}

function getPalette( aoStoryController )
{
    var dvPalette = document.createElement("DIV");
    dvPalette.className = "clsMainPalette";
    dvPalette.id = "divMainPalette";
    
    var currPage = aoStoryController.GetPage(aoStoryController.CurrentPageCursor);
    
    
    var txtTotalNumberOfPages = document.createTextNode( "total pages : " + aoStoryController.CurrentStory.PageCount + " curr page name " + TheUte().decode64( currPage.Name));
    dvPalette.appendChild( txtTotalNumberOfPages );
    return dvPalette;
}

function getStoryMetaInfoPanel( aoStoryController )
{
    var values = new Array();
    values.push( document.createTextNode(" Story Title: " + TheUte().decode64( aoStoryController.CurrentStory.Title )));
    
    var oGrid2 = newGrid2("MetaInfoPane",1,1,values,1);
    oGrid2.init( oGrid2 );
    
    return oGrid2.gridTable;

}


