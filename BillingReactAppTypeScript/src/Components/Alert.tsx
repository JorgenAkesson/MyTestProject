import { Button } from "@mui/material";
import { ReactNode } from "react";

interface Props {
    children: ReactNode;
    onClose: () => void;
}

function Alert({ children, onClose }: Props) {

    function DoSomething() {
        onClose();
    }

    return (
        <>
            <div className="alert alert-primary">{children}
                <Button type="button"
                    className="btn-close" data-bs-dismiss="alert" aria-label="Close"
                    onClick={DoSomething}></Button>
            </div>
        </>
    )
}
export default Alert