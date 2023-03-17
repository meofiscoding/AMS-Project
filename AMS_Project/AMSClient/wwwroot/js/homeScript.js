<script src="js/helper.js"></script>
$(document).ready(function () {
  debugger;
  // Get the token from local storage
  const token = localStorage.getItem("token");
  //redirect to the login page if the local storage is empty
  if (token == null) {
    window.location.href = "/Login";
    return;
  }

  // Decode the token to get the expiration time
  const decodedToken = jwt_decode(token);
  // Check if the token has expired
  const expirationTime = decodedToken.exp;
  //get user name from token
  //const userName = decodedToken.unique_name;

  if (Date.now() >= expirationTime * 1000) {
    // Prompt the user to log in again
    alert("Your session has expired. Please log in again.");
  } else {
    $("#create-class-form").validate({
      rules: {
        classname: {
          required: true,
        },
      },
    });

    $("#create-class-button").click(function () {
      //show modal
      $("#exampleModal").modal("show");
    });
    // Make the AJAX call to the API to load class Students
    $.ajax({
      url: "https://localhost:7290/api/ClassStudents",
      type: "GET",
      headers: { Authorization: "Bearer " + token },
      success: function (result) {
        //check if result.classes has element in list
        if (result.classes.length === 0) {
          // Display the Join Class or Create Class button based on the user's role
          if (result.role.toLowerCase() === "student") {
            $("#join-class-button").removeClass("d-none");
          } else if (result.role.toLowerCase() === "teacher") {
            $("#create-class-button").removeClass("d-none");
          }
        } else {
          debugger;
          // Display the classes
          result.classes.forEach(function (cls) {
            const listItem = ` <div class="classroom-card">
                            <div class="classroom-card-header">
                                <div class="classroom-card-icon"><i class="fa fa-chalkboard"></i></div>
                                <div class="classroom-card-title">
                                    <a class="classroom-card-link" href="/Class/Details/${
                                      cls.id
                                    }">${cls.className}</a>
                                    <p>Class Code: ${cls.classCode}</p>
                                    <p>Created Date: ${convertDate(
                                      cls.createAt
                                    )}</p>
                                    <span class="badge badge-${
                                      cls.classStatus == 1
                                        ? "success"
                                        : "danger"
                                    }" id="status-active">${
              cls.classStatus == 1 ? "Active" : "Stopped"
            }</span>
                                </div>
                            </div>
                            <div class="classroom-card-body">
                                <p>${cls.classDescription}</p>  
                            </div>
                            <div class="classtoom-card-footer">

                            </div>
                        </div>`;
            $(".classrooms").append(listItem);
          });
        }
      },
      error: function (xhr, status, error) {
        alert(xhr.responseText);
      },
    });

    $("#submit-create-class").click(function (event) {
      event.preventDefault();
      //validate the form
      if (!$("#create-class-form").valid()) {
        return;
      }
      //generate random class code with 4 digit
      var classCode = Math.floor(1000 + Math.random() * 9000);

      // Make the AJAX call to create the class
      $.ajax({
        url: "https://localhost:7290/api/Classes",
        method: "POST",
        data: JSON.stringify({
          ClassName: $("#class-name").val(),
          ClassDescription: $("#class-description").val(),
          ClassCode: classCode.toString(),
          ClassStatus: 1,
          TeacherId: parseInt(decodedToken.sub, 10),
        }),
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + localStorage.getItem("token"),
        },
        success: function (result) {
          //add d-none class to button
          $("#create-class-button").removeClass("d-none");
          //add html to show class
          const classHTML = ` <div class="classroom-card">
                <div class="classroom-card-header">
                    <div class="classroom-card-icon"><i class="fa fa-chalkboard"></i></div>
                    <div class="classroom-card-title">
                        <a class="classroom-card-link" href="/Class/Details/${
                          result.id
                        }">${result.className}</a>
                        <p>Class Code: ${result.classCode}</p>
                        <p>Created Date: ${convertDate(result.createAt)}</p>
                        <span class="badge badge-${
                          result.classStatus == 1 ? "success" : "danger"
                        }" id="status-active">${
            result.classStatus == 1 ? "Active" : "Stopped"
          }</span>
                    </div>
                </div>
                <div class="classroom-card-body">
                    <p>${result.classDescription}</p>  
                </div>
                <div class="classtoom-card-footer">
                    
                </div>
            </div>`;
          $(".classrooms").append(classHTML);
          //add d-none class to button
          $("#create-class-button").addClass("d-none");
          //close modal
          $("#exampleModal").modal("hide");
        },
        error: function (xhr, status, error) {
          alert(xhr.responseText);
        },
      });
    });
  }
});
