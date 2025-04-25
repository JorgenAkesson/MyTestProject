import { ReactNode } from "react";
import Header from "./Header";

interface Props {
    children: ReactNode;
}
function Layout({ children }: Props) {

    return (
        <>
            <Header></Header>
            <div style={{ width: '100%', background: '#f9f9f9' }} >
                {children}
            </div>
        </>
    )
}
export default Layout