
function StoryPropertyEditor(aoStoryController)
{

    var _storyController = aoStoryController;
    this.storyController = _storyController;

    this.init = StoryPropertyEditorInit;
    
    var _container = null;
    this.container = _container;
    
    this.getSPEheader     = getSPEheader;
    this.saveTitleAndDesc = saveTitleAndDesc;

    
}

function StoryPropertyEditorInit()
{
 
    var values = new Array();
    values[0] =  this.getSPEheader();
    var oGrid2 = newGrid2("storyPropertyEditorMainGrid",3,1,values);
    oGrid2.init( oGrid2 );

    this.container = oGrid2.gridTable;
  
}

function getSPEheader()
{
    var values = new Array();
    values[0] = document.createTextNode("Story Title");
    values[1] = TheUte().getInputBox(TheUte().decode64( this.storyController.CurrentStory.Title ),"txtStoryName",null,null,"clsInputField");
    values[2] = document.createTextNode("Story Description");
    values[3] = TheUte().getTextArea(TheUte().decode64( this.storyController.CurrentStory.Description ),"txtStoryDescription",null,null,"clsTextBox");
    
    values[5] = TheUte().getButton("cmd_SaveStory","Save","Save Story Title and Description",this.saveTitleAndDesc,"clsButtonAction");
    var oGrid2 = newGrid2("storyPropertyEditorHeader",3,2,values);
    oGrid2.init( oGrid2 );

    return oGrid2.gridTable;

}




function saveTitleAndDesc()
{
    
    var txtStoryName = TheUte().findElement("txtStoryName","input");
    var txtStoryDescription = TheUte().findElement("txtStoryDescription","textarea");
    
    //simple validation tbd
    
    var updateStoryMeta = newMacro("UpdateStoryMeta");
    addParam( updateStoryMeta,"txtStoryName"    ,TheUte().encode64( txtStoryName.value ));
    addParam( updateStoryMeta,"txtStoryDescription"        ,TheUte().encode64( txtStoryDescription.value ));
    addParam( updateStoryMeta,"StoryID"        ,oStoryPropertyEditor.storyController.CurrentStory.ID );
    
    //alert( serializeMacroForRequest( updateStoryMeta) );
    processRequest( updateStoryMeta );
    
}