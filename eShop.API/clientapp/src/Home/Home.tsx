import Button from "@material-ui/core/Button";
import React from "react";
import About from "../About/About";
import './Home.scss';
import Feedback from "../Feedback/Feedback";

const Home = () => {
    return (
        <div>
            <section className="section-a">
                <div className="container">
                    <div>
                        <h1>Лучшие костюмы. Без ограничений.</h1>
                        <p>
                            Не смейте звать меня на вечеринку, на которой дресс-код не похож на ассортимент этого магазина.
                        </p>
                        <Button variant="outlined" color="primary">
                            Узнать больше
                        </Button>
                    </div>
                    <img src="https://www.lonsdale.com/images/products/63808304_l.jpg" alt=""/>
                </div>
            </section>
            
            <section id="about" className="section-b">
                <div className="overlay">
                    <div className="section-b-inner py-5">
                        <h3 className="text-2">Стиль, которому завидуют местные денди</h3>
                        <h2 className="text-5 mt-1">Оденься как модный житомирский стиляга.</h2>
                        <p className="mt-1">
                            Костюмы, в которых ходят на светские рауты
                        </p>
                    </div>
                </div>
            </section>
            <Feedback />
            <About/>
        </div>
        
    )
}

export default Home;