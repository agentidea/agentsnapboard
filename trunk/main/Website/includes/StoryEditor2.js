//////////////////////
//
//   StoryEditor
//
//   begin
//
///////////////////////

function newStoryEditor2(storyController)
{
    var storyEditor = new StoryEditor2();
    storyEditor.init( storyController );
    return storyEditor;
}

function StoryEditor2()
{

    var _storyController = null;
    this.StoryController = _storyController;
    
    var _container = null;
    this.container = _container;
    this.init = initStoryEditor;
    
    this.getStoryHeader = getStoryHeader;
    this.getPageNavigator = getPageNavigator;
    this.getPageGrid = getPageGrid;
    this.decorateGrid = decorateGrid;
    this.getPlainOldGrid = getPlainOldGrid;
    
    this.pageNav_change = pageNav_change;
    this.newPage_add = newPage_add;
    this.page_delete = page_delete;
    this.page_next = page_next;
    this.page_previous = page_previous;
    this.page_first = page_first;
    this.page_last = page_last;
    
    var _pageAttachPoint = null;
    this.PageAttachPoint = _pageAttachPoint;
    
    var _divForPageCursor = null;
    this.divForPageCursor = _divForPageCursor;
         
    this.loadPageCursor   = loadPageCursor;
    this.loadPageName     = loadPageName;
    
    this.displayPageChange = displayPageChange;
         
    var _thePageElementEditor = null;
    this.ThePageElementEditor = _thePageElementEditor;
    
    this.getEnlargeShrinkGridControl = getEnlargeShrinkGridControl;
    this.getPageGridPanel = getPageGridPanel;
   // this.getDebugPanel = getDebugPanel;
    this.Grid_ContractExpand = Grid_ContractExpand;
    
    this.getPageNavList = getPageNavList;
    this.refreshPageNavList = refreshPageNavList;
    this.loadCurrPage = loadCurrPage;
    
    this.save_page_name_clicked = save_page_name_clicked;
    this.save_page_name = save_page_name;
    this.save_page_name_cancel = save_page_name_cancel;
    var _lastGoodPageName = null;
    this.lastGoodPageName = _lastGoodPageName;
    
    this.showJason = showJason;
    this.log     = debugLog;
    
    this.getSnippet = getSnippet;
    this.updatePageInfo = updatePageInfo;
    
    
}


function loadCurrPage()
{
    TheUte().removeChildren(this.PageAttachPoint);
    
    var bottomGridValues = new Array();
    bottomGridValues[0]   = this.getPageGridPanel();
    bottomGridValues[1]   = this.ThePageElementEditor.container.gridTable;
    var oBottomGrid       = newGrid2("oBottomGrid",1,2,bottomGridValues,1);
    oBottomGrid.init(oBottomGrid);
    
    this.PageAttachPoint.appendChild( oBottomGrid.gridTable );
}



function initStoryEditor(storyController)
{

    this.StoryController = storyController;
    this.ThePageElementEditor = new PageElementEditor();
    this.ThePageElementEditor.init(); 
    
    
    var divPageAttachPoint = document.createElement("div");
    this.PageAttachPoint = divPageAttachPoint;
    
    if(storyController.CurrentStory.PageCount == 0)
    {
        alert("There are no pages yet for this story.  \r\n \r\n \t PLEASE \r\n\r\n ADD one by clicking on the red button with the plus (+) sign.  \r\n\r\n\r\n \t ===:) ");

    }
    else
    {
        //there must be pages so go to first page
        //$TODO user bookmarks!!!!
        storyController.CurrentPageCursor = 0;
        this.loadCurrPage();
    }
    

    var values = new Array();
    values[0] = this.getStoryHeader();
    values[1] = this.getPageNavigator();
    values[2] = this.PageAttachPoint;
    //values[3] = this.getDebugPanel();

    var oGrid2 = newGrid2("storyEditorMainGrid",4,1,values,2);
    oGrid2.init( oGrid2 );

    this.container = oGrid2.gridTable;

}

