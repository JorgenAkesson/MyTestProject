import React, { useState } from 'react';

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

        //setBillings([{ "id": 1, "patientName": "PatientName1" }])
    }

    return (
        <>
            <p>Billings</p><button onClick={handleGetBillings}>Get Billings</button>
            <div>
                {
                    billings.map((b) => {
                        return <li key={b.id}>{b.patientName}</li>
                    })
                }
            </div>
        </>
    );
}

export default BillingList;