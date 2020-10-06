function GetStaff(type) {
  switch (type) {
    case "administrative":
      var request = 'https://localhost:44386/api/Staffs/?type=';
      request = request + type;
      fetch(request).then((res) => res.json()).then((data) => BuildAdmintable(data))
      break;

    case "teaching":
      var request = 'https://localhost:44386/api/Staffs/?type=';
      request = request + type;
      fetch(request).then((res) => res.json()).then((data) => Buildteachingtable(data))
      break;
    case "support":
      var request = 'https://localhost:44386/api/Staffs/?type=';
      request = request + type;
      fetch(request).then((res) => res.json()).then((data) => Buildsupporttable(data))
      break;
  }
}

function Buildteachingtable(data) {
  document.getElementById("StaffType").innerHTML = "Teaching Staff";
  var table = document.getElementById('stafftable')
  table.innerHTML = `<th>Staff Id</th>
    <th >Name</th>
    <th>Phone</th>
    <th>Email</th>
    <th>classname</th>
    <th>Subject</th>
    `;
  for (var i = 0; i < data.length; i++) {
    var row = `<tr>
            <th class="teaching" >${data[i].id}</th>
            <th >${data[i].name}</th>
            <th>${data[i].phone}</th>
           <th>${data[i].email}</th>
           <th>${data[i].className}</th>
           <th>${data[i].subject}</th>
           <td><button data-id="${data[i].id}" onclick="Edit(this)" data-type="teaching">edit</button></td>
           <td><button onclick="Delete(this)" data-id="${data[i].id}">Delete</button></td>
           </tr>`

    table.innerHTML = table.innerHTML + row
  }
  table.innerHTML = table.innerHTML + `<br><br><button onclick="LoadTeachingStaffForm()">Add Staff</button>`
}

function Buildsupporttable(data) {
  document.getElementById("StaffType").innerHTML = "Support Staff";
  var table = document.getElementById('stafftable')
  table.innerHTML = `<th>Staff Id</th>
    <th >Name</th>
    <th>Phone</th>
    <th>Email</th>
    <th>Designation</th>`;
  for (var i = 0; i < data.length; i++) {
    var row = `<tr>
            <th class="support" >${data[i].id}</th>
            <th >${data[i].name}</th>
            <th>${data[i].phone}</th>
           <th>${data[i].email}</th>
           <th>${data[i].designation}</th>
           <td><button onclick="Edit(this)" data-id="${data[i].id}" data-type="support">edit</button></td>
           <td><button onclick="Delete(this)" data-id="${data[i].id}">Delete</button></td>
           </tr>`
    table.innerHTML = table.innerHTML + row
  }
  table.innerHTML = table.innerHTML + `<br><br><button onclick="LoadSupportStaffForm()">Add Staff</button>`
}

function BuildAdmintable(data) {
  document.getElementById("StaffType").innerHTML = "Administrative Staff";
  var table = document.getElementById('stafftable')
  table.innerHTML = `<th >Staff Id</th>
    <th >Name</th>
    <th>Phone</th>
    <th>Email</th>
    <th>Designation</th>
    `;
  for (var i = 0; i < data.length; i++) {
    var row = `<tr >
            <th class="admin">${data[i].id}</th>
            <th >${data[i].name}</th>
            <th>${data[i].phone}</th>
           <th>${data[i].email}</th>
           <th>${data[i].designation}</th>
           <td><button onclick="Edit(this)" data-id="${data[i].id}" data-type="admin">edit</button></td>
           <td><button onclick="Delete(this)" data-id="${data[i].id}" >Delete</button></td>
           </tr>`

    table.innerHTML = table.innerHTML + row
  }
  table.innerHTML = table.innerHTML + `<br><br><button onclick="LoadAdminStaffForm()">Add Staff</button>`
}


function myFunction() {
  document.getElementById("myDropdown").classList.toggle("show");
}

function LoadAdminStaffForm() {
  var next = "EditandAdd/Admin.html?id=" + 0 + "&type=admin";
  window.open(next, "_self")
}

function LoadTeachingStaffForm() {
  var next = "EditandAdd/Teaching.html?id=" + 0 + "&type=teaching"
  window.open(next, "_self")
}
function LoadSupportStaffForm() {
  var next = "EditandAdd/Support.html?id=" + 0 + "&type=support";
  window.open(next, "_self")
}

function Edit(button) {
  var id = button.getAttribute("data-id")
  var type = button.getAttribute("data-type")
  switch (type) {
    case "admin":
      var next = "EditandAdd/Admin.html?id=" + id + "&type=" + type
      window.open(next, "_self")
      break;
    case "support":
      var next = "EditandAdd/Support.html?id=" + id + "&type=" + type
      window.open(next, "_self")
      break;
    case "teaching":
      var next = "EditandAdd/Teaching.html?id=" + id + "&type=" + type
      window.open(next, "_self")
      break;
  }
}

function Delete(button) {
  var id = button.getAttribute("data-id")
  var select = confirm("Press ok to delete");
  if (select) {
    request = "https://localhost:44386/api/Staffs/" + id
    fetch(request, {
      method: 'DELETE'
    }).then(response => {
      if (response.ok) {
        location.reload();
      }
    })
  }
}