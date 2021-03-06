import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { CJCore } from './components/CJCore';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/App' component={Home} />
            <Route path='/CJ' component={CJCore} /> {/*For now, put the CJ Engine component here*/}          
      </Layout>
    );
  }
}