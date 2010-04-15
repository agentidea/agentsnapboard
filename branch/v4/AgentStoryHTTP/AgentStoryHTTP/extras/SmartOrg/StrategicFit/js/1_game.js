
function scalePulldownFire(ev) {

    ev = ev || window.event;
    var elem = ev.srcElement;
    var gameCode = StrategicFitGame.code;
    var elemBits = elem.id.split('_');


    var selValue = elem.options[elem.selectedIndex].value;
    if (selValue == -1) return;

    updateColValue(elemBits[1], gameCode, selValue);


}

function updateColValue(colToUpdate,gameCode,valueToUpdate)
{
 try {
        var extraSetColVal2 = newMacro("extraSetColVal2");
        addParam(extraSetColVal2, "colName", colToUpdate);
        addParam(extraSetColVal2, "gameCode", gameCode);
        addParam(extraSetColVal2, "colDoubleValue", valueToUpdate);
        addParam(extraSetColVal2, "StoryID", storyView.StoryController.CurrentStory.ID);
        addParam(extraSetColVal2, "tx_id64", TheUte().encode64(gUserCurrentTxID));

        processRequest(extraSetColVal2);

        
    }
    catch (e) {
        storyView.log("col setting2 setting error " + e.description);
    }

}

function removeGameData() {

    var c = confirm("Are you sure you want to do this? \r\n\t This action cannot be undone!");
    if (c) {
        StrategicFitGame.deleteData();
        location.href = location.href;
    }

}

var StrategicFitGame =
{

    code: "StrategicFit",
    refreshController: function(reveal) {
        try {
            var macroName = "extra" + this.code + "Controller";
            var extraController = newMacro(macroName);
            addParam(extraController, "targetDiv", "stratTable");
            gReveal = reveal;
            addParam(extraController, "reveal", reveal);
            addParam(extraController, "storyID", storyView.StoryController.CurrentStory.ID);
            addParam(extraController, "gameCode", this.code);
            addParam(extraController, "tx_id64", TheUte().encode64(gUserCurrentTxID));
            processRequest(extraController);
        }
        catch (e) {
            alert(macroName + " report error " + e.description);
        }
    },
    refreshReveal: function(reveal) {
        try {
            var macroName = "extra" + this.code + "Reveal";
            var refreshRev = newMacro(macroName);
            addParam(refreshRev, "targetDiv", "stratTable");
            gReveal = reveal;
            addParam(refreshRev, "reveal", reveal);
            addParam(refreshRev, "storyID", storyView.StoryController.CurrentStory.ID);
            addParam(refreshRev, "gameCode", this.code);
            addParam(refreshRev, "tx_id64", TheUte().encode64(gUserCurrentTxID));
            processRequest(refreshRev);
        }
        catch (e) {
            alert(macroName + " report error " + e.description);
        }
    },


    deleteData: function() {

        try {
            var extraDeleteGameData2 = newMacro("extraDeleteGameData2");
            addParam(extraDeleteGameData2, "gameCode", this.code);
            addParam(extraDeleteGameData2, "tx_id64", TheUte().encode64(gUserCurrentTxID));

            processRequest(extraDeleteGameData2);


        }
        catch (e) {
            alert("game data deletion error " + e.description);
        }

    },

    getInputScreen: function(tupleIndex) {

        var storyTuples = storyView.StoryController.CurrentStory.storyTuples;
        var dv = document.createElement("DIV");
        var numTuples = storyTuples.length;

        var rows = 1;
        var aGUID = "xyz";
        var cols = numTuples;

        var border = 1;
        dv.style.width = "900px";
        var headerRow = "";

        var currentTuple = storyTuples[tupleIndex];

        var dvNoteHeader = document.createElement("DIV");
        dvNoteHeader.style.fontSize = "12pt";
        var txtNoteHeader = document.createTextNode(currentTuple.name);
        dv.appendChild(dvNoteHeader.appendChild(txtNoteHeader));

        var dvNote = document.createElement("DIV");
        dvNote.className = "clsPageNote";
        var txtNote = document.createTextNode(TheUte().decode64(currentTuple.value));
        dvNote.appendChild(txtNote);
        dv.appendChild(dvNote);
        var spacerDv = document.createElement("DIV");
        spacerDv.style.height = 25;

        dv.appendChild(spacerDv);

        this.loadSummary();
        return dv;

    },

    loadSummary: function() {
        try {
            var macroName = "extraStrategicFitSummary";
            var sfSummar = newMacro(macroName);
            addParam(sfSummar, "targetDiv", "dvSummary");
            addParam(sfSummar, "storyID", storyView.StoryController.CurrentStory.ID);
            addParam(sfSummar, "gameCode", this.code);
            addParam(sfSummar, "tx_id64", TheUte().encode64(gUserCurrentTxID));
            processRequest(sfSummar);
        }
        catch (e) {
            alert(macroName + " report error " + e.description);
        }

    },

    scaleDropDown: function(id, index) {

        var scaleVals = [-1, 1, 2, 3, 4];
        var scaleText = ["", "Poor", "OK", "Good", "Excellent"];
        var scaleColors = ["#FFFFFF", "#FF0066", "#3300FF", "#009900", "#009999"];


        return TheUte().getSelectColor("sel_" + id, scaleVals, scaleText, scaleColors, scalePulldownFire, index, "");

    }

}





