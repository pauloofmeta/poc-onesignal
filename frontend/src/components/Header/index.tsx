import { Layout } from "antd";
import './style.css'

export function Header() {
  return (
    <Layout.Header className="header">
      <div className="logo" />
      <h1>Push Notification POC</h1>
    </Layout.Header>
  );
}