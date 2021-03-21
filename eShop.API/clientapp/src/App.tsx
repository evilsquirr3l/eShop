import React from 'react';
import './App.scss';
import NavBar from "./Navbar/Navbar";
import {BrowserRouter, Route} from "react-router-dom";
import Home from "./Home/Home";

function App() {
    return (
        <BrowserRouter>
            <div>
                <NavBar/>
                <div>
                    <Route exact path='/' component={Home}/>
                </div>
            </div>
        </BrowserRouter>
    );
}

export default App;
