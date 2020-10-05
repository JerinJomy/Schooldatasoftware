var Params = new URLSearchParams(document.location.search.substring(1));
var id=Params.get('id');
function staff(){	
	var request='https://localhost:44386/api/Staffs/'+id
	fetch(request).then((res) =>res.json()).then((data)=>GetData(data))
}
// {"designation":"Ofiice","name":"jerin","staffType":2,"phone":"940073","email":"jerin@123","id":1}
function GetData(data)
{
	var name= data.name
	var email=data.email
	var phone=data.phone
	var id=data.id
	var staffType=data.staffType
	document.getElementById("name").value = name
	document.getElementById("phone").value = phone
	document.getElementById("email").value = email
	document.getElementById("id").innerHTML="StaffId:"+id
	switch(staffType)
	{
		case 1:
			var classname=data.className
			var subject=data.subject
			document.getElementById("subject").value=subject 
			document.getElementById("classname").value=classname
			break;
		case 2:
			var designation=data.designation
			document.getElementById("designation").value=designation
			break;
		case 3:
		    var designation=data.designation
			document.getElementById("designation").value=designation
			break;
	}
}

function Teachingstaff()
{
	var name=document.getElementById("name").value
	var phone=document.getElementById("phone").value
	var email=document.getElementById("email").value
    var classname=document.getElementById("classname").value
    var subject=document.getElementById("subject").value
	var request="https://localhost:44386/api/Staffs/"+id
	fetch(request, { 
      
	  // Adding method type 
	  method: "PUT", 
		
	  // Adding body or contents to send 
	  body: JSON.stringify({ 
	   "subject": subject,
	  "email": email,
	  "name": name,
	  "phone": phone,
      "staffType": 1,
	  "className":classname,
	  "id":id
	  }), 
		
	  // Adding headers to the request 
	  headers: { 
		  "Content-type": "application/json; charset=UTF-8"
	  } 
  }) 
	
  // Converting to JSON 
  .then(response => { 
  if(response.ok)
  {
	window.open("file:///C:/Users/jerin/Documents/programing/web%20design/my%20work/staffs.html","_self")
  }
  }) 
}

function Adminstaff()
{
	var name=document.getElementById("name").value
	var phone=document.getElementById("phone").value
	var email=document.getElementById("email").value
    var designation=document.getElementById("designation").value
	var request="https://localhost:44386/api/Staffs/"+id
	fetch(request, { 
      
	  // Adding method type 
	  method: "PUT", 
		
	  // Adding body or contents to send 
	  body: JSON.stringify({ 
	   "designation": designation,
	  "email": email,
	  "name": name,
	  "phone": phone,
	  "staffType": 2,
	  "id":id
	  }), 
		
	  // Adding headers to the request 
	  headers: { 
		  "Content-type": "application/json; charset=UTF-8"
	  } 
  }) 
	
  // Converting to JSON 
  .then(response => { 
  if(response.ok)
  {
	window.open("file:///C:/Users/jerin/Documents/programing/web%20design/my%20work/staffs.html","_self")
  }
  }) 
}

function Supportstaff()
{
	var name=document.getElementById("name").value
	var phone=document.getElementById("phone").value
	var email=document.getElementById("email").value
    var designation=document.getElementById("designation").value
	var request="https://localhost:44386/api/Staffs/"+id
	fetch(request, { 
      
	  // Adding method type 
	  method: "PUT", 
		
	  // Adding body or contents to send 
	  body: JSON.stringify({ 
	   "designation": designation,
	  "email": email,
	  "name": name,
	  "phone": phone,
	  "staffType": 3,
	  "id":id
	  }), 
		
	  // Adding headers to the request 
	  headers: { 
		  "Content-type": "application/json; charset=UTF-8"
	  } 
  }) 
	
  // Converting to JSON 
  .then(response => { 
  if(response.ok)
  {
	window.open("file:///C:/Users/jerin/Documents/programing/web%20design/my%20work/staffs.html","_self")
  }
  }) 
}
staff();