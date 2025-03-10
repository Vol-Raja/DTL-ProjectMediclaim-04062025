

   var p = document.querySelectorAll('p, a, button, td, li, span');
   var pm = document.querySelectorAll('header .sub_header ul li, header .sub_header ul li a');
   function increaseFontSize() {
        $('h1').css('fontSize' , '2.7rem');
    	$('h2').css('fontSize' , '2.2rem');
    	$('h3').css('fontSize' , '1.95rem');
    	$('h4').css('fontSize' , '1.7rem');
    	$('h5').css('fontSize' , '1.45rem');
    	$('h6').css('fontSize' , '1.2rem');
    	
   		for(i=0;i<p.length;i++) {
          p[i].style.fontSize = "17px"
       }
   		for(j=0;j<pm.length;j++) {
            pm[j].style.fontSize = "16px"
         }
     }
    
    function decreaseFontSize() {
        $('h1').css('fontSize' , '2.3rem');
    	$('h2').css('fontSize' , '1.8rem');
    	$('h3').css('fontSize' , '1.55rem');
    	$('h4').css('fontSize' , '1.3rem');
    	$('h5').css('fontSize' , '1.05rem');
    	$('h6').css('fontSize' , '0.8rem');
    	
  		for(i=0;i<p.length;i++) {
          p[i].style.fontSize = "13px"
       }  
  		for(j=0;j<pm.length;j++) {
            pm[j].style.fontSize = "12px"
         }
      }
    function normalFontSize() {
    	$('h1').css('fontSize' , '2.5rem');
    	$('h2').css('fontSize' , '2rem');
    	$('h3').css('fontSize' , '1.75rem');
    	$('h4').css('fontSize' , '1.5rem');
    	$('h5').css('fontSize' , '1.25rem');
    	$('h6').css('fontSize' , '1rem');
    	
    	for(i=0;i<p.length;i++) {
           p[i].style.fontSize = "15px"
        } 
    	for(j=0;j<pm.length;j++) {
          pm[j].style.fontSize = "14px"
         }
      }   
	
                    