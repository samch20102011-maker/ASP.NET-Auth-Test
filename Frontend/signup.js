const form = document.getElementById("signupForm");
const message = document.getElementById("message");

form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    try {
        const response = await fetch("http://localhost:5180/api/auth/register", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username, password })
        });

        const data = await response.json();

        if (response.ok) {
            message.style.color = "green";
            message.textContent = data.message;
        } else {
            message.style.color = "red";
            message.textContent = data.message;
        }
    } catch (error) {
        message.style.color = "red";
        message.textContent = "Error connecting to server.";
        console.error(error);
    }
});