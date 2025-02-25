# Employee Management App

## Overview
This is a simple React-based employee management application built using Vite. It fetches employee data from an API and displays it in a responsive table with styling.

## Features
- **Fetch Employee Data:** Uses `fetch()` to retrieve employee details from an API.
- **React Hooks:** Implements `useState` and `useEffect`.
- **Dynamic Table Rendering:** Displays employees in a structured table with flexible column widths.
- **Loading State:** Shows a loading message until data is fetched.
- **Styled UI:** Improved table styling with borders, colors, and shadows.

## Tech Stack
- **Frontend:** React, Vite
- **Styling:** Inline CSS
- **API:** REST API (mocked with `localhost:7251`)

## Installation

1. **Install Dependencies:**
   ```sh
   npm install
   ```

2. **Run the Development Server:**
   ```sh
   npm run dev
   ```

3. **Build the Project:**
   ```sh
   npm run build
   ```

4. **Preview the Build:**
   ```sh
   npm run preview
   ```

## Key Concepts

### 1. React Hooks Used:
- **useState:** Manages component state for employee data and loading state.
- **useEffect:** Fetches employee data when the component mounts.

### 2. API Call
The application fetches employee data from an API using the `fetch` function inside `useEffect`.

```js
useEffect(() => {
  fetch('https://localhost:7251/api/Employee/GetAllEmployees')
    .then(response => response.json())
    .then(data => {
      setEmployees(data);
      setLoading(false);
    })
    .catch(error => {
      console.error('Data fetching failed.', error);
      setLoading(false);
    });
}, []);
```

### 3. Event Handling
- The app does not have explicit event handling but listens for API responses to update the UI dynamically.
- Data is conditionally rendered based on the `loading` state.

```js
if (loading) {
  return <div>Loading employee data...</div>;
}
```

## File Structure
```
employee-management/
│── src/
│   ├── App.jsx  # Main component with API calls and UI rendering
│   ├── App.css  # Styles for the application
│── public/
│── index.html
│── package.json
│── vite.config.js
│── README.md
```

## Future Enhancements
- Add employee search and filtering.
- Implement sorting functionality.
- Improve UI with Material UI or Tailwind.
- Introduce error handling UI.

## License
This project is licensed under the MIT License.

