const API_URL = "/api/tasks";

// Fetch and display tasks on page load
document.addEventListener("DOMContentLoaded", loadTasks);

function loadTasks() {
    fetch(API_URL)
        .then(response => response.json())
        .then(tasks => {
            const taskList = document.getElementById("taskList");
            taskList.innerHTML = "";

            tasks.forEach(task => {
                const li = document.createElement("li");
                li.innerHTML = `
                    <span contenteditable="true" onBlur="updateTask(${task.id}, this)">${task.title}</span>
                    <button class="delete" onclick="deleteTask(${task.id})">Delete</button>
                `;
                taskList.appendChild(li);
            });
        })
        .catch(error => console.error("Error fetching tasks:", error));
}

function addTask() {
    const taskTitle = document.getElementById("taskTitle").value;
    if (!taskTitle) return;

    fetch(API_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ title: taskTitle, completed: false })
    })
        .then(() => {
            document.getElementById("taskTitle").value = "";
            loadTasks();
        })
        .catch(error => console.error("Error adding task:", error));
}

function updateTask(id, element) {
    const updatedTitle = element.textContent;

    fetch(`${API_URL}/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ id, title: updatedTitle, completed: false })
    })
        .then(() => loadTasks())
        .catch(error => console.error("Error updating task:", error));
}

function deleteTask(id) {
    fetch(`${API_URL}/${id}`, { method: "DELETE" })
        .then(() => loadTasks())
        .catch(error => console.error("Error deleting task:", error));
}
