import { useState, useEffect } from "react";
import "./App.css";
import "./components/TaskBoard.css";

function App() {
  const [tasks, setTasks] = useState([]);
  const [newTask, setNewTask] = useState("");

  useEffect(() => {
    fetch("/api/Tasks")
      .then((res) => res.json())
      .then((data) => setTasks(data))
      .catch(console.error);
  }, []);

  const handleDragStart = (e, id) => {
    e.dataTransfer.setData("taskId", id.toString());
  };

  const handleDrop = (e, completed) => {
    e.preventDefault();
    const id = e.dataTransfer.getData("taskId");
    updateTaskStatus(id, completed);
  };

  const updateTaskStatus = (id, completed) => {
    const taskToUpdate = tasks.find((t) => t.id.toString() === id);
    if (!taskToUpdate) return;

    const updatedTask = { ...taskToUpdate, isCompleted: completed };

    // Update frontend
    setTasks(tasks.map((t) => (t.id.toString() === id ? updatedTask : t)));

    // Update backend
    fetch(`/api/Tasks/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(updatedTask),
    }).catch(console.error);
  };

  const addTask = (completed) => {
    if (!newTask.trim()) return;

    const task = { title: newTask, isCompleted: completed };

    // Save to backend
    fetch("/api/Tasks", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(task),
    })
      .then((res) => res.json())
      .then((savedTask) => setTasks([...tasks, savedTask]))
      .catch(console.error);

    setNewTask("");
  };

  const deleteTask = (id) => {
    // Update frontend immediately
    setTasks(tasks.filter((t) => t.id !== id));

    // Delete from backend
    fetch(`/api/Tasks/${id}`, { method: "DELETE" }).catch(console.error);
  };

  const handleKeyDown = (e, completed) => {
    if (e.key === "Enter") addTask(completed);
  };

  return (
    <div className="task-board">
      <h1>Task Manager</h1>
      <div className="task-columns">
        {/* Uncompleted Tasks */}
        <div
          className="task-column uncompleted"
          onDragOver={(e) => e.preventDefault()}
          onDrop={(e) => handleDrop(e, false)}
        >
          <h3>To Do</h3>
          {tasks
            .filter((t) => !t.isCompleted)
            .map((t) => (
              <div
                key={t.id}
                className="task-item"
                draggable
                onDragStart={(e) => handleDragStart(e, t.id)}
              >
                <span className="task-text">{t.title}</span>
                <button
                  className="complete-btn"
                  onClick={() => updateTaskStatus(t.id, true)}
                >
                  ✅
                </button>
                <button className="delete-btn" onClick={() => deleteTask(t.id)}>
                  ❌
                </button>
              </div>
            ))}
          <input
            value={newTask}
            onChange={(e) => setNewTask(e.target.value)}
            onKeyDown={(e) => handleKeyDown(e, false)}
            placeholder="Add task..."
          />
        </div>

        {/* Completed Tasks */}
        <div
          className="task-column completed"
          onDragOver={(e) => e.preventDefault()}
          onDrop={(e) => handleDrop(e, true)}
        >
          <h3>Done</h3>
          {tasks
            .filter((t) => t.isCompleted)
            .map((t) => (
              <div
                key={t.id}
                className="task-item"
                draggable
                onDragStart={(e) => handleDragStart(e, t.id)}
              >
                <span className="task-text">{t.title}</span>
                <button
                  className="undo-btn"
                  onClick={() => updateTaskStatus(t.id, false)}
                >
                  ↩️
                </button>
                <button className="delete-btn" onClick={() => deleteTask(t.id)}>
                  ❌
                </button>
              </div>
            ))}
        </div>
      </div>
    </div>
  );
}

export default App;
