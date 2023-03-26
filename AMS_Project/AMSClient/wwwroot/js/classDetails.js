$(document).ready(function () {
  debugger;
  // Create a connection to the SignalR hub
  const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();
  //add click event to button send-message
  $("#chat-window-container").on("click", ".send-message", function (e) {
    e.preventDefault();
    var message = $(this).closest(".chat-window").find(".chat-message").val();
    var groupId = $(this).closest(".chat-window").data("group-id");
    //get current user email
    var userEmail = decodedToken.unique_name;
    //get h4 html in chat-header
    var group = $(this).closest(".chat-window").find(".chat-header h4").html();
    //call api to save message to database
    $.ajax({
      url: "https://localhost:7290/api/Groups/" + groupId + "/messages",
      type: "POST",
      data: JSON.stringify({
        Message: message,
        GroupId: groupId,
        UserEmail: userEmail,
      }),
      contentType: "application/json",
      success: function (data) {
        // Send the message to the server
        connection
          .invoke("SendMessage", group + "-c" + groupId, userEmail, message)
          .catch((error) => alert(error));
        $(this).closest(".chat-window").find(".chat-message").val("");
      },
      error: function (error) {
        console.log(error);
      },
    });
  });

  //add click event to close-chat-window
  $("#chat-window-container").on("click", ".close-chat-window", function (e) {
    //check if chat-body has d-none class
    if ($(this).closest(".chat-window").find(".chat-body").hasClass("d-none")) {
      //add d-none to chat-body
      $(this).closest(".chat-window").find(".chat-body").removeClass("d-none");
      //add d-none to chat-footer
      $(this)
        .closest(".chat-window")
        .find(".chat-footer")
        .removeClass("d-none");
    } else {
      //remove d-none to chat-body
      $(this).closest(".chat-window").find(".chat-body").addClass("d-none");
      //remove d-none to chat-footer
      $(this).closest(".chat-window").find(".chat-footer").addClass("d-none");
    }
  });

  //#send-file on change
  $("#chat-window-container").on("change", "#send-file", function (e) {
    var file = e.target.files[0];
    var groupId = $(this).closest(".chat-window").data("group-id");
    //get current user email
    var userEmail = decodedToken.unique_name;
    //get h4 html in chat-header
    var group = $(this).closest(".chat-window").find(".chat-header h4").html();
    // Convert the File object to an IFormFile
    // Create a new FormData instance
    var formData = new FormData();
    formData.append("file", file, file.name);
    //appen groupId
    formData.append("groupId", groupId);
    $.ajax({
      url: "https://localhost:7290/api/Groups/uploadfile",
      type: "POST",
      // headers: { Authorization: "Bearer " + token },
      data: formData,
      contentType: false,
      processData: false,
      success: function (data) {
        // On success, send the file path to the server via SignalR
        connection
          .invoke("SendMessage", group + "-c" + groupId, userEmail, data)
          .catch((error) => alert(error));
      },
      error: function (error) {
        console.log(error);
      },
    });
  });

  //Receive a message from the server
  connection.on("ReceiveMessage", function (user, groupId, message) {
    //Add the message to the chat window
    const chatWindow = $(".chat-window[data-group-id='" + groupId + "']");
    const chatMessages = chatWindow.find(".chat-messages");
    chatMessages.append(
      `<div class="message">
          <img src="https://avatars.dicebear.com/api/avataaars/${user}.svg" width="30px" height="30px" style="border-radius: 50%; margin-right: 10px" /> 
          <div>
              <strong>${user}</strong>
              ${message} 
          </div>
          </div>`
    );
  });

  //on UserJoined
  connection.on("UserJoined", function (user, groupId) {
    //Add the message to the chat window
    const chatWindow = $(".chat-window[data-group-id='" + groupId + "']");
    //remove d-none in chat-body and chat-footer
    chatWindow.find(".chat-body").removeClass("d-none");
    chatWindow.find(".chat-footer").removeClass("d-none");
    const chatMessages = chatWindow.find(".chat-messages");
    chatMessages.append(
      `<div class="message">
          <img src="https://avatars.dicebear.com/api/avataaars/${user}.svg" width="30px" height="30px" style="border-radius: 50%; margin-right: 10px" />
          <div>
              <strong>${user}</strong>
              joined the group
          </div>
          </div>`
    );
  });

  //on Receive File
  connection.on("ReceiveFile", function (user, groupId, fileName, fileUrl) {
    //Add the message to the chat window
    const chatWindow = $(".chat-window[data-group-id='" + groupId + "']");
    chatWindow.find(".chat-body").removeClass("d-none");
    chatWindow.find(".chat-footer").removeClass("d-none");
    // Create a new element to display the file
    var fileLink = `<a href="${fileUrl}" target="_blank">${fileName}</a>`;
    const chatMessages = chatWindow.find(".chat-messages");
    chatMessages.append(`<div class="message">
    <img src="https://avatars.dicebear.com/api/avataaars/${user}.svg" width="30px" height="30px" style="border-radius: 50%; margin-right: 10px" /> 
    <div>
        <strong>${user}</strong>
        ${fileLink} 
    </div>
    </div>`);
  });

  // Start the connection to the hub
  connection.start().catch((error) => alert(error));
  //replace src attribute in $(".teacher-ava")
  $(".post-ava").attr(
    "src",
    `https://avatars.dicebear.com/api/avataaars/${decodedToken.unique_name}.svg`
  );

  document
    .getElementById("assignment-attachment")
    .addEventListener("click", function () {
      document.getElementById("attachment-input").click();
    });

  var AssignmentFiles = [];
  $("#attachment-input").on("change", function (e) {
    debugger;
    var files = e.target.files;
    for (var i = 0; i < files.length; i++) {
      var file = files[i];
      AssignmentFiles.push(file);
      var fileUI = "";
      //check file type
      if (file.type.match("image.*")) {
        fileUI = `<div class="file-ui"><img src="${URL.createObjectURL(
          file
        )}" width="50%"/>`;
      } else if (file.type.match("video.*")) {
        fileUI = ` <div class="file-ui"><video controls>
              <source src="${URL.createObjectURL(file)}" type="video/mp4">
              Your browser does not support the video tag.
              </video>`;
      } else {
        fileUI =
          '<div class="file-ui">' +
          '<div style="display: flex; align-items-center">' +
          '<i class="fa-solid fa-file-lines fa-2x"></i>' +
          '<strong style="margin-left: 10px">' +
          file.name +
          "</strong>" +
          "</div>";
      }
      //close button
      fileUI +=
        '<div class="close-button" style="margin-left: 10px">' +
        '<i class="fa-solid fa-times fa-2x"></i>' +
        "</div>" +
        "</div>";
      $(".selected-Assfiles").append(fileUI);
    }
  });

  $(".selected-Assfiles").on("click", ".close-button", function (e) {
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

  $("#assignBtn").click(function (e) {
    //create a new FormData object
    var formData = new FormData();
    // Append the classId and content from text area to the FormData object
    formData.append("ClassId", classId);
    //get text from text area
    formData.append("Title", $("#title").val());
    formData.append("Deadline", $("#dueDate").val());
    formData.append("Description", $("#description").val());
    // Loop through each file and append it to the FormData object
    for (var i = 0; i < AssignmentFiles.length; i++) {
      var file = AssignmentFiles[i];
      formData.append("Files", file, file.name);
    }

    // Make an AJAX call to your server to add the post
    $.ajax({
      url: "https://localhost:7290/api/Assignments",
      type: "POST",
      headers: { Authorization: "Bearer " + token },
      data: formData,
      processData: false,
      contentType: false,
      success: function (data) {
        //refresh the page
        location.reload();
      },
      error: function (xhr, status, error) {
        alert(xhr.responseText);
      },
    });
  });

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
      var fileUI = "";
      //check file type
      if (file.type.match("image.*")) {
        fileUI = `<div class="file-ui"><img src="${URL.createObjectURL(
          file
        )}" width="50%"/>`;
      } else if (file.type.match("video.*")) {
        fileUI = ` <div class="file-ui"><video controls>
              <source src="${URL.createObjectURL(file)}" type="video/mp4">
              Your browser does not support the video tag.
              </video>`;
      } else {
        fileUI = $(
          '<div class="file-ui">' +
            '<div style="display: flex; align-items-center">' +
            '<i class="fa-solid fa-file-lines fa-2x"></i>' +
            '<strong style="margin-left: 10px">' +
            file.name +
            "</strong>" +
            "</div>"
        );
      }
      //close button
      fileUI +=
        '<div class="close-button" style="margin-left: 10px">' +
        '<i class="fa-solid fa-times fa-2x"></i>' +
        "</div>" +
        "</div>";
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
                                    <img class="mr-3 rounded-circle" src="https://avatars.dicebear.com/api/avataaars/${
                                      post.user.userEmail
                                    }.svg" alt="User Avatar" width="50" height="50">
                                    <div class="media-body">
                                        <h5 class="mt-0">${
                                          post.user.fullName ==
                                          decodedToken.FullName
                                            ? "You"
                                            : post.user.fullName
                                        }</h5>
                                        ${
                                          post.user.userRole.roleName.toLowerCase() ==
                                          "teacher"
                                            ? 'Teacher <i class="fa-solid fa-crown"></i>'
                                            : ""
                                        }
                                        <p class="text-muted">Posted on ${moment(
                                          post.createdAt
                                        ).fromNow()}</p>
                                    </div>
                                </div>
                                <div class="post-header-right">
                                    <i class="fa-solid fa-ellipsis-v"></i>
                                </div>
                            </div>
                            <div class="post-body">
                                <h5 class="card-text">${post.postContent}</h5>
                            </div>
                            <div class="my-3 px-3" >
                                ${renderResources(post.resources)}
                            </div>
                            <div class="card mb-3">
                        </div>
                        <div class="card">
                                  ${renderComments(post.comments)}
                        </div>
                        <div class="card-body">
                        <form class="col-12">
                            <div class="form-group" style="display:flex">
                            <img class="mr-3 rounded-circle" src="https://avatars.dicebear.com/api/avataaars/${
                              decodedToken.unique_name
                            }.svg" width = "50" height="50" >
                                <textarea class="form-control mb-3" id="comment" rows="3" placeholder="Add a comment..."></textarea>
                            </div>
                            <button type="button" class="btn btn-primary add-comment" data-post-id="${
                              post.id
                            }" disabled>Submit</button>
                        </form>
                    </div>
                        </div>
                    `);
          postContainer.append(postUI);
        });
      }

      // Function to render the resources (attachments) of a post
      function renderResources(resources) {
        var resourcesHtml = "";

        for (var i = 0; i < resources.length; i++) {
          var resource = resources[i];
          resourcesHtml +=
            `
                <div class="col-12">
                    <div class="card mb-3" style= "background: #e3e4eb;">
                        <div class="card-body">
                            <a href="` +
            resource.fileUrl +
            `" download>` +
            resource.resourceName +
            `</a>
              <i class="fa-solid fa-download"></i>
                        </div>
                    </div>
                </div>`;
        }

        return resourcesHtml;
      }

      //function to render comments of a post
      function renderComments(comments) {
        var html = "";
        if (comments.length > 0) {
          html += `<div class="comment">
                             <strong>Comments</strong>
                      </div>
                 <ul class="list-group list-group-flush">`;
        }
        for (var i = 0; i < comments.length; i++) {
          var comment = comments[i];
          html += ` <li class="list-group-item">
                            <div class="media">
                                <img class="mr-3 rounded-circle" src="https://avatars.dicebear.com/api/avataaars/${
                                  comment.user.userEmail
                                }.svg" alt="User Avatar" width="50" height="50">
                                <div class="media-body">
                                    <strong class="mt-0">${
                                      comment.user.fullName ==
                                      decodedToken.FullName
                                        ? "You"
                                        : comment.user.fullName
                                    }</strong>
                                      ${
                                        comment.user.userRole.roleName.toLowerCase() ==
                                        "teacher"
                                          ? '<i class="fa-solid fa-crown"></i>'
                                          : ""
                                      }
                                    <p class="card-text">${comment.content}</p>
                                    <p class="text-muted">Commented on ${moment(
                                      comment.createdAt
                                    ).fromNow()}</p>
                                </div>
                            </div>
                        </li>`;
          if (i == comments.length - 1) {
            html += `</ul>`;
          }
        }
        return html;
      }

      //add comment
      $(".post-container").on("click", ".add-comment", function (e) {
        debugger;
        //get comment
        var comment = $(this).closest("form").find("#comment").val();
        var postId = $(this).data("post-id");
        $.ajax({
          url: "https://localhost:7290/api/Comments",
          type: "POST",
          contentType: "application/json",
          headers: { Authorization: "Bearer " + token },
          data: JSON.stringify({ Content: comment, PostId: postId }),
          success: function (data) {
            //remove comment from text
            $(e.target).closest("form").find("#comment").val("");
            //append comment
            var html = ` <li class="list-group-item">
           <div class="media">
               <img class="mr-3 rounded-circle" src="https://avatars.dicebear.com/api/avataaars/${
                 data.user.userEmail
               }.svg" alt="User Avatar" width="50" height="50">
               <div class="media-body">
                   <strong class="mt-0">${
                     data.user.fullName == decodedToken.FullName
                       ? "You"
                       : data.user.fullName
                   }</strong>
                     ${
                       data.user.userRole.roleName.toLowerCase() == "teacher"
                         ? '<i class="fa-solid fa-crown"></i>'
                         : ""
                     }
                   <p class="card-text">${data.content}</p>
                   <p class="text-muted">Commented on ${moment().fromNow()}</p>
               </div>
           </div>
       </li>`;
            $(e.target).closest(".card-body").prev().find("ul").append(html);
          },
          error: function (xhr, status, err) {
            alert("Failed to add comment");
          },
        });
      });

      //on input change
      $(".post-container").on("input", "#comment", function () {
        //check if it has value
        if ($(this).val() != "") {
          //remove disabled attribute in .add-comment
          $(".add-comment").removeAttr("disabled");
        } else {
          //add disabled attribute in .add-comment
          $(".add-comment").attr("disabled", "disabled");
        }
      });
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
              debugger;
              var html = `<tr>
                              <td><input type="checkbox" class="user-checkbox" id="${student.id}"></td>
                              <td class="user-avatar text-center"><img src="https://avatars.dicebear.com/api/avataaars/${student.userEmail}.svg" alt="${student.fullName}"></td>
                              <td class="user-name">${student.fullName}</td>
                              <td class="user-email">${student.userEmail}</td>
                            </tr>`;
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

    if (tabIndex == 3) {
      //load group
      $.ajax({
        url: "https://localhost:7290/api/Groups?classId=" + classId,
        type: "GET",
        // headers: { Authorization: "Bearer " + token },
        success: function (data) {
          //clear chat-list
          $("#chat-window-container").empty();
          //clear group-list
          $("#group-list").empty();
          if (data.length == 0) {
            //remove d-none class from .no-group
            $(".no-group").removeClass("d-none");
          } else {
            //loop through data
            for (var i = 0; i < data.length; i++) {
              var group = data[i];
              //join teacher of class to group SignalR
              //get teacherEmail from Cookies
              var teacherEmail = Cookies.get("teacherEmail");
              connection
                .invoke(
                  "JoinGroup",
                  group.groupName + "-c" + group.id,
                  teacherEmail
                )
                .catch(function (err) {
                  return console.error(err.toString());
                });
              //Generate chat html
              var chatWindow = $("<div>")
                .addClass("chat-window")
                .attr("data-group-id", group.id);
              var chatHeader = $("<div>").addClass("chat-header");
              //add h4 with group.groupName text
              chatHeader.append($("<h4>").text(group.groupName));
              //add button close chat inside chatHeader
              chatHeader.append(
                $("<button>").addClass("close-chat-window").html("-")
              );
              //add chatHeader to chatWindow
              chatWindow.append(chatHeader);
              //add div with chat-body to chatWindow
              chatWindow.append($("<div>").addClass("chat-body"));
              //add div with class chat-messages to chat-body
              chatWindow
                .find(".chat-body")
                .append($("<div>").addClass("chat-messages"));
              //add div with class chat-footer to chatWindow
              chatWindow.append($("<div>").addClass("chat-footer"));
              //add form with class chat-form to chat-footer
              chatWindow
                .find(".chat-footer")
                .append($("<form>").addClass("chat-form"));
              //add div with class input-group inside form
              chatWindow
                .find(".chat-form")
                .append($("<div>").addClass("input-group"));
              //add input type = text class="form-control chat-message" placeholder="Type your message..." inside div
              chatWindow
                .find(".input-group")
                .append(
                  $("<input>")
                    .attr("type", "text")
                    .addClass("form-control chat-message")
                    .attr("placeholder", "Type your message...")
                );
              //add div with class input-group-append inside div
              chatWindow
                .find(".input-group")
                .append($("<div>").addClass("input-group-append"));
              //add button with class btn btn-primary chat-send inside div
              chatWindow
                .find(".input-group-append")
                .append(
                  $("<button>")
                    .attr("type", "button")
                    .addClass("btn btn-primary send-message")
                    .text("Send")
                );
              //add label for="send-file" class="btn btn-secondary" text is file inside div
              chatWindow
                .find(".input-group-append")
                .append(
                  $("<label>")
                    .attr("for", "send-file")
                    .addClass("btn btn-secondary")
                    .text("File")
                );
              //add input type="file" class="d-none" id="send-file" inside div
              chatWindow
                .find(".input-group-append")
                .append(
                  $("<input>")
                    .attr("type", "file")
                    .addClass("d-none")
                    .attr("id", "send-file")
                );
              //add css right to chatWindow
              chatWindow.css("right", i * 360 + "px");
              //append chatWindow to chat-container
              $("#chat-window-container").append(chatWindow);
              //Generate group html
              var html = `<div class="col-md-4 mb-4">
              <div class="card">
                <div class="card-header text-center">
                 <strong>${group.groupName}</strong>
                </div>
                <div class="card-body">
                  <div class="student-list">`;
              //loop through student list in data[i]
              for (var j = 0; j < group.groupStudents.length; j++) {
                var student = group.groupStudents[j].user;
                //join group SignalR
                connection
                  .invoke(
                    "JoinGroup",
                    group.groupName + "-c" + group.id,
                    student.userEmail
                  )
                  .catch(function (err) {
                    return console.error(err.toString());
                  });
                html += `<div class="student-card mb-3">
                <div style="display:flex; align-items:center">
                     <img src="https://avatars.dicebear.com/api/avataaars/${student.userEmail}.svg" alt="Student 2" class="avatar" width="50">
                    <h5 class="card-title mx-3">${student.fullName}</h5>
                </div>
                <div class="circle-wrap">
                  <div class="circle">
                      <div class="mask half">
                          <div class="fill"></div>
                      </div>
                      <div class="inside-circle"> 75% </div>
                      <div class="mask full">
                        <div class="fill"></div>
                      </div>
                  </div>
                </div>
            </div>`;
              }
              html += `
                  </div>
                </div>
              </div>
            </div>`;
              $("#group-list").append(html);
            }
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
      //save data.teacher.userEmail to cookie
      Cookies.set("teacherEmail", data.teacher.userEmail);
      //set class-title html is class name
      $(".class-title").html(data.className);
      $("#stream-classname").html(data.className);
      $("#stream-classcode").html("Class Code: " + data.classCode);
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
      url: "https://localhost:7290/api/ClassStudents/" + classId + "/students",
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

  //exportToCsv
  function exportToCsv(filename, rows) {
    debugger;
    var processRow = function (row) {
      var finalVal = "";
      for (var j = 0; j < row.length; j++) {
        var innerValue = row[j] === null ? "" : row[j].toString();
        if (row[j] instanceof Date) {
          innerValue = row[j].toLocaleString();
        }
        var result = innerValue.replace(/"/g, '""');
        if (result.search(/("|,|\n)/g) >= 0) result = '"' + result + '"';
        if (j > 0) finalVal += ",";
        finalVal += result;
      }
      return finalVal + "\n";
    };

    var csvFile = "";
    for (var i = 0; i < rows.length; i++) {
      csvFile += processRow(rows[i]);
    }

    var blob = new Blob([csvFile], { type: "text/csv;charset=utf-8;" });
    if (navigator.msSaveBlob) {
      navigator.msSaveBlob(blob, filename);
    } else {
      var link = document.createElement("a");
      if (link.download !== undefined) {
        var url = URL.createObjectURL(blob);
        link.setAttribute("href", url);
        link.setAttribute("download", filename);
        link.style.visibility = "hidden";
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
      }
    }
  }

  //on Click #export-student-list to export student list to excel
  $("#export-student-list").click(function () {
    debugger;
    var rows = [];
    rows.push(["Group Name", "Student Name", "Student Email"]);
    var studentList = $("#student-list");
    //get all rows in student-list table
    var students = studentList.find("tr");
    //loop through each row
    students.each(function () {
      var student = $(this);
      //get student name and email
      var studentName = student.find(".user-name").html();
      var studentEmail = student.find(".user-email").html();
      //add student name and email to rows
      rows.push(["", studentName, studentEmail]);
    });
    //export to excel
    exportToCsv("student-list.csv", rows);
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

  //-------------------------------- HANDLE CSV FILE --------------------------------

  var uploadCSV = $("#upload-csv");

  //on dragover event
  uploadCSV.on("dragover", function (e) {
    e.preventDefault();
    //scale .upload-icon
    $(".upload-icon").css("transform", "scale(1.2)");
  });

  //on dragleave event
  uploadCSV.on("dragleave", function (e) {
    e.preventDefault();
    //scale .upload-icon
    $(".upload-icon").css("transform", "scale(1)");
  });

  //drop event
  uploadCSV.on("drop", function (e) {
    e.preventDefault();
    //scale .upload-icon
    $(".upload-icon").css("transform", "scale(1)");
    //get files
    const files = e.originalEvent.dataTransfer.files;
    handleFiles(files);
  });

  //on click #csv-file
  $("#csv-file").change(function (e) {
    const files = e.target.files;
    handleFiles(files);
  });

  function handleFiles(files) {
    const file = files[0];
    if (!file || file.type !== "text/csv") {
      alert("Please select a CSV file");
      return;
    }

    Papa.parse(file, {
      header: true,
      complete: function (results) {
        const groups = {};
        let currentGroup = null;
        let currentStudents = [];

        for (let i = 0; i < results.data.length - 1; i++) {
          const group_name = results.data[i]["Group Name"] || currentGroup;
          const student_email = results.data[i]["Student Email"];

          if (group_name !== currentGroup) {
            if (currentGroup !== null) {
              groups[currentGroup] = currentStudents;
            }
            currentGroup = group_name;
            currentStudents = [];
          }
          currentStudents.push(student_email);
        }
        if (currentGroup !== null) {
          groups[currentGroup] = currentStudents;
        }

        sendGroupsToApi(groups);
      },
    });

    function sendGroupsToApi(groups) {
      $.ajax({
        url: "https://localhost:7290/api/Groups/addGroup",
        type: "POST",
        contentType: "application/json",
        headers: { Authorization: "Bearer " + token },
        data: JSON.stringify({ Groups: groups, ClassId: classId }),
        success: function (data) {
          alert(data);
          location.reload();
        },
        error: function (jqXHR, textStatus, errorThrown) {
          alert(jqXHR.responseText);
        },
      });
    }
  }
});