function getStoryHeader()
{

    var values = new Array();
    
     var viewLink = document.createElement("a");
     viewLink.href = "Story.aspx?StoryID=" + this.StoryController.CurrentStory.ID;
     
     var propertiesLink = document.createElement("a");
     propertiesLink.href = "StoryProperties.aspx?StoryID=" + this.StoryController.CurrentStory.ID;
     
     var viewDiv = document.createElement("div");
     var shareDiv = document.createElement("div");
     var propertiesDiv = document.createElement("div");
     
     viewDiv.appendChild(viewLink);
     viewDiv.className="clsSmallLink";
     
     propertiesDiv.appendChild(propertiesLink);
     propertiesDiv.className="clsSmallLink";
     propertiesDiv.title = "Edit the name and description of this story";
     var propertiesLinkText = document.createTextNode(" ( properties ) ");
     propertiesLink.appendChild(propertiesLinkText);
     
     var viewLinkText = document.createTextNode("( view )");
     viewLink.appendChild(viewLinkText);
     
     var shareLink = document.createElement("a");
     shareLink.href = "ShareStory.aspx?StoryID=" + this.StoryController.CurrentStory.ID;
     
     shareDiv.appendChild(shareLink);
     shareDiv.className="clsSmallLink";
     shareDiv.title = " edit this stories sharing permissions";
     
     var shareLinkText = document.createTextNode("( sharing )");
     shareLink.appendChild(shareLinkText);

    values[0] = document.createTextNode("Story ["+ this.StoryController.CurrentStory.ID +"] Name");
    values[1] = document.createTextNode( TheUte().decode64( this.StoryController.CurrentStory.Title) );
    values[2] = viewDiv;
    values[3] = shareDiv;
    values[4] = propertiesDiv;
    
    
    
    var oGrid2 = newGrid2("storyHeaderGrid",1,5,values);
    oGrid2.init( oGrid2 );
    return oGrid2.gridTable;
   
   
}

function getEnlargeShrinkGridControl()
{

    var enlargeShrinkWidgetContainerValues = new Array();
    enlargeShrinkWidgetContainerValues[0] = TheUte().getButton("cmdContract","<","shrink page grid proportionally",this.Grid_ContractExpand,"clsButtonAction");
    enlargeShrinkWidgetContainerValues[3] = TheUte().getButton("cmdExpand",">","enlarge page grid proportionally",  this.Grid_ContractExpand  ,"clsButtonAction");
   
    var enlargeShrinkWidgetContainer = newGrid2("enlargeShrinkWidgetContainer",2,2,enlargeShrinkWidgetContainerValues);
    enlargeShrinkWidgetContainer.init( enlargeShrinkWidgetContainer );
    
    return enlargeShrinkWidgetContainer.gridTable;


}

function getDebugPanel()
{
    var dateToDisplay = "";
    try
    {
        var dateobj = new Date();
        dateToDisplay = dateobj;
    }
    catch(e)
    {
        alert("date issues please report this bug " + e.description);
        dateToDisplay = e.description;
    }
    

    var values = new Array();
    values[0] = TheUte().getButton("cmdShowJason","show story JSON","", this.showJason ,"clsButtonDebug");
    values[1] = TheUte().getTextArea("Bukabits $> Page loaded at your local time: " + dateToDisplay + "","txtDebug",null,null,"clsTextBox");
   
    var oGrid2 = newGrid2("debugPanel",1,2,values);
    oGrid2.init( oGrid2 );
    return oGrid2.gridTable;
}

function showJason()
{
    var json = oStoryEditor22.StoryController.CurrentStory.toJSONString();
    oStoryEditor22.log( json );
}

function debugLog(msg)
{
    var debugLogTxtArea = TheUte().findElement("txtDebug","textarea");
    debugLogTxtArea.value = msg; // + "\r\n" + debugLogTxtArea.value;
}

function getPageGridPanel()
{
    var values = new Array();
    values[0] = this.getPageGrid();
    values[1] = this.getEnlargeShrinkGridControl();
    var oGrid2 = newGrid2("gridPageGridPanel",1,2,values);
    oGrid2.init( oGrid2 );
    return oGrid2.gridTable;
}


function getPageGrid()
{
    var cols = this.StoryController.GetPage(this.StoryController.CurrentPageCursor).gridCols;
    var rows = this.StoryController.GetPage(this.StoryController.CurrentPageCursor).gridRows;
    return getPlainOldGrid(rows,cols,this.ThePageElementEditor.pageElement_Selected,this.ThePageElementEditor.pageElement_Blur).gridTable;
}


function getPlainOldGrid(rows, cols, gridCellSelectedCallback,gridCellBlurCallback)
{
    var values = new Array();
    var numberOfCells = rows * cols;
    var i = 0;
    var xy = "";
    
    var colCursor = 0;
    var rowCursor = 1;
    
    for(i = 0;i<numberOfCells;i++)
    {
        if(colCursor == cols)
        {
            colCursor = 1;
            rowCursor++;
        }
        else
        {
            colCursor++;
        }
        
        xy = colCursor + "~" + rowCursor;
        var te = TheUte().getTextArea( "" ,"storyCell_" + xy,gridCellSelectedCallback,gridCellBlurCallback,"clsStoryCell");
        values[i] = te;
    }
    
    var pageGrid = newGrid2("pageGrid",rows,cols,values);
    pageGrid.init( pageGrid );
    return pageGrid;
}


function displayPageChange()
{
   oStoryEditor2.loadCurrPage();
   oStoryEditor2.loadPageCursor();
   oStoryEditor2.loadPageName();
   oStoryEditor2.decorateGrid();
}


