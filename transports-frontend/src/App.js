import React, { useState, useEffect, Fragment } from 'react';
import './App.css';
import axios from 'axios';

const api = 'http://localhost:59257';

function App() {
  const [drivers, setDrivers] = useState([]);
  const [showFrame, setShowFrame] = useState(false);

  const [name, setName] = useState('');
  const [age, setAge] = useState(16);
  const [rang, setRang] = useState(1);

  const [isUpdate, setIsUpdate] = useState(false);
  const [updateItemId, setUpdateItemId] = useState();

  const loadDrivers = () => {
    axios.get(api + '/api/drivers').then(({data}) => {
      setDrivers(data);
    })
  }

  const deleteDriver = (id) => {
    axios.delete(api + '/api/drivers?id=' + id).then(() => {
      setDrivers(drivers.filter(x => x.DriverId !== id));
    })
  }

  const createDriver = () => {
    setName('');
    setAge(16);
    setRang(1);
    axios.post(api + '/api/drivers', {
      Name: name,
      Age: age,
      Rang: rang
    }).then(loadDrivers);
  }

  const updateDriver = () => {
    setName('');
    setAge(16);
    setRang(1);
    axios.put(api + '/api/drivers', {
      DriverId: updateItemId,
      Name: name,
      Age: age,
      Rang: rang
    }).then(loadDrivers);
  }

  const submit = () => {
    if(isUpdate) {
      updateDriver();
      setIsUpdate(false);
      setUpdateItemId('');
      return;
    }
    createDriver();
  }

  const clear = () => {
    setIsUpdate(false);
    setUpdateItemId('');
    setName('');
    setAge(16);
    setRang(1);
  }

  const setUpdate = (id) => {
    setIsUpdate(true);
    setUpdateItemId(id);
    const driver = drivers.find(x => x.DriverId === id);
    setName(driver.Name);
    setAge(driver.Age);
    setRang(driver.Rang);
  }

  const handleFrameMessage = ({data}) => {
    let recievedData;
    try {
      recievedData = JSON.parse(data);
    }
    catch(e) { return;}

    if(recievedData.type === 'DELETE') {
      setDrivers(drivers.filter(x => x.DriverId !== recievedData.payload));
    }

    if(recievedData.type === 'POST' || recievedData.type === 'PUT') {
      loadDrivers()
    }
  }

  useEffect(() => {
    loadDrivers();
  }, []
  )

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
                  <button className='custom-button custom-button-primary' onClick={() => setUpdate(item.DriverId)}>Update</button>
                  <button onClick={() => deleteDriver(item.DriverId)} className='custom-button custom-button-accent'>Delete</button>
                </div>
              }
            </div>
          })
        }
        </div> 
        {
          !showFrame && (
            <div className='form-container'>
            <form>
              <div className="form-group">
                <label htmlFor="name">Name</label>
                <input id="name" type="text" aria-describedby="emailHelp" value={name} onChange={(event) => setName(event.target.value)} placeholder="Enter name" />
              </div>
              <div className="form-group">
                <label htmlFor="age">Age</label>
                <input type="number" id="age" value={age} onChange={(event) => setAge(event.target.value)} placeholder="Age" />
              </div>
              <div className="form-group">
                <label htmlFor="rang">Rang</label>
                <input type="number" id="rang" placeholder="Rang" onChange={(event) => setRang(event.target.value)} value={rang} />
              </div>
              <div className='form-group'>
                <input type="button" id='submit' value='Submit' onClick={() => submit()}/>
              </div>
              {
                isUpdate && 
                  <div className='form-group'>
                  <input type="button" id='clear' style={{ background: '#606040'}} value='Clear' onClick={() => clear()}/>
                </div>
              }
            </form>
          </div>
          )
        }
    </div>
    <button id='togle_frame' onClick={() => {
      if(showFrame) {
        setShowFrame(false);
        window.addEventListener('message', handleFrameMessage, false);
        return;
      }

      setShowFrame(true);
      window.addEventListener('message', handleFrameMessage, false);

    } }>Use IFrame</button>
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
