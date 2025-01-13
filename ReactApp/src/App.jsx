import { useState } from 'react'
import './App.css'
import BillingList from './Components/BillingList'

function App() {
    const [count, setCount] = useState(0)

    return (
        <>
            <button onClick={() => setCount((count) => count + 1)}>
                count is {count}
            </button>
            <BillingList />
        </>
    )
}

export default App
