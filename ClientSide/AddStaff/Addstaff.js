function Supportstaff() {
    // e.preventDefault();
    var name = document.getElementById("name").value;
    var phone = document.getElementById("phone").value;
    var email = document.getElementById("Email").value;
    var designation = document.getElementById("designation").value;
    console.log(name)

    fetch("https://localhost:44386/api/Staffs/", {

        // Adding method type 
        method: "POST",

        // Adding body or contents to send 
        body: JSON.stringify({
            "designation": designation,
            "email": email,
            "name": name,
            "phone": phone,
            "staffType": 3
        }),

        // Adding headers to the request 
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })

        // Converting to JSON 
        .then(response => {
            if (response.ok) {

                window.open('file:///C:/Users/jerin/Documents/programing/web%20design/my%20work/staffs.html', "_self")
            }
        })
}

function Administrativestaff() {
    e.preventDefault();
    var name = document.getElementById("name").value;
    var phone = document.getElementById("phone").value;
    var email = document.getElementById("Email").value;
    var designation = document.getElementById("designation").value;
    console.log(name)

    fetch("https://localhost:44386/api/Staffs/", {

        // Adding method type 
        method: "POST",

        // Adding body or contents to send 
        body: JSON.stringify({
            "designation": designation,
            "email": email,
            "name": name,
            "phone": phone,
            "staffType": 2
        }),

        // Adding headers to the request 
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })

        // Converting to JSON 
        .then(response => {
            if (response.ok) {

                window.open('file:///C:/Users/jerin/Documents/programing/web%20design/my%20work/staffs.html', "_self")
            }
        })
}
function Teachingstaff() {
    var name = document.getElementById("name").value;
    var phone = document.getElementById("phone").value;
    var email = document.getElementById("Email").value;
    var classname = document.getElementById("classname").value;
    var subject = document.getElementById("subject").value;

    fetch("https://localhost:44386/api/Staffs/", {

        // Adding method type 
        method: "POST",

        // Adding body or contents to send 
        body: JSON.stringify({
            "subject": subject,
            "email": email,
            "name": name,
            "phone": phone,
            "staffType": 1,
            "className": classname
        }),

        // Adding headers to the request 
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })

        // Converting to JSON 
        .then(response => {
            if (response.ok) {

                window.open("file:///C:/Users/jerin/Documents/programing/web%20design/my%20work/staffs.html", "_self")
            }
        })
}  