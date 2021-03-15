import { Button } from "@material-ui/core";
import React from "react";
import './Feedback.scss';

const Feedback = () => {
    return (
      <div>
          <div className="testimonials">
              <h1>Отклики житомирской свиты:</h1>
              <div className="test-body">
                  <div className="item">
                      <img src="http://www.avotarov.ru/picture/avatar-120/kartinki/670.gif" />
                          <div className="name">I love anime</div>
                          <small className="design">Student</small>
                          <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Aliquid autem ex labore necessitatibus nulla quia quibusdam quo rem? Ad alias est, exercitationem facilis maxime nemo optio pariatur quo rem vero?</p>
                  </div>
                  <div className="item">
                      <img src="http://www.avotarov.ru/picture/avatar-120/kartinki/670.gif" />
                          <div className="name">I love anime</div>
                          <small className="design">Student</small>
                          <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Assumenda esse minus natus obcaecati recusandae rerum voluptate. Cupiditate deleniti, eum illo incidunt, mollitia nemo neque nisi officiis quas sed tempora temporibus.</p>
                  </div>
                  <div className="item">
                      <img src="http://www.avotarov.ru/picture/avatar-120/kartinki/670.gif" />
                          <div className="name">I love anime</div>
                          <small className="design">Student</small>
                          <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Aliquam aperiam asperiores aspernatur consequatur culpa cum, dolor dolorem dolorum eaque eligendi et exercitationem explicabo harum in laborum sapiente sit tempora tenetur!</p>
                  </div>
              </div>
              <Button variant="outlined" color="primary" size="large">Хочу больше отзывов!</Button>
          </div>
      </div>  
    );
}

export default Feedback;