function pageNav_change()
{
    var nPageCursor = ( this.value * 1 ) -1;
    var prevCursor = oStoryEditor2.StoryController.CurrentPageCursor;
    oStoryEditor2.StoryController.CurrentPageCursor = nPageCursor;
    oStoryEditor2.displayPageChange();
    this.selectedIndex = 0;
}

function page_next()
{
   // alert(oStoryEditor2.StoryController.CurrentStory.PageCount + " :: " + oStoryEditor2.StoryController.CurrentPageCursor);
    //get curr page
    if( oStoryEditor2.StoryController.CurrentPageCursor == oStoryEditor2.StoryController.CurrentStory.PageCount - 1)
    {   //reset
        oStoryEditor2.StoryController.CurrentPageCursor = 0;
    }
    else
    {
        oStoryEditor2.StoryController.CurrentPageCursor++;
    }

   oStoryEditor2.displayPageChange();
}
function page_previous()
{
    //get curr page
    if( oStoryEditor2.StoryController.CurrentPageCursor == 0)
    {   //reset
        oStoryEditor2.StoryController.CurrentPageCursor = oStoryEditor2.StoryController.CurrentStory.PageCount - 1;
    }
    else
    {
        oStoryEditor2.StoryController.CurrentPageCursor--;
    }

   oStoryEditor2.displayPageChange();

}

function page_last()
{
   oStoryEditor2.StoryController.CurrentPageCursor = oStoryEditor2.StoryController.CurrentStory.PageCount - 1;
  
   oStoryEditor2.displayPageChange();
}

function page_first()
{
   oStoryEditor2.StoryController.CurrentPageCursor = 0;
  
   oStoryEditor2.displayPageChange();
}

function page_delete()
{

    var currPageCursor = oStoryEditor2.StoryController.CurrentPageCursor;

    var p = oStoryEditor2.StoryController.GetPage( currPageCursor );
    if(p != null)
    {
         var sName = TheUte().decode64( p.Name );
    
    }
    var res = confirm("are you sure you want to delete page - " + sName);
    if(res)
    {
    
       var macroCreateNewPage = newMacro("RemovePage");
       addParam( macroCreateNewPage,"StoryID",oStoryEditor2.StoryController.CurrentStory.ID );
       addParam( macroCreateNewPage,"PageID",p.ID );
       
      // alert( serializeMacroForRequest( macroCreateNewPage) );
       
       processRequest( macroCreateNewPage );
    }
    


}

function newPage_add()
{
    var pageName = window.prompt("enter page name","");
    if(pageName != null)
    {
        var xy = window.prompt("enter grid dimensions eg rows x cols 5x5 ","5 5");
        if(xy ==null)
            xy = "3 3";
        
        var dimsArray = xy.split(' ');
        
        var x = dimsArray[0]*1;
        var y = dimsArray[1]*1;
        
      pageName = TheUte().encode64( TheUte().filterText( pageName ) );


       //if connected to inet, update db?
       var macroCreateNewPage = newMacro("CreateNewPage");
       addParam( macroCreateNewPage,"pageName",pageName);
       addParam( macroCreateNewPage,"gridCols",y);
       addParam( macroCreateNewPage,"gridRows",x);
       addParam( macroCreateNewPage,"StoryID",oStoryEditor2.StoryController.CurrentStory.ID );
       addParam( macroCreateNewPage,"StoryOpenedBy",oStoryEditor2.StoryController.CurrentStory.StoryOpenedBy );
       
      // alert( serializeMacroForRequest( macroCreateNewPage) );
       
       processRequest( macroCreateNewPage );
    }
}
function loadPageName()
{
    TheUte().removeChildren ( this.divForPageName );
    var divName = document.createElement("div");
    
    var currPageCursor = this.StoryController.CurrentPageCursor;
   // alert("about ot laod page " + currPageCursor);
    
    var p = this.StoryController.GetPage( currPageCursor );
    if(p != null)
    {
         var sName = TheUte().decode64( p.Name );
         var txtName = document.createTextNode( sName );
         divName.appendChild(txtName);
         
         var values = new Array();
         values.push( TheUte().getInputBox( sName ,"txtPageName",this.save_page_name_clicked,null,"clsPageNameInput","click here to edit page name") );
         values.push( TheUte().getButton("cmdSavePageNameEdit","save","save page name",this.save_page_name,"clsButtonActionHidden") );
         values.push( TheUte().getButton("cmdCancelPageNameEdit","cancel","cancel this edit",this.save_page_name_cancel,"clsButtonCancelHidden") );
         
        var oGrid2 = newGrid2("pageNameWidget",1,3,values);
        oGrid2.init( oGrid2 );

         
         
         this.divForPageName.appendChild( oGrid2.gridTable );
    }
   
}

