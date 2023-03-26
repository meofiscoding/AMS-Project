$(document).ready(function () {
  debugger;
  $("#create-class-form").validate({
    rules: {
      classname: {
        required: true,
      },
    },
  });

  // Get the join class button and the modal
  const joinClassButton = document.querySelector("#join-class-button");
  const joinClassModal = document.querySelector("#joinClassModal");

  // Get the input field and the join button from the modal
  const classCodeInput = joinClassModal.querySelector("#classCode");
  const joinClassBtn = joinClassModal.querySelector("#joinClassBtn");

  // Add a click event listener to the join class button
  joinClassButton.addEventListener("click", () => {
    // Show the modal
    $(joinClassModal).modal("show");
  });

  // Add a click event listener to the join button in the modal
  joinClassBtn.addEventListener("click", () => {
    // Get the value of the class code input field
    const classCode = classCodeInput.value;

    // Make an AJAX request to add the student to the class
    // Replace the URL and data with your own API endpoint and data
    $.ajax({
      url: "https://localhost:7290/api/ClassStudents/JoinClass/" + classCode,
      type: "POST",
      headers: { Authorization: "Bearer " + token },
      success: function (response) {
        debugger;
        // Handle the success response
        console.log(response);

        // Hide the modal
        $(joinClassModal).modal("hide");

        // Reset the input field
        classCodeInput.value = "";
        var classHtml = ` <div class="classroom-card">
                            <div class="classroom-card-header">
                                <div class="classroom-card-icon"><i class="fa fa-chalkboard"></i></div>
                                <div class="classroom-card-title">
                                    <a class="classroom-card-link" href="/Class/Details/${
                                      response.id
                                    }">${response.className}</a>
                                    <p>Class Code: ${response.classCode}</p>
                                    <p>Created Date: ${moment(
                                      response.createAt
                                    ).fromNow()}</p>
                                    <span class="badge badge-${
                                      response.classStatus == 1
                                        ? "success"
                                        : "danger"
                                    }" id="status-active">${
                                      response.classStatus == 1 ? "Active" : "Stopped"
        }</span>
                                </div>
                            </div>
                            <div class="classroom-card-body">
                                <p>${response.classDescription}</p>  
                            </div>
                            <div class="classtoom-card-footer">

                            </div>
                        </div>`;
        $(".classrooms").append(classHtml);
      },
      error: function (error) {
        // Handle the error response
        alert(error.responseText);
      },
    });
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
      if (result.role.toLowerCase() === "student") {
        $("#create-class-button").addClass("d-none");
      } else if (result.role.toLowerCase() === "teacher") {
        $("#join-class-button").addClass("d-none");
      }
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
                                    <p>Created Date: ${moment(
                                      cls.createAt
                                    ).fromNow()}</p>
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
        //add html to show class
        const classHTML = ` <div class="classroom-card">
                <div class="classroom-card-header">
                    <div class="classroom-card-icon"><i class="fa fa-chalkboard"></i></div>
                    <div class="classroom-card-title">
                        <a class="classroom-card-link" href="/Class/Details/${
                          result.id
                        }">${result.className}</a>
                        <p>Class Code: ${result.classCode}</p>
                        <p>Created Date: ${moment(
                          result.createAt
                        ).fromNow()}</p>
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
        //close modal
        $("#exampleModal").modal("hide");
      },
      error: function (xhr, status, error) {
        alert(xhr.responseText);
      },
    });
  });
});
