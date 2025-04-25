import { Alert, Button, Input } from "@mui/material";
import { useState } from "react";

function Test() {

    const [variable, setValue] = useState("Kalle");
    const handleChange = e => {
        setValue(e.target.value);
    };
    const [alertVisible, setAlertVisibility] = useState(false);

    function DoSomething() {
        console.log("Lable:" + variable);
    }

    return (
        <>
            <div>
                <Input defaultValue={variable}
                    onChange={handleChange} />
                <Button
                    onClick={DoSomething}>Test
                </Button>
                <div>
                    {alertVisible && <Alert onClose={() => setAlertVisibility(false)}> Hello world!</Alert >}
                    <Button variant="contained" onClick={() => setAlertVisibility(true)} >Alert</Button>
                </div>
            </div>
        </>
    )
}
export default Test