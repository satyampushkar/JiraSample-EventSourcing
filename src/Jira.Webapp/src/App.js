import logo from './logo.svg';
import { BrowserRouter, Route, Routes } from "react-router-dom";
import './App.css';
import LoginSignup from './Components/LoginSetup/LoginSignup';
import Home from './Components/Homepage/Home';
import NotFoundPage from './Components/Homepage/NotFoundPage'

function App() {
  return (
    <Routes>
        <Route path='/' element={<Home />} />
        <Route path="/login" element={<LoginSignup />} />
        <Route path="/home" element={<Home />} />
        <Route path="*" element={<NotFoundPage />} />
      </Routes>
    // <Route exact path="/login">
    //   {SessionManager.getToken() ? <Redirect to="/" /> : <Route path="/login" element={<Login />}/>}
    // </Route>

    // <div>
    //   <LoginSignup/>
    // </div>
  );
}

export default App;
