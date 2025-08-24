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
    e.dataTransfer.setData("taskId", id.toString()); // ensure string
  };

  const handleDrop = (e, completed) => {
  e.preventDefault();
  const id = e.dataTransfer.getData("taskId");
  
  const taskToUpdate = tasks.find(t => t.id.toString() === id);
  if (!taskToUpdate) return;

  const updatedTask = { ...taskToUpdate, isCompleted: completed };
  
  // Update frontend
  setTasks(tasks.map(t => t.id.toString() === id ? updatedTask : t));
  
  // Update backend
  fetch(`/api/Tasks/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(updatedTask),
  })
    .then(res => res.json())
    .catch(console.error);
};

  const handleKeyDown = (e, completed) => {
    if (e.key === "Enter") addTask(completed);
  };

  return (
    <div className="task-board">
      <h1>Task Manager</h1>
      <div className="task-columns">
        <div
          className="task-column uncompleted"
          onDragOver={(e) => e.preventDefault()}
          onDrop={(e) => handleDrop(e, false)}
        >
          <h3>Uncompleted</h3>
          {tasks.filter((t) => !t.isCompleted).map((t) => (
            <div
              key={t.id}
              className="task-item"
              draggable
              onDragStart={(e) => handleDragStart(e, t.id)}
            >
              {t.title}
            </div>
          ))}
          <input
            value={newTask}
            onChange={(e) => setNewTask(e.target.value)}
            onKeyDown={(e) => handleKeyDown(e, false)}
            placeholder="Add task..."
          />
        </div>

        <div
          className="task-column completed"
          onDragOver={(e) => e.preventDefault()}
          onDrop={(e) => handleDrop(e, true)}
        >
          <h3>Completed</h3>
          {tasks.filter((t) => t.isCompleted).map((t) => (
            <div
              key={t.id}
              className="task-item"
              draggable
              onDragStart={(e) => handleDragStart(e, t.id)}
            >
              {t.title}
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default App;