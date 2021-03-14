import React from 'react';
import './App.css';
import NavBar from "./Navbar/Navbar";
import {Route} from "react-router-dom";
import Home from "./Home/Home";
import About from "./About/About";

function App() {
    return (
        <div>
            <NavBar/>
            <div>
                <Route path='/about' render={About}/>
                <Route exact path='/' component={Home}/>
            </div>
        </div>
    );
}

export default App;