function save_page_name_clicked()
{
    var txtPageName = TheUte().findElement("txtPageName","input");
    oStoryEditor2.lastGoodPageName = txtPageName.value;
    txtPageName.className = "clsPageNameInputEdit";
    
    var cmdSavePageNameEdit = TheUte().findElement("cmdSavePageNameEdit","input");
    cmdSavePageNameEdit.style.display = "inline";
    var cmdCancelPageNameEdit = TheUte().findElement("cmdCancelPageNameEdit","input");
    cmdCancelPageNameEdit.style.display = "inline";
}

function save_page_name_cancel()
{
    var txtPageName = TheUte().findElement("txtPageName","input");
    txtPageName.value =  oStoryEditor2.lastGoodPageName;
    txtPageName.className = "clsPageNameInput";
    
    var cmdSavePageNameEdit = TheUte().findElement("cmdSavePageNameEdit","input");
    cmdSavePageNameEdit.style.display = "none";
    var cmdCancelPageNameEdit = TheUte().findElement("cmdCancelPageNameEdit","input");
    cmdCancelPageNameEdit.style.display = "none";
}

function save_page_name()
{
    var txtPageName = TheUte().findElement("txtPageName","input");

    
    var pageName = txtPageName.value;
    
    pageName = TheUte().encode64( TheUte().filterText( pageName ) );

    var currPage = oStoryEditor2.StoryController.GetPage(oStoryEditor2.StoryController.CurrentPageCursor);
    oStoryEditor2.updatePageInfo(currPage.ID,currPage.gridCols,currPage.gridRows,oStoryEditor2.StoryController.CurrentStory.ID,pageName );


    this.style.display = "none";
    var cmdCancelPageNameEdit = TheUte().findElement("cmdCancelPageNameEdit","input");
    cmdCancelPageNameEdit.style.display = "none";
    
    txtPageName.className = "clsPageNameInput";
}


function loadPageCursor()
{
    TheUte().removeChildren ( this.divForPageCursor );
    var divCursor = document.createElement("div");
    var txtCursor = document.createTextNode( this.StoryController.CurrentPageCursor + 1 );
    divCursor.appendChild(txtCursor);
    this.divForPageCursor.appendChild( divCursor );
    
}
function refreshPageNavList()
{
    var divToRefresh = TheUte().findElement("divForPageSelBox","div");
    TheUte().removeChildren( divToRefresh );
    
    var pageNavList = oStoryEditor2.getPageNavList();
    if(pageNavList != null)
        divToRefresh.appendChild( pageNavList );
}


function getPageNavList()
{

    var options = new Array();
    options[0] = " goto ";
    for( var i = 0; i < this.StoryController.CurrentStory.PageCount; i++)
    {
        //alert("looking up page " + i);
        var currPage = this.StoryController.GetPage(i);
       // alert(TheUte().decode64(currPage.Name) + " ::* " + currPage.PageElementMapCount );
        
        if(currPage == null)
        {
          alert("Page " + i + " not found");
          return;
         }
        // alert(options);
        options[i+1] = TheUte().decode64( currPage.Name ) + " (" + currPage.PageElementMapCount + ")";
    }
    
    var oSelBox = TheUte().getSelect2("selPageNavigator",options,this.pageNav_change);
   
    return oSelBox;

}

function getPageNavigator()
{
    var values = new Array();
    
    values[0] = TheUte().getButton("cmdFirstPage","<<","go to first page",this.page_first,"clsButton");
    values[1] = TheUte().getButton("cmdPreviousPage","<","go to previous page",this.page_previous,"clsButton");
    values[2] = document.createTextNode(" page ");
    this.divForPageCursor = document.createElement("div");
    this.loadPageCursor();
    values[3] = this.divForPageCursor;
    values[4] = TheUte().getButton("cmdNextPage",">","go to next page",this.page_next,"clsButton");
    values[5] = TheUte().getButton("cmdLastPage",">>","go to last page",this.page_last,"clsButton");
    
    
    var divForPageSelBox = document.createElement("div");
    divForPageSelBox.id = "divForPageSelBox";
    
    var pageNavList = this.getPageNavList();
    if( pageNavList != null )
        divForPageSelBox.appendChild( pageNavList );
    
    values[6] = divForPageSelBox;
    
    this.divForPageName = document.createElement("div");
    this.loadPageName();
    
    values[7] = this.divForPageName;
    values[8] = TheUte().getButton("cmdAddNewPage","+","add new page",this.newPage_add,"clsButton");
    values[9] = TheUte().getButton("cmdDeletePage","-","delete this page",this.page_delete,"clsButton");
    

    
    var oGrid2 = newGrid2("storyMainGrid",1,10,values);
    oGrid2.init( oGrid2 );
    return oGrid2.gridTable;
}



