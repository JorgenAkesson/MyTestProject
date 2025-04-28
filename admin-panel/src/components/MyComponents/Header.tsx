import { AppBar, Button, Toolbar } from "@mui/material";

const headersData = [
    {
        label: "Home",
        href: "/",
    },    {
        label: "Test",
        href: "/test",
    },
    {
        label: "List",
        href: "/list",
    },
    {
        label: "Billing",
        href: "/billing",
    },
    {
        label: "Log Out",
        href: "/logout",
    },
]

function Header() {
    const displayDesktop = () => {
        return (
            <Toolbar>
                {getMenuButtons()}
            </Toolbar>
        );
    };

    const getMenuButtons = () => {
        return headersData.map(({ label, href }) => {
            return (
                <Button color="inherit" href={href} >
                    {label}
                </Button>
            );
        });
    };

    return (
        <>
            <header>
                <AppBar position="static">{displayDesktop()}
                </AppBar>
            </header>
        </>
    )
}
export default Header