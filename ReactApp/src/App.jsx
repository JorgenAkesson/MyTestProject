import { useState } from 'react'
import './App.css'
import BillingList from './Components/BillingList'
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import 'bootstrap/dist/css/bootstrap.css';

function App() {
    return (
        <>
            <div>
                <BillingList />
            </div>
        </>
    )
}

export default App