function Grid_ContractExpand()
{
    //alert(this.id);
    //gridPageGridPanel.gridCell.0.0
    
    var gridContainer = TheUte().findElement("gridPageGridPanel.gridCell.0.0","div");
   
    TheUte().removeChildren(gridContainer);

    //get the current size of the grid
    var thePage = oStoryEditor2.StoryController.GetPage(oStoryEditor2.StoryController.CurrentPageCursor);
	var rows = thePage.gridRows;
	var cols = thePage.gridCols;
	
	//alert(rows + " - " + cols);
	rows = rows * 1;
	cols = cols * 1;
	
	
	if(this.id == "cmdContract")
	{
	    rows = rows - 1;
	    cols = cols - 1;
	}
	else
	{
	    rows = rows + 1;
	    cols = cols + 1;	
	}
	
	thePage.gridRows = rows;
	thePage.gridCols = cols;
	
	//load new grid
	var newGrid = oStoryEditor2.getPageGrid();
	if( newGrid != null)
	{
	    gridContainer.appendChild( newGrid );
	  // alert("update this page " + thePage.Name + " with new grid dimensions " + rows + " x " + cols);
	   
       var currPage = oStoryEditor2.StoryController.GetPage(oStoryEditor2.StoryController.CurrentPageCursor);

       oStoryEditor2.updatePageInfo(currPage.ID,cols,rows,oStoryEditor2.StoryController.CurrentStory.ID,currPage.Name);
	   
	    //repaint grid
	    //done via callback command.
	}
	else
	{
	    alert("invalid grid");
	}

}

function updatePageInfo(pageID,rows,cols,storyID,pageName)
{

       var macroUpdatePage = newMacro("UpdatePage");
       addParam( macroUpdatePage,"PageID",pageID );
       addParam( macroUpdatePage,"GridX",cols );
       addParam( macroUpdatePage,"GridY",rows );
       addParam( macroUpdatePage,"StoryID",storyID);
       addParam( macroUpdatePage,"PageName",pageName );
       
       //alert( serializeMacroForRequest( macroUpdatePage) );
       processRequest( macroUpdatePage );  	   

}



//decorateGrid with PageElements
function decorateGrid()
{

    var currPage = this.StoryController.GetPage(oStoryEditor2.StoryController.CurrentPageCursor);
    if(currPage == null) return;
    
    var cols = currPage.gridCols;
    var rows = currPage.gridRows;
    var numberOfCells = cols * rows;
    
    var rowCursor = 1;
    var colCursor = 0;
    
    for(var i = 0; i<numberOfCells;i++)
    {
        if(colCursor == cols)
        {
            colCursor = 1;
            rowCursor++;
        }
        else
        {
            colCursor++;
        }
       
        var pe = this.StoryController.FindPageElementByCoord(oStoryEditor2.StoryController.CurrentStory,currPage,colCursor,rowCursor);
   
        if(pe != null)
        {
            //color grid cell
            //storyCell_1~1
            var key = "storyCell_" + colCursor + "~" + rowCursor;
            var elem = TheUte().findElement(key,"textarea");
            elem.value = pe.Type;
            elem.title = TheUte().decode64(pe.Value);
            // elem.title = pe.Value;
            //elem.className = "clsStoryCellSelected";
            elem.style.backgroundColor = "black";
            elem.style.color = "#E1EC32";
            
        }
     }
}

function getSnippet(index)
{
    var snippet = "uninitialized";
    
    switch(index)
    {
        case 1:                 //text
            snippet="<div style='background-color:white ; color:black ; width:200px;font-size:10pt;'>\r\n \r\n yourContentHere \r\n\r\n</div>";             
            break;
        
        case 2:                 //audio
            snippet="";
            break;
        
        case 3:                 //video
        
            var youtube = "<object width='425' height='350'><param name='movie' value='http://www.youtube.com/v/XFnK0mNMKjk'></param><param name='wmode' value='transparent'></param><embed src='http://www.youtube.com/v/XFnK0mNMKjk' type='application/x-shockwave-flash' wmode='transparent' width='425' height='350'></embed></object>";
            snippet=youtube;             
            break;
        
        case 4:                 //image
            snippet="<img src=' ImageUrlHere ' >"
            break;
        
        case 5:                 //random
            snippet="<pre> *** </pre>"
            break;
    }

    
    
    
    return snippet;
}

//////////////////////
//
//   StoryEditor
//
//   end
//
///////////////////////

//////////////////////
//
//   PageElementEditor
//
//   begin
//
///////////////////////





