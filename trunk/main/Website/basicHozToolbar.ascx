<%@ Control Language="C#" AutoEventWireup="true" CodeFile="basicHozToolbar.ascx.cs" Inherits="basicHozToolbar" %>



<!-- b yahoo menu -->
<!-- Menu files -->
<link rel="stylesheet" type="text/css" href="<%= IncludeMaster %>/build/menu/assets/menu.css">

<script type="text/javascript" src="<%= IncludeMaster %>/build/yahoo/yahoo.js"></script>

<!-- Dependency source files -->

<script type="text/javascript" src="<%= IncludeMaster %>/build/event/event.js"></script>
<script type="text/javascript" src="<%= IncludeMaster %>/build/dom/dom.js"></script>

<!-- Container source file -->
<script type="text/javascript" src="<%= IncludeMaster %>/build/connection/container/container_core.js"></script>
<!-- Menu source file -->
<script type="text/javascript" src="<%= IncludeMaster %>/build/menu/menu.js"></script>
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
                <li class="yuimenubaritem first-of-type"><a href="./login.aspx">Login</a></li>
                <li class="yuimenubaritem"><a href="./Platform2.aspx">Story</a></li>
                <li class="yuimenubaritem"><a href="./MessageManager.aspx">Message</a></li>
                <li class="yuimenubaritem"><a href="./ListMembers.aspx">Members</a></li>
                <li class="yuimenubaritem"><a href="./StoryEditor4.aspx?StoryID=69">Info</a></li>
                
            </ul>
        </div>
    </div>

</div>
   
     </td>
     <td>
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
                    
                        "Login": [ 
                        
                            { text: "Login", url: "./Login.aspx?msg=Please%20Login%20by%20entering%20your%20email%20and%20password%20below." }
                        ],
                        
                         "Story": [ 
                        
                            { text: "Create New", url: "./CreateNewStory.aspx" },
                            { text: "Table of Contents", url: "./Platform2.aspx" },
                            { text: "Propegate", url: "./propegateHTML.aspx" }
                           
                        ],
    
                        "Message": [
    
                            { text: "Compose New", url: "./SendNewEmail.aspx" },
                            { text: "Mailbox", url: "./MessageManager.aspx" }
                        ],
                        
                        "Members": [
    
                            { text: "My Profile", url: "./EditUserInfo.aspx" },
                            { text: "Add New", url: "./addSingleUser.aspx" },
                            { text: "List", url: "./ListMembers.aspx" },
                            { text: "Invite", url: "./CreateInvitations2.aspx" },
                            { text: "Import", url: "./AddUsers.aspx" },
                            { text: "Groups", submenu: { id: "Groups", itemdata: [
        
                                    { text: "Create New", url:"./AddNewGroup.aspx" },
                                    { text: "Manage", url:"./ManageUsersGroups.aspx" }
                                   
        
                                ] }
                            
                            }           
                        
                        ],
                        
                        "Info": [
    
                            { text: "Help", url: "./StoryEditor4.aspx?StoryID=<%= HelpStoryID %>"  },
                            { text: "Contact", url: "./contact.aspx" },
                            { text: "Log", url: "./Main.aspx" },
                            { text: "Library", url: "./PageElementViewer.aspx" },
                            { text: "About", url: "http://www.agentidea.com" }
                        
                        ]
                    
                    };


                    this.getItem(0).cfg.setProperty("submenu", { id:"Login", itemdata: oSubmenuData["Login"] });
                    this.getItem(1).cfg.setProperty("submenu", { id:"Story", itemdata: oSubmenuData["Story"] });
                    this.getItem(2).cfg.setProperty("submenu", { id:"Message", itemdata: oSubmenuData["Message"] });
                    this.getItem(3).cfg.setProperty("submenu", { id:"Members", itemdata: oSubmenuData["Members"] });
                    
                    this.getItem(4).cfg.setProperty("submenu", { id:"Info", itemdata: oSubmenuData["Info"] });
                    
                    

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
