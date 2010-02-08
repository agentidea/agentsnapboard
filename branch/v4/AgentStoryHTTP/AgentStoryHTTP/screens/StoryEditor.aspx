<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoryEditor.aspx.cs" Inherits="AgentStoryHTTP.screens.StoryEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>AgentStory - Story Editor</title>
     <link href="./../style/main.css" rel="stylesheet" type="text/css" />
    
    <!-- AgentStory JavaScript libraries -->
    <script src="./../includes/PageUtils.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/core.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/grid.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/json.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/StoryElements.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/StoryControllers.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/palette.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/StoryEditor.js" language="javascript" type="text/javascript"></script>
    <script src="./../includes/corecommands.js" language="javascript" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
    var oStoryEditor = null;

    function init()
    {
 
    //var storyJSON = "{'ID':48,'Title':'how noa came to be known as singer','StoryOpenedBy':25,'Description':'desc','Pages':{'page_0':{'Name':'applesPageeeeeee','GUID':null,'ID':0,'PageElementMaps':{'pageElementMap_1_2':{'PageElementID':12,'X':1,'Y':2},'pageElementMap_3_3':{'PageElementID':13,'X':3,'Y':3},'pageElementMap_2_3':{'PageElementID':14,'X':2,'Y':3},'pageElementMap_1_3':{'PageElementID':15,'X':1,'Y':3}},'PageElementMapCount':4,'gridCols':3,'gridRows':3},'page_1':{'Name':'beets','GUID':null,'ID':0,'PageElementMaps':{},'PageElementMapCount':0,'gridCols':5,'gridRows':5},'page_2':{'Name':'carrots','GUID':null,'ID':0,'PageElementMaps':{},'PageElementMapCount':0,'gridCols':5,'gridRows':7},'page_3':{'Name':'eggsPage','GUID':'6944476b-471c-447b-ba81-8d7411cfc361','ID':'60','PageElementMaps':{'pageElementMap_2_2':{'PageElementID':49,'X':2,'Y':2}},'PageElementMapCount':1,'gridCols':'5','gridRows':'5'}},'PageCount':4,'PageElements':{'pageElementItem_12':{'Type':'text','TypeID':1,'Value':'PGI+TG9uZWx5IEdpcmwhPC9iPg==','Tags':null,'ID':12},'pageElementItem_13':{'Type':'video','TypeID':3,'Value':'PG9iamVjdCB3aWR0aD0nNDI1JyBoZWlnaHQ9JzM1MCc+PHBhcmFtIG5hbWU9J21vdmllJyB2YWx1ZT0naHR0cDovL3d3dy55b3V0dWJlLmNvbS92L1hGbkswbU5NS2prJz48L3BhcmFtPjxwYXJhbSBuYW1lPSd3bW9kZScgdmFsdWU9J3RyYW5zcGFyZW50Jz48L3BhcmFtPjxlbWJlZCBzcmM9J2h0dHA6Ly93d3cueW91dHViZS5jb20vdi9YRm5LMG1OTUtqaycgdHlwZT0nYXBwbGljYXRpb24veC1zaG9ja3dhdmUtZmxhc2gnIHdtb2RlPSd0cmFuc3BhcmVudCcgd2lkdGg9JzQyNScgaGVpZ2h0PSczNTAnPjwvZW1iZWQ+PC9vYmplY3Q+','Tags':null,'ID':13},'pageElementItem_14':{'Type':'image','TypeID':4,'Value':'PGltZyBzcmM9J2h0dHA6Ly9waG90b2dyYXBoeS5hZ2VudGlkZWEuY29tL2ltYWdlcy9sb25lbHlHaXJsU2V0XzAwNF9UTi5qcGcnIC8+IA==','Tags':null,'ID':14},'pageElementItem_15':{'Type':'image','TypeID':4,'Value':'PGltZyBzcmM9J2h0dHA6Ly9waG90b2dyYXBoeS5hZ2VudGlkZWEuY29tL2ltYWdlcy9BbGxpc29uV2FnbmVyVHVtYmxlVHVybl9UTi5qcGcnIC8+IA==','Tags':null,'ID':15},'pageElementItem_49':{'Type':'TEXT','TypeID':1,'Value':'ZWdncw==','Tags':'eggytaggy','ID':49}},'PageElementCount':5}";
    
    var storyJSON = "<%= oStory.GetStoryJSON() %>";
    //load story by way of JSON
    eval(" var story = " + storyJSON + ";");
   

    
       var storyController = newStoryController(story);
      

     //load story programatically
     // var story = newStory(<%= CurrUserID %>,<%= oStory.ID %>,'<%= oStory.Title %>','desc');  
      /*
       var oPage = newPage("applesPageeeeeee",3,3);
       storyController.AddPage(oPage);
       
        storyController.AddPage( newPage("beets",5,5) );
        storyController.AddPage( newPage("carrots",7,5) );
       
        var pageElementID = 0;
        pageElementID = 12;
        var oPageElement = newPageElement(pageElementID,"text",1,TheUte().encode64("<b>Lonely Girl!</b>" ));
        storyController.AddPageElement( story, oPageElement );
 
        var vidURL = "<object width='425' height='350'><param name='movie' value='http://www.youtube.com/v/XFnK0mNMKjk'></param><param name='wmode' value='transparent'></param><embed src='http://www.youtube.com/v/XFnK0mNMKjk' type='application/x-shockwave-flash' wmode='transparent' width='425' height='350'></embed></object>";
        
        pageElementID = 13;
        var oPageElementV = newPageElement(pageElementID,"video",3,TheUte().encode64(vidURL) );
        storyController.AddPageElement( story, oPageElementV ); 
        
        pageElementID = 14;
        var oPageElementI = newPageElement(pageElementID,"image",4,TheUte().encode64("<img src='http://photography.agentidea.com/images/lonelyGirlSet_004_TN.jpg' /> " ));
        storyController.AddPageElement( story, oPageElementI );        
       
        pageElementID = 15;
        var oPageElementII = newPageElement(pageElementID,"image",4,TheUte().encode64("<img src='http://photography.agentidea.com/images/AllisonWagnerTumbleTurn_TN.jpg' /> "));
        storyController.AddPageElement( story, oPageElementII );      
        
        var oPageElementMap = newPageElementMap(12, 1,2);
        storyController.AddPageElementMap( oPage, oPageElementMap );
        var oPageElementMapV = newPageElementMap(13, 3,3);
        storyController.AddPageElementMap( oPage, oPageElementMapV );
        var oPageElementMapI = newPageElementMap(14, 2,3);
        storyController.AddPageElementMap(  oPage,oPageElementMapI );      
        var oPageElementMapII = newPageElementMap(15, 1,3);
        storyController.AddPageElementMap(  oPage,oPageElementMapII );    
        */
        
        oStoryEditor = newStoryEditor( storyController ); 
        
        if( oStoryEditor.container != null)
        {
            var dv = TheUte().findElement("divBodyAttachPoint","div");
            dv.appendChild( oStoryEditor.container );
             
            oStoryEditor.decorateGrid();
        }

    }

    </script>
</head>
<body onload="init();">
    <form id="form1" runat="server">
        <div id="divToolBarAttachPoint" runat="server" class="clsToolbar">
        </div>
         <div id="divMsgAttachPoint" runat="server" class="clsMsg">
        </div>
        <div id="divBodyAttachPoint" runat="server" class="clsBody">
        </div>
        <div id="divFooter" runat="server" class="clsFooter">
        </div>
        <div id="divLog" runat="server" class="clsLog">
        </div>
     </form>   
</body>
</html>