function PageElementEditor()
{

    var _container = null;
    this.container = _container;
    
    this.init = initPageElementEditor;
    
    var _bCommited = false;
    this.bCommited = _bCommited;
    
    var _type = "unitialized";
    this.Type = _type;
    
    var _typeID = -1;
    this.TypeID = _typeID;

    var _txtMsg = null;
    this.msgBox = _txtMsg;
    
    var _tagBox = null;
    this.tagBox = _tagBox;
    
    var _prevSelectedTypeIndex = 0;
    this.prevSelectedTypeIndex = _prevSelectedTypeIndex;
    
    var _divPageElementCoord = null;
    this.divPageElementCoord = _divPageElementCoord;
    var _divPageElementType = null;
    this.divPageElementType = _divPageElementType;    
    var _divPreview = null;
    this.divPreview = _divPreview;
    
    var _currSelPageElement = null;
    this.CurrentPageElement = _currSelPageElement;
    
    var _currGridX = -1;
    var _currGridY = -1;
    this.CurrGridX  = _currGridX;
    this.CurrGridY  = _currGridY;
    
    var _mediaOptions = new Array();
    this.mediaOptions = _mediaOptions;
    
    this.getPageElementEditorControlBar = getPageElementEditorControlBar;
    this.getMediaTypeChoices = getMediaTypeChoices;
    this.changeDisplayOfType = changeDisplayOfType;
    this.changeDisplayOfCoord = changeDisplayOfCoord;
    
    this.pageElement_Selected = pageElement_Selected; 
    this.pageElement_Blur = pageElement_Blur;
    
    this.mediaTypeChoice_change = mediaTypeChoice_change;
    this.PageElement_Update = PageElement_Update;
    this.PageElement_Preview = PageElement_Preview;
    
    this.txtCell_Content_Clicked = txtCell_Content_Clicked;
    this.ContentEditor_Clear = ContentEditor_Clear;
    this.changeMediaOptions = changeMediaOptions;
    
    
}

 
   
    
function initPageElementEditor()
{
   //$to do: load from db
    this.mediaOptions[0] = "<- change type";
    this.mediaOptions[1] = "TEXT";
    this.mediaOptions[2] = "AUDIO";
    this.mediaOptions[3] = "VIDEO";
    this.mediaOptions[4] = "IMAGE";
    this.mediaOptions[5] = "PREFORMATTED";


    this.Type = "nil";

 //render PageElementEditor 
    var values = new Array();

    values[0] = this.getPageElementEditorControlBar().gridTable;

    
    var divCellContentHeader = document.createElement("div");
    divCellContentHeader.className = "clsContentHeader";
    divCellContentHeader.appendChild( document.createTextNode("cell content") );
    
    values[1] = divCellContentHeader;
    
    this.msgBox = TheUte().getTextArea("","txtMsg",this.txtCell_Content_Clicked,null,"clsTextBox");
    values[2] = this.msgBox;
    
    var divCellTagHeader = document.createElement("div");
    divCellTagHeader.className = "clsTagsHeader";
    divCellTagHeader.appendChild( document.createTextNode("tags ( separated by spaces, no quotes for now. )") );
    values[3] = divCellTagHeader;
    this.tagBox = TheUte().getTextArea("","txtTags",null,null,"clsTagBox");
    values[4] = this.tagBox;
    
    this.divPreview = document.createElement("div");
    values[5] = this.divPreview;

    var oGrid2 = newGrid2("PageElementEditor",6,1,values,3);
    oGrid2.init( oGrid2 );
    
    this.container = oGrid2;
    
}

function txtCell_Content_Clicked()
{
    
    if(this.value.trim().length == 0)
    {
        this.value = "";
        //default action to make this of type text
        oStoryEditor2.ThePageElementEditor.changeMediaOptions(1);
        
    }
}

