const apiUrl = "http://localhost:5000/api/tasks";

async function fetchTasks() {
    const response = await fetch(apiUrl);
    const tasks = await response.json();
    const list = document.getElementById("taskList");
    list.innerHTML = "";
    tasks.forEach(task => {
        const li = document.createElement("li");
        li.innerHTML = `
            ${task.title} - ${task.completed ? "✅" : "❌"}
            <button onclick="toggleTask(${task.id}, ${task.completed})">✔</button>
            <button onclick="deleteTask(${task.id})">❌</button>
        `;
        list.appendChild(li);
    });
}

async function addTask() {
    const taskInput = document.getElementById("taskInput");
    await fetch(apiUrl, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ title: taskInput.value, completed: false })
    });
    taskInput.value = "";
    fetchTasks();
}

async function toggleTask(id, completed) {
    await fetch(`${apiUrl}/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ completed: !completed })
    });
    fetchTasks();
}

async function deleteTask(id) {
    await fetch(`${apiUrl}/${id}`, { method: "DELETE" });
    fetchTasks();
}

fetchTasks();
