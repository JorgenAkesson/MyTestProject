import React, { useState } from 'react';
import Button from '@mui/material/Button';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';


function BillingList() {

    const [billings, setBillings] = useState([]);

    function handleGetBillings() {

        const url = "https://localhost:7252/api/Billings";

        fetch(url)
            .then(response => response.json())
            .then(data => {
                setBillings(data);
                console.log(data);
            })
            .catch(error => console.error(error)); // Handle errors
    }

    return (
        <>
            <br />
            <br />
            <Button variant="contained" onClick={handleGetBillings}>Get Billings</Button>
            <h1>Billings</h1>
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