import logo from './logo.svg';
import { BrowserRouter, Route, Routes } from "react-router-dom";
import './App.css';
import LoginSignup from './Components/LoginSetup/LoginSignup';
import Home from './Components/Homepage/Home';
import NotFoundPage from './Components/Homepage/NotFoundPage'
import ErrorPage from './Components/Homepage/ErrorPage'
import ItemDetails from './Components/Homepage/ItemDetails'
import AddEditItem from './Components/Homepage/AddEditItem'

function App() {
  return (
    <Routes>
        <Route path='/' element={<Home />} />
        <Route path="/login" element={<LoginSignup />} />
        <Route path="/home" element={<Home />} />
        <Route path="/items" element={<Home />} />
        <Route path="/item/edit/:id" element={<AddEditItem />} />
        <Route path="/item/:id" element={<ItemDetails />} />
        <Route path="/item/add" element={<AddEditItem />} />
        
        <Route path="/error" element={<ErrorPage />} />
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
