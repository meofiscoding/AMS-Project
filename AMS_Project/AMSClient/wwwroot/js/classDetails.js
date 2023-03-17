<script src="js/helper.js"></script>
$(document).ready(function () {
  debugger;
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
    document
      .getElementById("upload-button")
      .addEventListener("click", function () {
        document.getElementById("upload-input").click();
      });
    var listFiles = [];
    $("#upload-input").on("change", function (e) {
      var files = e.target.files;
      for (var i = 0; i < files.length; i++) {
        var file = files[i];
        listFiles.push(file);
        var fileUI = $(
          '<div class="file-ui">' +
            '<div style="display: flex; align-items-center">' +
            '<i class="fa-solid fa-file-lines fa-2x"></i>' +
            '<strong style="margin-left: 10px">' +
            file.name +
            "</strong>" +
            "</div>" +
            //close button
            '<div class="close-button" style="margin-left: 10px">' +
            '<i class="fa-solid fa-times fa-2x"></i>' +
            "</div>" +
            +"<div>"
        );
        $(".selected-files").append(fileUI);
      }
    });

    //close button on click
    $(".selected-files").on("click", ".close-button", function (e) {
      var fileUI = $(this).parent();
      var fileName = fileUI.find("strong").text();
      for (var i = 0; i < listFiles.length; i++) {
        if (listFiles[i].name == fileName) {
          listFiles.splice(i, 1);
          break;
        }
      }
      fileUI.remove();
    });

    const postInput = document.getElementById("post-input");
    // Add event listener to input
    postInput.addEventListener("click", () => {
      postInput.rows = 5; // Increase rows to 5
    });

    // Add event listener to document
    document.addEventListener("click", (event) => {
      if (!postInput.contains(event.target)) {
        postInput.rows = 1; // Reduce rows to 1
      }
    });
    // Get all the tabs and the content
    const tabButtons = document.querySelectorAll(".tablinks");
    const tabContents = document.querySelectorAll(".tabcontent");
    var url = window.location.href;
    const classId = url.substring(url.lastIndexOf("/") + 1);

    function showTab(tabIndex) {
      // Hide all tab contents
      tabContents.forEach((content) => {
        content.style.display = "none";
      });

      // Remove active class from all tab buttons
      tabButtons.forEach((button) => {
        button.classList.remove("active");
      });

      // Show the selected tab content and set the corresponding button as active
      const selectedTab = tabContents[tabIndex];
      const selectedButton = tabButtons[tabIndex];

      if (tabIndex == 0) {
        var postContainer = $(".post-container");
        postContainer.empty();
          $.ajax({
            url: "https://localhost:7290/api/Posts/getbyclass/" + classId,
            type: "GET",
            headers: { Authorization: "Bearer " + token },
            success: function (data) {
              // Render the posts on the page
              renderPosts(data);
            },
            error: function (xhr, status, err) {
              alert("Failed to get posts for class " + classId);
            },
          });

          // Function to render the posts on the page
         function renderPosts(posts) {
             // Loop through each post and append it to the container
                posts.forEach((post) => {
                    var postUI = $(`
                        <div class="card mb-3">
                             <div class="card-body">
                               <div class="media">
                                    <img class="mr-3 rounded-circle" src="https://avatars.dicebear.com/api/avataaars/${post.user.userEmail}.svg" alt="User Avatar" width="50" height="50">
                                    <div class="media-body">
                                        <h5 class="mt-0">${post.user.fullName}</h5>
                                        <p class="text-muted">Posted on ${convertDate(post.createdAt)}</p>
                                    </div>
                                </div>
                                <div class="post-header-right">
                                    <i class="fa-solid fa-ellipsis-v"></i>
                                </div>
                            </div>
                            <div class="post-body">
                                <h5 class="card-text">${post.postContent}</h5>
                            </div>
                            <hr>
                            <div class="mb-3">
                                <a href="file.pdf">Attachment</a>
                            </div>
                            <div class="card mb-3">
                            <div class="card-body">
                                <form class="col-12">
                                    <div class="form-group">
                                        <textarea class="form-control" id="comment" rows="3" placeholder="Add a comment..."></textarea>
                                    </div>
                                    <button type="submit" class="btn btn-primary">Submit</button>
                                </form>
                            </div>
                        </div>
                        <div class="card">
                            <div class="comment">
                                Comments
                            </div>
                            <ul class="list-group list-group-flush">
                                <li class="list-group-item">
                                    <div class="media">
                                        <img class="mr-3 rounded-circle" src="avatar.jpg" alt="User Avatar" width="50" height="50">
                                        <div class="media-body">
                                            <h5 class="mt-0">Jane Doe</h5>
                                            <p class="card-text">Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                                            <p class="text-muted">Posted on January 2, 2023</p>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                        </div>
                    `);
                    postContainer.append(postUI);
                });
                    
         }
      }


      if (tabIndex == 2) {
        $("#student-list").empty();
        $("#teacher-list").empty();
        //call ajax to get data
        $.ajax({
          url: "https://localhost:7290/api/ClassStudents/" + classId,
          type: "GET",
          headers: { Authorization: "Bearer " + token },
          success: function (data) {
            //get data element that has data.userRole.roleName = "student"
            var studentList = data.filter(function (item) {
              return item.userRole.roleName.toLowerCase() == "student";
            });
            //get first data element that has data.userRole.roleName = "teacher"
            var teacherList = data.filter(function (item) {
              return item.userRole.roleName.toLowerCase() == "teacher";
            });
            //remove duplicates in teacherList
            teacherList = teacherList.filter(
              (v, i, a) => a.findIndex((t) => t.id === v.id) === i
            );

            if (studentList.length == 0) {
              $("#student-list").append("<li>No student in this class</li>");
            } else {
              //looping through studentList and append to student-list
              for (var i = 0; i < studentList.length; i++) {
                var student = studentList[i];
                var html = `<div id="user-item-template">
                                                    <li>
                                                        <input type="checkbox" class="user-checkbox" id=${student.id}>
                                                        <span class="user-name">${student.fullName}</span>
                                                    </li>
                                                </div>`;
                //append html to student-list
                $("#student-list").append(html);
              }
            }

            //looping through teacherList and append to teacher-list
            for (var i = 0; i < teacherList.length; i++) {
              var teacher = teacherList[i];
              var html = `<div id="user-item-template">
                                                <li>
                                                <i class="fa-solid fa-graduation-cap"></i>
                                                    <a class="teacher-name">${teacher.fullName}</a>
                                                </li>
                                            </div>`;
              //append html to teacher-list
              $("#teacher-list").append(html);
            }
          },
          error: function (xhr, status, error) {
            alert(xhr.responseText);
          },
        });
      }

      selectedTab.style.display = "block";
      selectedButton.classList.add("active");
    }

    // Show the first tab by default
    showTab(0);

    // Add click event listeners to tab buttons
    tabButtons.forEach((button, index) => {
      button.addEventListener("click", () => {
        showTab(index);
      });
    });

    //make an ajax call
    $.ajax({
      url: "https://localhost:7290/api/Classes/" + classId,
      type: "GET",
      headers: { Authorization: "Bearer " + token },
      success: function (data) {
        //set class-title html is class name
        $(".class-title").html(data.className);
        $("#stream-classname").html(data.className);
        $("#stream-classcode").html("Class Code: " + data.classCode);
        //replace src attribute in $(".teacher-ava")
        $(".teacher-ava").attr(
          "src",
          `https://avatars.dicebear.com/api/avataaars/${data.teacher.userEmail}.svg`
        );
      },
      error: function (xhr, status, error) {
        alert(xhr.responseText);
      },
    });

    var allSelectted = "";
    var listSelected = [];
    function extractLast(term) {
      //check if term contain ","
      if (term.indexOf(",") == -1) {
        return term;
      } else {
        return term.split(",").pop().trim();
      }
    }

    $("#search-input").autocomplete({
      source: function (request, response) {
        var term = extractLast(request.term);
        debugger;
        // Make an AJAX call to your server to get a list of matching students based on the search term
        $.ajax({
          url: "https://localhost:7290/api/Users",
          type: "GET",
          headers: { Authorization: "Bearer " + token },
          data: { search: term },
          success: function (data) {
            //get data element that has data.userRole.roleName = "student"
            var studentList = data.filter(function (item) {
              return item.userRole.roleName.toLowerCase() == "student";
            });

            //remove element in studentList if it is in listSelected
            for (var i = 0; i < listSelected.length; i++) {
              var selected = listSelected[i];
              studentList = studentList.filter(function (item) {
                return item.id != selected;
              });
            }
            // Map the array of student objects to an array of label/value pairs
            var results = studentList.map(function (student) {
              return {
                label: student.userEmail,
                value: student.id,
              };
            });
            response(results);
          },
          error: function (xhr, status, error) {
            alert(xhr.responseText);
          },
        });
        // Return the list of matching students as an array
      },
      minLength: 2,
      multiple: true, // Enable multiple select
      select: function (event, ui) {
        //append ui.item.value to allSelectted
        allSelectted += ui.item.label + ", ";
        listSelected.push(ui.item.value);
        $(this).val(allSelectted);
        return false;
      },
      focus: function (event, ui) {
        // Handle the focus event when a student is highlighted in the autocomplete dropdown
      },
    });

    //each time user delete a character in search-input
    $("#search-input").on("keydown", function (event) {
      //check if user press backspace
      if (event.keyCode == 8) {
        //get the last character in allSelectted
        var lastChar = allSelectted[allSelectted.length - 1];
        //check if lastChar is ","
        if (lastChar == ",") {
          //remove the last character in allSelectted
          allSelectted = allSelectted.substring(0, allSelectted.length - 1);
          //remove the last element in listSelected
          listSelected.pop();
        }
      }
    });

    //onClick in #addToClass
    $("#addToClass").click(function () {
      //call api to add user to class using listSelected
      $.ajax({
        url:
          "https://localhost:7290/api/ClassStudents/" + classId + "/students",
        type: "POST",
        contentType: "application/json",
        headers: { Authorization: "Bearer " + token },
        data: JSON.stringify(listSelected),
        success: function (data) {
          alert(data);
          location.reload();
        },
        error: function (xhr, status, error) {
          alert(xhr.responseText);
        },
      });
    });

    $(".btn-post").click(function (e) {
      //create a new FormData object
      var formData = new FormData();
      // Append the classId and content from text area to the FormData object
      formData.append("ClassId", classId);
      //get text from text area
      formData.append("Content", $("#post-input").val());
      // Loop through each file and append it to the FormData object
      for (var i = 0; i < listFiles.length; i++) {
        var file = listFiles[i];
        formData.append("Files", file, file.name);
      }

      // Make an AJAX call to your server to add the post
      $.ajax({
        url: "https://localhost:7290/api/Posts",
        type: "POST",
        headers: { Authorization: "Bearer " + token },
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
          alert(data);
          location.reload();
        },
        error: function (xhr, status, error) {
          alert(xhr.responseText);
        },
      });
    });
  }
});
