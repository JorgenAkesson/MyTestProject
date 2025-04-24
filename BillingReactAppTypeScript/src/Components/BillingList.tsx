import { useState } from 'react';
import Button from '@mui/material/Button';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
//import PermanentDrawer from './PermanentDrawer'
import 'bootstrap/dist/css/bootstrap.css'

interface Props {
    items: string[];
    heading: string;
    onSelectItem: (item: string) => void; 
}

function BillingList({ items, heading, onSelectItem }: Props) {

    let [billings, setBillings] = useState<{ id: number; patientName: string; }[]>([]);
    //let items = ["Varberg", "Falkenberg"];
    let [selectedIndex, setSelectedIndex] = useState(-1)


    function handleGetBillings() {
        const url = "https://localhost:7252/api/Billings";
        fetch(url)
            .then(response => response.json())
            .then(data => {
                setBillings(data);
                console.log(data);
            })
            .catch(error => console.error(error)); // Handle errors

        // If API is not available data is set here.
        let data: { id: number, patientName: string }[] = [
            { "id": 0, "patientName": "Jörgen" },
            { "id": 1, "patientName": "Ingrid" }
        ];
        setBillings(data);
    }

    return (
        <>
            <h1>{heading}</h1>
            <ul className="list-group">
                {items.map((item, index) =>
                    <li
                        className={selectedIndex === index ? "list-group-item active" : "list-group-item"}
                        key={item}
                        onClick={() => {
                            setSelectedIndex(index);
                            onSelectItem(item);
                        }}>
                        {item}
                    </li>)}
            </ul>
            <br />
            <Button variant="contained" onClick={handleGetBillings}>Get Billings</Button>
            <h1>Billings</h1>
            {/*<PermanentDrawer />*/}
            <TableContainer component={Paper} sx={{ width: "max-content" }}>
                <Table sx={{ innerWidth: 650 }} aria-label="simple table">
                    <TableHead>
                        <TableRow>
                            <TableCell>Id</TableCell>
                            <TableCell sx={{ width: 200 }}>Patient Name</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            billings.map((row) => (
                                <TableRow key={row.id} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                                    <TableCell component="th" scope="row">{row.id}</TableCell>
                                    <TableCell component="th" scope="row">{row.patientName}</TableCell>
                                </TableRow>
                            ))
                        }
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    );
}

export default BillingList;