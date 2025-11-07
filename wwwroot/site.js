import { getItems } from "./api.js";

const authRegisterUrl = "api/auth/register";
const authLoginUrl = "api/auth/login";
const loginForm = document.getElementById("loginForm");
const loginSection = document.getElementById("login-section");
loginForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    const errorMessageElement = document.getElementById("errorMessage");

    try {
        const response = await fetch(authLoginUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({username, password}),
        });

        const data = await response.json(); // contains access token/refresh token

        if (response.ok)
        {
            loginSection.style.display = "none";
            await getItems(data.accessToken);
        } else {
            console.log("Login failed.");
        }

    } catch (error)
    {
        console.error("Error during login: ", error);
        errorMessageElement.textContent = "An error occurred during login. Please try again later.";
    }
});

