		var lastNotify = new Date();
			
		function checkEnter(e){ //e is event object passed from function invocation
          
            var characterCode// literal character code will be stored in this variable

            if(e && e.which){ //if which property of event object is supported (NN4)
                e = e
                characterCode = e.which //character code is contained in NN4's which property
            }
            else{
                e = event
                characterCode = e.keyCode //character code is contained in IE's keyCode property
            }

            if(characterCode == 13){ //if generated character code is equal to ascii 13 (if enter key)
                CallSendMsg();
                return false 
            }
            else{
				var now = new Date();
				var elapse = now.getSeconds() - lastNotify.getSeconds();
				var sameMinute = now.getMinutes() - lastNotify.getMinutes();
				if( sameMinute != 0 || elapse >= 3 )
				{
					// Set typing notification
					SetTypingNotification(); 
					
					lastNotify = new Date();
				}
				
                return true 
            }                              
        }   
       
	function SetTypingNotification() 
    {
		//Get text control
        var txt1 = $get("txtMsg");
       
        //Call server side method
        PageMethods.SetTypingNotification(getCookie('chatId'), txt1.value, OnSetTypingNotificationComplete);
    }
    
    function OnSetTypingNotificationComplete(result,methodName)
    {
    } 

    function CallSendMsg() 
    {
        //Get text control
        var txt1 = $get("txtMsg");

        //Call server side function
        PageMethods.SendMsg(txt1.value,getCookie('chatId'),OnCallSendMsgComplete);
    }
    
    function OnCallSendMsgComplete(result,methodName)
    {
        //Get text control
        var txt1 = $get("txtMsg");
        //delete text in txtMsg
        txt1.value='';
        //focus
        txt1.focus();
    }
    
    function getCookie(c_name)
    {
    if (document.cookie.length>0)
      {
      c_start=document.cookie.indexOf(c_name + "=")
      if (c_start!=-1)
        { 
        c_start=c_start + c_name.length+1 
        c_end=document.cookie.indexOf(";",c_start)
        if (c_end==-1) c_end=document.cookie.length
        return unescape(document.cookie.substring(c_start,c_end))
        } 
      }
    return ""
    }