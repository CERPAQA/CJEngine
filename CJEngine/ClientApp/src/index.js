import 'bootstrap/dist/css/bootstrap.css';
import "./css/CJ.css";
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import {  } from './components/CJCore';
//import { ElapsedTimer } from './components/ElapsedTimer';
//import { Clock } from './components/Clock';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
        <App />
  </BrowserRouter>,
    rootElement);

/*function Update() {
    ReactDOM.render(
        <Clock />,
        document.getElementById('root')
    );
}*/
//setInterval(Update, 1000);
//setInterval(CJCore.judgePair("item1"), 15000); //tried to use class method
registerServiceWorker();