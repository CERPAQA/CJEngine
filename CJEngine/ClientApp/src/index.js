import 'bootstrap/dist/css/bootstrap.css';
import "./css/CJ.css";
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { CJCore } from './components/CJCore';
//import { ElapsedTimer } from './components/ElapsedTimer';
import { Clock } from './components/Clock';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
  <BrowserRouter basename={baseUrl}>
        <App />
  </BrowserRouter>,
    rootElement);

function Update() {
    ReactDOM.render(
        <Clock />,
        document.getElementById('elapsedTime'),
    );
}

function Judge() {
    document.getElementById("itemOne").click();
}

setInterval(Update, 1000);
//setInterval(Judge, 5000); //this works!!!
registerServiceWorker();