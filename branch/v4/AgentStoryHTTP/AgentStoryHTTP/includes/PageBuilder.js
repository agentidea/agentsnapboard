//PageBuilder prototypes

function flexGrid()
{

    
    var _numRows = 0;
    var _numCols = 0;
    
    this.numRows = _numRows;
    this.numCols = _numCols;

    this.Render = RenderGrid;
    this.expandGridEast = expandGridEast;
    this.DecorateGridCellsWithWidgets = DecorateGridCellsWithWidgets;
    this.StoryCellWidgetGrid = StoryCellWidgetGrid;

}



function StoryCellWidgetGrid(id)
{
    var values = new Array();
    values[0] = TheUte().getButton("cmdAddVideo_" + id,"v","add video",this.expandGridEast,"clsButtonAction");
    values[1] = TheUte().getButton("cmdAddText_" + id,"t","add text",this.expandGridEast,"clsButtonText");
    values[2] = TheUte().getButton("cmdAddImage_" + id,"i","add image",this.expandGridEast,"clsButtonAction");
    var oGrid2 = newGrid2("storyCellGrid_" + id,1,3,values);
    oGrid2.initializeGrid( oGrid2 );

    return oGrid2;
}




function DecorateGridCellsWithWidgets(grid)
{

    //TheUte().setGridCell("mainGrid",1,3,this.StoryCellWidgetGrid("tst").gridTable);
    
    
    
    
    

}


function RenderGrid(rows,cols,divAttachPoint)
{
    var myAttachPoint = TheUte().findElement("divPageGridAttachPoint","div");
    var grid = TheUte().newGrid("mainGrid",rows,cols,myAttachPoint);
    this.DecorateGridCellsWithWidgets();
}

function expandGridEast()
{
   // var v = window.event;
	// var srcElem = v.srcElement;
	alert("expand grid east " + this.id);
	
	
}

