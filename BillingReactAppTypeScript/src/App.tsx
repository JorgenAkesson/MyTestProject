import './App.css'
import BillingList from './Components/BillingList'
import 'bootstrap/dist/css/bootstrap.css'
import Test from './Components/Test'
import Layout from './Components/Layout'
import { Routes, Route } from 'react-router-dom';
import Billing from './Components/Billing'
import HomePage from './Components/HomePage'

function App() {
    let items = ["Varberg", "Falkenberg"];

    const handleSelectItem = (item: string) => {
        console.log(item);
    }

    return (
        <>
            <Layout>
                <Routes>
                    <Route path="/" element={<HomePage/>} />
                    <Route path="/test" element={<Test/>} />
                    <Route path="/list" element={<BillingList items={items} heading="Cities" onSelectItem={handleSelectItem} />} />
                    <Route path="/billing" element={<Billing/>} />
                    <Route path="/logout" element={<h1>Nothing yet!</h1>} />
                </Routes>
            </Layout>
        </>
    )
}

export default App
