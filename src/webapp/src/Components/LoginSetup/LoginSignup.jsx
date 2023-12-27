import React, { Component, useState } from 'react';
import axios from 'axios'; 
import './LoginSignup.css'
import SessionManager from "./SessionManager";

import user_icon from '../Assets/person.png'
import email_icon from '../Assets/email.png'
import password_icon from '../Assets/password.png'

const LoginSignup = (props) => {

    const apiUrl = "";

    const [action, setAction] = useState("Sign Up");
    const [data, setdata] = useState({ Name: '', Email: '', Password: '' }) 

    async function RegisterUser(){
        let postData = { Name: data.Name, Email: data.Email, Password: data.Password }
        axios.post(apiUrl, postData)  
        .then((response) => { 
            console.log(response.data);  
            if (response.data.Status == 'Invalid')  
                alert('Invalid User');  
            else  
                SessionManager.setUserSession(response.Email, response.token)
                props.history.push('/Home')  
        })
        .catch(error => console.log(error));  

    }

    async function LoginUser(){
        let postData = { Email: data.Email, Password: data.Password }
        axios.post(apiUrl, postData)  
        .then((response) => { 
            console.log(response.data);  
            if (response.data.Status == 'Invalid')  
                alert('Invalid User');  
            else  
                SessionManager.setUserSession(response.Email, response.token)
                props.history.push('/Home')  
        })
        .catch(error => console.log(error)); 
    }

    const onChange = (e) => {  
        //e.persist(); 
        setdata({ ...data, [e.target.name]: e.target.value });  
    }

    const onKeyDown = (e) => {
        if (e.key === 'Enter') {
            this.login();
        }
    }


    return (
        <div className='container'>
           <div className="header">
            <div className="text">{action}</div>
            <div className="underline"></div>
            </div> 
            <div className="inputs">
                {
                    action==="Login" 
                    ? <div></div> 
                    : <div className="input">
                        <img src={user_icon} alt="" />
                        <input type="text" placeholder='Name' name="Name" value={data.Name} onChange={onChange} onKeyDown={onKeyDown} /> 
                    </div>
                }
                <div className="input">
                    <img src={email_icon} alt="" />
                    <input type="email" placeholder='Email Id' name="Email" value={data.Email} onChange={onChange} onKeyDown={onKeyDown} /> 
                </div>
                <div className="input">
                    <img src={password_icon} alt="" />
                    <input type="password" placeholder='Password' name="Password" value={data.Password} onChange={onChange} onKeyDown={onKeyDown} /> 
                </div>
            </div>
            {
                action==="Sign Up"
                ? <div> </div>
                : <div className="forgot-password"><span>Forgot Password?</span></div>
            }
            <div className="submit-container">
                {
                    action==="Sign Up"
                    ? <div className="submit" onClick={RegisterUser}>Sign Up</div>
                    : <div className="submit" onClick={LoginUser}>Login </div>
                }
            </div>
            {
                action==="Sign Up"
                ? <div className="forgot-password">Already have an account? <span onClick={()=>{setAction("Login")}}>Login</span></div>
                : <div className="forgot-password">Don't have an account? <span onClick={()=>{setAction("Sign Up")}}>Sign Up</span></div>
            }
        </div>
    )
}

export default LoginSignup;