function PageElement_Update()
{


    
    var currPageElem = oStoryEditor2.ThePageElementEditor.CurrentPageElement;
    var posX = oStoryEditor2.ThePageElementEditor.CurrGridX ;
    var posY = oStoryEditor2.ThePageElementEditor.CurrGridY ;

    var sContentVal = oStoryEditor2.ThePageElementEditor.msgBox.value;
    var sContentValFiltered = TheUte().filterText( sContentVal );
    if( sContentValFiltered != sContentVal)
    {
        alert("some characters were changed as they not recognized as ASCII.");
        oStoryEditor2.ThePageElementEditor.msgBox.value = sContentValFiltered;
    }
    
    
    var sContentVal64 = TheUte().encode64( sContentValFiltered );
   
    
    if(posX == -1 || posY == -1)
    {
        alert("Please select a cell in the grid first");
        return;
    }

    if( oStoryEditor2.ThePageElementEditor.TypeID == -1 )
    {
       //set to be text
        oStoryEditor2.ThePageElementEditor.changeMediaOptions(1);
    }
    

    if( currPageElem == null )
    {

        
        
        
       var currPage = oStoryEditor2.StoryController.GetPage(oStoryEditor2.StoryController.CurrentPageCursor);

       var macroCreateNewPageElementAndMap = newMacro("CreateNewPageElementAndMap");
       addParam( macroCreateNewPageElementAndMap,"CurrentPageCursor",oStoryEditor2.StoryController.CurrentPageCursor);
       addParam( macroCreateNewPageElementAndMap,"PageID",currPage.ID);
       addParam( macroCreateNewPageElementAndMap,"GridX",posX);
       addParam( macroCreateNewPageElementAndMap,"GridY",posY);
       addParam( macroCreateNewPageElementAndMap,"Value",sContentVal64);
       addParam( macroCreateNewPageElementAndMap,"tags",oStoryEditor2.ThePageElementEditor.tagBox.value);
       addParam( macroCreateNewPageElementAndMap,"TypeID",oStoryEditor2.ThePageElementEditor.TypeID );
       addParam( macroCreateNewPageElementAndMap,"StoryID",oStoryEditor2.StoryController.CurrentStory.ID );
       addParam( macroCreateNewPageElementAndMap,"StoryOpenedBy",oStoryEditor2.StoryController.CurrentStory.StoryOpenedBy );
       
       //alert( serializeMacroForRequest( macroCreateNewPageElementAndMap) );
       processRequest( macroCreateNewPageElementAndMap );  

       
    }
    else
    {
       //update existing page element to page on server db
       var macroUpdatePageElementAndMap = newMacro("UpdatePageElement");
       addParam( macroUpdatePageElementAndMap,"PageElementID",currPageElem.ID);
       addParam( macroUpdatePageElementAndMap,"GridX",posX);
       addParam( macroUpdatePageElementAndMap,"GridY",posY);
       addParam( macroUpdatePageElementAndMap,"StoryID",oStoryEditor2.StoryController.CurrentStory.ID);
       addParam( macroUpdatePageElementAndMap,"Value",sContentVal64);
       addParam( macroUpdatePageElementAndMap,"tags",oStoryEditor2.ThePageElementEditor.tagBox.value);
       addParam( macroUpdatePageElementAndMap,"TypeID",oStoryEditor2.ThePageElementEditor.TypeID );
       addParam( macroUpdatePageElementAndMap,"StoryOpenedBy",oStoryEditor2.StoryController.CurrentStory.StoryOpenedBy );
      
       //alert( serializeMacroForRequest( macroUpdatePageElementAndMap) );
       processRequest( macroUpdatePageElementAndMap );  
    }
    

    
   // alert(oStoryEditor2.StoryController.CurrentStory.toJSONString() );
}
function PageElement_Preview()
{
    oStoryEditor2.ThePageElementEditor.divPreview.innerHTML = "&nbsp;";
    oStoryEditor2.ThePageElementEditor.divPreview.innerHTML = oStoryEditor2.ThePageElementEditor.msgBox.value;
}

function ContentEditor_Clear()
{
    oStoryEditor2.ThePageElementEditor.msgBox.value = "";
    oStoryEditor2.ThePageElementEditor.divPreview.innerHTML = "&nbsp;";
}

function mediaTypeChoice_change()
{

   var indexMediaOptions = this.value;

    oStoryEditor2.ThePageElementEditor.changeMediaOptions( indexMediaOptions );
    this.selectedIndex = 0;
}

function getMediaTypeChoices()
{
    var oSelBox = TheUte().getSelect2("selMediaTypeChoice",this.mediaOptions,this.mediaTypeChoice_change);
    return oSelBox;
   
}

function changeMediaOptions(indx)
{
    var indexMediaOptions = indx *1;
    oStoryEditor2.ThePageElementEditor.TypeID = indexMediaOptions;
    oStoryEditor2.ThePageElementEditor.Type = oStoryEditor2.ThePageElementEditor.mediaOptions[ indexMediaOptions ];
    oStoryEditor2.ThePageElementEditor.changeDisplayOfType( oStoryEditor2.ThePageElementEditor.Type );
    
    var cleanSnippetText = oStoryEditor2.getSnippet(indexMediaOptions);
    var prevSnippetText = null;
    
    oStoryEditor2.getSnippet(oStoryEditor2.ThePageElementEditor.prevSelectedTypeIndex);//this.
    var msgBoxText = oStoryEditor2.ThePageElementEditor.msgBox.value.trim();
    
    if( msgBoxText.length == 0 )
    {
        oStoryEditor2.ThePageElementEditor.msgBox.value = cleanSnippetText;
    }
    else
    {
        //there was stuff in the box, has it changed?
        if(oStoryEditor2.ThePageElementEditor.prevSelectedTypeIndex > 0)
        {
            prevSnippetText = oStoryEditor2.getSnippet(oStoryEditor2.ThePageElementEditor.prevSelectedTypeIndex);
            if(prevSnippetText == oStoryEditor2.ThePageElementEditor.msgBox.value.trim())
            {
                //no change, so can overwrite information
                oStoryEditor2.ThePageElementEditor.msgBox.value = cleanSnippetText;
                
            }
        }
    }
    
    oStoryEditor2.ThePageElementEditor.prevSelectedTypeIndex = indexMediaOptions;
}

