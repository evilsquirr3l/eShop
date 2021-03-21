import React from "react";
import Button from "@material-ui/core/Button";
import './Intro.scss';

const Intro = () => {
    return (
        <section className="intro-section">
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
    );
}

export default Intro;