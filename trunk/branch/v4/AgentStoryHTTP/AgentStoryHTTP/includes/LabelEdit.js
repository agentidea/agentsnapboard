/*

    AgentIdea - Story Label Edit
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

to verify search for Grant Steinfeld or Oren Kredo
http://www.copyright.gov/records/cohm.html

*/

function LabelEdit(aVal,aID, aReadOnly, aType, aSaveCallback, aLabelClassName,aLabelClassNameOnMouseOver)
{

    var _id = aID;
    this.id = _id;
    
    var _val = aVal;
    this.val = _val;
    
    var _bReadOnly = aReadOnly;
    this.readonly = _bReadOnly;
    
    var _container = document.createElement("DIV");
    this.container = _container;
    
    var _label =  document.createElement("DIV");;
    this.label = _label;
    if(aReadOnly == false)
        _label.title = "click here to edit this text";
    
    var _editArea = document.createElement("DIV");
    this.editArea = _editArea;
    
    var _inputBoxClassName = "clsLabelEditInputBox";
    var _textBoxClassName = "clsLabelEditTextBox";
    var _labelClassName   = null;
    
    if( aLabelClassName != null)
    {
         _labelClassName = aLabelClassName;
    }
    else
    {
          _labelClassName = "clsLabelEdit";
    }
    _label.className = _labelClassName;
    
    var _labelMouseOverClassName = null;
    if( aLabelClassNameOnMouseOver != null )
    {
        _labelMouseOverClassName = aLabelClassNameOnMouseOver;
    }
    else
    {
        _labelMouseOverClassName = "clsLabelEditMouseOver";
    }

    var _focusHandler = null;
    var _blurHandler = null;
    
    this.inputBoxClassName = _inputBoxClassName;
    this.textBoxClassName = _textBoxClassName;
   
    var _type = aType;
    this.type = _type;
    
    var _saveCallback = aSaveCallback;
    this.saveCallback = _saveCallback;
    

    var _textArea = TheUte().getTextArea(aVal.trim() ,aID,null,null,_textBoxClassName);
    this.textArea = _textArea;
    
    var _inputArea = TheUte().getInputBox(aVal.trim() ,aID,null,null,_inputBoxClassName,null);
    this.inputArea = _inputArea;
    
    var _textBox = null;
    this.textBox = _textBox;
    
    this.init = LabelEditInit;
    
    this.updateValues = function(newVal)
    {
       _textArea.value  = newVal;
       _inputArea.value = newVal;
       _label.innerHTML = newVal;
    }
    
    this.labelMouseOver = function (ev)
    {
         _label.className = _labelMouseOverClassName;
         
    }
    
    this.labelMouseOut = function (ev)
    {
        _label.className = _labelClassName;
    }
    
    this.labelClick = function (ev)
    {
       
        _editArea.style.display = "block";
        _label.style.display = "none";
    
    }
    
    this.saveClick = function (ev)
    {
        /* trouble accessing vars defined above as null */
        var sToSave = null;
        if(_type == "textarea")
        {
            sToSave = _textArea.value;
        }
        else
        {
            sToSave = _inputArea.value;
        }
        
        _val = sToSave.trim();
        
       _saveCallback( sToSave );
       
       _label.innerHTML = crlfToBR( sToSave );
       _editArea.style.display = "none";
       _label.style.display = "block";
    }
    
    this.cancelClick = function (ev)
    {
         if(_type == "textarea")
        {
            _textArea.value = _val.trim();
        }
        else
        {
            _inputArea.value = _val.trim();
        }
        _editArea.style.display = "none";
        _label.style.display = "block";
    }
    
   
    

}

    function LabelEditInit()
    {

        this.container.id = "containerLabelEdit_" + this.id;
        this.editArea.id = "editEdit_" + this.id;
        this.label.id = "labelEdit_" + this.id;
        
        if(this.type == "textarea")
        {
            this.textBox = this.textArea;
        }
        else
        {
             this.textBox = this.inputArea;
        }
        
        
        var editValues = new Array();
        
        editValues.push(this.textBox);
        editValues.push(TheUte().getButton("cmdSave_" + this.id,"Save","save changes",this.saveClick,"clsButton"));
        editValues.push(TheUte().getButton("cmdCxl_" + this.id,"Cancel","cancel changes",this.cancelClick,"clsButton"));
        
        var oEditGrid = newGrid2("editGrid_" + this.id,1,3,editValues,0);
        oEditGrid.init( oEditGrid );
        
        this.editArea.appendChild(oEditGrid.gridTable);
        
        if( this.readonly == false)
        {
            this.label.onclick = this.labelClick;
            this.label.onmouseover = this.labelMouseOver;
            this.label.onmouseout = this.labelMouseOut;
            
        }
            
        var txtNode = document.createTextNode( this.val );
        this.label.appendChild( txtNode );
        
        this.label.style.display = "block";
        this.editArea.style.display = "none";
        
        this.container.appendChild( this.label );
        this.container.appendChild( this.editArea );
         
    }
    
    function BRtoCrlf ( s )
    {
        alert("not yet implemented");
    }
    
    function crlfToBR ( s )
    {
        var len = s.length;
        var newString = "";
        
        var i = 0;
        for(;i<len;i++)
        {
            var cc = s.charCodeAt(i);
            var ch = s.charAt(i);
            
            if(cc == 13)
            {
                newString += "<br />";
            }
            else
            {
                if(cc == 10)
                {
                
                }
                else
                {
                    newString += ch;
                }
            }
            
        }
        
        return newString;
}