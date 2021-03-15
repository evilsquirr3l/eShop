import React from 'react';
import './App.scss';
import NavBar from "./Navbar/Navbar";
import {BrowserRouter, Route} from "react-router-dom";
import Home from "./Home/Home";
import About from "./About/About";

function App() {
    return (
        <BrowserRouter>
            <div>
                <NavBar/>
                <div>
                    <Route path='/about' render={About}/>
                    <Route exact path='/' component={Home}/>
                </div>
            </div>
        </BrowserRouter>
    );
}

export default App;
