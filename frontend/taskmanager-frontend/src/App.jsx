import { useState, useEffect } from "react";

function App() {
  const [tasks, setTasks] = useState([]);

  useEffect(() => {
    fetch("/api/Tasks")
      .then((res) => res.json())
      .then(setTasks)
      .catch(console.error);
  }, []);

  return (
    <div style={{ padding: "2rem" }}>
      <h1>Task Manager</h1>
      <ul>
        {tasks.map((t) => (
          <li key={t.id}>
            {t.title}{" "}
            {t.isCompleted ? "✅ Done" : "⏳ Pending"}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;