function getPageElementEditorControlBar()
{
    var values = new Array();
    
    this.divPageElementCoord = document.createElement("div");
    values[0] = this.divPageElementCoord;
    this.divPageElementType = document.createElement("div");
    values[1] = this.divPageElementType;    
    values[2] = this.getMediaTypeChoices();
    values[3] = TheUte().getButton("cmdPreviewPageElement","preview","preview page element",  this.PageElement_Preview  ,"clsButtonAction");
    values[4] = TheUte().getButton("cmdUpdatePageElement","update","update page element",  this.PageElement_Update  ,"clsButtonAction");
    values[5] = TheUte().getButton("cmdClear","clear editor","",  this.ContentEditor_Clear  ,"clsButtonCancel");
    
    
    var oGrid2 = newGrid2("PageElementEditorControlBar",1,6,values);
    oGrid2.init( oGrid2 );
    
    return oGrid2;
}

function pageElement_Selected()
{
   
   var storyCellIDbits = this.id.split('_');
   var cartesianBits = storyCellIDbits[1].split('~');
   var posX = cartesianBits[0] * 1;
   var posY = cartesianBits[1] * 1;
   
   
    oStoryEditor2.ThePageElementEditor.CurrGridX = posX;
    oStoryEditor2.ThePageElementEditor.CurrGridY = posY;
    //alert(oStoryEditor2.ThePageElementEditor.msgBox.value);
    if( oStoryEditor2.ThePageElementEditor.msgBox.value.trim().length == 0 || oStoryEditor2.ThePageElementEditor.bCommited == true )
    {
        oStoryEditor2.ThePageElementEditor.msgBox.value = "";
        oStoryEditor2.ThePageElementEditor.tagBox.value = "";
        oStoryEditor2.ThePageElementEditor.divPreview.innerHTML = "<DIV></DIV>";
        oStoryEditor2.ThePageElementEditor.bCommited = false;
    }
    this.style.backgroundColor = "#DFFC19";
    this.style.color ="black";
  
  
   //look up in PageElementMap to see if there is an entry for this cell?
   var page = null;
   page = oStoryEditor2.StoryController.GetPage(oStoryEditor2.StoryController.CurrentPageCursor);
   var pe = oStoryEditor2.StoryController.FindPageElementByCoord(oStoryEditor2.StoryController.CurrentStory,page, posX,posY);

   var sType = null;
   
   if(pe != null)
   {
       oStoryEditor2.ThePageElementEditor.CurrentPageElement = pe;
       
    
       oStoryEditor2.ThePageElementEditor.msgBox.value = TheUte().decode64(pe.Value);
      
       
       oStoryEditor2.ThePageElementEditor.tagBox.value = pe.Tags;
       oStoryEditor2.ThePageElementEditor.divPreview.innerHTML = TheUte().decode64(pe.Value);
       sType = pe.Type;
       oStoryEditor2.ThePageElementEditor.Type = pe.Type;
       oStoryEditor2.ThePageElementEditor.TypeID = pe.TypeID;
       
   }
   else
   {
        //no page element set to null
        oStoryEditor2.ThePageElementEditor.CurrentPageElement = null;
        oStoryEditor2.ThePageElementEditor.Type = "nil";
        oStoryEditor2.ThePageElementEditor.TypeID = -1;
        
        sType = "nil";
   }

    oStoryEditor2.ThePageElementEditor.changeDisplayOfType(sType);
    oStoryEditor2.ThePageElementEditor.changeDisplayOfCoord(oStoryEditor2.ThePageElementEditor.CurrGridX, oStoryEditor2.ThePageElementEditor.CurrGridY);


}

function changeDisplayOfType(msg)
{
   var txtDiv = document.createElement("div");
   txtDiv.id = "divTxt";
   txtDiv.className = "clsPageElementIndicator";
   var txt = document.createTextNode( msg );
   txtDiv.appendChild(txt);
   
   TheUte().removeChildren( oStoryEditor2.ThePageElementEditor.divPageElementType );
   oStoryEditor2.ThePageElementEditor.divPageElementType.appendChild( txtDiv );
}

function changeDisplayOfCoord(x,y)
{
   var txtDiv = document.createElement("div");
   txtDiv.id = "divTxt";
   txtDiv.className = "clsPageElementIndicator";
   var s = x + "." + y;
   txtDiv.title = "Page Element Navigator x.y coordinates " + s;
   var txt = document.createTextNode( s );
   txtDiv.appendChild(txt);
   
   TheUte().removeChildren( oStoryEditor2.ThePageElementEditor.divPageElementCoord  );
   oStoryEditor2.ThePageElementEditor.divPageElementCoord.appendChild( txtDiv );
}
function pageElement_Blur()
{
  //  oStoryEditor2.ThePageElementEditor.msgBox.value = "";
  //  alert( oStoryEditor2.ThePageElementEditor.divPageElementType.innerHTML );
  //  TheUte().removeChildren( oStoryEditor2.ThePageElementEditor.divPageElementType );
 //  this.style.backgroundColor = "yellow";
}

//////////////////////
//
//   PageElementEditor
//
//   end
//
///////////////////////