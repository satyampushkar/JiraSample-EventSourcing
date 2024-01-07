import React, { Component, useState, useEffect } from 'react';
import axios from 'axios'; 
import SessionManager from "../LoginSetup/SessionManager";
import { Navigate } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router-dom";

const AddEditItem = () => {

    const params = useParams();
    const isAddMode = !params.id;
    
    const navigate = useNavigate();
    const baseUrl = "https://localhost:5001/jira";

    const [data, setdata] = useState({ name: '', description: '', itemType: '', asignee: '', itemStatus: '' }) 

    const onSubmit = async () => {
        if (isAddMode){
            //debugger; 
            let postData = { Name: data.name, Description: data.description, itemType: data.itemType, Asignee: data.asignee  }
            const headers = {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer '+ SessionManager.getToken()
              }
            axios.post(baseUrl + "/item", postData, { headers: headers})  
            .then((response) => { 
                console.log(response);  
                debugger;          
                if (response.status === 201) 
                {
                    console.log(response?.data);
                    navigate("/item/"+ response?.data.id);
                    //setdata(response?.data);
                    // viewItemDetails(response?.data.id)
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
        }
        else{
            let putData = { Name: data.name, Description: data.description, itemType: data.itemType, Asignee: data.asignee, itemStatus: data.itemStatus  }
            axios.put(baseUrl + "/item/"+ params.id, putData, { headers: {'Authorization': 'Bearer '+ SessionManager.getToken()}})  
            .then((response) => { 
                console.log(response);  
                debugger;          
                if (response.status === 204) 
                {
                    console.log(response?.data);
                    navigate("/item/"+ params.id)
                    //setdata(response?.data);
                    //viewItemDetails(params.id)
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
        }
    }

    const onChange = (e) => {  
        e.persist(); 
        setdata({ ...data, [e.target.name]: e.target.value });  
    }

    useEffect(() => {
        if (!isAddMode) {
            debugger;
            axios.get(baseUrl + "/item/" + params.id, { headers: {'Authorization': 'Bearer '+ SessionManager.getToken()}})  
            .then((response) => { 
                console.log(response);  
                //debugger;          
                if (response.status === 200) 
                {
                    console.log(response?.data);
                    setdata(response?.data);
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
        }
    }, []);

    const onCancel = () => {
        navigate("/items/");
    }

    return(
        <div className='homePageContainer'>
            <h1>{isAddMode ? 'Add New Jira Item' : 'Edit Jira Item'}</h1>
            <div className="form-row">
                <div className="form-group col-5">
                    <label>Name</label>
                    <input name="name" type="text" value={data.name} onChange={onChange}  />
                </div>                
            </div>
            <div className="form-row">
                <div className="form-group col-5">
                    <label>Description</label>
                    <input name="description" type="text" value={data.description} onChange={onChange}  />
                </div>
            </div>
            <div className="form-row">
                <div className="form-group col">
                    <label>Type</label>
                    <select name="itemType" value={data.itemType} onChange={onChange} >
                        <option value="Epic">Epic</option>
                        <option value="Story">Story</option>
                        <option value="Task">Task</option>
                        <option value="SubTask">SubTask</option>
                        <option value="Bug">Bug</option>
                    </select>
                </div>            
            </div>
            <div className="form-row">
                <div className="form-group col-5">
                    <label>Asignee</label>
                    <input name="asignee" type="text"  value={data.asignee} onChange={onChange} />
                </div>
            </div>
            {!isAddMode &&
                <div className="form-row">
                    <div className="form-group col">
                        <label>Status</label>
                        <select name="itemStatus" value={data.itemStatus} onChange={onChange} >
                            <option value="ToDo">ToDo</option>
                            <option value="InProgress">InProgress</option>
                            <option value="InTesting">InTesting</option>
                            <option value="InReview">InReview</option>
                            <option value="Done">Done</option>
                            <option value="Blocked">Blocked</option>
                            <option value="Cancelled">Cancelled</option>
                        </select>
                    </div>            
                </div>
            }
            <div className="form-group">
                <button type="submit" className="btn btn-primary" onClick={() => onSubmit() }> Save </button>
                <button type="submit" className="btn btn-primary" onClick={() => onCancel() }> Cancel </button>
            </div>
        </div>
    )
}

export default AddEditItem;