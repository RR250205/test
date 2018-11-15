// Copyright 2006 WildStorms Digital.
// All Rights Reserved
//
// Ajax prototype
//
// Author: Ng Teng Yong
//


var req;			//xmlhttprequest object

//initialize cross browser xmlhttprequest object
function Initialize()
{
	try
	{
		req=new ActiveXObject("Msxml2.XMLHTTP");
	}
	catch(e)
	{
		try
		{
			req=new ActiveXObject("Microsoft.XMLHTTP");
		}
		catch(oc)
		{
			req=null;
		}
	}

	if(!req && typeof XMLHttpRequest!="undefined")
	{
		req=new XMLHttpRequest();
	}
}

function ShowDiv(divid)
{
	if (document.layers) 
		document.layers[divid].visibility="show";
	else
	{ 
		document.getElementById(divid).style.visibility="visible";
		document.getElementById(divid).style.borderStyle="solid";
		document.getElementById(divid).style.borderWidth="1px";
	}
}

function HideDiv(divid)
{
	if (document.layers) 
		document.layers[divid].visibility="hide";
	else 
	{
		document.getElementById(divid).style.visibility="hidden";
		document.getElementById(divid).style.borderStyle="none";
	}
}

function movein(which,overcolor)
{
	which.style.background=overcolor;
}

function moveout(which,outcolor)
{
	which.style.background=outcolor;
}

//tbid - Textbox id
function press(tbid,text,dd_press)
{
	el(tbid).value=text;
	HideDiv(dd_press);
}

//SendQuery function sends request to server asyncrhonously.
function SendQuery(arg,key,dd_,styshet_)
{
	Initialize();
	var baseurl=window.location.href; //get the url of the current web page
	
	//check if other HTTP GET argument exist,if yes use "?" else use "&" to concatenate http request string
	if (baseurl.indexOf("?")==-1)
		var url=baseurl+"?"+arg+"="+key; //the get argument must be unique for each control.arg is a unique variable of a web page
	else
		var url=baseurl+"&"+arg+"="+key;	
		
	if(req!=null)
	{
		req.onreadystatechange = function() {Process(dd_,styshet_);}; //event handler for each state change
		req.open("GET", url, true);
		req.send(null);
	}
}

function Process(dd,styshet)
{
	if (req.readyState == 4)
	{
		if (req.status == 200)// only if "OK"
		{
			if(req.responseText=="")
				HideDiv(dd);//hide drop down area
			else
			{
				ShowDiv(dd);//show drop down area
				var xml = xmlParse(req.responseText);
				var xslt = xmlParse(styshet);
				var html = xsltProcess(xml, xslt);
				if (html=="")
					HideDiv(dd);
				else	
					el(dd).innerHTML=html;
					
			}
		}
		else
		{
			el(dd).innerHTML="There was a problem retrieving data:<br>"+ req.statusText;
		}
	}
}

