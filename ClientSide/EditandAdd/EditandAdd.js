var Params = new URLSearchParams(document.location.search.substring(1));
var id = Params.get('id');
var type = Params.get('type');
function SelectForm() {
	if (id != 0) {
		GetStaffById();
	}
}
function SelectRequest() {
	if (id == 0) {
		var request = "https://localhost:44386/api/Staffs/"
		AddandEdit('POST', request);
	}
	else {
		var request = "https://localhost:44386/api/Staffs/" + id
		var select = confirm("Press ok to edit");
		if (select) {
			AddandEdit('PUT', request)
		}
		else {
			window.open("../staffs.html", "_self")
		}
	}
}
function GetStaffById() {
	var request = 'https://localhost:44386/api/Staffs/' + id
	fetch(request).then((res) => res.json()).then((data) => PopulateForm(data))
}

function PopulateForm(data) {
	var name = data.name
	var email = data.email
	var phone = data.phone
	var id = data.id
	var staffType = data.staffType

	switch (staffType) {
		case 1:
			document.getElementById("teachingname").value = name
			document.getElementById("teachingphone").value = phone
			document.getElementById("teachingemail").value = email
			document.getElementById("teachingid").innerHTML = "StaffId:" + id
			var classname = data.className
			var subject = data.subject
			document.getElementById("subject").value = subject
			document.getElementById("classname").value = classname
			break;
		case 2:
			document.getElementById("adminname").value = name
			document.getElementById("adminphone").value = phone
			document.getElementById("adminemail").value = email
			document.getElementById("adminid").innerHTML = "StaffId:" + id
			var designation = data.designation
			document.getElementById("admindesignation").value = designation
			break;
		case 3:
			document.getElementById("supportname").value = name
			document.getElementById("supportphone").value = phone
			document.getElementById("supportemail").value = email
			document.getElementById("supportid").innerHTML = "StaffId:" + id
			var designation = data.designation
			document.getElementById("supportdesignation").value = designation
			break;
	}
}

function AddandEdit(method, request) {
	var request = request
	var data = {}
	switch (type) {
		case "teaching":
			var name = document.getElementById("teachingname").value
			var phone = document.getElementById("teachingphone").value
			var email = document.getElementById("teachingemail").value
			var classname = document.getElementById("classname").value
			var subject = document.getElementById("subject").value
			data = { "subject": subject, "email": email, "name": name, "phone": phone, "staffType": 1, "className": classname, "id": id }
			break;
		case "admin":
			var name = document.getElementById("adminname").value
			var phone = document.getElementById("adminphone").value
			var email = document.getElementById("adminemail").value
			var designation = document.getElementById("admindesignation").value
			data = { "email": email, "name": name, "phone": phone, "staffType": 2, "designation": designation, "id": id }
			break;
		case "support":
			var name = document.getElementById("supportname").value
			var phone = document.getElementById("supportphone").value
			var email = document.getElementById("supportemail").value
			var designation = document.getElementById("supportdesignation").value
			data = { "email": email, "name": name, "phone": phone, "staffType": 3, "designation": designation, "id": id }
			break;
	}
	fetch(request, {
		method: method,
		body: JSON.stringify(data),
		headers: {
			"Content-type": "application/json; charset=UTF-8"
		}
	})
		.then(response => {
			if (response.ok) {
				window.open("../staffs.html", "_self")
			}
		})
}
