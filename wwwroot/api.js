// Once the user has logged in, we need to pass in the accessToken within the response headers
// and get the data, which in this case is the todos of the user and display it in the dashboard.html
// page. 
const todosUrl = "api/todoitems/mytodos";

export async function getItems(token) {
    try {
        const response = await fetch(todosUrl, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
        });
        
        const userTodos = await response.json();
        console.log(userTodos);
        
    } catch (error) {
        console.error("Error: ", error);
    }
}