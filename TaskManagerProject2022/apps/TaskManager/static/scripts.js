// Переименовывание задач
$("#rename-task").blur(function () {
    var task_id = $(document.getElementById("selected-task-id")).val();
    var new_name = $(this).val();
    var csrf = $(document.getElementsByName("csrfmiddlewaretoken")).val();
    console.log(`Renaming task id=${task_id}, new name: ${new_name}. Token: ${csrf}`);
    $.post({
        type: "POST",
        url: "/",
        data: {
            'csrfmiddlewaretoken': csrf,
            '_rename': new_name,
            'task_id': task_id
        },
    });
});

// Переименовывание подзадач
$(document.getElementsByName("rename-subtask")).blur(function () {
    // var task_id = $(document.getElementById("rename-subtask-id")).val();
    var task_id = $(this)[0].form[1].value; // шаткая конструкция, но это быстрофикс!
    var new_name = $(this).val();
    var csrf = $(document.getElementsByName("csrfmiddlewaretoken")).val();
    console.log(`Renaming task id=${task_id}, new name: ${new_name}. Token: ${csrf}`);
    $.post({
        type: "POST",
        url: "/",
        data: {
            'csrfmiddlewaretoken': csrf,
            '_rename': new_name,
            'task_id': task_id
        },
    });
});

// Добавление описаний
$("#set-task-description").blur(function () {
    var task_id = $(document.getElementById("selected-task-id")).val();
    var new_description = $(this).val();
    if (new_description == "") {
        new_description = "None";
    }
    var csrf = $(document.getElementsByName("csrfmiddlewaretoken")).val();
    console.log(`Updating description for task id=${task_id}: ${new_description}. Token: ${csrf}`);
    $.post({
        type: "POST",
        url: "/",
        data: {
            'csrfmiddlewaretoken': csrf,
            '_set_description': new_description,
            'task_id': task_id
        },
    });
});

// Установка дедлайна
$("#set-deadline").blur(function () {
    var task_id = $(document.getElementById("selected-task-id")).val();
    var new_deadline = $(this).val();
    if (new_deadline == "") {
        new_deadline = "1970-01-01";
    }
    var csrf = $(document.getElementsByName("csrfmiddlewaretoken")).val();
    console.log(`Updating deadline for task id=${task_id}: ${new_deadline}. Token: ${csrf}`);
    $.post({
        type: "POST",
        url: "/",
        data: {
            'csrfmiddlewaretoken': csrf,
            '_set_deadline': new_deadline,
            'task_id': task_id
        },
    });
});
