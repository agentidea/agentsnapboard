// mouse move and drag and drop javascript.
//http://www.webreference.com/programming/javascript/mk/column2/

var dragObject  = null;
var mouseOffset = null;
var mousePosREF = null;


function mouseCoords(ev){
	if(ev.pageX || ev.pageY){
		return {x:ev.pageX, y:ev.pageY};
	}
	return {
		x:ev.clientX + document.body.scrollLeft - document.body.clientLeft,
		y:ev.clientY + document.body.scrollTop  - document.body.clientTop
	};
}



function getMouseOffset(target, ev){
	ev = ev || window.event;

	var docPos    = getPosition(target);
	var mousePos  = mouseCoords(ev);
	return {x:mousePos.x - docPos.x, y:mousePos.y - docPos.y};
}

function getPosition(e){
	var left = 0;
	var top  = 0;

	while (e.offsetParent){
		left += e.offsetLeft;
		top  += e.offsetTop;
		e     = e.offsetParent;
	}

	left += e.offsetLeft;
	top  += e.offsetTop;

	return {x:left, y:top};
}

function mouseMove(ev){
	ev           = ev || window.event;
	var mousePos = mouseCoords(ev);
	
	mousePosREF = mousePos;
	
	xy( mousePos );

	if(dragObject){
		

		
		var y = mousePos.y - mouseOffset.y;
		var x = mousePos.x - mouseOffset.x;
		//get attributes off original object.
		
//		history( dragObject.id + " " + dragObject.style.width);
		
		var w = dragObject.style.width;
		var h = dragObject.style.height;
		var color= dragObject.style.backgroundColor;
		
		var cssTextString = 'visibility:visible;  background-color:'+color+'; position:absolute;'; 
        cssTextString += ' top:' + y ;
        cssTextString += 'px;';
        cssTextString += ' left:' + x;
        cssTextString += 'px;';
        cssTextString += ' z-index:' + (1);
        
        history( cssTextString );
        
        dragObject.style.cssText = cssTextString;
        
       dragObject.style.width=w;
       dragObject.style.height=h;
        dragObject.setAttribute("title",x + ' x ' + y);

		return false;
	}
}
function mouseUp(){
	
	if(dragObject != null)
	{
	    history( "released " + dragObject.id  + " @position " + dragObject.style.left + " x " + dragObject.style.top );
	    dragObject = null;
	}
	
	
}

function makeDraggable(item){
	if(!item) return;
	item.onmousedown = function(ev){
		dragObject  = this;
		mouseOffset = getMouseOffset(this, ev);
		return false;
	}
}




