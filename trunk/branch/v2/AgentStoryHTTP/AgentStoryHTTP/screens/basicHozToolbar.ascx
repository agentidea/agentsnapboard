<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="basicHozToolbar.ascx.cs" Inherits="AgentStoryHTTP.screens.basicHozToolbar" %>



<!-- b yahoo menu -->
<!-- Menu files -->
<link rel="stylesheet" type="text/css" href="../includes/YUI/build/menu/assets/menu.css">

<script type="text/javascript" src="../includes/YUI/build/yahoo/yahoo.js"></script>

<!-- Dependency source files -->

<script type="text/javascript" src="../includes/YUI/build/event/event.js"></script>
<script type="text/javascript" src="../includes/YUI/build/dom/dom.js"></script>

<!-- Container source file -->
<script type="text/javascript" src="../includes/YUI/build/connection/container/container_core.js"></script>
<!-- Menu source file -->
<script type="text/javascript" src="../includes/YUI/build/menu/menu.js"></script>
<!-- e yahoo menu -->






<!-- basic hoz toolbar ascx -->

<script language="javascript" type="text/javascript">

function toggleToolbar()
{

//    var dvToolbar = document.getElementById("divMainToolBar");
//    
//    if(dvToolbar != null)
//    {
//        if(dvToolbar.style.display == "none")
//        {
//            dvToolbar.style.display = "inline";    
//        }
//        else
//        {
//            dvToolbar.style.display = "none";
//        }
//    
//    }

    window.location.href = "./Platform2.aspx";
}


</script>

<div>
<table id="outerContainer" border=0 width="66%">
<tr>
    <td><div class='clsKlubIcon' onmouseover="this.className='clsKlubIconMouseOver';" onmouseout="this.className='clsKlubIcon';" onclick="toggleToolbar();"><%= ClubLogo %></div>
      <div id="divUserNameAttachPoint" runat="server" class="clsUserNameLabel"></div>
      </td>
    <td>
    <div id="divMainToolBar">
     <!-- YAHOO MENU BAR start: stack grids here -->
    <div id="productsandservices" class="yuimenubar">
        <div class="bd">
            <ul class="first-of-type">
                
                <li class="yuimenubaritem first-of-type"><a href="./Platform2.aspx">Story</a></li>
                <li class="yuimenubaritem"><a href="./MessageManager.aspx">Message</a></li>
                <li class="yuimenubaritem"><a href="./ListMembers.aspx">Members</a></li>
                <li class="yuimenubaritem"><a href="./StoryEditor4.aspx?StoryID=<%= HelpStoryID %>">Info</a></li>
                
            </ul>
        </div>
    </div>

</div>
   
     </td>
     <td>
        <div id="dvLogIn" class="yuimenubaritem" runat="server"><a href="./Login.aspx">Login</a></div>
        <div id="dvLogOut" class="yuimenubaritem" runat="server"><a href="./LogOut.aspx">Logout</a></div>
     
     </td>
</tr>

</table>
</div>


<!-- YAHOO MENU BAR INITIALIZATION SCRIPT -->
<script type="text/javascript">

            YAHOO.example.onMenuBarReady = function() {
                
                // "beforerender" event handler for the menu bar

                function onMenuBarBeforeRender(p_sType, p_sArgs, p_oMenu) {

                    var oSubmenuData = {
                    
                       
                        
                         "Story": [ 
                        
                            { text: "Create New", url: "./CreateNewStory.aspx" },
                            { text: "Table of Contents", url: "./Platform2.aspx" }
                           
                        ],
    
                        "Message": [
    
                            { text: "Compose New", url: "./SendNewEmail.aspx" },
                            { text: "Mailbox", url: "./MessageManager.aspx" }
                        ],
                        
                        "Members": [
    
                            { text: "My Profile", url: "./EditUserInfo.aspx" },
                            { text: "New User", url: "./addSingleUser.aspx" },
                            { text: "List Members", url: "./ListMembers.aspx" },
                            { text: "New Group", url:"./AddNewGroup.aspx" },
                            { text: "Manage Groups", url:"./ManageUsersGroups.aspx" }
                            
                        ],
                        
                        "Info": [
    
                            { text: "Help", url: "./StoryEditor4.aspx?StoryID=<%= HelpStoryID %>"  },
                            { text: "Log", url: "./logViewer.aspx" },
                            { text: "Library", url: "./PageElementViewer.aspx" }
                           
                        
                        ]
                    
                    };


                    
                    this.getItem(0).cfg.setProperty("submenu", { id:"Story", itemdata: oSubmenuData["Story"] });
                    this.getItem(1).cfg.setProperty("submenu", { id:"Message", itemdata: oSubmenuData["Message"] });
                    this.getItem(2).cfg.setProperty("submenu", { id:"Members", itemdata: oSubmenuData["Members"] });
                    this.getItem(3).cfg.setProperty("submenu", { id:"Info", itemdata: oSubmenuData["Info"] });
                    
                    

                }


                // Instantiate and render the menu bar

                var oMenuBar = new YAHOO.widget.MenuBar("productsandservices", { autosubmenudisplay:true, showdelay:250, hidedelay:750, lazyload:true });


                // Subscribe to the "beforerender" event

                oMenuBar.beforeRenderEvent.subscribe(onMenuBarBeforeRender, oMenuBar, true);


                // Render the menu bar

                oMenuBar.render();
                
            };


            // Initialize and render the menu bar when it is available in the DOM

            YAHOO.util.Event.onContentReady("productsandservices", YAHOO.example.onMenuBarReady);

        </script>

