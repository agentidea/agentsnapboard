function spamticker(a_spam)
{
  window.spam=this;
  
  this.width     = 134;
  this.a_spam    = a_spam;
  this.pos       = 0;
  this.cell      = [];
  
  // init spamticker
  this.init=function()
  {
    // get line
    this.dots = this.get_line();

    for(i=0;i<7;i++)
    this.cell[i]="";
    
    // get spam as string
    this.spam_str=this.get_spam_str();;
    
    for(i=0;i<this.spam_str.length;i++)
    {
      chr=this.spam_str.charAt(i);
      
      if(chars[chr])
	{
	  this.cell[0]=this.cell[0]+chars[chr][1]+".";
	  this.cell[1]=this.cell[1]+chars[chr][2]+".";
	  this.cell[2]=this.cell[2]+chars[chr][3]+".";
	  this.cell[3]=this.cell[3]+chars[chr][4]+".";
	  this.cell[4]=this.cell[4]+chars[chr][5]+".";
	  this.cell[5]=this.cell[5]+chars[chr][6]+".";
	  this.cell[6]=this.cell[6]+chars[chr][7]+".";
	}
    }
    
    // add offset
    for(i=0;i<7;i++)
    this.cell[i]=this.cell[i]+this.dots;
  };
    

  this.get_line=function()
  {
    var line='';
    for(i=0;i<this.width;i++)    
    line+='.';
    
    return line;
  };

  this.get_spam_str=function()
  {
    var spam_str='';

    for(var i=0; i<this.a_spam.length; i++)
    spam_str+=this.a_spam[i].toUpperCase()+' - ';

    return '       '+spam_str;
  };
  


  // tick spam
  this.tick=function()
  {
    var line=[];
    var output="";
    
    for(i=0;i<7;i++)
    line[i]="";  
    
    for(ii=0;ii<7;ii++)
    {
      for(i=0;i<this.width;i++)
	line[ii]=line[ii]+this.cell[ii].charAt(this.pos+i);
      
      if(ii<6)
	line[ii]=line[ii]+"\n";
    }
    
    
    for(i=0;i<7;i++)
    output+=line[i];
    
    document.getElementById("ticker").innerHTML="<pre>"+this.dots+"\n"+this.dots+"\n"+this.dots+"\n"+output+"\n"+this.dots+"\n"+this.dots+"</pre>";  
    this.pos=this.pos+2;
    
    
    if(this.pos>this.cell[0].length-this.width)
    this.pos=0;
    
    // recursive
    setTimeout("window.spam.tick()", 80);
  };


  // inititalise the spamticker
  this.init();
}



// start ticker onload
window.onload=function()
{
  window.spam.tick();
};

