function GetStaff(type)
{
  switch(type)
   {
  case "administrative":
    var request='https://localhost:44386/api/Staffs/?type=';
    request=request+type;
    fetch(request).then((res) =>res.json()).then((data)=>BuildAdmintable(data))
    break;

  case "teaching":
    var request='https://localhost:44386/api/Staffs/?type=';
    request=request+type;
    fetch(request).then((res) =>res.json()).then((data)=>Buildteachingtable(data))
    break;
  case "support":
    var request='https://localhost:44386/api/Staffs/?type=';
    request=request+type;
    fetch(request).then((res) =>res.json()).then((data)=>Buildsupporttable(data))
    break;
  }
}

function Buildteachingtable(data)
    {
      document.getElementById("title").innerHTML = "Teaching Staff";
      var table=document.getElementById('table')
      table.innerHTML=`<th>Staff Id</th>
    <th i>Name</th>
    <th>Phone</th>
    <th>Email</th>
    <th>classname</th>
    <th>Subject</th>
    `;
      for(var i=0;i<data.length ;i++)
      {
        var row=`<tr>
            <th i>${data[i].id}</th>
            <th i>${data[i].name}</th>
            <th>${data[i].phone}</th>
           <th>${data[i].email}</th>
           <th>${data[i].className}</th>
           <th>${data[i].subject}</th>
           </tr>`

        table.innerHTML=table.innerHTML+row
      }
    }

function Buildsupporttable(data)
    {
      document.getElementById("title").innerHTML = "Support Staff";
      var table=document.getElementById('table')
      table.innerHTML=`<th>Staff Id</th>
    <th i>Name</th>
    <th>Phone</th>
    <th>Email</th>
    <th>Designation</th>`;
      for(var i=0;i<data.length ;i++)
      {
        var row=`<tr>
            <th i>${data[i].id}</th>
            <th i>${data[i].name}</th>
            <th>${data[i].phone}</th>
           <th>${data[i].email}</th>
           <th>${data[i].designation}</th>

           </tr>`

        table.innerHTML=table.innerHTML+row
      }
    }

   function BuildAdmintable(data)
    {
      document.getElementById("title").innerHTML = "Administrative Staff";
      var table=document.getElementById('table')
      table.innerHTML=`<th>Staff Id</th>
    <th i>Name</th>
    <th>Phone</th>
    <th>Email</th>
    <th>Designation</th>`;
      for(var i=0;i<data.length ;i++)
      {
        var row=`<tr>
            <th i>${data[i].id}</th>
            <th i>${data[i].name}</th>
            <th>${data[i].phone}</th>
           <th>${data[i].email}</th>
           <th>${data[i].designation}</th>

           </tr>`

        table.innerHTML=table.innerHTML+row
      }
    }


function myFunction() {
  document.getElementById("myDropdown").classList.toggle("show");
}

// Close the dropdown if the user clicks outside of it
window.onclick = function(event) {
  if (!event.target.matches('.dropbtn')) {
    var dropdowns = document.getElementsByClassName("dropdown-content");
    var i;
    for (i = 0; i < dropdowns.length; i++) {
      var openDropdown = dropdowns[i];
      if (openDropdown.classList.contains('show')) {
        openDropdown.classList.remove('show');
      }
    }
  }
}