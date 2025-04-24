import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import BillingList from './Components/BillingList'
import Alert from './Components/Alert'
import 'bootstrap/dist/css/bootstrap.css'
import Button from '@mui/material/Button';

function App() {
    const [count, setCount] = useState(0)
    let items = ["Varberg", "Falkenberg"];
    const [alertVisible, setAlertVisibility] = useState(false);

    const handleSelectItem = (item: string) => {
        console.log(item);
    }

    return (
        <>
            <div>
                {alertVisible && <Alert onClose={() => setAlertVisibility(false)}> Hello world!</Alert >}
                <Button variant="contained" onClick={() => setAlertVisibility(true)} >Alert</Button>
                <BillingList items={items} heading="Cities" onSelectItem={handleSelectItem} />
            </div>
            <div>
                <a href="https://vite.dev" target="_blank">
                    <img src={viteLogo} className="logo" alt="Vite logo" />
                </a>
                <a href="https://react.dev" target="_blank">
                    <img src={reactLogo} className="logo react" alt="React logo" />
                </a>
            </div>
            <h1>Vite + React</h1>
            <div className="card">
                <button onClick={() => setCount((count) => count + 1)}>
                    count is {count}
                </button>
            </div>
        </>
    )
}

export default App
