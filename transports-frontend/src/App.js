import React, { useState, useEffect, Fragment } from 'react';
import './App.css';
import axios from 'axios';

const api = 'http://localhost:59257';

function App() {
  const [drivers, setDrivers] = useState([]);
  const [showFrame, setShowFrame] = useState(false);
  const loadDrivers = () => {
    axios.get(api + '/api/drivers').then(({data}) => {
      setDrivers(data);
    })
  }

  useEffect(() => {
    loadDrivers();
  })

  return (<Fragment>
    <div className='container'>
        <div>
          <div className='main-label'>
            Drivers 
          </div>
        {
          drivers.map((item, i) => {
            return <div key={i} className='item'>
              <Label value={item.DriverId} label='Driver Id: ' />
              <Label value={item.Name} label='Driver Name: ' />
              <Label value={item.Age} label='Driver Age: '/>
              <Label value={item.Rang} label='Driver Rang: ' /> 
              {
                !showFrame &&
                <div className="controls">
                  <button className='custom-button custom-button-primary'>Update</button>
                  <button className='custom-button custom-button-accent'>Delete</button>
                </div>
              }
            </div>
          })
        }
        </div>
        <div>
          FORM
        </div>
    </div>
    <button className='togleframe' onClick={() => setShowFrame(!showFrame)}>Use IFrame</button>
    {
      showFrame &&
        <div className='frame-container'>
          <iframe title='frame' src={api} width='1200' height='400' />
        </div>
    }

    </Fragment>);
}

const Label = ({label, value}) => {
  return (<div>
    <span className='label'>{label}</span>
    <span>{value}</span>
  </div>)
}

export default App;