/*

1 POOR
2 OK
3 GOOD
4 EXCELLENT


CameraLink
ClothPrinter
CreativeStudio
Cutter
FullPage	
PhotoKiosk
PreciseDose
RealPhoto
RPTV
SpotInks

Camera Link
Cloth Printer
Creative Studio
Cutter
Full Page	
Photo Kiosk
Precise Dose
Real Photo
RPTV
Spot Inks

RPTV, which adapts HP's "wobulation" printing technology to deliver High Definition Rear Projection TV with superior performance and radically reduced costs. Inkjet print heads are designed to purposefully wobble as they scan the page, and the jets fire with precise timing, which increases the effective resolution of the printer. The same technology can be applied to a low-cost version of Texas Instrument's ubiquitous DLP chips (arrays of controllable little mirrors used to distribute light) to boost the resolution to High Definition 1080p standards. Without wobulation, achieving this resolution with DLP chips is very expensive. But entering this business would take us up against Sony, Samsung and other established TV players.

 
Precise Dose, which builds on core HP Inkjet technology to precisely control and measure minute quantities of liquid. This project inventively delivers micro-fluids for administration of precise doses of drugs, particularly applicable for inhalation therapies where it is otherwise extremely difficult to measure whether patients have absorbed their prescribed doses. However, this business takes HP into the regulated medical device business, where we have no real competency.
  
Creative Studio, for in-store production of photo albums, poster collages and other creative consumer projects. A series of anthropological studies of customers revealed that the #2 use of 4"x6" prints (after tossing them into a proverbial shoebox, which actually ranks #1) is to place them in photo albums. So why not simply print the whole album? A technical breakthrough in algorithms for automatic layout has made this a real possibility, even though the print quality is not as good as the 4"x6" images they are replacing.
 
Photo Kiosk, a high-speed, self-service dispenser of quality 4"x6" prints. Incremental innovations in print quality, machine reliability and print speed have reached an important threshold where they can be deployed via an ATM for printing. Grocery stores and non-traditional locations, like company cafeterias and office buildings, could easily get into the photo printing business. But will ubiquity and convenience really drive customer behavior?
 
Cloth Printer, for imaging on customized apparel. By adapting our ink systems and increasing the range of materials we can print upon, this printer can write directly to a wide variety of cloth and related materials. In a retail outlet, customers can purchase anything from t-shirts to towels and have personal images printed on these items while they wait. 
 
Cutter, a specialty print head enabling creative die-cuts, embossing, and decorative edges on photos, achieved by converting ink jets into air jets. This product appeals both to the high margin quilter and hobby segments as well as to personal or small business segments ordering limited runs of holiday cards or business cards.
 
Real Photo, a high-resolution process allowing in-store Inkjet printers to rival premium, professional-quality photo printers. Portrait quality photographs or fine art could be printed while you wait. 
 
Full-Page Print Head. Instead of one print head scanning the page, which slows the printing process and requires complex mechanics, a full-page head would print out a page at the full speed of the paper handling subsystem and completely eliminate the need for print head motion. This simplification increases reliability while lowering production cost. This feature could be added to many printers in the HP line and further enhance our competitive advantage.
 
Camera Link, enhancing HP's cameras with cellular connections for direct-to-Internet fulfillment via our Snapfish division. Imagine sharing a picture from your camera with a friend, pressing a button to order it, and having it show up by mail the next day! This enhancement would create a feature that no other camera manufacturer can deliver, thereby increasing our market share and profitability of our struggling digital camera business.
 
Spot Inks, which would interject versatility into HP printers by increasing the palette of specialty colors by an order of magnitude as well as introducing metallic and invisible inks. The current CYMK ink systems (short for cyan, magenta, yellow, and key (black)) have a limited gamut of colors and can produce no special effects. Spot Inks lets our commercial printers access markets like specialty cards and marketing materials, where precise colors are important, as well as the security segment, with invisible inks for authentication.

*/
