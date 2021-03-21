import React from "react";
import './Description.scss';

const Description = () => {
    return (
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
    );
}

export default Description;