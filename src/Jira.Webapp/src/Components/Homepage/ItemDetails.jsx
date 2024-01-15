import React, { Component, useState, useEffect } from 'react';
import axios from 'axios'; 
import SessionManager from "../LoginSetup/SessionManager";
import { Navigate } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router-dom";

const ItemDetails = () => {

    const params = useParams();
    const navigate = useNavigate();
    const baseUrl = "http://localhost:5001/jira";
    const [jiraItem, setItem] = useState([]);
    const [jiraItemHistory, setItemHistory] = useState([]);

    // Fetch users on component mount
    useEffect(() => {
        fetchItemDetails();
    }, []);

    // Fetch users from API
    const fetchItemDetails = async () => {
        axios.get(baseUrl + "/item/" + params.id, { headers: {'Authorization': 'Bearer '+ SessionManager.getToken()}})  
        .then((response) => { 
            console.log(response);  
            //debugger;          
            if (response.status === 200) 
            {
                console.log(response?.data);
                setItem(response?.data);
            }                 
            else 
            {
                alert('Error in getting data'); 
                navigate("/notFoundPage");
            }
        })
        .catch(error => {
            console.log(error); 
            if(error.response.status === 401){
                navigate("/login");
            }
            else{
                navigate("/error");
            }
        });      
    };

    const fetchitemHistory = async () => {
        axios.get(baseUrl + "/item/" + params.id + "/history", { headers: {'Authorization': 'Bearer '+ SessionManager.getToken()}})  
        .then((response) => { 
            console.log(response);  
            debugger;          
            if (response.status === 200) 
            {
                console.log(response?.data);
                setItemHistory(response?.data.jiraItemHistoryResponses);
            }                 
            else 
            {
                alert('Error in getting data'); 
                navigate("/notFoundPage");
            }
        })
        .catch(error => {
            console.log(error); 
            if(error.response.status === 401){
                navigate("/login");
            }
            else{
                navigate("/error");
            }
        });        
    };

    const gotoItemsPage = () => {
        navigate("/items");
    }


    if (SessionManager.getToken()){
        return(
            <div className='homePageContainer'>
                <h2>Jira Item Details</h2>
                <p>Name: {jiraItem.name}</p>
                <p>Description: {jiraItem.description}</p>
                <p>Type: {jiraItem.itemType}</p>
                <p>Status: {jiraItem.itemStatus}</p>
                <p>Asignee: {jiraItem.asignee}</p>
                <p>Author: {jiraItem.author}</p>
                <br />
                <br />
                <button onClick={() => gotoItemsPage()}>GoTo All Items</button>
                <br />
                <br />
                <button onClick={() => fetchitemHistory()}>Show History</button>
                <ul>
                    {jiraItemHistory.map((history) => (
                    <li key={history.id}> 
                        Action {history.actionPerformed} performed at {history.actionPerformedAt} chnaging the value: {history.changedValue} <br />
                    </li>
                    ))}
                </ul>
            </div>
        )
    }
    else {
        return <Navigate replace to="/login" />;
    }
}

export default ItemDetails;