//story tuple manager
//



var StoryTupleManager = {

    grid: null,
    tupEditors: null,
    rootDiv: null,

    initRoot: function() {

    if (this.rootDiv != null)
        TheUte().removeChildren(this.rootDiv);

        this.rootDiv = document.createElement("DIV");

    },
    init: function(oStory) {

        var storyTuples = oStory.storyTuples;
        var numTuples = storyTuples.length;

        this.tupEditors = new Array();

        for (var i = 0; i < numTuples; i++) {

            var t = storyTuples[i];
            var tmpTupEditor = new tupleEditor(t.id, t.guid, t.name, t.code, t.units, t.description, t.numValue, t.value);

            this.tupEditors.push(tmpTupEditor.grid.gridTable);

        }

        this.loadGrid();


    },
    newEditor: function() {

        var tmpTupEditor = new tupleEditor(-1, "", "name", "code", "units", "desc", "", "");
        this.tupEditors.push(tmpTupEditor.grid.gridTable);
        this.loadGrid();

    },
    loadGrid: function() {
        this.initRoot();
        this.grid = newGrid2("gridEditors", this.tupEditors.length / 3, 3, this.tupEditors, 1);
        this.grid.init(this.grid);
        this.rootDiv.appendChild(this.grid.gridTable);

    }



};



function tupleEditor(aID,aGuid,aName,aCode,aUnits,aDesc,aNumVal,aVal) {

    var _id = aID;
    this.id = _id;
    
    var _guid = aGuid;
    this.guid = _guid;

    var _txtName = TheUte().getInputBox(aName, "txtName", null, null, "clsInput", "tuple name");
    this.txtName = _txtName;

    var _txtCode = TheUte().getInputBox(aCode, "txtCode", null, null, "clsInput", "tuple code");
    this.txtCode = _txtCode;

    var _txtUnits = TheUte().getInputBox(aUnits, "txtUnits", null, null, "clsInput", "tuple units");
    this.txtUnits = _txtUnits;

    var _txtNumValue = TheUte().getInputBox(aNumVal, "txtNumValue", null, null, "clsInput", "tuple numeric value");
    this.txtNumValue = _txtNumValue;


    if (aDesc != null)
        aDesc = TheUte().decode64(aDesc);

    var _txtDescription = TheUte().getTextArea(aDesc, "txtDescription", null, null, "clsTupleTextArea");
    this.txtDescription = _txtDescription;

    var _txtValue = TheUte().getTextArea(aVal, "txtValue", null, null, "clsTupleTextArea");
    this.txtValue = _txtValue;


    var _cmdUpdate = TheUte().getButton("saveTuple", "Save", "Save any changes", null, "clsButtonAction");
    this.cmdUpdate = _cmdUpdate;
    _cmdUpdate.onclick = function() {

    alert("saving" + _guid);
    }

    var tupleElements = new Array();

    tupleElements.push(_txtName);
    tupleElements.push(_txtCode);
    tupleElements.push(_txtUnits);
    tupleElements.push(_txtNumValue);
    tupleElements.push(_txtDescription);
    tupleElements.push(_txtValue);
    tupleElements.push(_cmdUpdate);

    var _grid = newGrid2("gridTupleElements", tupleElements.length,1 , tupleElements, 1);
    _grid.init(_grid);


    this.grid = _grid;
    

   this.load = function(aTuple) {
    

   }




}