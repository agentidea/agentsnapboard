//selector objects
function selWidget(aID)
{
    var _selWidget = null;
    this.selWidget = _selWidget;
    
    var _id = aID;
    this.id = _id;
    
    var _multiple = false;
    this.multiple = _multiple;

    this.init = initSelWidget;
    this.setCallback = SelWidgetSetCallback;
}

function initSelWidget(selItems,callback,size,className,defaultID)
{
    if(this.selWidget == null)
        this.selWidget = document.createElement("SELECT");
        
    this.selWidget.id = this.id;
    //alert(this.selWidget.id );
    
    if(size>1)
        this.selWidget.multiple = true;
    
    if(className != null)
        this.selWidget.className = className;
    
    this.setCallback( callback );
    
    this.selWidget.size = size;

		for(i=0;i<selItems.length;i++)
		{
			if(document.all)
			{
			    var ooption = document.createElement("OPTION");
			    this.selWidget.options.add(ooption);
			    ooption.innerText = selItems[i].text;
			    ooption.value = selItems[i].id;
			    
			    if(selItems[i].title != null)
			    {
			        //alert(selItems[i].title);
			        ooption.title = selItems[i].title;
			    }
			   
                if(defaultID != null)
                {
                    
                    if( (defaultID*1) == (selItems[i].id*1) )
                    {
                       ooption.selected = true;
                    }
                }
			   
			    
			}
			else
			{
			    this.selWidget.options[i] = new Option(selItems[i].text,selItems[i].id + "");
			    if(selItems[i].title != null)
			    {
			        this.selWidget.options[i].title = selItems[i].title;
			    }
			    
			    if(defaultID != null)
                {
                    
                    if( (defaultID*1) == (selItems[i].id*1) )
                    {
                       this.selWidget.options[i].selected = true;
                    }
                }
			}
			
			
		}
		
		
		
        
        
}

function SelWidgetSetCallback(callback)
{
    if(callback != null)
		    this.selWidget.onchange = callback;
}

function buildSelItems( pipedIDs , pipedText, bDeBase64, pipedTitles )
{
    var arrayItems = new Array();
    
    var aIDs = pipedIDs.split('|');
    var aTexts = pipedText.split('|');
    var aTitles = null;
    
    if(pipedTitles != null)
        aTitles = pipedTitles.split('|');
    
    for(var i = 0;i<aIDs.length;i++)
    {
        var txt = aTexts[i];
        if(bDeBase64) 
            txt = TheUte().decode64( txt );
            
        var selIte = null;
        if(aTitles != null)
            selIte = new selItem(aIDs[i],txt,aTitles[i]);
        else
            selIte = new selItem(aIDs[i],txt,null);
         
        arrayItems.push( selIte );
    }
    
    return arrayItems;

}

function selItem(aID,aText,aTitle)
{
    var _text = aText;
    this.text = _text;
    
    var _title = aTitle;
    this.title = _title;
    
    var _id = aID;
    this.id = _id;
}




