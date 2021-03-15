import React from "react";
import './Navbar.scss';

function NavBar() {
    return (
        <div>
            <nav className="navbar">
                <div className="container">
                    <h1 className="logo">Coffins</h1>
                    <ul className="nav">
                        <li><a href="#home">Home</a></li>
                        <li><a href="#about">About</a></li>
                    </ul>
                </div>
            </nav>
        </div>
    )
}

export default NavBar;