

function AlertArea(s) {

    var _message = s;
    this.message = _message;


    var _AlertPanel = document.createElement("DIV");
    _AlertPanel.className = "clsAlertArea";

    var _txtAlert = TheUte().getInputBox("", "txtAlertBox",null,null,"clsAlertAreaTxtBox",null);
    //val,id,focusHandler,blurHandler,className,title
    _AlertPanel.appendChild(_txtAlert);
    
    this.container = _AlertPanel;



    this.log = function(s) {
        var ta = document.getElementById("txtAlertBox") ;
        ta.value = s;
    }

    _AlertPanel.ondblclick = function(ev) {
        ev = ev || window.event;
        ev.cancelBubble = true;


        location.href = "login.aspx";



    }



}

/*

Webpage error details

User Agent: Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0)
Timestamp: Thu, 4 Feb 2010 20:15:51 UTC


Message: 'dvValueOfFundedProjects' is null or not an object
Line: 24
Char: 1
Code: 0
URI: http://localhost:8181/screens/SlideNavigator.aspx?StoryID=1&PageCursor=0&toolBR=BASIC



*/