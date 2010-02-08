

function LoginWidget(aLoginCallBack)
{

    var _loginCallback = aLoginCallBack;
    var _userNameInput = TheUte().getInputBox("","txtUserName",null,null,"clsLoginField","email address");
    var _pwdInput = TheUte().getPassword("","txtPwd",null,null,"clsLoginFieldPassword","password");
    
 
    
    this.init = function (commandText)
    {
        var loginGridVals = new Array();
        var _cmdLogin = TheUte().getButton("cmdLogin",commandText,"login",null,"clsLoginButton");
        _cmdLogin.onclick = function ()
        {
            _loginCallback( TheUte().encode64(_userNameInput.value) , TheUte().encode64(_pwdInput.value)     );
        }

        var txtEmail = document.createTextNode("Email/Username");
        var lblEmail = document.createElement("DIV");
        lblEmail.className = "clsLoginLabel";
        lblEmail.appendChild( txtEmail );
        
        var txtPassword = document.createTextNode("Password");
        var lblPassword = document.createElement("DIV");
        lblPassword.className = "clsLoginLabel";
        lblPassword.appendChild( txtPassword );
        
        var dvCommandLogin = document.createElement("DIV");
        dvCommandLogin.className = "clsLoginButtons";
        dvCommandLogin.appendChild(_cmdLogin);
        
        var thinSliver = document.createElement("DIV");
        thinSliver.style.height = "2px";
        
        loginGridVals.push( lblEmail );
        loginGridVals.push( _userNameInput );
        loginGridVals.push( lblPassword );
        loginGridVals.push( _pwdInput );
       // loginGridVals.push( thinSliver );
        loginGridVals.push( dvCommandLogin );

        var oLoginGrid = newGrid2("loginGrid",loginGridVals.length ,1,loginGridVals,0);
        oLoginGrid.init( oLoginGrid );

       return oLoginGrid.gridTable;
    }

}

