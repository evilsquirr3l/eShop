import React from 'react';
import './App.css';
import {NavLink, Route} from 'react-router-dom'


function App() {
    return (
        <NavBar/>
    );
}

function NavBar() {
    return (
        <div>
            <NavLink to='/about'>
                About
            </NavLink>
            <NavLink to='/'>
                Home
            </NavLink>
            <div>
                <Route path='/about' render={About}>О сайте</Route>
                <Route exact path='/' component={Home}>Главная</Route>
            </div>
        </div>

    )
}

function Home() {
    return (
        <div>home</div>
    )
}

function About() {
    return (
        <div>About</div>
    )
}

export default App;
