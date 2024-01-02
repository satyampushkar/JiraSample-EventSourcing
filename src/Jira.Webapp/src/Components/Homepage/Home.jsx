import React, { Component, useState, useEffect } from 'react';
import axios from 'axios'; 
import SessionManager from "../LoginSetup/SessionManager";
import { Navigate } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import './Home.css'

const Home = () => {

    const navigate = useNavigate();
    const baseUrl = "https://localhost:5003/api/jira";
    const [jiraItems, setItems] = useState([]);

    // Fetch users on component mount
    useEffect(() => {
        fetchItems();
    }, []);

    // Fetch users from API
    const fetchItems = async () => {
        axios.get(baseUrl + "/items", { headers: {'Authorization': 'Bearer '+ SessionManager.getToken()}})  
        .then((response) => { 
            console.log(response);  
            debugger;          
            if (response.status === 200) 
            {
                console.log(response?.data);
                setItems(response?.data.jiraItems);
            }                 
            else 
            {
                alert('Error in getting data'); 
                navigate("/notFoundPage");
            }
        })
        .catch(error => console.log(error));         
    };

    // const [authenticated, setauthenticated] = useState(null);

    // useEffect(() => {
    //     const loggedInUser = SessionManager.getToken();
    //     if (loggedInUser) {
    //       setauthenticated(loggedInUser);
    //     }
    //   }, []);

    // if (!authenticated) {
    if (SessionManager.getToken()) {
        return (
            <div className='homePageContainer'>
                <h2>All Jira Items:</h2>
                <table className="table table-striped" aria-labelledby="tableLabel">
                    <thead>
                    <tr>
                        <th>Name</th>
                        <th>Description</th>
                        <th>Type</th>
                        <th>Status</th>
                        <th>Asignee</th>
                        <th>Author</th>
                    </tr>
                    </thead>
                    <tbody>
                    {jiraItems.map(jiraItem =>
                        <tr key={jiraItem.id}>
                            <td>{jiraItem.name}</td>
                            <td>{jiraItem.description}</td>
                            <td>{jiraItem.itemType}</td>
                            <td>{jiraItem.itemStatus}</td>
                            <td>{jiraItem.asignee}</td>
                            <td>{jiraItem.author}</td>
                        </tr>
                    )}
                    </tbody>
                </table>
            </div>
            );        
    } else {
        return <Navigate replace to="/login" />;
    }
}

export default Home;