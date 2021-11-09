import React, { useEffect } from 'react';
import OneSignal from 'react-onesignal';
import { Layout } from "antd";
import './App.css';
import { Header } from './components/Header';
import { Outlet, Route, Routes } from 'react-router';
import AllNotifications from './pages/AllNotifications';
import Login from './pages/login';

function App() {
  useEffect(() => {
    OneSignal.init({
      appId: "a64f5787-7c3e-4e57-a658-e06a0d7a0728"
    })
    OneSignal.getUserId((userId) => {
      console.log('Id do Usuario: ', userId);
    })
  }, []);

  return (
    <Routes>
      <Route path="/" element={<LoggedLayout />} >
        <Route index element={<AllNotifications/>} />
      </Route>

      <Route path="/login" element={<Login/>} />
    </Routes>
  );
}

export default App;



function LoggedLayout() {
  return (
    <Layout>
      <Header />
      <Layout.Content>
        <Outlet />
      </Layout.Content>
    </Layout>
  );
}
