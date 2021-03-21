import React from "react";
import './Home.scss';
import Feedback from "../Feedback/Feedback";
import Intro from "../Intro/Intro";
import Description from "../Description/Description";

const Home = () => {
    return (
        <div>
            <Intro />
            <Description />
            <Feedback />
        </div>
    )
}

export default Home;