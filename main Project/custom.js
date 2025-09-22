function OpenCreateForm() {
    $(".form-div").removeClass("d-none");
    $("#studentsTable").addClass("d-none");
    $("#AddBtn").removeClass("d-none");
    $("#UpdateBtn").addClass("d-none");

    $("#studentId").val(""); // clear the form"
    // Clear all form fields before preparing for new entry
    $("#studentId").val("");
    $("#StudentName").val("");
    $("#StudentClass").val("");
    $("#StudentAge").val("");
    $("#StudentEmail").val("");
    $("#StudentPhone").val("");
    $("#StudentAddress").val("");
}

function ReadAPI() {
    var xhr = new XMLHttpRequest();    
    xhr.open("GET", "/custom/studentpreset/get", true);
    xhr.setRequestHeader("Content-Type", "application/json");

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                try {
                    var result = JSON.parse(xhr.responseText);
                    var students = result.data; // your API returns { data: [...] }

                    // Clear existing rows
                    var $tableBody = $("#studentsTable tbody");
                    $tableBody.empty();

                    // Hide the form and show the table
                    $(".form-div").addClass("d-none");
                    $("#studentsTable").removeClass("d-none");

                    // Loop through students and append rows
                    $.each(students, function (index, student) {
                        var row = `
                                    <tr>
                                        <td>${student.id}</td>
                                        <td>${student.studentName}</td>
                                        <td>${student.studentClass}</td>
                                        <td>${student.studentAge}</td>
                                        <td>${student.studentEmail}</td>
                                        <td>${student.studentPhone}</td>
                                        <td>${student.studentAddress.replace(/\n/g, "<br/>")}</td>
                                        <td>
                                           <button
                                                class="btn btn-warning text-white update-btn"
                                                data-id="${student.id}"
                                                data-name="${student.studentName}"
                                                data-class="${student.studentClass}"
                                                data-age="${student.studentAge}"
                                                data-email="${student.studentEmail}"
                                                data-phone="${student.studentPhone}"
                                                data-address="${student.studentAddress}"
                                                onclick="UpdateForm(this)">
                                                Update
                                            </button>
                                           <button class="btn btn-danger text-white" onclick="DeleteAPI(${student.id})">Delete</button>
                                        </td>
                                    </tr>
                                `;
                        $tableBody.append(row);
                    });
                } catch (e) {
                    console.error("Parse error:", e.message);
                }
            } else {
                console.error("Failed to load students:", xhr.status);
            }
        }
    };

    xhr.send();
}
function UpdateForm(button) {
    const $btn = $(button);

    // Show the form and hide the table
    $(".form-div").removeClass("d-none");
    $("#studentsTable").addClass("d-none");
    $("#AddBtn").addClass("d-none");
    $("#UpdateBtn").removeClass("d-none");

    // Populate the form fields using data-* attributes
    $("#studentId").val($btn.data("id"));
    $("#StudentName").val($btn.data("name"));
    $("#StudentClass").val($btn.data("class"));
    $("#StudentAge").val($btn.data("age"));
    $("#StudentEmail").val($btn.data("email"));
    $("#StudentPhone").val($btn.data("phone"));
    $("#StudentAddress").val($btn.data("address"));   
}

function UpdateAPI() {
    var studentData = {
        id: $("#studentId").val(),
        studentName: $("#StudentName").val(),
        studentClass: $("#StudentClass").val(),
        studentAge: $("#StudentAge").val(),
        studentEmail: $("#StudentEmail").val(),
        studentPhone: $("#StudentPhone").val(),
        studentAddress: $("#StudentAddress").val()
    };

    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/custom/studentpreset/update", true);
    xhr.setRequestHeader("Content-Type", "application/json");

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                var response = JSON.parse(xhr.responseText);
                alert(response.HttpStatusMessage || "Student updated successfully!");

                ReadAPI(); // to refresh the student table
                $(".form-div").addClass("d-none");
                $("#studentsTable").removeClass("d-none");
                $("#studentId").val(""); // clear the form"
            } else {
                alert("Update failed. Status: " + xhr.status);
                console.error("Error:", xhr.responseText);
            }
        }
    };

    xhr.send(JSON.stringify(studentData));
}

function DeleteAPI(id) {
    if (!confirm("Are you sure you want to delete this student?")) {
        return; // cancel if user says no
    }

    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/custom/studentpreset/delete", true);
    xhr.setRequestHeader("Content-Type", "application/json");

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                var response = JSON.parse(xhr.responseText);
                alert(response.HttpStatusMessage || "Student deleted successfully!");
                ReadAPI(); // refresh table
            } else {
                alert("Failed to delete student. Status: " + xhr.status);
                console.error("Error:", xhr.responseText);
            }
        }
    };

    xhr.send(id);
}


//function ReadAPI() {
//    var xhr = new XMLHttpRequest();
//    xhr.open("GET", "/custom/helloworld/hello", true);
//    //xhr.open("GET", "/custom/studentpreset/get", true);
//    xhr.setRequestHeader("Content-Type", "application/json");

//    xhr.onreadystatechange = function () {
//        if (xhr.readyState === 4) {
//            if (xhr.status === 200) {
//                console.log("Response:", JSON.parse(xhr.responseText));

//                studentsTable
//            } else {
//                console.error("Failed:", xhr.status);
//            }
//        }
//    };
//    xhr.send();
//}
