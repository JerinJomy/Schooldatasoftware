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
var type="administrative"

function Buildteachingtable(data)
    {
      document.getElementById("title").innerHTML = "Teaching Staff";
      var table=document.getElementById('table')
      table.innerHTML=`<th>Staff Id</th>
    <th >Name</th>
    <th>Phone</th>
    <th>Email</th>
    <th>classname</th>
    <th>Subject</th>
    `;
      for(var i=0;i<data.length ;i++)
      {
        var row=`<tr class="hover">
            <th class="teaching" >${data[i].id}</th>
            <th >${data[i].name}</th>
            <th>${data[i].phone}</th>
           <th>${data[i].email}</th>
           <th>${data[i].className}</th>
           <th>${data[i].subject}</th>
           </tr>`

        table.innerHTML=table.innerHTML+row
      }
      table.innerHTML=table.innerHTML+`<br><br><button onclick="AddTeachingStaff()">Add Staff</button>`
    }

function Buildsupporttable(data)
    {
      document.getElementById("title").innerHTML = "Support Staff";
      var table=document.getElementById('table')
      table.innerHTML=`<th>Staff Id</th>
    <th >Name</th>
    <th>Phone</th>
    <th>Email</th>
    <th>Designation</th>`;
      for(var i=0;i<data.length ;i++)
      {
        var row=`<tr>
            <th class="support" >${data[i].id}</th>
            <th >${data[i].name}</th>
            <th>${data[i].phone}</th>
           <th>${data[i].email}</th>
           <th>${data[i].designation}</th>
           </tr>`

        table.innerHTML=table.innerHTML+row
      }
      table.innerHTML=table.innerHTML+`<br><br><button onclick="AddSupportStaff()">Add Staff</button>`
    }

   function BuildAdmintable(data)
    {
      document.getElementById("title").innerHTML = "Administrative Staff";
      var table=document.getElementById('table')
      table.innerHTML=`<th >Staff Id</th>
    <th >Name</th>
    <th>Phone</th>
    <th>Email</th>
    <th>Designation</th>`;
      for(var i=0;i<data.length ;i++)
      {
        var row=`<tr >
            <th class="admin">${data[i].id}</th>
            <th >${data[i].name}</th>
            <th>${data[i].phone}</th>
           <th>${data[i].email}</th>
           <th>${data[i].designation}</th>

           </tr>`

        table.innerHTML=table.innerHTML+row
      }
      table.innerHTML=table.innerHTML+`<br><br><button onclick="AddAdminStaff()">Add Staff</button>`
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

function AddAdminStaff()
{
  window.open('AddStaff/Administrative.html',"_self")
}

function AddTeachingStaff()
{
  window.open('AddStaff/Teaching.html',"_self")
}
function AddSupportStaff()
{
  window.open('AddStaff/Support.html',"_self")
}

 function EditStaff(){
      var table = document.getElementById("table");
var rows = table.rows;
 for (var i = 0; i < rows.length; i++) 
 {
            
      rows[i].onclick = function () { getval(this); };
            
  }
        function getval(row)
         {
            var id=row.cells[0].innerHTML
            var type=row.cells[0].className
            switch(type)
            {
              case "admin":
                var next="EditStaff/Admin.html?id="+id
                window.open(next,"_self")
                break;
              case "support":
                var next="EditStaff/Support.html?id="+id
                window.open(next,"_self")
                break;
              case "teaching":
                var next="EditStaff/Teaching.html?id="+id
                window.open(next,"_self")
                break;
            }
        }
  }
  GetStaff(type)