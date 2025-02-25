import { useState, useEffect } from 'react';
import './App.css';

function App() {
  const [employees, setEmployees] = useState([]);
  const [loading, setLoading] = useState(true);

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

  if (loading) {
    return <div style={{ textAlign: 'center', padding: '20px', fontSize: '18px' }}>Loading employee data...</div>;
  }

  return (
    <div style={{ padding: '20px', maxWidth: '900px', margin: 'auto' }}>
      <h1 style={{ textAlign: 'center', color: '#333', marginBottom: '20px' }}>Employee Data</h1>
      <table style={{ width: '100%', borderCollapse: 'collapse', boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.1)' }}>
        <thead>
          <tr style={{ backgroundColor: '#007BFF', color: 'white', textAlign: 'left' }}>
            <th style={headerStyle}>EmpID</th>
            <th style={headerStyle}>First Name</th>
            <th style={headerStyle}>Last Name</th>
            <th style={headerStyle}>Email</th>
            <th style={headerStyle}>DOB</th>
            <th style={headerStyle}>Phone</th>
          </tr>
        </thead>
        <tbody>
          {employees.map(emp => (
            <tr key={emp.empID} style={rowStyle}>
              <td style={cellStyle}>{emp.empID}</td>
              <td style={cellStyle}>{emp.firstName}</td>
              <td style={cellStyle}>{emp.lastName}</td>
              <td style={cellStyle}>{emp.email}</td>
              <td style={cellStyle}>{emp.dob}</td>
              <td style={cellStyle}>{emp.phone}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

const headerStyle = {
  padding: '10px',
  minWidth: '120px',
  borderBottom: '2px solid #ccc',
};

const rowStyle = {
  backgroundColor: '#f9f9f9',
  borderBottom: '1px solid #ddd',
};

const cellStyle = {
  padding: '10px',
  borderBottom: '1px solid #ddd',
};

export default App;
